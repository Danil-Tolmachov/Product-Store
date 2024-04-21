import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { type IProduct } from '../../interfaces/IProduct';
import ProductService from '../../services/product.service';

@Component({
  selector: 'app-product',
  standalone: true,
  imports: [],
  templateUrl: './product.component.html',
  styleUrl: './product.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ProductComponent implements OnInit {
  productId: number = 0;

  product: IProduct = {
    id: 0,
    name: '',
    price: 0,
    discount: 0,
    unitMeasure: '',
    imagePaths: [],
    category: null,
    description: '',
    specifications: [],
  };

  constructor(
    private readonly titleService: Title,
    private readonly productService: ProductService,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    // Get productId
    this.route.paramMap
      .subscribe((params) => {
        this.productId = parseInt(params.get('productId') ?? '0', 10);
      })
      .unsubscribe();

    // Get product
    this.productService
      .getProduct(this.productId)
      .subscribe((product) => {
        this.product = product;

        // Set Title
        this.titleService.setTitle(this.product.name);
      })
      .unsubscribe();
  }
}
