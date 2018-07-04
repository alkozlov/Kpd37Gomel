import { Component, OnInit } from '@angular/core';
import {AuthenticationService} from "../service/authentication.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  public tenant: string;
  public apartment: string;

  constructor(private authService: AuthenticationService) {
    this.tenant = '';
    this.apartment = '';
  }

  ngOnInit(): void {
    this.tenant = this.authService.getCurrentTenant();
    this.apartment = this.authService.getCurrentTenantApartment();
  }

  public logout() {
    this.authService.logout();
  }
}
