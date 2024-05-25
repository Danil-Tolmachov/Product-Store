import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { take } from 'rxjs';
import { trigger, transition, style, animate } from '@angular/animations';
import { type IProduct } from '../../../interfaces/IProduct';
import ImageContainerComponent from '../../image-container/image-container.component';
import CartService from '../../../services/cart.service';

@UntilDestroy()
@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [CommonModule, RouterLink, ImageContainerComponent],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  animations: [
    trigger('fadeInOutAnimation', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('0.2s', style({ opacity: 1 })),
      ]),
      transition(':leave', [animate('0.2s', style({ opacity: 0 }))]),
    ]),
  ],
})
export default class ProductCardComponent {
  @Input() product: IProduct = {
    id: 0,
    name: 'null',
    price: 0,
    discount: 0,
    unitMeasure: '',
    imagePaths: [],
    category: null,
    description: '',
    specifications: [],
  };

  constructor(private readonly cartService: CartService) {}

  addButtonClick(id: number): void {
    this.cartService
      .addCartItem(id)
      .pipe(untilDestroyed(this), take(1))
      .subscribe();
  }
}