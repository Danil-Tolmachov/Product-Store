import { AsyncPipe } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

const countPresets: number[] = [4, 8, 12, 16, 20];
const DEFAULT_PRESET: number = 8;

@Component({
  selector: 'app-count-selector',
  standalone: true,
  imports: [AsyncPipe],
  templateUrl: './count-selector.component.html',
  styleUrl: './count-selector.component.scss',
})
export class CountSelectorComponent {
  protected options: number[] = countPresets;
  defaultPreset: number = DEFAULT_PRESET;

  @Input() chosenPreset: Observable<number> | null = null;

  constructor(private readonly router: Router) {}

  onSelectionChange(event: Event): void {
    const selectedValue = (event.target as HTMLInputElement).value;

    this.router.navigate([], {
      queryParams: { page: 1, count: +selectedValue },
    });
  }
}
