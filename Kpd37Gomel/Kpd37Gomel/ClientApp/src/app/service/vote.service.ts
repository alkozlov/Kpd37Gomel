
import {throwError as observableThrowError,  Observable } from 'rxjs';

import {catchError, map} from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpErrorResponse, HttpParams } from '@angular/common/http';





import { IVote } from '../vote';
import tabs from 'devextreme/ui/tabs';

@Injectable()
export class VoteService {
  constructor(private http: HttpClient) { }

  public getVoteDetails(voteId: any): Observable<any> {
    return this.http.get(
        'api/v1/votes/' + voteId,
        {
          headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage['auth_token']
          })
        }).pipe(
      map(data => data as any));
  }

  public getVoteList(): Observable<Array<IVote>> {
    return this.http.get(
        'api/v1/votes',
        {
          headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage['auth_token']
          })
        }).pipe(
      map(data => data as Array<IVote>));
  }

  // =============== OData implementation
  private voteODataApiUrl: string = 'odata/Vote';

  public createVote(vote: IVote, params: HttpParams = null): Observable<IVote> {
    if (params === null) {
      params = new HttpParams();
    }

    var headers: HttpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage['auth_token']
    });

    return this.http.post(
        this.voteODataApiUrl,
        vote,
        {
          headers,
          params
        }).pipe(
      map((response: IVote) => response),
      catchError(this.handleError),);
  }

  public getVotes(params: HttpParams = null): Observable<Array<IVote>> {
    if (params === null) {
      params = new HttpParams();
    }

    var headers: HttpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage['auth_token']
    });

    return this.http.get(
        this.voteODataApiUrl,
        {
          headers,
          params
        }).pipe(
      map((response: HttpResponse<any>) => (response as any).value as Array<IVote>),
      catchError(this.handleError),);
  }

  public getVoteById(key: any, params: HttpParams = null): Observable<IVote> {
    if (params === null) {
      params = new HttpParams();
    }

    var headers: HttpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage['auth_token']
    });

    return this.http.get(
        this.voteODataApiUrl + '(' + key + ')',
        {
          headers,
          params
        }).pipe(
      map((response: IVote) => response),
      catchError(this.handleError),);
  }
  
  public updateVote(key: any, vote: IVote, params: HttpParams = null): Observable<IVote> {
    if (params === null) {
      params = new HttpParams();
    }

    var headers: HttpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage['auth_token']
    });

    return this.http.patch(
        this.voteODataApiUrl + '(' + key + ')',
        vote,
        {
          headers,
          params
        }
      ).pipe(
      map((response: IVote) => response),
      catchError(this.handleError),);
  }

  public deleteVote(key: any): Observable<Object> {
    var headers: HttpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage['auth_token']
    });

    return this.http.delete(
        this.voteODataApiUrl + '(' + key + ')',
        {
          headers
        }).pipe(
      catchError(this.handleError));
  }

  public sendVote(voteId: any, voteVariandId: any): Observable<Object> {
    var headers: HttpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage['auth_token']
    });

    var data = { VariantId: voteVariandId };

    return this.http.post(
        this.voteODataApiUrl + '(' + voteId + ')' + '/Default.SendVote',
        data,
        {
          headers
        }).pipe(
      catchError(this.handleError));
  }

  public getVoteCommonResult(voteId: any): Observable<any> {
    var headers: HttpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage['auth_token']
    });

    return this.http.get(
        this.voteODataApiUrl + '(' + voteId + ')' + '/Default.GetCommonResult',
        {
          headers
        }).pipe(
      map((response: any) => response),
      catchError(this.handleError),);
  }

  private handleError(errorResponse: HttpErrorResponse) {
    return observableThrowError(errorResponse.error.error.message ||
      'Произошла неизвестная ошибка. Пожалуйста обратитесь к администратору.');
  }
}
