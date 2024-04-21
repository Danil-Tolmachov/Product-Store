import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Observable, take } from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { AsyncPipe } from '@angular/common';
import ProductListComponent from '../../components/product-list/product-list.component';
import { type IProduct } from '../../interfaces/IProduct';
import ControlsFilterBarComponent from '../../components/controls-filter-bar/controls-filter-bar.component';
import ProductService from '../../services/product.service';
import { type ICategory } from '../../interfaces/ICategory';
import CategoryService from '../../services/category.service';

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
    private readonly categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    // Set Title
    this.titleService.setTitle(this.title);

    // Get products
    this.productsList$ = this.productService
      .getProducts()
      .pipe(untilDestroyed(this), take(1));

    // Get categories
    this.categoriesList$ = this.categoryService
      .getCategories()
      .pipe(untilDestroyed(this), take(1));
  }
}
