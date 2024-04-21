import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { type Observable, map, tap, BehaviorSubject } from 'rxjs';
import { IUser, IUserResponse } from '../interfaces/IUser';
import { IOrder, IOrderResponse } from '../interfaces/IOrder';
import environment from '../environments/environment';
import IRegistrationModel from '../interfaces/models/IRegistrationModel';
import TokenService from './token.service';

const url = `${environment.apiUrl}/auth`;

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
  ) {
    if (this.checkAuthenticated()) {
      this.getUser().subscribe();
    }
  }

  /**
   * Checks if the user is authenticated based on the presence of tokens and their expiration.
   * @returns True if the user is authenticated, false otherwise.
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
   * @returns An observable emitting the current user.
   */
  getUser(): Observable<IUser> {
    const link = `${url}/user`;
    return this.http.get<IUserResponse>(link).pipe(
      map((userResponse) => UserService.adaptUser(userResponse)),
      tap((user) => this.currentUserSubject.next(user))
    );
  }

  /**
   * Logs in the user session and updates tokens and current user.
   * @param username The username of the user.
   * @param password The password of the user.
   * @returns An observable emitting the token response.
   */
  loginSession(username: string, password: string): Observable<ITokenResponse> {
    return this.getTokens(username, password).pipe(
      tap((response) => {
        this.tokenService.setTokens(response.token, response.refreshToken);

        this.getUser().subscribe((user) => {
          this.currentUserSubject.next(user);
        });
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
   * @returns An observable emitting void.
   */
  refreshSession(): Observable<void> {
    return this.getRefreshedTokens().pipe(
      map((response) => {
        this.tokenService.setTokens(response.token, response.refreshToken);
      }),
      tap(() =>
        this.getUser().subscribe((user) => this.currentUserSubject.next(user))
      )
    );
  }

  /**
   * Registers a new user.
   * @param model The registration model containing user details.
   * @returns An observable emitting null.
   */
  registration(model: IRegistrationModel) {
    const link = `${url}/register`;

    return this.http.post<null>(link, model);
  }

  /**
   * Retrieves tokens from server for a given username and password.
   * @param username The username of the user.
   * @param password The password of the user.
   * @returns An observable emitting the token response.
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
   * @returns An observable emitting the token response.
   */
  private getRefreshedTokens(): Observable<ITokenResponse> {
    const link = `${url}/refresh`;
    const token = this.tokenService.getCurrentTokens().refreshToken;

    return this.http.post<ITokenResponse>(link, { token });
  }

  /**
   * Adapts a user response from the server to the client-side model.
   * @param apiUser The user response received from the server.
   * @returns The adapted client-side user model.
   */
  private static adaptUser(apiUser: IUserResponse): IUser {
    return {
      id: apiUser.id,
      isActive: apiUser.isActive,
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
   * @param apiOrder The order response received from the server.
   * @returns The adapted client-side order model.
   */
  private static adaptOrder(apiOrder: IOrderResponse): IOrder {
    return {
      id: apiOrder.id,
      userComment: apiOrder.userComment,
      status: apiOrder.status,
      userUsername: apiOrder.userUsername,
      employeeId: apiOrder.employeeId,
      details: apiOrder.details,
    };
  }
}

/**
 * Interface representing a token response from the server.
 */
interface ITokenResponse {
  token: string;
  refreshToken: string;
}
