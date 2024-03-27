import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { ProductItemBriefComponent } from './product-list/product-item-brief/product-item-brief.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,
            NavBarComponent, 
            ProductItemBriefComponent ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Products Store';
}
