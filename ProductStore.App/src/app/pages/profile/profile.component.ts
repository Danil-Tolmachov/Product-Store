import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { trigger, transition, style, animate } from '@angular/animations';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { take, tap } from 'rxjs';
import MessageService from '../../core/services/message.service';
import UserService from '../../core/services/user.service';
import ButtonComponent from '../../shared/components/button/button.component';
import TextPanelComponent from '../../shared/components/text-panel/text-panel.component';

@UntilDestroy()
@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TextPanelComponent,
    ButtonComponent,
  ],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
  animations: [
    trigger('fadeInOutAnimation', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('0.1s', style({ opacity: 1 })),
      ]),
      transition(':leave', [animate('0.1s', style({ opacity: 0 }))]),
    ]),
  ],
})
export default class ProfileComponent implements OnInit {
  user$ = this.userService.currentUser;

  userForm: FormGroup | null = null;

  get firstName() {
    return this.userForm?.get('firstName')!;
  }

  get lastName() {
    return this.userForm?.get('lastName')!;
  }

  get address() {
    return this.userForm?.get('address')!;
  }

  constructor(
    private readonly userService: UserService,
    private readonly messageService: MessageService,
    private readonly formBuilder: FormBuilder
  ) {
    this.userService.currentUser
      .pipe(
        tap((user) => {
          if (user != null) {
            this.userForm = this.formBuilder.group({
              firstName: new FormControl(user.firstName, [
                Validators.required,
                Validators.minLength(3),
              ]),
              lastName: new FormControl(user.lastName, [
                Validators.required,
                Validators.minLength(3),
              ]),
              address: new FormControl(user.address, Validators.minLength(6)),
            });
          }
        })
      )
      .subscribe();
  }

  ngOnInit(): void {
    this.userService.getUser().pipe(untilDestroyed(this), take(1)).subscribe();
  }

  onSubmit(): void {
    if (!this.userForm?.valid) {
      this.messageService.showMessage({
        header: 'Invalid Input',
        message: ['Fill the fields correctly.'],
      });
    }

    this.userService
      .updateUser({
        firstName: this.userForm?.get('firstName')?.value,
        lastName: this.userForm?.get('lastName')?.value,
        address: this.userForm?.get('address')?.value,
      })
      .subscribe({
        error: (error) => {
          if (error instanceof HttpErrorResponse) {
            this.ValidateHttpError(error);
          }
        },
      });

    this.messageService.showMessage({
      header: 'Update Successful',
      message: ['User has been updated.'],
    });
  }

  private ValidateHttpError(error: HttpErrorResponse): void {
    if (error.status === 400) {
      this.messageService.showMessage({
        header: 'Update Failed',
        message: ['Invalid input.'],
      });
    }

    if (error.status === 401) {
      this.messageService.showMessage({
        header: 'Update Failed',
        message: ['Authentication fail.'],
      });
    }
  }
}
