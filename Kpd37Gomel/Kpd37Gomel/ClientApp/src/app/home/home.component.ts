import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthenticationService } from "../service/authentication.service";
import { NavigationMenuService } from "../service/navigation-menu.service";
import { ToastService } from "../service/toast.service";

import { Observable } from 'rxjs';

import { IUserData } from '../user-data';
import { IMenuItem } from "../menu-item";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  public currentUser: IUserData;
  public menuItems: Observable<Array<IMenuItem>>;

  constructor(private authService: AuthenticationService,
    private navMenuService: NavigationMenuService,
    private toastService: ToastService) {
    this.currentUser = this.authService.getCurrentUser();
  }

  ngOnInit(): void {
    this.currentUser = this.authService.getCurrentUser();
    this.menuItems = this.navMenuService.getMenuItems();
  }

  public logout() {
    this.authService.logout();
  }

  //private loadMenuItems(): void {
  //  this.navMenuService.getMenuItems().subscribe(
  //    data => {
  //      this.menuItems = data;
  //    },
  //    error => { this.handleHttpErrorResponse(error); });
  //}

  private handleHttpErrorResponse(error: HttpErrorResponse) {
    if (error.status === 401) {

    } else {
      this.toastService.showErrorToast(error.error.message);
    }
  }
}
