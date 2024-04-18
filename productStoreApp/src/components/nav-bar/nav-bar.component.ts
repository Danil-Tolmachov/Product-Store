import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import CartPanelComponent from './cart-panel/cart-panel.component';
import UserService from '../../services/user.service';
import { AuthDropdownComponent } from '../auth-dropdown/auth-dropdown.component';
import { Observable } from 'rxjs';
import { IUser } from '../../interfaces/IUser';

interface INavButton {
  title: string;
  link: string;
  styles: object;
}

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [
    RouterLink,
    RouterLinkActive,
    CommonModule,
    CartPanelComponent,
    AuthDropdownComponent,
  ],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss',
})
export default class NavBarComponent implements OnInit {
  @ViewChild(AuthDropdownComponent)
  authDropdownInstance: AuthDropdownComponent | null = null;

  @ViewChild(CartPanelComponent)
  cartPanelInstance: CartPanelComponent | null = null;

  user: IUser | null = null;

  navButtons: INavButton[] = [
    {
      title: 'About Us',
      link: '/about-us',
      styles: {
        '--pill-accent': 'var(--french-violet)',
      },
    },
    {
      title: 'Delivery',
      link: '/delivery',
      styles: {
        '--pill-accent': 'var(--french-violet)',
      },
    },
    {
      title: 'Home',
      link: '/home',
      styles: {
        '--pill-accent': 'var(--french-violet)',
      },
    },
  ];

  constructor(
    private readonly userService: UserService,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.userService.currentUser.subscribe((user) => {
      this.user = user;
    });
  }

  loginButtonClick(): void {
    if (this.userService.checkAuthenticated()) {
      this.authDropdownInstance?.switchDropdown();
    } else {
      this.router.navigate(['/', 'login']);
    }
  }

  cartButtonClick(): void {
    this.cartPanelInstance?.switchCartPanel();
  }
}
