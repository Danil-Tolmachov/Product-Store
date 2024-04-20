import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import ImageContainerComponent from '../../../image-container/image-container.component';
import { type ICartItem } from '../../../../interfaces/ICartItem';

@Component({
  selector: 'app-cart-item',
  standalone: true,
  imports: [ImageContainerComponent],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class CartItemComponent {
  @Input() item: ICartItem = {
    product: {
      id: 0,
      name: '',
      price: 0,
      discount: 0,
      unitMeasure: '',
      imagePaths: [],
      category: null,
      description: '',
      specifications: [],
    },
    quantity: 0,
  };
}
