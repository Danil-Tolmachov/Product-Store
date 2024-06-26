import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { Observable, map, switchMap, tap } from 'rxjs';
import { CommonModule } from '@angular/common';
import SpecificationsContainerComponent from './specifications-container/specifications-container.component';
import { IProduct } from '../../core/interfaces/IProduct';
import CartService from '../../core/services/cart.service';
import ProductService from '../../core/services/product.service';
import ButtonComponent from '../../shared/components/button/button.component';
import ImageContainerComponent from '../../shared/components/image-container/image-container.component';
import ImageCarouselComponent from './image-carousel/image-carousel.component';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [
    CommonModule,
    ImageContainerComponent,
    ImageCarouselComponent,
    ButtonComponent,
    SpecificationsContainerComponent,
  ],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ProductComponent implements OnInit {
  protected showSpecifications: boolean = true;

  protected quantityValue: number = 1;

  productId$: Observable<number> | null = null;

  product$: Observable<IProduct> | null = null;

  constructor(
    private readonly titleService: Title,
    private readonly productService: ProductService,
    private readonly cartService: CartService,
    private readonly route: ActivatedRoute,
    private readonly cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    // Get productId
    this.productId$ = this.route.paramMap.pipe(
      map((params) => {
        return parseInt(params.get('productId') ?? '0', 10);
      })
    );

    // Get product
    this.product$ = this.productId$.pipe(
      switchMap((id) => {
        return this.productService.getProduct(id);
      }),
      tap((product) => {
        // Set Title
        this.titleService.setTitle(product.name);

        this.cdr.markForCheck();
      }),
      tap((product) => {
        if (product.specifications.length < 1) {
          this.showSpecifications = false;
        }
      })
    );
  }

  onChangeQuantity(newValue: string): void {
    const quantityNumber = parseInt(newValue, 10);
    this.quantityValue = quantityNumber;
  }

  addButtonClick(productId: number): void {
    this.cartService
      .addCartItem(productId, this.quantityValue)
      .pipe(
        tap(() => {
          this.cdr.markForCheck();
        })
      )
      .subscribe();
  }
}
