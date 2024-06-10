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
import { IProductPage, ICategory } from '../../core/interfaces/IProduct';
import CartService from '../../core/services/cart.service';
import CategoryService from '../../core/services/category.service';
import ProductService from '../../core/services/product.service';
import UserService from '../../core/services/user.service';
import { PageBarSkeletonComponent } from '../../shared/components/page-bar/page-bar-skeleton/page-bar-skeleton.component';
import { PageBarComponent } from '../../shared/components/page-bar/page-bar.component';
import ProductListSkeletonComponent from '../../shared/components/product-list/product-list-skeleton/product-list-skeleton.component';
import ProductListComponent from '../../shared/components/product-list/product-list.component';
import { ControlsFilterBarSkeletonComponent } from './controls-filter-bar/controls-filter-bar-skeleton/controls-filter-bar-skeleton.component';
import ControlsFilterBarComponent from './controls-filter-bar/controls-filter-bar.component';

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
  countSubject$ = new BehaviorSubject<number>(PRODUCTS_PER_PAGE);
  pagesCountSubject$ = new BehaviorSubject<number>(1);

  pageQuery: Observable<number> = this.route.queryParams.pipe(
    untilDestroyed(this),
    map((params) => {
      return params['page'] ?? DEFAULT_PAGE;
    }),
    tap((page) => {
      this.pageSubject$.next(page);
    })
  );

  countQuery: Observable<number> = this.route.queryParams.pipe(
    untilDestroyed(this),
    map((params) => {
      return params['count'] ?? PRODUCTS_PER_PAGE;
    }),
    tap((count) => {
      this.countSubject$.next(+count);
    })
  );

  productsPage$: Observable<IProductPage> = this.pageQuery.pipe(
    untilDestroyed(this),
    switchMap((page) =>
      this.countQuery.pipe(
        switchMap((count) => this.productService.getProductsPage(page, count))
      )
    ),
    tap((page) => {
      this.pagesCountSubject$.next(+(page.pagesCount ?? 1));
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
    if (this.userService.checkAuthenticated()) {
      this.userService
        .getUser()
        .pipe(untilDestroyed(this), take(1))
        .subscribe();
    }

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
