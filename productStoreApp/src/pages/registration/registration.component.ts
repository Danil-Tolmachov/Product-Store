import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import UserService from '../../services/user.service';

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
    private formBuilder: FormBuilder
  ) {}

  onSubmit(): void {
    if (!this.validateInputValues()) {
      throw new Error('Invalid input');
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
          console.log('Error: ' + error);
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
}
