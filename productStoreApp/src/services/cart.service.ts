import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import {
  type Observable,
  map,
  BehaviorSubject,
  tap,
  take,
  catchError,
  EMPTY,
} from 'rxjs';
import environment from '../environments/environment.development';
import { ICartItem, ICartItemResponse } from '../interfaces/ICartItem';
import ProductService from './product.service';
import type IAddCartItemModel from '../interfaces/models/IAddCartItemModel';
import { IImage, IImageResponse } from '../interfaces/IImage';
import { ICart, ICartResponse } from '../interfaces/ICart';

const url = environment.apiUrl + 'v1';
const urlImg = url + '/image/product';

@UntilDestroy()
@Injectable({
  providedIn: 'root',
})
export default class CartService {
  private cartSubject = new BehaviorSubject<ICart | null>(null);

  public cart: Observable<ICart | null> = this.cartSubject.asObservable();

  constructor(private readonly http: HttpClient) {}

  /**
   * Retrieves user's cart from the server.
   * @returns An observable emitting an cart object.
   */
  getCart(): Observable<ICart> {
    const link = `${url}/cart`;

    return this.http.get<ICartResponse>(link).pipe(
      map((cart) => CartService.adaptCart(cart)),
      tap((cart) => this.cartSubject.next(cart)),
      catchError((error, caught) => {
        if (error.status === 0) {
          return EMPTY;
        }

        return caught;
      })
    );
  }

  /**
   * Adds an item to the cart.
   * @param {number} productId - The ID of the product to add.
   * @param {number} [quantity=1] - The quantity of the product to add. Defaults to 1.
   * @returns {Observable<void>} An observable that completes when the item is added.
   */
  addCartItem(productId: number, quantity: number = 1): Observable<void> {
    const link = `${url}/cart`;
    const data: IAddCartItemModel = {
      productId,
      quantity,
    };

    return this.http.post<void>(link, data).pipe(
      tap(() => {
        this.getCart().pipe(untilDestroyed(this), take(1)).subscribe();
      })
    );
  }

  /**
   * Deletes an item from the cart.
   * @param {number} productId - The ID of the product to delete.
   * @returns {Observable<void>} An observable that completes when the item is deleted.
   */
  deleteCartItem(productId: number): Observable<void> {
    const link = `${url}/cart/${productId}`;

    return this.http.delete<void>(link).pipe(
      tap(() => {
        this.getCart().pipe(untilDestroyed(this), take(1)).subscribe();
      })
    );
  }

  /**
   * Clears the UI cart.
   */
  clearCart(): void {
    this.cartSubject.next(null);
  }

  /**
   * Adapts a cart response from the server to a client-side cart object.
   * @param {ICartResponse} apiCartItem - The cart response from the server.
   * @returns {ICart} The adapted client-side cart object.
   */
  static adaptCart(apiCartItem: ICartResponse): ICart {
    return {
      items: (apiCartItem.items ?? []).map((item) => this.adaptCartItem(item)),
      total: apiCartItem.total,
    };
  }

  /**
   * Adapts a cart item received from the server to the client-side model.
   * @param {ICartItemResponse} apiCartItem - The cart item received from the server.
   * @returns {ICartItem} The adapted client-side cart item.
   */
  static adaptCartItem(apiCartItem: ICartItemResponse): ICartItem {
    return {
      product: ProductService.adaptProduct(apiCartItem.product),
      quantity: apiCartItem.quantity,
    };
  }

  /**
   * Adapts an image response from the server to the client-side model.
   * @param {IImageResponse} apiImage - The image response from the server.
   * @returns {IImage} The adapted client-side image.
   */
  static adaptImageResponse(apiImage: IImageResponse): IImage {
    const convertedPath = `${urlImg}/${apiImage.path}`;

    return {
      path: apiImage.path,
      alt: convertedPath,
    };
  }
}
