import { Component, Input } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { IProduct } from '../../interfaces/IProduct';
import { ProductService } from '../../services/product.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-product',
    standalone: true,
    imports: [],
    templateUrl: './product.component.html',
    styleUrl: './product.component.scss'
})
export class ProductComponent {
    productId: number = 0;
    product: IProduct = { 
        id: 0, 
        name: '', 
        price: 0, 
        discount: 0,
        unitMeasure: '',
        imagePathes: [],
        category: null,
        description: '',
        specifications: [] 
    };
    
    constructor(private titleService: Title, private productService: ProductService, private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        // Get productId
        this.route.paramMap.subscribe(params => {
            this.productId = parseInt(params.get('productId') ?? "0");
        });

        // Get product
        this.productService.getProduct(this.productId).subscribe(product => {
            this.product = product;

            // Set Title
            this.titleService.setTitle(this.product.name);
        });
    }
}
