import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { trigger, transition, style, animate } from '@angular/animations';

@Component({
    selector: 'app-cart-panel',
    standalone: true,
    imports: [ CommonModule ],
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

    switchCartPanel(): void {
        this.isActive = !this.isActive;
    }
}
