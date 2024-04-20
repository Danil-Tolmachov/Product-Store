import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { type Observable, map, BehaviorSubject, tap } from 'rxjs';
import environment from '../environments/environment.development';
import { ICartItem, ICartItemResponse } from '../interfaces/ICartItem';
import ProductService from './product.service';
import { IProductResponse } from '../interfaces/IProduct';

const url = environment.apiUrl;

@Injectable({
  providedIn: 'root',
})
export default class CartService {
  private cartItemsSubject: BehaviorSubject<ICartItem[]>;

  carItems: Observable<ICartItem[]>;

  constructor(private readonly http: HttpClient) {
    this.cartItemsSubject = new BehaviorSubject<ICartItem[]>([]);
    this.carItems = this.cartItemsSubject.asObservable();
  }

  getCartItems(): Observable<ICartItem[]> {
    const link = `${url}/cart`;

    return this.http.get<ICartItemResponse[]>(link).pipe(
      map((response) =>
        response.map((cartItem) => CartService.adaptCartItem(cartItem))
      ),
      tap((cartItems) => this.cartItemsSubject.next(cartItems))
    );
  }

  static adaptCartItem(apiCartItem: ICartItemResponse): ICartItem {
    const productResponse: IProductResponse = {
      id: apiCartItem.productId,
      name: apiCartItem.productName,
      price: apiCartItem.productPrice,
      discount: apiCartItem.productDiscount,
      unitMeasure: '',

      categoryName: apiCartItem.productCategoryName,
      categoryId: apiCartItem.productCategoryId,
      description: apiCartItem.productDescription,
      specifications: [],
      imagePaths: [],
    };

    return {
      product: ProductService.adaptProduct(productResponse),
      quantity: apiCartItem.quantity,
    };
  }
}
