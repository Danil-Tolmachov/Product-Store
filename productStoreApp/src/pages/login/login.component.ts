import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { take } from 'rxjs';
import UserService from '../../services/user.service';
import MessageService from '../../services/message.service';
import LinkButtonComponent from '../../components/link-button/link-button.component';
import ButtonComponent from '../../components/button/button.component';

@UntilDestroy()
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink,
    LinkButtonComponent,
    ButtonComponent,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class LoginComponent {
  loginForm = this.formBuilder.group({
    username: '',
    password: '',
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
      .loginSession(
        this.loginForm.value.username!,
        this.loginForm.value.password!
      )
      .pipe(untilDestroyed(this), take(1))
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

  private processRequestError(error: HttpErrorResponse) {
    if (error.status === 400 || error.status === 415) {
      this.messageService.showMessage({
        header: 'Authorization failed',
        message: ['Invalid input.'],
      });
    }

    if (error.status === 401) {
      this.messageService.showMessage({
        header: 'Authorization failed',
        message: ['Invalid credentials.'],
      });
    }

    if (error.status === 0) {
      this.messageService.showMessage({
        header: 'Authorization failed',
        message: [
          'Failed request to server.',
          'Rectify internet connection or try again later.',
        ],
      });
    }
  }
}
