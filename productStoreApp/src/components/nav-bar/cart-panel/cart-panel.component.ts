import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { trigger, transition, style, animate } from '@angular/animations';
import { CartItemComponent } from './cart-item/cart-item.component';
import { ICartProduct } from '../../../interfaces/ICartProduct';

@Component({
    selector: 'app-cart-panel',
    standalone: true,
    imports: [ CommonModule, CartItemComponent ],
    templateUrl: './cart-panel.component.html',
    styleUrl: './cart-panel.component.scss',
    animations: [
        trigger('fadeInOutAnimation', [
            transition(':enter', [
                style({ opacity: 0 }),
                animate('0.1s', style({ opacity: 1 }))
            ]),
            transition(':leave', [
                animate('0.2s', style({ opacity: 0 }))
            ])
        ])
    ],
})
export class CartPanelComponent {
    isActive: boolean = false;
    @Input() cartProducts: ICartProduct[] = [
        { 
            product: 
            {
                id: 1, 
                name: 'Soda Coca-Cola Zero, 1.25', 
                price: 1.5, 
                discount: 0.5,
                unitMeasure: '',
                imagePathes: [],
                category: { id: 1, name: 'Drinks', items: []},
                description: 'Description1',
                specifications: [
                    { name: 'Name1', value: 'Value1' }
                ] 
            },
            quantity: 2
        },
        { 
            product: 
            {
                id: 2, 
                name: 'Soda Coca-Cola Cherry-Vanilia, 0.355', 
                price: 0.7, 
                discount: 0.1, 
                unitMeasure: '',
                imagePathes: [],
                category: { id: 1, name: 'Drinks', items: []},
                description: 'Description2',
                specifications: [
                    { name: 'Name2', value: 'Value2' }
                ] 
            },
            quantity: 5
        },
    ];

    switchCartPanel(): void {
        this.isActive = !this.isActive;
    }
}
