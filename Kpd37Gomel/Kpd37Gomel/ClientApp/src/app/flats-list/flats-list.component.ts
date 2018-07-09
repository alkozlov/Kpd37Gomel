import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

import * as AspNetData from "devextreme-aspnet-data";

import { IApartment } from "../apartment";

import ODataStore from "devextreme/data/odata/store";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: 'app-flats-list',
  templateUrl: './flats-list.component.html',
  styleUrls: ['./flats-list.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FlatsListComponent implements OnInit {
  public apartments: Array<IApartment>;
  public dataSource: any;

  private url: string;

  public store: ODataStore;
  public apartmentsDataSource: any;

  constructor() {
    this.url = "api/v1/apartments";

    var dataSourceOptions = new Object({
      key: "id",
      loadUrl: this.url,
      insertUrl: this.url,
      updateUrl: this.url,
      deleteUrl: this.url,
      onBeforeSend: function (operation, jQueryAjaxSettings) {
        jQueryAjaxSettings.headers = { "Authorization": 'Bearer ' + localStorage['auth_token'] };
      }
    });
    this.dataSource = AspNetData.createStore(dataSourceOptions);



    this.store = new ODataStore({
      url: "odata/Apartment",
      key: "Id",
      keyType: "Guid",
      version: 4,
      beforeSend: (e) => {
        e.headers = {
          "Authorization": 'Bearer ' + localStorage['auth_token'],
        };
      }
      // Other ODataStore options go here
    });

    this.apartmentsDataSource = new DataSource(new Object({
        store: this.store,
        sort: [{ selector: 'ApartmentNumber', desc: false }]
      })
    );
  }

  tenantDataSources = {};

  getTenantsDataSource(id: any) {
    if (!this.tenantDataSources[id]) {
      var tenantDataSourceOptions = new Object({
        key: "id",
        noDataText: 'Нет данных.',
        loadUrl: this.url + "/" + id + "/tenants",
        insertUrl: this.url + "/" + id + "/tenants",
        updateUrl: this.url + "/" + id + "/tenants",
        deleteUrl: this.url + "/" + id + "/tenants",
        onBeforeSend: function (operation, jQueryAjaxSettings) {
          jQueryAjaxSettings.headers = { "Authorization": 'Bearer ' + localStorage['auth_token'] };
        }
      });

      this.tenantDataSources[id] = AspNetData.createStore(tenantDataSourceOptions);
    }

    return this.tenantDataSources[id];
  }

  ngOnInit() {
  }
}
