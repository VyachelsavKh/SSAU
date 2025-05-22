import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const userRoles = this.authService.getRoles();
    const requiredRoles = route.data['roles'] as string[];

    const hasRole = requiredRoles.some(role => userRoles.includes(role));

    if (!hasRole) {
      this.router.navigate(['/forbidden']);
      return false;
    }

    return true;
  }
}
