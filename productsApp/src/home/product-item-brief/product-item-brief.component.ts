import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-product-item-brief',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-item-brief.component.html',
  styleUrl: './product-item-brief.component.scss'
})
export class ProductItemBriefComponent {
  @Input() imageSrc: string = '';
}
