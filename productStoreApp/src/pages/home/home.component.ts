import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Observable, filter, switchMap, take } from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { AsyncPipe } from '@angular/common';
import ProductListComponent from '../../components/product-list/product-list.component';
import { type IProduct } from '../../interfaces/IProduct';
import ControlsFilterBarComponent from '../../components/controls-filter-bar/controls-filter-bar.component';
import ProductService from '../../services/product.service';
import { type ICategory } from '../../interfaces/ICategory';
import CategoryService from '../../services/category.service';
import CartService from '../../services/cart.service';
import UserService from '../../services/user.service';

@UntilDestroy()
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ProductListComponent, ControlsFilterBarComponent, AsyncPipe],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class HomeComponent implements OnInit {
  title: string = 'Product Store';

  categoriesList$: Observable<ICategory[]> | null = null;

  productsList$: Observable<IProduct[]> | null = null;

  constructor(
    private readonly titleService: Title,
    private readonly productService: ProductService,
    private readonly categoryService: CategoryService,
    private readonly cartService: CartService,
    private readonly userService: UserService,
    private readonly cd: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    // Set Title
    this.titleService.setTitle(this.title);

    // Get products
    this.productsList$ = this.productService.getProducts();

    // Get categories
    this.categoriesList$ = this.categoryService.getCategories();

    this.userService.currentUser
      .pipe(
        untilDestroyed(this),
        filter((user) => !!user),
        take(1),
        switchMap(() => this.cartService.getCart())
      )
      .subscribe();
  }
}
