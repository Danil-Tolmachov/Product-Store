import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { type Observable, map, BehaviorSubject, tap, switchMap } from 'rxjs';
import environment from '../environments/environment.development';
import UserService from './user.service';
import { IOrder, IOrderResponse } from '../interfaces/IOrder';
import { IOrderDetail, IOrderDetailResponse } from '../interfaces/IOrderItem';
import { ICategory } from '../interfaces/ICategory';
import { IProduct, IProductResponse } from '../interfaces/IProduct';
import { IImageResponse } from '../interfaces/IImage';

const url = environment.apiUrl;
const urlImg = `${url}/image/product`;

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

  getOrder(orderId: number): Observable<IOrder> {
    const link = `${url}/order/${orderId}`;

    return this.http.get<IOrderResponse>(link).pipe(
      untilDestroyed(this),
      map((response) => OrderService.adaptOrder(response))
    );
  }

  getOrders(): Observable<IOrder[]> {
    const link = `${url}/order`;

    return this.http.get<IOrderResponse[]>(link).pipe(
      untilDestroyed(this),
      map((response) => {
        return (response ?? []).map((order) => OrderService.adaptOrder(order));
      }),
      tap((orders) => {
        this.ordersSubject.next(orders);
      })
    );
  }

  submitCart(data: ISubmitCartModel): Observable<void> {
    const link = `${url}/order`;

    return this.http.post<void>(link, data).pipe(
      untilDestroyed(this),
      switchMap(() => {
        return this.getOrders();
      }),
      map(() => {
        return;
      })
    );
  }

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
      map(() => {
        return;
      })
    );
  }

  clearOrders(): void {
    this.ordersSubject.next(null);
  }

  /**
   * Adapts an order response from the server to the client-side model.
   * @param apiOrder The order response received from the server.
   * @returns The adapted client-side order model.
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

  static adaptDetail(apiDetail: IOrderDetailResponse): IOrderDetail {
    return {
      unitPrice: apiDetail.unitPrice,
      quantity: apiDetail.quantity,
      product: this.adaptProduct(apiDetail.product),
    };
  }

  /**
   * Adapts a product received from the server to the client-side model.
   * @param apiProduct The product received from the server.
   * @returns The adapted client-side product.
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

  static adaptImageResponse(apiImage: IImageResponse) {
    const convertedPath = `${urlImg}/${apiImage.path}`;

    return {
      path: convertedPath,
      alt: apiImage.alt,
    };
  }
}
