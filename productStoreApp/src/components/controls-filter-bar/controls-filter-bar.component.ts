import { Component } from '@angular/core';
import { CategorySelectorComponent } from './category-selector/category-selector.component';
import { Category } from '../../interfaces/Category';

@Component({
    selector: 'app-controls-filter-bar',
    standalone: true,
    imports: [CategorySelectorComponent],
    templateUrl: './controls-filter-bar.component.html',
    styleUrl: './controls-filter-bar.component.scss'
})
export class ControlsFilterBarComponent {
    categories: Category[] = [
        {
          id: 1,
          name: 'Category1',
          items: [],
        },
        {
          id: 2,
          name: 'Category2',
          items: [],
        },
        {
          id: 3,
          name: 'Category3',
          items: [],
        },
    ];
}
