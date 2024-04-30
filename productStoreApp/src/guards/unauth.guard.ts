import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import UserService from '../services/user.service';

/**
 * A guard that prevents access to certain routes for authenticated users.
 * If a user is authenticated, the guard redirects them to the home page.
 */
@Injectable({
  providedIn: 'root',
})
export default class UnAuthGuard {
  constructor(
    private readonly userService: UserService,
    private readonly router: Router
  ) {}

  /**
   * Checks if the user is authenticated.
   * If authenticated, redirects to the home page.
   * @returns False if the user is authenticated, true otherwise.
   */
  canActivate(): boolean {
    if (!this.userService.checkAuthenticated()) {
      this.router.navigate(['home']);
      return false;
    }
    return true;
  }
}
