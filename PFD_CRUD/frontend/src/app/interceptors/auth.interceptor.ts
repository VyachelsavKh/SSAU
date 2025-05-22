import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const isAuthRequest = req.url.endsWith('/login') || req.url.endsWith('/register');

    if (isAuthRequest) {
      return next.handle(req);
    }

    const authHeader = this.authService.getAuthorizationHeader();
    if (authHeader) {
      const cloned = req.clone({
        headers: req.headers.set('Authorization', authHeader),
      });
      return next.handle(cloned);
    }

    return next.handle(req);
  }
}
