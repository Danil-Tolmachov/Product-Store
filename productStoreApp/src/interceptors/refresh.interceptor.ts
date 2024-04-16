import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HTTP_INTERCEPTORS,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, Subject, catchError, switchMap, throwError } from 'rxjs';
import UserService from '../services/user.service';

@Injectable()
export class RefreshInterceptor implements HttpInterceptor {
  private refreshSubject: Subject<any> = new Subject<any>();

  constructor(private readonly userService: UserService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (req.url.endsWith('/logout') || req.url.endsWith('/refresh')) {
      return next.handle(req);
    }

    if (!this.userService.hasTokens()) {
      return next.handle(req);
    }

    return next.handle(req).pipe(
      catchError((error, caught) => {
        if (error instanceof HttpErrorResponse) {
          if (RefreshInterceptor.checkTokenExpiryErr(error)) {
            return this.ifTokenExpired().pipe(
              switchMap(() => {
                return next.handle(req);
              })
            );
          }
          return throwError(() => error);
        }

        return caught;
      })
    );
  }

  private ifTokenExpired() {
    this.refreshSubject.subscribe({
      complete: () => {
        this.refreshSubject = new Subject<any>();
      },
    });

    if (this.refreshSubject.observed) {
      // to get new access token and refresh token pair
      this.userService
        .refreshSession()
        .pipe(
          catchError((error, caught) => {
            if (RefreshInterceptor.checkRefreshTokenExpiryErr(error)) {
              this.ifRefreshTokenExpired();
            }

            return caught;
          })
        )
        .subscribe(this.refreshSubject);
    }
    return this.refreshSubject;
  }

  private ifRefreshTokenExpired() {
    this.userService.logoutSession();
  }

  private static checkTokenExpiryErr(error: HttpErrorResponse): boolean {
    return error.status !== null && error.status === 401;
  }

  private static checkRefreshTokenExpiryErr(error: HttpErrorResponse): boolean {
    return error.status !== null && error.status === 400;
  }
}

export const refreshInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: RefreshInterceptor,
  multi: true,
};
