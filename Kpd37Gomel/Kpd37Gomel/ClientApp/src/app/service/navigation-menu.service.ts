import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

import { IMenuItem } from "../menu-item";

@Injectable()
export class NavigationMenuService {

  constructor(private http: HttpClient) { }

  public getMenuItems(): Observable<Array<IMenuItem>> {
    return this.http.get(
        'api/v1/menu',
        {
          headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage['auth_token']
          })
        })
      .map(data => data as Array<IMenuItem>);
  }
}
