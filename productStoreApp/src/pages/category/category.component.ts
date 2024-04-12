import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { Category } from '../../interfaces/Category';
import { ProductListComponent } from '../../components/product-list/product-list.component';
import { ControlsFilterBarComponent } from '../../components/controls-filter-bar/controls-filter-bar.component';

@Component({
    selector: 'app-category',
    standalone: true,
    imports: [ ProductListComponent,
               ControlsFilterBarComponent ],
    templateUrl: './category.component.html',
    styleUrl: './category.component.scss'
})
export class CategoryComponent {
    categoryId: string = '0';
    categoryObj: Category = {
        id: 1,
        name: 'Category1',
        items: [
            { 
                id: 2, 
                name: 'Soda Coca-Cola Cherry-Vanilia, 0.355', 
                price: 0.7, 
                discount: 0.1, 
                imageUrl: new URL('https://images.silpo.ua/products/1600x1600/webp/20495df9-d0f7-406d-806f-3314f1243e73.png'),
                category: { id: 1, name: 'Category1', items: []},
                description: 'Description2',
                specifications: [
                    { name: 'Name2', value: 'Value2' }
                ] 
            },
        ],
    };

    constructor(private route: ActivatedRoute,
                private router: Router,
                private titleService: Title) {
    }

    ngOnInit(): void {
        // Get categoryId
        this.route.paramMap.subscribe(params => {
            this.categoryId = params.get('categoryId') ?? this.categoryId;
        });

        // Redirect '/home' if No Category
        if (this.categoryId == '0') {
            this.router.navigate(['/home'])
        }

        // Set title
        this.titleService.setTitle("Category - " + this.categoryObj.name);
    }
}
