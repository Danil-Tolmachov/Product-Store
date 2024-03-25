import { Component } from '@angular/core';
import { ProductItemBriefComponent } from './product-item-brief/product-item-brief.component';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [ProductItemBriefComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent {

}
