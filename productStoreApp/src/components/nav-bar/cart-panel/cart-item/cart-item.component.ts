import { Component, Input } from '@angular/core';
import { ImageContainerComponent } from '../../../image-container/image-container.component';
import { ICartProduct } from '../../../../interfaces/ICartProduct';

@Component({
  selector: 'app-cart-item',
  standalone: true,
  imports: [ ImageContainerComponent ],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.scss'
})
export class CartItemComponent {
  @Input() item: ICartProduct = { 
    product: 
    {
      id: 0, 
      name: '', 
      price: 0, 
      discount: 0,
      unitMeasure: '',
      imagePathes: [],
      category: null,
      description: '',
      specifications: [] 
    },
    quantity: 0,
  }
}
