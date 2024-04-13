import { Component, Input } from '@angular/core';
import { ICategory } from '../../../interfaces/ICategory';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-category-selector',
    standalone: true,
    imports: [ CommonModule ],
    templateUrl: './category-selector.component.html',
    styleUrl: './category-selector.component.scss'
})
export class CategorySelectorComponent {
    @Input() categories: ICategory[] = [];

    constructor(private router: Router){
    }

    onSelectionChange(event: Event): void {
        let selectedValue = (event.target as HTMLInputElement).value;
        this.router.navigate(['/category/' + selectedValue]);
    }
}
