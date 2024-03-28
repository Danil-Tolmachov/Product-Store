import { Component, Input } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Product } from '../../interfaces/Product';

@Component({
    selector: 'app-product',
    standalone: true,
    imports: [],
    templateUrl: './product.component.html',
    styleUrl: './product.component.scss'
})
export class ProductComponent {
    @Input() product: Product = { 
        id: 0, 
        name: '', 
        price: 0, 
        discount: 0,
        imageUrl: null,
        category: null,
        description: '',
        specifications: [] 
    };
    
    constructor(private titleService: Title) {
        // Set Title
        titleService.setTitle(this.product.name);
    }
}
