import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { ToastService } from "./toast.service";

import { ILoginModel } from '../login-model';
import { IUserData, UserData } from '../user-data';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class AuthenticationService {

  constructor(private http: HttpClient,
    private router: Router,
    private toastService: ToastService) { }

  public login(loginModel: ILoginModel) {
    return this.http.post('api/v1/authentication/login', loginModel, httpOptions).subscribe(
      data => {
        localStorage['auth_token'] = (data as any).token;
        localStorage['tenant'] = JSON.stringify((data as any).tenantTiny);
        this.router.navigate(['/']);
      },
      error => {
        this.handleHttpErrorResponse(error);
      });
  }

  public logout() {
    if (localStorage['auth_token']) {
      localStorage.removeItem('auth_token');
      localStorage.removeItem('tenant');
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

  public getCurrentUser(): IUserData {
    if (localStorage['tenant']) {
      var parsedUserData = JSON.parse(localStorage['tenant']);
      return new UserData(parsedUserData.fullName, parsedUserData.apartmentNumber, parsedUserData.isAdmin);
    }

    return UserData.getDefaultUser();
  }
}
