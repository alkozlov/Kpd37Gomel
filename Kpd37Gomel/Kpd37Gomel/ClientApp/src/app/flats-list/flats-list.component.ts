import { Component, OnInit } from '@angular/core';

import ODataStore from "devextreme/data/odata/store";
import DataSource from "devextreme/data/data_source";

@Component({
  selector: 'app-flats-list',
  templateUrl: './flats-list.component.html',
  styleUrls: ['./flats-list.component.css']
})
export class FlatsListComponent implements OnInit {
  private url: string;

  public apartmentsDataStore: any;
  public apartmentsDataSource: any;

  constructor() {
    this.url = "odata/Apartment";

    this.apartmentsDataStore = new ODataStore({
      url: this.url,
      key: "Id",
      keyType: "Guid",
      version: 4,
      beforeSend: (e) => {
        e.headers = {
          "Authorization": 'Bearer ' + localStorage['auth_token'],
        };
      }
    });

    this.apartmentsDataSource = new DataSource(new Object({
        store: this.apartmentsDataStore,
        sort: [{ selector: 'ApartmentNumber', desc: false }]
      })
    );
  }

  ngOnInit() {
  }
}
