import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';

/**
 * Interface representing a pair of tokens.
 */
export interface ITokens {
  token: string;
  refreshToken: string;
}

@Injectable({
  providedIn: 'root',
})
export default class TokenService {
  constructor(private readonly cookieService: CookieService) {}

  /**
   * Checks if both token and refresh token are present in cookies.
   * @returns {boolean} True if both tokens are present, false otherwise.
   */
  hasTokens(): boolean {
    if (!this.cookieService.get('token')) {
      return false;
    }

    if (!this.cookieService.get('refresh_token')) {
      return false;
    }

    return true;
  }

  /**
   * Retrieves the expiration date of the token from cookies.
   * @returns {Date | null} The expiration date of the token, or null if not available or invalid.
   */
  getExpiration(): Date | null {
    const expiration = this.cookieService.get('expires_at');

    if (expiration == null || Number.isNaN(Date.parse(expiration))) {
      return null;
    }

    const expiresAt = new Date(Date.parse(expiration));
    return expiresAt;
  }

  /**
   * Deletes session-related cookies.
   */
  deleteSessionCookies(): void {
    this.cookieService.delete('token');
    this.cookieService.delete('refresh_token');
    this.cookieService.delete('expires_at');
  }

  /**
   * Sets the token and refresh token in cookies.
   * @param {string} token - The JWT token to set.
   * @param {string} refreshToken - The refresh token to set.
   */
  setTokens(token: string, refreshToken: string): void {
    this.cookieService.set('token', token);
    this.cookieService.set('refresh_token', refreshToken);

    const decoded = jwtDecode(token);
    const expiration: number = decoded.exp!;
    const expirationDate: string = new Date(expiration * 1000).toISOString();

    this.cookieService.set('expires_at', expirationDate);
  }

  /**
   * Retrieves the current tokens from cookies.
   * @returns {ITokens} An object containing the token and refresh token.
   */
  getCurrentTokens(): ITokens {
    return {
      token: this.cookieService.get('token'),
      refreshToken: this.cookieService.get('refresh_token'),
    };
  }
}
