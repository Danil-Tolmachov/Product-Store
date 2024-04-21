import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import ProductItemBriefComponent from './product-item-brief/product-item-brief.component';
import { type IProduct } from '../../interfaces/IProduct';
import CartService from '../../services/cart.service';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { take } from 'rxjs';

@UntilDestroy()
@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [ProductItemBriefComponent],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ProductListComponent {
  @Input() productsList: IProduct[] | null = [];

  constructor(private readonly cartService: CartService) {}

  addToCart(id: number): void {
    this.cartService
      .addCartItem(id)
      .pipe(untilDestroyed(this), take(1))
      .subscribe();
  }
}
