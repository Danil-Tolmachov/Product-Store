import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import TextPanelComponent from '../../components/text-panel/text-panel.component';

@Component({
  selector: 'app-about-us',
  standalone: true,
  imports: [TextPanelComponent],
  templateUrl: './about-us.component.html',
  styleUrl: './about-us.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class AboutUsComponent {
  title = 'About Us';

  constructor(private readonly titleService: Title) {
    // Set Title
    titleService.setTitle(this.title);
  }
}
