import { Component } from '@angular/core';
import { ProductListComponent } from '../../components/product-list/product-list.component';
import { IProduct } from '../../interfaces/IProduct';
import { Title } from '@angular/platform-browser';
import { ControlsFilterBarComponent } from '../../components/controls-filter-bar/controls-filter-bar.component';
import { ProductService } from '../../services/product.service';
import { ICategory } from '../../interfaces/ICategory';
import { CategoryService } from '../../services/category.service';

@Component({
    selector: 'app-home',
    standalone: true,
    imports: [ ProductListComponent,
               ControlsFilterBarComponent ],
    templateUrl: './home.component.html',
    styleUrl: './home.component.scss'
})
export class HomeComponent {
    title: string = 'Product Store';
    productsList: IProduct[] = [];
    categoriesList: ICategory[] = [];

    constructor(private titleService: Title, private productService: ProductService, private categoryService: CategoryService) { 
    }

    ngOnInit(): void {
        // Set Title
        this.titleService.setTitle(this.title);

        // Get products
        this.productService.getProducts().subscribe(products => {
            this.productsList = products;
        });

        // Get categories
        this.categoryService.getCategories().subscribe(categories => {
            this.categoriesList = categories;
        });
    }
}
