import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import CategorySelectorComponent from './category-selector/category-selector.component';
import { type ICategory } from '../../interfaces/ICategory';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-controls-filter-bar',
  standalone: true,
  imports: [CategorySelectorComponent, CommonModule],
  templateUrl: './controls-filter-bar.component.html',
  styleUrl: './controls-filter-bar.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ControlsFilterBarComponent {
  @Input() categories: ICategory[] | null = [];
}
