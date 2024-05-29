import { Injectable, Injector } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpRequest,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, catchError, switchMap, throwError } from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import UserService from '../services/user.service';
import TokenService from '../services/token.service';
import { IUser } from '../interfaces/IUser';
import MessageService from '../services/message.service';

@UntilDestroy()
@Injectable()
export default class RefreshInterceptor implements HttpInterceptor {
  constructor(private readonly injector: Injector) {}

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
    const userService: UserService = this.injector.get(UserService);
    const tokenService: TokenService = this.injector.get(TokenService);
    const messageService: MessageService = this.injector.get(MessageService);

    // Skip interception if no tokens are available
    if (!tokenService.hasTokens()) {
      return next.handle(req);
    }

    // Skip interception if refreshing
    if (req.url.endsWith('refresh')) {
      return next.handle(req);
    }

    // Refresh if token has expired
    if (tokenService.hasTokens()) {
      const expiration = tokenService.getExpiration();

      if (expiration === null || expiration <= new Date()) {
        return this.handle401Error(userService, messageService).pipe(
          switchMap(() => {
            return next.handle(req);
          })
        );
      }
    }

    // Refresh if suddenly unauthorized
    return next.handle(req).pipe(
      catchError((error) => {
        if (error.status === 401) {
          return this.handle401Error(userService, messageService).pipe(
            switchMap(() => {
              return next.handle(req);
            })
          );
        }

        return throwError(() => error);
      })
    );
  }

  private handle401Error(
    userService: UserService,
    messageService: MessageService
  ): Observable<IUser> {
    return userService.refreshSession().pipe(
      untilDestroyed(this),
      catchError((error) => {
        // If the refresh token is also expired, log out the user
        if (error.status === 401) {
          messageService.showMessage({
            header: 'Expired session',
            message: ['Unauthorized'],
          });
          userService.logoutSession();
        }

        return throwError(() => error);
      })
    );
  }
}
