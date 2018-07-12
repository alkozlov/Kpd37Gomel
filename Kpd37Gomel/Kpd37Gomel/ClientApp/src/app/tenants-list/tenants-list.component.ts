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
  private tenantsApiUrl: string;
  private apartmentsApiUrl: string;

  public apartmentsDataSource: any;

  private tenantsDataStore: any;
  public tenantsDataSource: any;

  constructor() {
    this.tenantsApiUrl = "odata/Tenant";
    this.apartmentsApiUrl = "api/v1/apartments";

    var apartmentsDataSourceOptions = new Object({
      key: "id",
      loadUrl: 'api/v1/apartments',
      onBeforeSend(operation, jQueryAjaxSettings) {
        jQueryAjaxSettings.headers = { "Authorization": 'Bearer ' + localStorage['auth_token'] };
      }
    });
    this.apartmentsDataSource = AspNetData.createStore(apartmentsDataSourceOptions);

    this.tenantsDataStore = new ODataStore({
      url: this.tenantsApiUrl,
      key: 'Id',
      keyType: 'Guid',
      version: 4,
      beforeSend: (e) => {
        e.headers = {
          "Authorization": 'Bearer ' + localStorage['auth_token'],
        };
      }
    });

    this.tenantsDataSource = new DataSource(new Object({
      store: this.tenantsDataStore,
      expand: ["Apartment($select=Id,ApartmentNumber)"],
      sort: [{ selector: 'ApartmentId', desc: false }]
      })
    );
  }

  ngOnInit() {
  }
}
