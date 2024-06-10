import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import ProductCardComponent from './product-card/product-card.component';
import { IProduct } from '../../../core/interfaces/IProduct';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [ProductCardComponent],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ProductListComponent {
  @Input() productsList: IProduct[] | null = [];
}
