import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { type Observable, map, tap, BehaviorSubject } from 'rxjs';
import { IUser, IUserResponse } from '../interfaces/IUser';
import { IOrder, IOrderResponse } from '../interfaces/IOrder';
import environment from '../environments/environment';
import IRegistrationModel from '../interfaces/models/IRegistrationModel';
import TokenService from './token.service';

const url = `${environment.apiUrl}/auth`;

interface ITokenResponse {
  token: string;
  refreshToken: string;
}

@Injectable({
  providedIn: 'root',
})
export default class UserService {
  private currentUserSubject: BehaviorSubject<IUser | null>;

  public currentUser: Observable<IUser | null>;

  constructor(
    private readonly http: HttpClient,
    private readonly tokenService: TokenService
  ) {
    this.currentUserSubject = new BehaviorSubject<IUser | null>(null);
    this.currentUser = this.currentUserSubject.asObservable();

    if (this.checkAuthenticated()) {
      this.getUser().subscribe();
    }
  }

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

  getUser(): Observable<IUser> {
    const link = `${url}/user`;
    return this.http.get<IUserResponse>(link).pipe(
      map((userResponse) => UserService.adaptUser(userResponse)),
      tap((user) => this.currentUserSubject.next(user))
    );
  }

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

  logoutSession(): void {
    this.tokenService.deleteSessionCookies();
    this.currentUserSubject.next(null);
  }

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

  registration(model: IRegistrationModel) {
    const link = `${url}/register`;

    return this.http.post<null>(link, model);
  }

  private getTokens(
    username: string,
    password: string
  ): Observable<ITokenResponse> {
    const link = `${url}/login`;

    return this.http.post<ITokenResponse>(link, { username, password });
  }

  private getRefreshedTokens(): Observable<ITokenResponse> {
    const link = `${url}/refresh`;
    const token = this.tokenService.getCurrentTokens().refreshToken;

    return this.http.post<ITokenResponse>(link, { token });
  }

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
