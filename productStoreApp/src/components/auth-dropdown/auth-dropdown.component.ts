import { animate, style, transition, trigger } from '@angular/animations';
import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';
import { RouterLink } from '@angular/router';
import UserService from '../../services/user.service';
import { UntilDestroy } from '@ngneat/until-destroy';
import CartService from '../../services/cart.service';
import { tap } from 'rxjs';

interface IDropdownLink {
  text: string;
  link: string;
}

@UntilDestroy()
@Component({
  selector: 'app-auth-dropdown',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './auth-dropdown.component.html',
  styleUrl: './auth-dropdown.component.scss',
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
export default class AuthDropdownComponent implements OnInit {
  isActive: boolean = false;

  dropdownLinks: IDropdownLink[] = [
    {
      text: 'My Profile',
      link: '/profile',
    },
    {
      text: 'My Orders',
      link: '/orders',
    },
  ];

  constructor(
    private readonly userService: UserService,
    private readonly cdr: ChangeDetectorRef,
    private readonly cartService: CartService
  ) {}

  ngOnInit(): void {
    this.userService.currentUser
      .pipe(
        tap((user) => {
          if (user == null) {
            this.isActive = false;
          }
        })
      )
      .subscribe();
  }

  switchDropdown(): void {
    this.isActive = !this.isActive;
    this.cdr.markForCheck();
  }

  logoutButtonClick(): void {
    this.userService.logoutSession();
    this.cartService.clearCart();
    this.cdr.markForCheck();
  }
}
