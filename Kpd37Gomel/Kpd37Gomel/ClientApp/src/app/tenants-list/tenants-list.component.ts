import { Component, OnInit } from '@angular/core';

import * as AspNetData from "devextreme-aspnet-data";

import { IApartment } from "../apartment";
import { ITenant } from "../tenant";

import ODataStore from "devextreme/data/odata/store";
import DataSource from "devextreme/data/data_source";
import CustomStore from 'devextreme/data/custom_store';
import { HttpParams, HttpClient, HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/toPromise';

@Component({
  selector: 'app-tenants-list',
  templateUrl: './tenants-list.component.html',
  styleUrls: ['./tenants-list.component.css']
})
export class TenantsListComponent implements OnInit {
  public tenants: Array<ITenant>;
  public dataSource: any;
  public apartmentsDataSource: any;

  private tenantApiUrl: string;
  private apartmentApiUrl: string;

  //public apartmentTenantsDataStore: any;
  //public apartmentTenantsDataSource: any;

  //public apartmentsDataStore: any;
  //public apartmentsDataSource: any;

  // https://js.devexpress.com/Documentation/ApiReference/UI_Widgets/dxDataGrid/Configuration/columns/lookup/

  constructor(private httpClient: HttpClient) {
    this.tenants = new Array<ITenant>();
    this.tenantApiUrl = "api/v1/tenants";

    var dataSourceOptions = new Object({
      key: ['tenantId', 'apartmentId'],
      loadUrl: this.tenantApiUrl,
      insertUrl: this.tenantApiUrl,
      updateUrl: this.tenantApiUrl,
      deleteUrl: this.tenantApiUrl,
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

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage['auth_token']
      })
    };

    //this.apartmentsDataSource = {
    //  store: new CustomStore({
    //    key: "id",
    //    loadMode: "raw",
    //    load: function () {
    //      // Returns an array of objects that have the following structure:
    //      // { id: 1, name: "John Doe" }
    //      return httpClient.get("api/v1/apartments", httpOptions)
    //        .toPromise();
    //    }
    //  }),
    //  sort: "apartmentNumber"
    //};




    //this.tenantApiUrl = "odata/ApartmentTenant";

    //this.apartmentTenantsDataStore = new ODataStore({
    //  url: this.tenantApiUrl,
    //  key: ['ApartmentId', 'TenantId'],
    //  keyType: { ApartmentId: "Guid", TenantId: "Guid" },
    //  version: 4,
    //  beforeSend: (e) => {
    //    e.headers = {
    //      "Authorization": 'Bearer ' + localStorage['auth_token'],
    //    };
    //  }
    //});

    //this.apartmentTenantsDataSource = new DataSource(new Object({
    //  store: this.apartmentTenantsDataStore,
    //  expand: ["Tenant", "Apartment"],
    //  sort: [{ selector: 'Apartment.ApartmentNumber', desc: false }]
    //  })
    //);


    //this.apartmentApiUrl = "odata/Apartment";
    //this.apartmentsDataStore = new ODataStore({
    //  url: this.apartmentApiUrl,
    //  key: "Id",
    //  keyType: "Guid",
    //  version: 4,
    //  beforeSend: (e) => {
    //    e.headers = {
    //      "Authorization": 'Bearer ' + localStorage['auth_token'],
    //    };
    //  }
    //});
  }

  ngOnInit() {
  }

}
