import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-text-panel',
  standalone: true,
  imports: [],
  templateUrl: './text-panel.component.html',
  styleUrl: './text-panel.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class TextPanelComponent {}
