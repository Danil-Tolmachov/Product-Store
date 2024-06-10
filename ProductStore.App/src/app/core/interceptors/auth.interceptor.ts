import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
} from '@angular/common/http';

import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

/**
 * Interceptor that adds an Authorization header with a JWT token to outgoing HTTP requests.
 */
@Injectable()
export default class AuthInterceptor implements HttpInterceptor {
  constructor(private readonly cookieService: CookieService) {}

  /**
   * Intercepts outgoing HTTP requests and adds an Authorization header with the JWT token.
   * @param req The HTTP request to be intercepted.
   * @param next The HTTP handler for the intercepted request.
   * @returns An observable of the HTTP event stream.
   */
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // Retrieve the JWT token from the cookie service
    const token = this.cookieService.get('token');

    // Check if a token is available
    if (token != null) {
      // Clone the request and add the Authorization header with the JWT token
      const authReq = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      });

      // Forward the modified request to the next handler
      return next.handle(authReq);
    }

    // If no token is available, forward the original request as is
    return next.handle(req);
  }
}
