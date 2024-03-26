import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Product } from '../../interfaces/Product';

@Component({
    selector: 'app-product-item-brief',
    standalone: true,
    imports: [CommonModule, RouterLink],
    templateUrl: './product-item-brief.component.html',
    styleUrl: './product-item-brief.component.scss'
})
export class ProductItemBriefComponent {
    @Input() product: Product = {
        id: 0,
        name: 'null',
        price: 0,
        discount: 0,
        imageUrl: null,
        description: 'Description1',
        specifications: [
            { name: 'Name1', value: 'Value1' }
        ]
    };
}
