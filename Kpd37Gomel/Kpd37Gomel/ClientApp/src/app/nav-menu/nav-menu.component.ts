import { Component, OnInit } from '@angular/core';
import { NavigationMenuService } from "../service/navigation-menu.service";
import {ToastService} from "../service/toast.service";
import {IMenuItem} from "../menu-item";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  public isExpanded = false;
  public menuItems: Array<IMenuItem>;

  constructor(private navMenuService: NavigationMenuService, private toastService: ToastService) {
    this.menuItems = new Array<IMenuItem>();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  ngOnInit() {
    this.loadMenuItems();
  }

  private loadMenuItems(): void {
    this.navMenuService.getMenuItems().subscribe(
      data => {
        this.menuItems = data;
      },
      error => { this.toastService.showErrorToast(error.message); });
  }
}
