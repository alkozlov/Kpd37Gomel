import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import ODataStore from "devextreme/data/odata/store";
import DataSource from "devextreme/data/data_source";

import { IApartment } from "../apartment"

@Component({
  selector: 'app-tenants-list',
  templateUrl: './tenants-list.component.html',
  styleUrls: ['./tenants-list.component.css']
})
export class TenantsListComponent implements OnInit {
  private tenantsApiUrl: string;
  private apartmentsApiUrl: string;

  public apartmentsDataSource: Array<IApartment>;

  private tenantsDataStore: any;
  public tenantsDataSource: any;

  constructor(private httpClient: HttpClient) {
    this.apartmentsDataSource = new Array<IApartment>();
    this.tenantsApiUrl = "odata/Tenant";
    this.apartmentsApiUrl = "api/v1/apartments";

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
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage['auth_token']
    });
    this.httpClient.get<Array<IApartment>>(this.apartmentsApiUrl, { headers }).subscribe(data => {
        this.apartmentsDataSource = data;
      },
      error => { console.log(error); }
    );
  }
}
