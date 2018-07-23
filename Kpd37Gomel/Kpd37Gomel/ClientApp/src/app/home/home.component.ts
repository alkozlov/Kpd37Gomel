import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthenticationService } from "../service/authentication.service";
import { NavigationMenuService } from "../service/navigation-menu.service";
import { ToastService } from "../service/toast.service";

import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';
import { Observable } from 'rxjs';

import { IUserData } from '../user-data';
import { IMenuItem } from "../menu-item";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  public isHandset: Observable<BreakpointState> = this.breakpointObserver.observe(Breakpoints.Handset);
  public currentUser: IUserData;
  public menuItems: Array<IMenuItem>;
  public menuItemsO: Observable<Array<IMenuItem>>;

  fillerNav = Array.from({ length: 10 }, (_, i) => `NNNNNNNNNNNNNNNNNNNNNav Iteeeeeeem ${i + 1}`);

  constructor(private authService: AuthenticationService,
    private navMenuService: NavigationMenuService,
    private toastService: ToastService,
    private breakpointObserver: BreakpointObserver) {
    this.currentUser = this.authService.getCurrentUser();
  }

  ngOnInit(): void {
    this.currentUser = this.authService.getCurrentUser();
    this.loadMenuItems();
    this.menuItemsO = this.navMenuService.getMenuItems();
  }

  public logout() {
    this.authService.logout();
  }

  private loadMenuItems(): void {
    this.navMenuService.getMenuItems().subscribe(
      data => {
        this.menuItems = data;
      },
      error => { this.handleHttpErrorResponse(error); });
  }

  private handleHttpErrorResponse(error: HttpErrorResponse) {
    if (error.status === 401) {

    } else {
      this.toastService.showErrorToast(error.error.message);
    }
  }
}
