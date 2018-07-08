import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthenticationService } from "../service/authentication.service";
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private router: Router,
    private authService: AuthenticationService) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

    if (this.authService.isAuthorized()) {
      return true;
    } else {
      if (state.url.indexOf('/login') > -1) {
        return true;
      } else {
        this.router.navigate(['/login'], { queryParams: { redirectUrl: state.url } });
        return false;
      }
    }
  }
}
