import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import UserService from '../../services/user.service';
import MessageService from '../../services/message.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.scss',
})
export default class RegistrationComponent {
  registrationForm = this.formBuilder.group({
    username: '',
    password: '',
    firstName: '',
    lastName: '',
    address: null,
    phone: null,
  });

  constructor(
    private readonly router: Router,
    private readonly userService: UserService,
    private readonly messageService: MessageService,
    private formBuilder: FormBuilder
  ) {}

  onSubmit(): void {
    if (!this.validateInputValues()) {
      this.messageService.showMessage({
        header: 'Invalid input',
        message: ['Required fields should not be empty'],
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
      });
  }

  private validateInputValues(): boolean {
    if (
      typeof this.registrationForm.value.username === 'string' &&
      this.registrationForm.value.username.trim().length === 0
    ) {
      return false;
    }

    if (
      typeof this.registrationForm.value.password === 'string' &&
      this.registrationForm.value.password.trim().length === 0
    ) {
      return false;
    }

    if (
      typeof this.registrationForm.value.firstName === 'string' &&
      this.registrationForm.value.firstName.trim().length === 0
    ) {
      return false;
    }

    if (
      typeof this.registrationForm.value.lastName === 'string' &&
      this.registrationForm.value.lastName.trim().length === 0
    ) {
      return false;
    }

    return true;
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