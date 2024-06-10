import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { BehaviorSubject, Observable, map, switchMap, take, tap } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { toSignal } from '@angular/core/rxjs-interop';
import { ICategory, IProductPage } from '../../core/interfaces/IProduct';
import CategoryService from '../../core/services/category.service';
import ProductService from '../../core/services/product.service';
import { PageBarSkeletonComponent } from '../../shared/components/page-bar/page-bar-skeleton/page-bar-skeleton.component';
import { PageBarComponent } from '../../shared/components/page-bar/page-bar.component';
import ProductListSkeletonComponent from '../../shared/components/product-list/product-list-skeleton/product-list-skeleton.component';
import ProductListComponent from '../../shared/components/product-list/product-list.component';
import ControlsFilterBarComponent from '../home/controls-filter-bar/controls-filter-bar.component';

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
  pageSubject$ = new BehaviorSubject<number>(DEFAULT_PAGE);
  countSubject$ = new BehaviorSubject<number>(PRODUCTS_PER_PAGE);
  pagesCountSubject$ = new BehaviorSubject<number>(1);

  categoryId$: Observable<number> = this.route.paramMap.pipe(
    untilDestroyed(this),
    take(1),
    map((params) => {
      return parseInt(params.get('categoryId') ?? '0', 10);
    })
  );

  category$: Observable<ICategory> = this.categoryId$.pipe(
    untilDestroyed(this),
    switchMap((id) => this.categoryService.getCategory(id))
  );

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
        switchMap((count) =>
          this.categoryId$.pipe(
            switchMap((categoryId) =>
              this.productService.getProductsPageByCategory(
                categoryId,
                page,
                count
              )
            ),
            tap((page) => {
              this.pagesCountSubject$.next(+(page.pagesCount ?? 1));
            })
          )
        )
      )
    )
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
        take(1),
        tap((id) => {
          if (id === 0) {
            this.router.navigate(['/home']);
          }
        })
      )
      .subscribe();

    this.category$
      .pipe(
        tap((category) => {
          // Set title
          this.titleService.setTitle(`Category - ${category.name}`);
        })
      )
      .subscribe();
  }
}
