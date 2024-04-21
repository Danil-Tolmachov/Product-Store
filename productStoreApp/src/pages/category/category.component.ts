import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { type ICategory } from '../../interfaces/ICategory';
import ProductListComponent from '../../components/product-list/product-list.component';
import ControlsFilterBarComponent from '../../components/controls-filter-bar/controls-filter-bar.component';
import CategoryService from '../../services/category.service';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [ProductListComponent, ControlsFilterBarComponent],
  templateUrl: './category.component.html',
  styleUrl: './category.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class CategoryComponent implements OnInit {
  categoryId: number = 0;

  category: ICategory = {
    id: 0,
    name: '',
    items: [],
  };

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly titleService: Title,
    private readonly categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    // Get categoryId
    this.route.paramMap
      .subscribe((params) => {
        this.categoryId = parseInt(params.get('categoryId') ?? '0', 10);
      })
      .unsubscribe();

    // Redirect '/home' if No Category
    if (this.categoryId === 0) {
      this.router.navigate(['/home']);
    }

    // Get category
    this.categoryService
      .getCategory(this.categoryId)
      .subscribe((category) => {
        this.category = category;
      })
      .unsubscribe();

    // Set title
    this.titleService.setTitle(`Category - ${this.category.name}`);
  }
}
