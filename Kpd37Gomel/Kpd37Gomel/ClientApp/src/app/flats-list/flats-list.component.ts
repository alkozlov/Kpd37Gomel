import { Component, OnInit, ChangeDetectionStrategy  } from '@angular/core';

import * as AspNetData from "devextreme-aspnet-data";

import { IApartment } from "../apartment";

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
