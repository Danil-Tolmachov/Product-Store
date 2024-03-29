import { CommonModule, } from '@angular/common';
import { Component, ViewChild, ChangeDetectorRef } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CartPanelComponent } from './cart-panel/cart-panel.component';


@Component({
    selector: 'app-nav-bar',
    standalone: true,
    imports: [RouterLink, RouterLinkActive, CommonModule, CartPanelComponent],
    templateUrl: './nav-bar.component.html',
    styleUrl: './nav-bar.component.scss'
})
export class NavBarComponent {
    @ViewChild(CartPanelComponent) cartPanelInstance: CartPanelComponent | null = null;
    cartButtonClasses: string[] = [
        'pill'
    ];

    cartButtonClick(): void {
        let array: string[] = this.cartButtonClasses;

        if (array.includes('pill-active')) {
            array.splice(array.indexOf('pill-active'), 1);
        }
        else {
            array.push('pill-active');
        }

        this.cartPanelInstance?.switchCartPanel();
    };
}
