import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import TextPanelComponent from '../../shared/components/text-panel/text-panel.component';

@Component({
  selector: 'app-delivery',
  standalone: true,
  imports: [TextPanelComponent],
  templateUrl: './delivery.component.html',
  styleUrl: './delivery.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class DeliveryComponent {
  title = 'Delivery Info';

  constructor(private readonly titleService: Title) {
    // Set Title
    titleService.setTitle(this.title);
  }
}
