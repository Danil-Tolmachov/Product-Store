import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { type IProduct } from '../../../interfaces/IProduct';
import ImageContainerComponent from '../../image-container/image-container.component';

@Component({
  selector: 'app-product-item-brief',
  standalone: true,
  imports: [CommonModule, RouterLink, ImageContainerComponent],
  templateUrl: './product-item-brief.component.html',
  styleUrl: './product-item-brief.component.scss',
})
export default class ProductItemBriefComponent {
  @Input() product: IProduct = {
    id: 0,
    name: 'null',
    price: 0,
    discount: 0,
    unitMeasure: '',
    imagePaths: [],
    category: null,
    description: 'Description1',
    specifications: [{ name: 'Name1', value: 'Value1' }],
  };
}
