import {
  ChangeDetectionStrategy,
  Component,
  Host,
  Input,
  OnInit,
} from '@angular/core';
import ImageContainerComponent from '../../../image-container/image-container.component';
import { type ICartItem } from '../../../../interfaces/ICartItem';
import CartPanelComponent from '../cart-panel.component';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import CartService from '../../../../services/cart.service';
import { Subject, debounceTime, switchMap } from 'rxjs';

@UntilDestroy()
@Component({
  selector: 'app-cart-item',
  standalone: true,
  imports: [ImageContainerComponent],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class CartItemComponent implements OnInit {
  private inputChangeSubject = new Subject<number>();

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

  constructor(
    @Host() private readonly cartPanel: CartPanelComponent,
    private readonly cartService: CartService
  ) {}

  ngOnInit(): void {
    // Check for input update
    this.inputChangeSubject
      .pipe(
        untilDestroyed(this),
        debounceTime(1500),
        switchMap((value) => {
          return this.cartService.addCartItem(this.item.product.id, value);
        })
      )
      .subscribe();
  }

  onInputChange(quantity: string): void {
    const quantityNumber = parseInt(quantity);
    this.inputChangeSubject.next(quantityNumber);
  }

  deleteButtonClick(id: number): void {
    console.log(this.item);
    this.cartPanel.deleteCartItem(id);
  }
}
