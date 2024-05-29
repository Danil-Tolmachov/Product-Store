import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
} from '@angular/core';
import { trigger, transition, style, animate } from '@angular/animations';
import { Observable } from 'rxjs';
import CartItemComponent from './cart-item/cart-item.component';
import CartService from '../../../services/cart.service';
import { ICart } from '../../../interfaces/ICart';
import CheckoutScreenService from '../../../services/checkout-screen.service';
import UserService from '../../../services/user.service';
import { IUser } from '../../../interfaces/IUser';
import ImageContainerComponent from '../../image-container/image-container.component';

@Component({
  selector: 'app-cart-panel',
  standalone: true,
  imports: [CommonModule, CartItemComponent, ImageContainerComponent],
  templateUrl: './cart-panel.component.html',
  styleUrl: './cart-panel.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  animations: [
    trigger('fadeInOutAnimation', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('0.1s', style({ opacity: 1 })),
      ]),
      transition(':leave', [animate('0.2s', style({ opacity: 0 }))]),
    ]),
  ],
})
export default class CartPanelComponent {
  isActive: boolean = false;

  cart$: Observable<ICart | null> = this.cartService.cart;
  user$: Observable<IUser | null> = this.userService.currentUser;

  constructor(
    private readonly cartService: CartService,
    private readonly checkoutScreenService: CheckoutScreenService,
    private readonly userService: UserService,
    private readonly cdr: ChangeDetectorRef
  ) {}

  switchCartPanel(): void {
    this.isActive = !this.isActive;
    this.cdr.markForCheck();
  }

  checkoutButtonClick(): void {
    this.checkoutScreenService.showScreen();
  }
}
