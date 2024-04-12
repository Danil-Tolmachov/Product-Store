import { Component, Input } from '@angular/core';
import { Category } from '../../../interfaces/Category';
import { Router } from '@angular/router';

@Component({
    selector: 'app-category-selector',
    standalone: true,
    imports: [],
    templateUrl: './category-selector.component.html',
    styleUrl: './category-selector.component.scss'
})
export class CategorySelectorComponent {
    @Input() categories: Category[] = [];

    constructor(private router: Router){
    }

    onSelectionChange(event: Event): void {
        let selectedValue = (event.target as HTMLInputElement).value;
        this.router.navigate(['/category/' + selectedValue]);
    }
}
