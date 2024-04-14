import { Component, Input } from '@angular/core';
import CategorySelectorComponent from './category-selector/category-selector.component';
import { type ICategory } from '../../interfaces/ICategory';

@Component({
  selector: 'app-controls-filter-bar',
  standalone: true,
  imports: [CategorySelectorComponent],
  templateUrl: './controls-filter-bar.component.html',
  styleUrl: './controls-filter-bar.component.scss',
})
export default class ControlsFilterBarComponent {
  @Input() categories: ICategory[] = [];
}
