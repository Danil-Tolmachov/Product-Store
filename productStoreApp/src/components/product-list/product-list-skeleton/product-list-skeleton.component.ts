import { trigger, transition, style, animate } from '@angular/animations';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-product-list-skeleton',
  standalone: true,
  imports: [],
  templateUrl: './product-list-skeleton.component.html',
  styleUrl: './product-list-skeleton.component.scss',
  animations: [
    trigger('fadeInOutAnimation', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('300ms', style({ opacity: 1 })),
      ]),
    ]),
  ],
})
export class ProductListSkeletonComponent {
  @Input() skeletonProductsCount: number = 8;
}
