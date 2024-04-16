import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { type Observable, map } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';
import { IUser, IUserResponse } from '../interfaces/IUser';
import { IOrder, IOrderResponse } from '../interfaces/IOrder';
import environment from '../environments/environment';

const url = `${environment.apiUrl}/auth`;

@Injectable({
  providedIn: 'root',
})
export default class UserService {
  constructor(
    private readonly http: HttpClient,
    private readonly cookieService: CookieService
  ) {}

  /**
   * Checks if the token is alright by making a request to the server.
   */
  isAuthenticated(): boolean {
    const expiration = this.getExpiration();

    if (expiration === null) {
      return false;
    }

    if (expiration <= new Date()) {
      return false;
    }

    if (!this.cookieService.get('token')) {
      return false;
    }

    if (!this.cookieService.get('refresh_token')) {
      return false;
    }

    return true;
  }

  hasTokens(): boolean {
    if (!this.cookieService.get('token')) {
      return false;
    }

    if (!this.cookieService.get('refresh_token')) {
      return false;
    }

    return true;
  }

  getExpiration(): Date | null {
    const expiration = this.cookieService.get('expires_at');

    if (expiration === null) {
      return null;
    }

    const expiresAt = new Date(Date.parse(expiration));
    return expiresAt;
  }

  getUser(): Observable<IUser> {
    const link = `${url}/user`;
    return this.http
      .get<IUserResponse>(link)
      .pipe(map((userResponse) => UserService.adaptUser(userResponse)));
  }

  loginSession(username: string, password: string): void {
    this.getTokens(username, password).subscribe((response) => {
      this.setTokens(response.token, response.refreshToken);
    });
  }

  logoutSession(): void {
    this.cookieService.delete('token');
    this.cookieService.delete('refresh_token');
    this.cookieService.delete('expires_at');
  }

  refreshSession(): Observable<void> {
    return this.getRefreshedTokens().pipe(
      map((response) => {
        this.setTokens(response.token, response.refreshToken);
      })
    );
  }

  setTokens(token: string, refreshToken: string): void {
    this.cookieService.set('token', token);
    this.cookieService.set('refresh_token', refreshToken);

    const decoded = jwtDecode(token);
    const expiration: number = decoded.exp!;
    const expirationDate: string = new Date(expiration * 1000).toISOString();

    this.cookieService.set('expires_at', expirationDate);
  }

  getTokens(username: string, password: string): Observable<ITokenResponse> {
    const link = `${url}/login`;

    return this.http.post<ITokenResponse>(link, { username, password });
  }

  getRefreshedTokens(): Observable<ITokenResponse> {
    const link = `${url}/refresh`;
    const token = this.cookieService.get('refresh_token');

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

interface ITokenResponse {
  token: string;
  refreshToken: string;
}
