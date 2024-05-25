import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { BehaviorSubject, Observable, map, switchMap, take, tap } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { toSignal } from '@angular/core/rxjs-interop';
import { type ICategory } from '../../interfaces/ICategory';
import ProductListComponent from '../../components/product-list/product-list.component';
import ControlsFilterBarComponent from '../../components/controls-filter-bar/controls-filter-bar.component';
import CategoryService from '../../services/category.service';
import ProductListSkeletonComponent from '../../components/product-list/product-list-skeleton/product-list-skeleton.component';
import ProductService from '../../services/product.service';
import { IProductPage } from '../../interfaces/IProduct';
import { PageBarComponent } from '../../components/page-bar/page-bar.component';
import { PageBarSkeletonComponent } from '../../components/page-bar/page-bar-skeleton/page-bar-skeleton.component';

const DEFAULT_PAGE = 1;
const PRODUCTS_PER_PAGE = 8;

@UntilDestroy()
@Component({
  selector: 'app-category',
  standalone: true,
  imports: [
    ProductListComponent,
    ProductListSkeletonComponent,
    ControlsFilterBarComponent,
    PageBarComponent,
    PageBarSkeletonComponent,
    AsyncPipe,
  ],
  templateUrl: './category.component.html',
  styleUrl: './category.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class CategoryComponent implements OnInit {
  categoryId$: Observable<number> = this.route.paramMap.pipe(
    untilDestroyed(this),
    take(1),
    map((params) => {
      return parseInt(params.get('categoryId') ?? '0', 10);
    })
  );

  products$: Observable<ICategory> = this.categoryId$.pipe(
    switchMap((id) =>
      this.categoryService.getCategory(id).pipe(
        tap((category) => {
          // Set title
          this.titleService.setTitle(`Category - ${category.name}`);
        })
      )
    )
  );

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
        switchMap((count) =>
          this.categoryId$.pipe(
            switchMap((categoryId) =>
              this.productService.getProductsPageByCategory(
                categoryId,
                page,
                count
              )
            )
          )
        )
      )
    )
  );

  pagesCount$: Observable<number | null> = this.productsPage$.pipe(
    map((page) => {
      return page.pagesCount;
    })
  );

  productsSignal = toSignal(this.productsPage$);

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly titleService: Title,
    private readonly categoryService: CategoryService,
    private readonly productService: ProductService
  ) {}

  ngOnInit(): void {
    // Redirect '/home' if No Category
    this.categoryId$
      .pipe(
        untilDestroyed(this),
        take(1),
        tap((id) => {
          if (id === 0) {
            this.router.navigate(['/home']);
          }
        })
      )
      .subscribe();
  }
}
