import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { type ICategory } from '../../../interfaces/ICategory';

@Component({
  selector: 'app-category-selector',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './category-selector.component.html',
  styleUrl: './category-selector.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class CategorySelectorComponent {
  @Input() categories: ICategory[] = [];

  constructor(private readonly router: Router) {}

  onSelectionChange(event: Event): void {
    const selectedValue = (event.target as HTMLInputElement).value;

    this.router.navigate([`/category/${selectedValue}`]);
  }
}
