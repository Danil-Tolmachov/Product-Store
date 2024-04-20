import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export default class TokenService {
  constructor(private readonly cookieService: CookieService) {}

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

    if (expiration == null || Number.isNaN(Date.parse(expiration))) {
      return null;
    }

    const expiresAt = new Date(Date.parse(expiration));
    return expiresAt;
  }

  deleteSessionCookies(): void {
    this.cookieService.delete('token');
    this.cookieService.delete('refresh_token');
    this.cookieService.delete('expires_at');
  }

  setTokens(token: string, refreshToken: string): void {
    this.cookieService.set('token', token);
    this.cookieService.set('refresh_token', refreshToken);

    const decoded = jwtDecode(token);
    const expiration: number = decoded.exp!;
    const expirationDate: string = new Date(expiration * 1000).toISOString();

    this.cookieService.set('expires_at', expirationDate);
  }

  getCurrentTokens(): ITokens {
    return {
      token: this.cookieService.get('token'),
      refreshToken: this.cookieService.get('refresh_token'),
    };
  }
}

interface ITokens {
  token: string;
  refreshToken: string;
}
