import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import CategorySelectorComponent from './category-selector/category-selector.component';
import { CountSelectorComponent } from './count-selector/count-selector.component';
import { ICategory } from '../../../core/interfaces/ICategory';

@Component({
  selector: 'app-controls-filter-bar',
  standalone: true,
  imports: [CategorySelectorComponent, CountSelectorComponent, CommonModule],
  templateUrl: './controls-filter-bar.component.html',
  styleUrl: './controls-filter-bar.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ControlsFilterBarComponent {
  @Input() categories: ICategory[] | null = [];

  @Input() chosenCountPreset: Observable<number> | null = null;
}
