import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { toSignal } from '@angular/core/rxjs-interop';
import {
  BehaviorSubject,
  Observable,
  filter,
  map,
  switchMap,
  take,
  tap,
} from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { AsyncPipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import ProductListComponent from '../../components/product-list/product-list.component';
import { IProductPage } from '../../interfaces/IProduct';
import ControlsFilterBarComponent from '../../components/controls-filter-bar/controls-filter-bar.component';
import ProductService from '../../services/product.service';
import { type ICategory } from '../../interfaces/ICategory';
import CategoryService from '../../services/category.service';
import CartService from '../../services/cart.service';
import UserService from '../../services/user.service';
import ProductListSkeletonComponent from '../../components/product-list/product-list-skeleton/product-list-skeleton.component';
import { PageBarComponent } from '../../components/page-bar/page-bar.component';
import { PageBarSkeletonComponent } from '../../components/page-bar/page-bar-skeleton/page-bar-skeleton.component';
import { ControlsFilterBarSkeletonComponent } from '../../components/controls-filter-bar/controls-filter-bar-skeleton/controls-filter-bar-skeleton.component';

const DEFAULT_PAGE = 1;
const PRODUCTS_PER_PAGE = 8;

@UntilDestroy()
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    ProductListComponent,
    ProductListSkeletonComponent,
    ControlsFilterBarSkeletonComponent,
    ControlsFilterBarComponent,
    PageBarComponent,
    PageBarSkeletonComponent,
    AsyncPipe,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class HomeComponent implements OnInit {
  title: string = 'Product Store';

  pageSubject$ = new BehaviorSubject<number>(DEFAULT_PAGE);

  pageQuery: Observable<number> = this.route.queryParams.pipe(
    map((params) => {
      return params['page'] ?? DEFAULT_PAGE;
    }),
    tap((page) => {
      this.pageSubject$.next(page);
    })
  );

  countSubject$ = new BehaviorSubject<number>(PRODUCTS_PER_PAGE);

  countQuery: Observable<number> = this.route.queryParams.pipe(
    map((params) => {
      return params['count'] ?? PRODUCTS_PER_PAGE;
    }),
    tap((count) => {
      this.countSubject$.next(+count);
    })
  );

  productsPage$: Observable<IProductPage> = this.pageQuery.pipe(
    switchMap((page) =>
      this.countQuery.pipe(
        switchMap((count) => this.productService.getProductsPage(page, count))
      )
    )
  );

  pagesCount$: Observable<number | null> = this.productsPage$.pipe(
    map((page) => {
      return page.pagesCount;
    })
  );

  categoriesList$: Observable<ICategory[]> =
    this.categoryService.getCategories();

  productsPageSignal = toSignal(this.productsPage$);

  categoriesListSignal = toSignal(this.categoriesList$);

  constructor(
    private readonly titleService: Title,
    private readonly route: ActivatedRoute,
    private readonly productService: ProductService,
    private readonly categoryService: CategoryService,
    private readonly cartService: CartService,
    private readonly userService: UserService
  ) {
    // Set Title
    this.titleService.setTitle(this.title);
  }

  ngOnInit(): void {
    this.userService.getUser().pipe(untilDestroyed(this), take(1)).subscribe();

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
