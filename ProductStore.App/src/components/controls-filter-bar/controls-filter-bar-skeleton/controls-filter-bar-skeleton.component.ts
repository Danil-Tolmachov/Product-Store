import { Component } from '@angular/core';
import { CategorySelectorSkeletonComponent } from '../category-selector/category-selector-skeleton/category-selector-skeleton.component';
import { CountSelectorSkeletonComponent } from '../count-selector/count-selector-skeleton/count-selector-skeleton.component';

@Component({
  selector: 'app-controls-filter-bar-skeleton',
  standalone: true,
  imports: [CategorySelectorSkeletonComponent, CountSelectorSkeletonComponent],
  templateUrl: './controls-filter-bar-skeleton.component.html',
  styleUrl: './controls-filter-bar-skeleton.component.scss',
})
export class ControlsFilterBarSkeletonComponent {}
