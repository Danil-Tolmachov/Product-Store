import { Component, Input } from '@angular/core';
import { ProductItemBriefComponent } from './product-item-brief/product-item-brief.component';
import { IProduct } from '../../interfaces/IProduct';

@Component({
    selector: 'app-product-list',
    standalone: true,
    imports: [ProductItemBriefComponent],
    templateUrl: './product-list.component.html',
    styleUrl: './product-list.component.scss'
})
export class ProductListComponent {
    @Input() productsList: IProduct[] = [];
}
