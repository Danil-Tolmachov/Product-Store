import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-product-item-brief',
  standalone: true,
  imports: [],
  templateUrl: './product-item-brief.component.html',
  styleUrl: './product-item-brief.component.scss'
})
export class ProductItemBriefComponent {
  @Input() imageSrc: string = '';
}
