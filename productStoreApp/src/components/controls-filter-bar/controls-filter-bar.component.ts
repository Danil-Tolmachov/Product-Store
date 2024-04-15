import { Component, Input } from '@angular/core';
import { CategorySelectorComponent } from './category-selector/category-selector.component';
import { ICategory } from '../../interfaces/ICategory';

@Component({
    selector: 'app-controls-filter-bar',
    standalone: true,
    imports: [CategorySelectorComponent],
    templateUrl: './controls-filter-bar.component.html',
    styleUrl: './controls-filter-bar.component.scss'
})
export class ControlsFilterBarComponent {
    @Input() categories: ICategory[] = [];

    ngOnInit(): void {
        console.log(this.categories);
    }
}
