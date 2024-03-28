import { Component } from '@angular/core';
import { TextPanelComponent } from '../../components/text-panel/text-panel.component';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-delivery',
    standalone: true,
    imports: [TextPanelComponent],
    templateUrl: './delivery.component.html',
    styleUrl: './delivery.component.scss'
})
export class DeliveryComponent {
    title = "Delivery Info";

    constructor(private titleService: Title) {
        // Set Title
        titleService.setTitle(this.title);
    }
}
