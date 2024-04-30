import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { type ICategory } from '../../interfaces/ICategory';
import ProductListComponent from '../../components/product-list/product-list.component';
import ControlsFilterBarComponent from '../../components/controls-filter-bar/controls-filter-bar.component';
import CategoryService from '../../services/category.service';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Observable, map, switchMap, take, tap } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@UntilDestroy()
@Component({
  selector: 'app-category',
  standalone: true,
  imports: [ProductListComponent, ControlsFilterBarComponent, AsyncPipe],
  templateUrl: './category.component.html',
  styleUrl: './category.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class CategoryComponent implements OnInit {
  categoryId: Observable<number>;

  category: Observable<ICategory>;

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly titleService: Title,
    private readonly categoryService: CategoryService
  ) {
    // Get categoryId
    this.categoryId = this.route.paramMap.pipe(
      untilDestroyed(this),
      take(1),
      map((params) => {
        return parseInt(params.get('categoryId') ?? '0', 10);
      })
    );

    // Get category observable
    this.category = this.categoryId.pipe(
      switchMap((id) =>
        this.categoryService.getCategory(id).pipe(
          tap((category) => {
            // Set title
            this.titleService.setTitle(`Category - ${category.name}`);
          })
        )
      )
    );
  }

  ngOnInit(): void {
    // Redirect '/home' if No Category
    this.categoryId
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
