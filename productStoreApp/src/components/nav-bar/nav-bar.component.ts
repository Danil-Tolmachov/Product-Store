import { AsyncPipe, CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  ViewChild,
} from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { UntilDestroy } from '@ngneat/until-destroy';
import CartPanelComponent from './cart-panel/cart-panel.component';
import UserService from '../../services/user.service';
import AuthDropdownComponent from '../auth-dropdown/auth-dropdown.component';
import { IUser } from '../../interfaces/IUser';

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
    AsyncPipe,
  ],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class NavBarComponent {
  @ViewChild(AuthDropdownComponent)
  authDropdownInstance: AuthDropdownComponent | null = null;

  @ViewChild(CartPanelComponent)
  cartPanelInstance: CartPanelComponent | null = null;

  user$: Observable<IUser | null> = this.userService.currentUser.pipe(
    tap(() => this.cdr.markForCheck())
  );

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
    private readonly router: Router,
    private readonly cdr: ChangeDetectorRef
  ) {}

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
