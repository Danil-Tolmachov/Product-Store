import { Component } from '@angular/core';
import { ProductItemBriefComponent } from './product-item-brief/product-item-brief.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ProductItemBriefComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
