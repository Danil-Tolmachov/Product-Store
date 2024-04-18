import { Component } from '@angular/core';
import { Router } from '@angular/router';
import UserService from '../../services/user.service';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export default class LoginComponent {
  loginForm = this.formBuilder.group({
    username: '',
    password: '',
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
      .loginSession(
        this.loginForm.value.username!,
        this.loginForm.value.password!
      )
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
      typeof this.loginForm.value.username === 'string' &&
      this.loginForm.value.username.trim().length === 0
    ) {
      return false;
    }

    if (
      typeof this.loginForm.value.password === 'string' &&
      this.loginForm.value.password.trim().length === 0
    ) {
      return false;
    }

    return true;
  }
}
