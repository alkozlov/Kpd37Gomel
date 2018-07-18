import { Component, OnInit, ViewChild } from '@angular/core';
import { DxDataGridComponent } from "devextreme-angular";
import { ActivatedRoute } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { ToastService } from "../service/toast.service";
import { VoteService } from "../service/vote.service";
import { AuthenticationService } from "../service/authentication.service";
import * as AspNetData from "devextreme-aspnet-data";
import ODataStore from "devextreme/data/odata/store";
import DataSource from "devextreme/data/data_source";
import CustomStore from 'devextreme/data/custom_store';
import 'rxjs/add/operator/toPromise';

import { VoteResultArea } from "../vote-result-area";
import { IUserData } from "../user-data";
import { IVote } from '../vote';
import { IVoteVariant } from '../vote-variant';

@Component({
  selector: 'app-vote',
  templateUrl: './vote.component.html',
  styleUrls: ['./vote.component.css']
})
export class VoteComponent implements OnInit {
  @ViewChild(DxDataGridComponent) apartmentVoteResultDataGrid: DxDataGridComponent;

  public vote: IVote;
  public voteResultAreas: Array<VoteResultArea>;
  public apartmentVoteResultDataSource: any;
  public currentUser: IUserData;
  public loadingVisible: boolean = false;

  public pieChartDataSource: any;

  public selectedVoteVariantId: any;

  constructor(
    private activatedRoute: ActivatedRoute,
    private toastService: ToastService,
    private voteService: VoteService,
    private authService: AuthenticationService) {

    this.activatedRoute.params.subscribe(params => this.loadVote(params['id']));
    this.selectedVoteVariantId = null;
    this.vote = { Id: null, IsPassed: false, Title: '', Description: '', Variants: [] } as IVote;

    //var dataSourceOptions = new Object({
    //  loadUrl: 'api/v1/votes/' + this.activatedRoute.snapshot.params.id + '/details',
    //  onBeforeSend(operation, jQueryAjaxSettings) {
    //    jQueryAjaxSettings.headers = { "Authorization": 'Bearer ' + localStorage['auth_token'] };
    //  }
    //});
    //this.apartmentVoteResultDataSource = AspNetData.createStore(dataSourceOptions);

    this.currentUser = this.authService.getCurrentUser();
  }

  ngOnInit() {
    this.currentUser = this.authService.getCurrentUser();
  }

  private loadVote(id: any) {
    if (this.activatedRoute.snapshot.queryParams['msg']) {
      this.toastService.showSuccessToast(this.activatedRoute.snapshot.queryParams['msg']);
    }

    var params: HttpParams = new HttpParams();
    params = params.append('$select', ['Id', 'Title', 'Description', 'CreateDateUtc', 'IsPassed'].join(','));
    params = params.append('$expand', 'Variants($orderby=SequenceIndex)');

    this.voteService.getVoteById(id, params)
      .subscribe(
        data => {
          this.vote = data;
          this.selectedVoteVariantId = this.vote.Variants[0].Id;

          this.pieChartDataSource = new DataSource({
            store: {
              type: 'odata',
              url: 'odata/Vote' + '(' + this.vote.Id + ')' + '/Default.GetCommonResults',
              key: 'Id',
              beforeSend: (e) => {
                e.headers = {
                  "Authorization": 'Bearer ' + localStorage['auth_token'],
                };
              }
            },
            paginate: false
          });

          this.apartmentVoteResultDataSource = new DataSource({
            store: {
              type: 'odata',
              url: 'odata/Vote' + '(' + this.vote.Id + ')' + '/Default.GetDetailedResults' + '?$select=Id,VoteRate&$expand=Apartment($select=Id,ApartmentNumber,TotalArea;$expand=Tenants($filter=IsOwner eq true)),VoteVariant($select=Id,Text)',
              key: 'Id',
              beforeSend: (e) => {
                e.headers = {
                  "Authorization": 'Bearer ' + localStorage['auth_token'],
                };
              }
            },
            paginate: false
          });
        },
        error => {
          this.loadingVisible = false;
          this.toastService.showErrorToast(error);
        },
        () => this.loadingVisible = false
      );
  }

  private refreshApartmentVoteResultDataGrid(): void {
    this.apartmentVoteResultDataGrid.instance.refresh();
  }

  public sendVote() {
    this.loadingVisible = true;

    this.voteService.sendVote(this.vote.Id, this.selectedVoteVariantId).subscribe(
      () => {
        this.vote.IsPassed = true;
        this.toastService.showSuccessToast('Ваш голос принят.');
        this.refreshApartmentVoteResultDataGrid();
      },
      error => {
        this.loadingVisible = false;
        this.toastService.showErrorToast(error);
      },
      () => this.loadingVisible = false
    );
  }

  public calculateCellValue(data): string {
    if (data.Apartment.Tenants && data.Apartment.Tenants.length > 0) {
      return [
        data.Apartment.Tenants[0].LastName, data.Apartment.Tenants[0].FirstName.substring(0, 1) + '.',
        data.Apartment.Tenants[0].MiddleName.substring(0, 1) + '.'
      ].join(" ");
    } else {
      return "СОБСТВЕННИК НЕ ЗАДАН!";
    }
  }
}
