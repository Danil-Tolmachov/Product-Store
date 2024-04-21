import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { type Observable, map, BehaviorSubject, tap, take } from 'rxjs';
import environment from '../environments/environment.development';
import { ICartItem, ICartItemResponse } from '../interfaces/ICartItem';
import ProductService from './product.service';
import { IProductResponse } from '../interfaces/IProduct';
import type IAddCartItemModel from '../interfaces/models/IAddCartItemModel';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import UserService from './user.service';

const url = environment.apiUrl;

@UntilDestroy()
@Injectable({
  providedIn: 'root',
})
export default class CartService {
  private cartItemsSubject = new BehaviorSubject<ICartItem[]>([]);

  public carItems: Observable<ICartItem[]> =
    this.cartItemsSubject.asObservable();

  constructor(
    private readonly http: HttpClient,
    private readonly userService: UserService
  ) {
    if (this.userService.checkAuthenticated()) {
      this.getCartItems().pipe(untilDestroyed(this), take(1)).subscribe();
    }
  }

  /**
   * Retrieves the cart items from the server.
   * @returns An observable emitting an array of cart items.
   */
  getCartItems(): Observable<ICartItem[]> {
    const link = `${url}/cart`;

    return this.http.get<ICartItemResponse[]>(link).pipe(
      map((response) =>
        response.map((cartItem) => CartService.adaptCartItem(cartItem))
      ),
      tap((cartItems) => this.cartItemsSubject.next(cartItems))
    );
  }

  addCartItem(productId: number, quantity: number = 1): Observable<void> {
    const link = `${url}/cart`;
    const data: IAddCartItemModel = {
      productId,
      quantity,
    };

    return this.http.post<void>(link, data).pipe(
      tap(() => {
        this.getCartItems().pipe(untilDestroyed(this), take(1)).subscribe();
      })
    );
  }

  deleteCartItem(productId: number): Observable<void> {
    const link = `${url}/cart/${productId}`;

    return this.http.delete<void>(link).pipe(
      tap(() => {
        this.getCartItems().pipe(untilDestroyed(this), take(1)).subscribe();
      })
    );
  }

  /**
   * Adapts a cart item received from the server to the client-side model.
   * @param apiCartItem The cart item received from the server.
   * @returns The adapted client-side cart item.
   */
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
      imagePaths: [apiCartItem?.imagePath],
    };

    return {
      product: ProductService.adaptProduct(productResponse),
      quantity: apiCartItem.quantity,
    };
  }
}
