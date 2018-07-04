import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

import { IVote } from '../vote';

@Injectable()
export class VoteService {

  constructor(private http: HttpClient) { }

  public createVote(vote: IVote): Observable<IVote> {
    return this.http.post(
      'api/v1/votes',
      vote,
      {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + localStorage['auth_token']
        })
      })
      .map(data => data as IVote);
  }

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

  public sendVote(voteId: any, voteVariant: any): Observable<any> {
    return this.http.post(
      'api/v1/votes/' + voteId + '/send-vote',
      voteVariant,
      {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + localStorage['auth_token']
        })
      })
      .map(data => data as any);
  }

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
}
