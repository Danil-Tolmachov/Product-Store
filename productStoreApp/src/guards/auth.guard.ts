import { Injectable } from '@angular/core';
import UserService from '../services/user.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export default class AuthGuard {
  constructor(
    private readonly userService: UserService,
    private readonly router: Router
  ) {}

  canActivate(): boolean {
    if (this.userService.checkAuthenticated()) {
      this.router.navigate(['home'])
      return false;
    } else {
      return true;
    }
  }
}
