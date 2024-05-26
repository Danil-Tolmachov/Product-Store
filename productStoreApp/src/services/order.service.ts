import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import {
  type Observable,
  map,
  BehaviorSubject,
  tap,
  switchMap,
  EMPTY,
  catchError,
} from 'rxjs';
import environment from '../environments/environment.development';
import UserService from './user.service';
import { IOrder, IOrderResponse } from '../interfaces/IOrder';
import { IOrderDetail, IOrderDetailResponse } from '../interfaces/IOrderItem';
import { ICategory } from '../interfaces/ICategory';
import { IProduct, IProductResponse } from '../interfaces/IProduct';
import { IImage, IImageResponse } from '../interfaces/IImage';

const url = environment.apiUrl + 'v1';
const urlImg = url + '/image/product';

interface ICancelOrderRequest {
  orderId: number;
}

interface ISubmitCartModel {
  comment: string | null;
}

@UntilDestroy()
@Injectable({
  providedIn: 'root',
})
export default class OrderService {
  private ordersSubject = new BehaviorSubject<IOrder[] | null>(null);

  public orders: Observable<IOrder[] | null> =
    this.ordersSubject.asObservable();

  constructor(
    private readonly http: HttpClient,
    private readonly userService: UserService
  ) {
    this.userService.currentUser
      .pipe(
        switchMap(() => {
          return this.getOrders();
        })
      )
      .subscribe();
  }

  /**
   * Retrieves an order by its ID from the server.
   * @param {number} orderId - The ID of the order to retrieve.
   * @returns {Observable<IOrder>} An observable emitting the order with the specified ID.
   */
  getOrder(orderId: number): Observable<IOrder> {
    const link = `${url}/order/${orderId}`;

    return this.http.get<IOrderResponse>(link).pipe(
      untilDestroyed(this),
      map((response) => OrderService.adaptOrder(response)),
      catchError((error, caught) => {
        if (error.status === 0) {
          return EMPTY;
        }

        return caught;
      })
    );
  }

  /**
   * Retrieves all orders from the server.
   * @returns {Observable<IOrder[]>} An observable emitting an array of orders.
   */
  getOrders(): Observable<IOrder[]> {
    const link = `${url}/order`;

    return this.http.get<IOrderResponse[]>(link).pipe(
      untilDestroyed(this),
      map((response) => {
        return (response ?? []).map((order) => OrderService.adaptOrder(order));
      }),
      tap((orders) => {
        this.ordersSubject.next(orders);
      }),
      catchError((error, caught) => {
        if (error.status === 0) {
          return EMPTY;
        }

        return caught;
      })
    );
  }

  /**
   * Submits the current cart as an order.
   * @param {ISubmitCartModel} data - The data for submitting the cart.
   * @returns {Observable<void>} An observable indicating the completion of the operation.
   */
  submitCart(data: ISubmitCartModel): Observable<void> {
    const link = `${url}/order`;

    return this.http.post<void>(link, data).pipe(
      untilDestroyed(this),
      switchMap(() => {
        return this.getOrders();
      }),
      map(() => {})
    );
  }

  /**
   * Cancels an order by its ID.
   * @param {number} orderId - The ID of the order to cancel.
   * @returns {Observable<void>} An observable indicating the completion of the operation.
   */
  cancelOrder(orderId: number): Observable<void> {
    const link = `${url}/order/cancel`;
    const data: ICancelOrderRequest = {
      orderId,
    };

    return this.http.post<void>(link, data).pipe(
      untilDestroyed(this),
      switchMap(() => {
        return this.getOrders();
      }),
      map(() => {})
    );
  }

  /**
   * Clears the current orders.
   */
  clearOrders(): void {
    this.ordersSubject.next(null);
  }

  /**
   * Adapts an order response from the server to the client-side model.
   * @param {IOrderResponse} apiOrder - The order response received from the server.
   * @returns {IOrder} The adapted client-side order model.
   */
  static adaptOrder(apiOrder: IOrderResponse): IOrder {
    return {
      id: apiOrder.id,
      userComment: apiOrder.userComment,
      status: apiOrder.status,
      total: apiOrder.total,
      employeeId: apiOrder.employeeId,
      isCompleted: apiOrder.isCompleted,
      isCanceled: apiOrder.isCanceled,
      details: (apiOrder.details || []).map((detail) => {
        return this.adaptDetail(detail);
      }),
    };
  }

  /**
   * Adapts an order detail response from the server to the client-side model.
   * @param {IOrderDetailResponse} apiDetail - The order detail response received from the server.
   * @returns {IOrderDetail} The adapted client-side order detail model.
   */
  static adaptDetail(apiDetail: IOrderDetailResponse): IOrderDetail {
    return {
      unitPrice: apiDetail.unitPrice,
      quantity: apiDetail.quantity,
      product: this.adaptProduct(apiDetail.product),
    };
  }

  /**
   * Adapts a product received from the server to the client-side model.
   * @param {IProductResponse} apiProduct - The product received from the server.
   * @returns {IProduct} The adapted client-side product.
   */
  static adaptProduct(apiProduct: IProductResponse): IProduct {
    const category: ICategory = {
      id: apiProduct.category.id,
      name: apiProduct.category.name,
      items: [],
    };

    return {
      id: apiProduct.id,
      name: apiProduct.name,
      price: apiProduct.price,
      discount: apiProduct.discount,
      unitMeasure: apiProduct.unitMeasure,
      category,
      description: apiProduct.description,
      specifications: apiProduct.specifications,
      imagePaths: (apiProduct.images ?? []).map((image) =>
        this.adaptImageResponse(image)
      ),
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
      path: convertedPath,
      alt: apiImage.alt,
    };
  }
}
