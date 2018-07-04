import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { ToastService } from "./toast.service";

import { ILoginModel } from '../login-model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class AuthenticationService {

  private tenant: string;
  private apartment: string;

  constructor(private http: HttpClient,
    private router: Router,
    private toastService: ToastService) { }

  public login(loginModel: ILoginModel) {
    return this.http.post('api/v1/authentication/login', loginModel, httpOptions).subscribe(
      data => {
        localStorage['auth_token'] = (data as any).token;
        this.tenant = (data as any).tenant;
        this.apartment = (data as any).apartment;
        this.router.navigate(['/']);
      },
      error => {
        this.handleHttpErrorResponse(error);
      });
  }

  public logout() {
    if (localStorage['auth_token']) {
      localStorage.removeItem('auth_token');
      this.tenant = '';
      this.apartment = '';
      this.router.navigate(['/login']);
    }
  }

  private handleHttpErrorResponse(error: HttpErrorResponse) {
    if (error.status === 401) {
      this.toastService.showErrorToast('Неверные данные.');
    } else {
      this.toastService.showErrorToast(error.message);
    }
  }

  public isAuthorized(): boolean {
    return localStorage.getItem('auth_token') !== null;
  }

  public getCurrentTenant(): string {
    return this.tenant;
  }

  public getCurrentTenantApartment(): string {
    return this.apartment;
  }
}
