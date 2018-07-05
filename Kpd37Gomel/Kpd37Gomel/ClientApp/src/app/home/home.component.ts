import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from "../service/authentication.service";

import { IUserData } from '../user-data';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  public currentUser: IUserData;

  constructor(private authService: AuthenticationService) {
    this.currentUser = this.authService.getCurrentUser();
  }

  ngOnInit(): void {
    this.currentUser = this.authService.getCurrentUser();
  }

  public logout() {
    this.authService.logout();
  }
}
