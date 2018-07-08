import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ToastService } from "./toast.service";

import { ILoginModel } from '../login-model';
import { IUserData, UserData } from '../user-data';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class AuthenticationService {

  constructor(private http: HttpClient,
    private router: Router,
    private toastService: ToastService) { }

  public login(loginModel: ILoginModel): Observable<Object> {
    return this.http.post('api/v1/authentication/login', loginModel, httpOptions);
  }

  public logout() {
    if (localStorage['auth_token']) {
      localStorage.removeItem('auth_token');
      localStorage.removeItem('tenant');
      this.router.navigate(['/login']);
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
