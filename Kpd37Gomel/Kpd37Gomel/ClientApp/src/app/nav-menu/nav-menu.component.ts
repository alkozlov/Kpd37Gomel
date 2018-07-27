import { Component, Input } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { NavigationMenuService } from "../service/navigation-menu.service";
import { ToastService } from "../service/toast.service";
import { Observable } from 'rxjs';
import { IMenuItem } from "../menu-item";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public isExpanded = false;

  @Input()
  public menuItems: Observable<Array<IMenuItem>>;

  constructor() {
    //this.menuItems = new Array<IMenuItem>();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  //ngOnInit() {
  //  this.loadMenuItems();
  //}

  //private loadMenuItems(): void {
  //  this.navMenuService.getMenuItems().subscribe(
  //    data => {
  //      this.menuItems = data;
  //    },
  //    error => { this.handleHttpErrorResponse(error); });
  //}

  //private handleHttpErrorResponse(error: HttpErrorResponse) {
  //  if (error.status === 401) {

  //  } else {
  //    this.toastService.showErrorToast(error.error.message);
  //  }
  //}
}
