import { Component } from '@angular/core';
import UserService from '../../services/user.service';
import TextPanelComponent from '../../components/text-panel/text-panel.component';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { tap } from 'rxjs';
import ButtonComponent from '../../components/button/button.component';
import MessageService from '../../services/message.service';
import { HttpErrorResponse } from '@angular/common/http';

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
})
export class ProfileComponent {
  user$ = this.userService.currentUser;

  userForm: FormGroup | null = null;

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
              firstName: user.firstName,
              lastName: user.lastName,
              address: user.address,
            });
          }
        })
      )
      .subscribe();
  }

  onSubmit(): void {
    if (!this.userForm?.valid) {
      return;
    }

    this.userService.updateUser({
      firstName: this.userForm?.get('firstName')?.value,
      lastName: this.userForm?.get('lastName')?.value,
      address: this.userForm?.get('address')?.value,
    }).subscribe({
      error: (error) => {
        if (error instanceof HttpErrorResponse) {
          this.ValidateHttpError(error);
        }
      }
    });

    this.messageService.showMessage({header: 'Update Successful', message: ['User has been updated.']})
  }

  private ValidateHttpError(error: HttpErrorResponse): void {
    if (error.status == 400) {
      this.messageService.showMessage({header: 'Update Failed', message: ['Invalid input.']})
    }

    if (error.status == 401) {
      this.messageService.showMessage({header: 'Update Failed', message: ['Authentication fail.']})
    }
  }
}
