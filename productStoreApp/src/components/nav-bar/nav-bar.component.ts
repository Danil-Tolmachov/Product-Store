import { AsyncPipe, CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import CartPanelComponent from './cart-panel/cart-panel.component';
import UserService from '../../services/user.service';
import AuthDropdownComponent from '../auth-dropdown/auth-dropdown.component';
import { IUser } from '../../interfaces/IUser';
import { Observable, Subscription, take, tap } from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
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
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class NavBarComponent implements OnInit {
  @ViewChild(AuthDropdownComponent)
  authDropdownInstance: AuthDropdownComponent | null = null;

  @ViewChild(CartPanelComponent)
  cartPanelInstance: CartPanelComponent | null = null;

  user$: Observable<IUser | null> | null = null;

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
    // Get user
    if (this.userService.checkAuthenticated()) {
      this.user$ = this.userService
        .getUser()
        .pipe(take(1), untilDestroyed(this));
    }
  }

  loginButtonClick(): void {
    if (this.userService.checkAuthenticated()) {
      // Show dropdown if authenticated
      this.authDropdownInstance?.switchDropdown();
    } else {
      // Redirect to login page if not authenticated
      this.router.navigate(['/', 'login']);
    }
  }

  cartButtonClick(): void {
    // Show cart panel
    this.cartPanelInstance?.switchCartPanel();
  }
}

interface INavButton {
  title: string;
  link: string;
  styles: object;
}
