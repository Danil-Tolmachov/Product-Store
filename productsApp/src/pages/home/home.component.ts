import { Component } from '@angular/core';
import { ProductListComponent } from '../../components/product-list/product-list.component';
import { Product } from '../../interfaces/Product';
import { Title } from '@angular/platform-browser';
import { ControlsFilterBarComponent } from '../../components/controls-filter-bar/controls-filter-bar.component';

@Component({
    selector: 'app-home',
    standalone: true,
    imports: [ ProductListComponent,
               ControlsFilterBarComponent ],
    templateUrl: './home.component.html',
    styleUrl: './home.component.scss'
})
export class HomeComponent {
    title: string = 'Product Store';
    productsList: Product[] = [
        { 
            id: 1, 
            name: 'Soda Coca-Cola Zero, 1.25', 
            price: 1.5, 
            discount: 0.5,
            imageUrl: new URL('https://images.silpo.ua/products/1600x1600/webp/68427050-3478-4e70-b7d7-1a1a5d9fd1cf.png'),
            category: { id: 1, name: 'Drinks', items: []},
            description: 'Description1',
            specifications: [
                { name: 'Name1', value: 'Value1' }
            ] 
        },
        { 
            id: 2, 
            name: 'Soda Coca-Cola Cherry-Vanilia, 0.355', 
            price: 0.7, 
            discount: 0.1, 
            imageUrl: new URL('https://images.silpo.ua/products/1600x1600/webp/20495df9-d0f7-406d-806f-3314f1243e73.png'),
            category: { id: 1, name: 'Drinks', items: []},
            description: 'Description2',
            specifications: [
                { name: 'Name2', value: 'Value2' }
            ] 
        },
    ];

    constructor(private titleService: Title) {
        // Set Title
        titleService.setTitle(this.title);
    }
}
