import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { ICategory } from '../../interfaces/ICategory';
import { ProductListComponent } from '../../components/product-list/product-list.component';
import { ControlsFilterBarComponent } from '../../components/controls-filter-bar/controls-filter-bar.component';
import { CategoryService } from '../../services/category.service';

@Component({
    selector: 'app-category',
    standalone: true,
    imports: [ ProductListComponent,
               ControlsFilterBarComponent ],
    templateUrl: './category.component.html',
    styleUrl: './category.component.scss'
})
export class CategoryComponent {
    categoryId: number = 0;
    category: ICategory = {
        id: 0,
        name: '',
        items: [],
    };

    constructor(private route: ActivatedRoute,
                private router: Router,
                private titleService: Title,
                private categoryService: CategoryService) {
    }

    ngOnInit(): void {
        // Get categoryId
        this.route.paramMap.subscribe(params => {
            this.categoryId = parseInt(params.get('categoryId') ?? '0');
        });

        // Redirect '/home' if No Category
        if (this.categoryId == 0) {
            this.router.navigate(['/home']);
        }

        // Get category
        this.categoryService.getCategory(this.categoryId).subscribe(category => {
            this.category = category;
            console.log(category);
        });

        // Set title
        this.titleService.setTitle("Category - " + this.category.name);
    }
}
