import { trigger, transition, style, animate } from '@angular/animations';
import { AsyncPipe } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-product-list-skeleton',
  standalone: true,
  imports: [AsyncPipe],
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
export default class ProductListSkeletonComponent {
  @Input() skeletonProductsCount$: Observable<number> | null = null;
}
