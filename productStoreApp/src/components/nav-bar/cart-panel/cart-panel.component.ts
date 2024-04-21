import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
} from '@angular/core';
import { trigger, transition, style, animate } from '@angular/animations';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Observable, take, } from 'rxjs';
import CartItemComponent from './cart-item/cart-item.component';
import { ICartItem } from '../../../interfaces/ICartItem';
import CartService from '../../../services/cart.service';

@UntilDestroy()
@Component({
  selector: 'app-cart-panel',
  standalone: true,
  imports: [CommonModule, CartItemComponent],
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

  cartProducts$: Observable<ICartItem[]> = this.cartService.carItems;

  constructor(
    private readonly cartService: CartService,
    private readonly cdr: ChangeDetectorRef
  ) {}

  switchCartPanel(): void {
    this.isActive = !this.isActive;
    this.cdr.markForCheck();
  }

  deleteCartItem(id: number): void {
    this.cartService
      .deleteCartItem(id)
      .pipe(untilDestroyed(this), take(1))
      .subscribe({
        complete: () => {
          this.cdr.markForCheck();
        },
      });
  }
}
