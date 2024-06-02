import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { trigger, transition, style, animate } from '@angular/animations';
import { CommonModule } from '@angular/common';
import UserService from '../../services/user.service';
import MessageService from '../../services/message.service';
import LinkButtonComponent from '../../components/link-button/link-button.component';
import ButtonComponent from '../../components/button/button.component';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    LinkButtonComponent,
    ButtonComponent,
  ],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
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
export default class RegistrationComponent {
  registrationForm = this.formBuilder.group({
    username: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
    firstName: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
    ]),
    lastName: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
    ]),
    address: new FormControl(null, Validators.minLength(6)),
    phone: new FormControl(null, [
      Validators.pattern('^\\+?[1-9]\\d{0,2}[-.\\s]?(\\(?\\d{1,4}\\)?[-.\\s]?)*\\d{1,4}[-.\\s]?\\d{1,9}$'),
      Validators.minLength(3),
    ]),
  });

  get username() {
    return this.registrationForm.get('username')!;
  }

  get password() {
    return this.registrationForm.get('password')!;
  }

  get firstName() {
    return this.registrationForm.get('firstName')!;
  }

  get lastName() {
    return this.registrationForm.get('lastName')!;
  }

  get address() {
    return this.registrationForm.get('address')!;
  }

  get phone() {
    return this.registrationForm.get('phone')!;
  }

  constructor(
    private readonly router: Router,
    private readonly userService: UserService,
    private readonly messageService: MessageService,
    private formBuilder: FormBuilder
  ) {}

  onSubmit(): void {
    if (!this.registrationForm.valid) {
      this.messageService.showMessage({
        header: 'Invalid input',
        message: ['Fill the fields correctly.'],
      });
      return;
    }

    this.userService
      .registration({
        username: this.registrationForm.value.username!,
        password: this.registrationForm.value.username!,
        firstName: this.registrationForm.value.username!,
        lastName: this.registrationForm.value.username!,
        address: this.registrationForm.value.username!,
        phone: this.registrationForm.value.username!,
      })
      .subscribe({
        complete: () => {
          this.router.navigate(['home']);
        },
        error: (error) => {
          this.processRequestError(error);
        },
      })
      .unsubscribe();
  }

  private processRequestError(error: HttpErrorResponse) {
    if (error.status === 400 || error.status === 415) {
      this.messageService.showMessage({
        header: 'Registration failed',
        message: ['Invalid input.'],
      });
    }

    if (error.status === 0) {
      this.messageService.showMessage({
        header: 'Registration failed',
        message: [
          'Failed request to server.',
          'Rectify internet connection or try again later.',
        ],
      });
    }
  }
}
