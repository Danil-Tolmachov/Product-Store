import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  type Observable,
  map,
  tap,
  BehaviorSubject,
  take,
  switchMap,
  EMPTY,
  catchError,
} from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { IUser, IUserResponse } from '../interfaces/IUser';
import { IOrder, IOrderResponse } from '../interfaces/IOrder';
import { IProduct, IProductResponse } from '../interfaces/IProduct';
import { ICategory } from '../interfaces/ICategory';
import { IOrderDetail, IOrderDetailResponse } from '../interfaces/IOrderItem';
import { IImageResponse } from '../interfaces/IImage';
import environment from '../environments/environment.prod';
import IRegistrationModel from '../interfaces/models/IRegistrationModel';
import TokenService from './token.service';

const urlBase = environment.apiUrl + 'v1';
const url = urlBase + '/auth';
const urlImg = urlBase + '/image/product';

/**
 * Interface representing a token response from the server.
 */
interface ITokenResponse {
  token: string;
  refreshToken: string;
}

interface IUpdateUserModel {
  firstName: string;
  lastName: string;
  address: string | null;
}

@UntilDestroy()
@Injectable({
  providedIn: 'root',
})
export default class UserService {
  private currentUserSubject = new BehaviorSubject<IUser | null>(null);

  public currentUser: Observable<IUser | null> =
    this.currentUserSubject.asObservable();

  constructor(
    private readonly http: HttpClient,
    private readonly tokenService: TokenService
  ) {}

  /**
   * Checks if the user is authenticated based on the presence of tokens and their expiration.
   * @returns {boolean} True if the user is authenticated, false otherwise.
   */
  checkAuthenticated(): boolean {
    const expiration = this.tokenService.getExpiration();

    if (expiration === null) {
      return false;
    }

    if (expiration <= new Date()) {
      return false;
    }

    if (!this.tokenService.hasTokens()) {
      return false;
    }

    return true;
  }

  /**
   * Retrieves the current user from the server.
   * @returns {Observable<IUser>} An observable emitting the current user.
   */
  getUser(): Observable<IUser> {
    const link = `${url}/user`;
    return this.http.get<IUserResponse>(link).pipe(
      map((userResponse) => UserService.adaptUser(userResponse)),
      tap((user) => this.currentUserSubject.next(user)),
      catchError((error, caught) => {
        if (error.status === 0) {
          return EMPTY;
        }

        this.currentUserSubject.next(null);
        return caught;
      })
    );
  }

  /**
   * Updates the user's information on the server.
   * @param {IUpdateUserModel} data - The user's updated information.
   * @returns {Observable<void>} An observable emitting void when the update is complete.
   */
  updateUser(data: IUpdateUserModel): Observable<void> {
    const link = `${url}/update`;

    return this.http.put<void>(link, data);
  }

  /**
   * Logs in the user session and updates tokens and current user.
   * @param {string} username - The username of the user.
   * @param {string} password - The password of the user.
   * @returns {Observable<ITokenResponse>} An observable emitting the token response.
   */
  loginSession(username: string, password: string): Observable<ITokenResponse> {
    return this.getTokens(username, password).pipe(
      tap((response) => {
        this.tokenService.setTokens(response.token, response.refreshToken);
      }),
      tap(() => {
        this.getUser().pipe(untilDestroyed(this), take(1)).subscribe();
      })
    );
  }

  /**
   * Logs out the user session by deleting tokens and current user.
   */
  logoutSession(): void {
    this.tokenService.deleteSessionCookies();
    this.currentUserSubject.next(null);
  }

  /**
   * Refreshes the user session tokens and updates the current user from the server.
   * @returns {Observable<IUser>} An observable emitting the updated user.
   */
  refreshSession(): Observable<IUser> {
    return this.getRefreshedTokens().pipe(
      untilDestroyed(this),
      map((response) => {
        this.tokenService.setTokens(response.token, response.refreshToken);
      }),
      switchMap(() => this.getUser())
    );
  }

  /**
   * Registers a new user.
   * @param {IRegistrationModel} model - The registration model containing user details.
   * @returns {Observable<null>} An observable emitting null when the registration is complete.
   */
  registration(model: IRegistrationModel) {
    const link = `${url}/register`;

    return this.http.post<null>(link, model);
  }

  /**
   * Retrieves tokens from server for a given username and password.
   * @param {string} username - The username of the user.
   * @param {string} password - The password of the user.
   * @returns {Observable<ITokenResponse>} An observable emitting the token response.
   */
  private getTokens(
    username: string,
    password: string
  ): Observable<ITokenResponse> {
    const link = `${url}/login`;

    return this.http.post<ITokenResponse>(link, { username, password });
  }

  /**
   * Retrieves refreshed tokens from server using the current refresh token.
   * @returns {Observable<ITokenResponse>} An observable emitting the token response.
   */
  private getRefreshedTokens(): Observable<ITokenResponse> {
    const link = `${url}/refresh`;
    const token = this.tokenService.getCurrentTokens().refreshToken;

    return this.http.post<ITokenResponse>(link, { token });
  }

  /**
   * Adapts a user response from the server to the client-side model.
   * @param {IUserResponse} apiUser - The user response received from the server.
   * @returns {IUser} The adapted client-side user model.
   */
  private static adaptUser(apiUser: IUserResponse): IUser {
    return {
      id: apiUser.id,
      username: apiUser.username,
      firstName: apiUser.firstName,
      lastName: apiUser.lastName,
      discount: apiUser.discount,
      address: apiUser.address,
      cartItems: apiUser.cartItems,
      contacts: apiUser.contacts,
      orders: (apiUser.orders || []).map((order) =>
        UserService.adaptOrder(order)
      ),
    };
  }

  /**
   * Adapts an order response from the server to the client-side model.
   * @param {IOrderResponse} apiOrder - The order response received from the server.
   * @returns {IOrder} The adapted client-side order model.
   */
  private static adaptOrder(apiOrder: IOrderResponse): IOrder {
    return {
      id: apiOrder.id,
      userComment: apiOrder.userComment,
      status: apiOrder.status,
      total: apiOrder.total,
      employeeId: apiOrder.employeeId,
      isCanceled: apiOrder.isCanceled,
      isCompleted: apiOrder.isCompleted,
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
      originalPrice: apiProduct.originalPrice,
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
   * @returns {{ path: string, alt: string }} The adapted client-side image.
   */
  static adaptImageResponse(apiImage: IImageResponse) {
    const convertedPath = `${urlImg}/${apiImage.path}`;

    return {
      path: convertedPath,
      alt: apiImage.alt,
    };
  }
}
