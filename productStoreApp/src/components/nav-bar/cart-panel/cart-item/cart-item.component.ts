import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  Host,
  Input,
} from '@angular/core';
import ImageContainerComponent from '../../../image-container/image-container.component';
import { type ICartItem } from '../../../../interfaces/ICartItem';
import CartPanelComponent from '../cart-panel.component';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { take } from 'rxjs';

@UntilDestroy()
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

  constructor(@Host() private readonly cartPanel: CartPanelComponent) {
  }

  deleteButtonClick(id: number): void {
    console.log(this.item);
    this.cartPanel.deleteCartItem(id);
  }
}
