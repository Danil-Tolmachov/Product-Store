import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBarComponent } from '../components/nav-bar/nav-bar.component';
import { ProductItemBriefComponent } from '../components/product-list/product-item-brief/product-item-brief.component';
import { FooterComponent } from '../components/footer/footer.component';
import { Title } from '@angular/platform-browser';

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterOutlet,
              NavBarComponent,
              ProductItemBriefComponent,
              FooterComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss'
})
export class AppComponent {
    title = 'Products Store';

    constructor(private titleService: Title){
        // Set Title
        titleService.setTitle(this.title);
    }
}