import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
  HttpInterceptor,
} from '@angular/common/http';
import {
  Observable,
  Subject,
  catchError,
  switchMap,
  take,
  throwError,
} from 'rxjs';
import UserService from '../services/user.service';
import TokenService from '../services/token.service';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Injectable()
export default class RefreshInterceptor implements HttpInterceptor {
  private refreshSubject: Subject<any> = new Subject<any>();

  constructor(
    private readonly userService: UserService,
    private readonly tokenService: TokenService
  ) {}

  /**
   * Intercepts outgoing HTTP requests and handles token expiration errors.
   * If a token expiration error occurs, it attempts to refresh the tokens and retries the original request.
   * @param req The HTTP request to be intercepted.
   * @param next The HTTP handler for the intercepted request.
   * @returns An observable of the HTTP event stream.
   */
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // Skip interception for logout and refresh requests
    if (req.url.endsWith('/logout') || req.url.endsWith('/refresh')) {
      return next.handle(req);
    }

    // Skip interception if no tokens are available
    if (!this.tokenService.hasTokens()) {
      return next.handle(req);
    }

    // Refresh if token is expired
    if (this.tokenService.hasTokens()) {
      const expiration = this.tokenService.getExpiration();

      if (expiration === null || expiration <= new Date()) {
        return this.ifTokenExpired().pipe(
          untilDestroyed(this),
          switchMap(() => {
            return next.handle(req);
          })
        );
      }
    }

    // Intercept the request and handle errors
    return next.handle(req).pipe(
      catchError((error, caught) => {
        if (error instanceof HttpErrorResponse) {
          // Check if the error indicates a token expiration
          if (RefreshInterceptor.checkTokenExpiryErr(error)) {
            // If token expired, attempt to refresh the tokens and retry the original request
            return this.ifTokenExpired().pipe(
              untilDestroyed(this),
              switchMap(() => {
                return next.handle(req);
              })
            );
          }
          // If error does not indicate token expiration, re-throw the error
          return throwError(() => error);
        }

        // If error is not an instance of HttpErrorResponse, return the caught observable
        return caught;
      })
    );
  }

  /**
   * Handles token expiration by attempting to refresh the tokens.
   * If the refresh token is also expired, it logs out the user.
   * @returns An observable that emits the refreshed tokens.
   */
  private ifTokenExpired(): Observable<void> {
    return this.userService.refreshSession().pipe(
      untilDestroyed(this),
      catchError((error, caught) => {
        // If the refresh token is also expired, log out the user
        this.ifRefreshTokenExpired();

        // Return the caught observable
        return caught;
      })
    );
  }

  /**
   * Logs out the user if the refresh token is expired.
   */
  private ifRefreshTokenExpired() {
    this.userService.logoutSession();
  }

  /**
   * Checks if the error indicates a token expiration.
   * @param error The HTTP error response.
   * @returns True if the error indicates a token expiration, false otherwise.
   */
  private static checkTokenExpiryErr(error: HttpErrorResponse): boolean {
    return error.status !== null && error.status === 401;
  }

  /**
   * Checks if the error indicates a refresh token expiration.
   * @param error The HTTP error response.
   * @returns True if the error indicates a refresh token expiration, false otherwise.
   */
  private static checkRefreshTokenExpiryErr(error: HttpErrorResponse): boolean {
    return error.status !== null && error.status === 400;
  }
}
