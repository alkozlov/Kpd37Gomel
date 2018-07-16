import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw'
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { IVote } from '../vote';
import tabs from 'devextreme/ui/tabs';

@Injectable()
export class VoteService {
  constructor(private http: HttpClient) { }

  //public createVote(vote: IVote): Observable<IVote> {
  //  return this.http.post(
  //    'api/v1/votes',
  //    vote,
  //    {
  //      headers: new HttpHeaders({
  //        'Content-Type': 'application/json',
  //        'Authorization': 'Bearer ' + localStorage['auth_token']
  //      })
  //    })
  //    .map(data => data as IVote);
  //}

  //public updateVote(voteId: string, vote: IVote): Observable<IVote> {
  //  return this.http.put(
  //      'api/v1/votes/' + voteId,
  //      vote,
  //      {
  //        headers: new HttpHeaders({
  //          'Content-Type': 'application/json',
  //          'Authorization': 'Bearer ' + localStorage['auth_token']
  //        })
  //      })
  //    .map(data => data as IVote);
  //}

  public getVoteDetails(voteId: any): Observable<any> {
    return this.http.get(
        'api/v1/votes/' + voteId,
        {
          headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage['auth_token']
          })
        })
      .map(data => data as any);
  }

  //public sendVote(voteId: any, voteVariantId: any): Observable<any> {
  //  var data = { id: voteVariantId };
  //  return this.http.post(
  //    'api/v1/votes/' + voteId + '/send-vote',
  //    data,
  //    {
  //      headers: new HttpHeaders({
  //        'Content-Type': 'application/json',
  //        'Authorization': 'Bearer ' + localStorage['auth_token']
  //      })
  //    })
  //    .map(data => data as any);
  //}

  public getVoteList(): Observable<Array<IVote>> {
    return this.http.get(
        'api/v1/votes',
        {
          headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + localStorage['auth_token']
          })
        })
      .map(data => data as Array<IVote>);
  }

  //public deleteVote(voteId: string): Observable<Object> {
  //  return this.http.delete(
  //    'api/v1/votes/' + voteId,
  //    {
  //      headers: new HttpHeaders({
  //        'Content-Type': 'application/json',
  //        'Authorization': 'Bearer ' + localStorage['auth_token']
  //      })
  //    });
  //}

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
        })
      .map((response: IVote) => response)
      .catch(this.handleError);
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
        })
      .map((response: HttpResponse<any>) => (response as any).value as Array<IVote>)
      .catch(this.handleError);
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
        })
      .map((response: IVote) => response)
      .catch(this.handleError);
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
      )
      .map((response: IVote) => response)
      .catch(this.handleError);
  }

  public deleteVote(key: any): Observable<void> {
    var headers: HttpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage['auth_token']
    });

    return this.http.delete(
        this.voteODataApiUrl + '(' + key + ')',
        {
          headers
        })
      .catch(this.handleError);
  }

  public sendVote(voteVariandId: any): Observable<void> {
    var headers: HttpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage['auth_token']
    });

    return this.http.post(
        this.voteODataApiUrl + '/Default.SendVote(VariantId=' + voteVariandId + ')',
        {
          headers
        })
      .catch(this.handleError);
  }

  private handleError(errorResponse: HttpErrorResponse) {
    return Observable.throw(errorResponse.error.error.message ||
      'Произошла неизвестная ошибка. Пожалуйста обратитесь к администратору.');
  }
}
