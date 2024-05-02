import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { type Observable, map, BehaviorSubject, tap, take } from 'rxjs';
import environment from '../environments/environment.development';
import { ICartItem, ICartItemResponse } from '../interfaces/ICartItem';
import ProductService from './product.service';
import type IAddCartItemModel from '../interfaces/models/IAddCartItemModel';
import UserService from './user.service';
import { IImageResponse } from '../interfaces/IImage';
import { ICart, ICartResponse } from '../interfaces/ICart';

const url = environment.apiUrl;
const urlImg = `${url}/image/product`;

@UntilDestroy()
@Injectable({
  providedIn: 'root',
})
export default class CartService {
  private cartSubject = new BehaviorSubject<ICart | null>(null);

  public cart: Observable<ICart | null> = this.cartSubject.asObservable();

  constructor(
    private readonly http: HttpClient,
    private readonly userService: UserService
  ) {}

  /**
   * Retrieves user's cart from the server.
   * @returns An observable emitting an cart object.
   */
  getCart(): Observable<ICart> {
    const link = `${url}/cart`;

    return this.http.get<ICartResponse>(link).pipe(
      map((cart) => CartService.adaptCart(cart)),
      tap((cart) => this.cartSubject.next(cart))
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
        this.getCart().pipe(untilDestroyed(this), take(1)).subscribe();
      })
    );
  }

  deleteCartItem(productId: number): Observable<void> {
    const link = `${url}/cart/${productId}`;

    return this.http.delete<void>(link).pipe(
      tap(() => {
        this.getCart().pipe(untilDestroyed(this), take(1)).subscribe();
      })
    );
  }

  clearCart(): void {
    this.cartSubject.next(null);
  }

  static adaptCart(apiCartItem: ICartResponse): ICart {
    return {
      items: (apiCartItem.items ?? []).map((item) => this.adaptCartItem(item)),
    };
  }

  /**
   * Adapts a cart item received from the server to the client-side model.
   * @param apiCartItem The cart item received from the server.
   * @returns The adapted client-side cart item.
   */
  static adaptCartItem(apiCartItem: ICartItemResponse): ICartItem {
    return {
      product: ProductService.adaptProduct(apiCartItem.product),
      quantity: apiCartItem.quantity,
    };
  }

  static adaptImageResponse(apiImage: IImageResponse) {
    const convertedPath = `${urlImg}/${apiImage.path}`;

    return {
      path: apiImage.path,
      alt: convertedPath,
    };
  }
}
