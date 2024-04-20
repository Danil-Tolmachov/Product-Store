import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-link-button',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './link-button.component.html',
  styleUrl: './link-button.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class LinkButtonComponent {
  @Input() routeLink: string | null = null;
}
