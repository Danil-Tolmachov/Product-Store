import { Component } from '@angular/core';
import { TextPanelComponent } from '../../components/text-panel/text-panel.component';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-about-us',
    standalone: true,
    imports: [TextPanelComponent],
    templateUrl: './about-us.component.html',
    styleUrl: './about-us.component.scss'
})
export class AboutUsComponent {
    title = "About Us";
    
    constructor(private titleService: Title) {
        // Set Title
        titleService.setTitle(this.title);
    }
}
