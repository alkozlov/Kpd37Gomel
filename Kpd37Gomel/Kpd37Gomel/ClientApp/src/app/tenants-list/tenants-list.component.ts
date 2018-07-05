import { Component, OnInit } from '@angular/core';

import * as AspNetData from "devextreme-aspnet-data";

import { IApartment } from "../apartment";
import { ITenant } from "../tenant";

@Component({
  selector: 'app-tenants-list',
  templateUrl: './tenants-list.component.html',
  styleUrls: ['./tenants-list.component.css']
})
export class TenantsListComponent implements OnInit {
  public tenants: Array<ITenant>;
  public dataSource: any;
  public apartmentsDataSource: any;

  private url: string;

  constructor() {
    this.tenants = new Array<ITenant>();
    this.url = "api/v1/tenants";

    var dataSourceOptions = new Object({
      key: ['tenantId', 'apartmentId'],
      loadUrl: this.url,
      insertUrl: this.url,
      updateUrl: this.url,
      deleteUrl: this.url,
      onBeforeSend(operation, jQueryAjaxSettings) {
        jQueryAjaxSettings.headers = { "Authorization": 'Bearer ' + localStorage['auth_token'] };
      }
    });
    this.dataSource = AspNetData.createStore(dataSourceOptions);

    var apartmentsDataSourceOptions = new Object({
      key: "id",
      loadUrl: 'api/v1/apartments',
      onBeforeSend(operation, jQueryAjaxSettings) {
        jQueryAjaxSettings.headers = { "Authorization": 'Bearer ' + localStorage['auth_token'] };
      }
    });
    this.apartmentsDataSource = AspNetData.createStore(apartmentsDataSourceOptions);
  }

  ngOnInit() {
  }

}
