import { Component, OnInit } from '@angular/core';

import * as AspNetData from "devextreme-aspnet-data";
import ODataStore from "devextreme/data/odata/store";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: 'app-tenants-list',
  templateUrl: './tenants-list.component.html',
  styleUrls: ['./tenants-list.component.css']
})
export class TenantsListComponent implements OnInit {
  private apartmentTenantsApiUrl: string;
  private apartmentsApiUrl: string;

  public apartmentsDataSource: any;
  private apartmentTenantsDataStore: any;
  public apartmentTenantsDataSource: any;

  constructor() {
    this.apartmentTenantsApiUrl = "odata/ApartmentTenant";
    this.apartmentsApiUrl = "api/v1/apartments";

    var apartmentsDataSourceOptions = new Object({
      key: "id",
      loadUrl: 'api/v1/apartments',
      onBeforeSend(operation, jQueryAjaxSettings) {
        jQueryAjaxSettings.headers = { "Authorization": 'Bearer ' + localStorage['auth_token'] };
      }
    });
    this.apartmentsDataSource = AspNetData.createStore(apartmentsDataSourceOptions);

    this.apartmentTenantsDataStore = new ODataStore({
      url: this.apartmentTenantsApiUrl,
      key: ['ApartmentId', 'TenantId'],
      keyType: { ApartmentId: "Guid", TenantId: "Guid" },
      version: 4,
      beforeSend: (e) => {
        if (e.method === 'PATCH') {
          e.method = 'PUT';
        }
        e.headers = {
          "Authorization": 'Bearer ' + localStorage['auth_token'],
        };
      }
    });

    this.apartmentTenantsDataSource = new DataSource(new Object({
      store: this.apartmentTenantsDataStore,
      expand: ["Apartment($select=Id,ApartmentNumber)", "Tenant"],
      sort: [{ selector: 'ApartmentId', desc: false }]
      })
    );
  }

  ngOnInit() {
  }
}
