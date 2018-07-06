import { Component, OnInit, ViewChild } from '@angular/core';
import { DxDataGridComponent } from "devextreme-angular";
import { ActivatedRoute } from '@angular/router';
import { ToastService } from "../service/toast.service";
import { VoteService } from "../service/vote.service";
import { AuthenticationService } from "../service/authentication.service";

import { VoteResultArea } from "../vote-result-area";
import { IUserData } from "../user-data";

import * as AspNetData from "devextreme-aspnet-data";

@Component({
  selector: 'app-vote',
  templateUrl: './vote.component.html',
  styleUrls: ['./vote.component.css']
})
export class VoteComponent implements OnInit {
  @ViewChild(DxDataGridComponent) apartmentVoteResultDataGrid: DxDataGridComponent;

  public voteDetails: any;
  public voteResultAreas: Array<VoteResultArea>;
  public apartmentVoteResultDataSource: any;
  public currentUser: IUserData;

  private selectedVoteVariant: any;

  constructor(
    private activatedRoute: ActivatedRoute,
    private toastService: ToastService,
    private voteService: VoteService,
    private authService: AuthenticationService) {

    this.activatedRoute.params.subscribe(params => this.loadVote(params['id']));
    this.selectedVoteVariant = '';
    this.voteDetails = {
      isPassed: false,
      vote: {
        title: '',
        description: '',
        variants: []
      },
      result: null
    };

    var dataSourceOptions = new Object({
      loadUrl: 'api/v1/votes/' + this.activatedRoute.snapshot.params.id + '/details',
      onBeforeSend: function (operation, jQueryAjaxSettings) {
        jQueryAjaxSettings.headers = { "Authorization": 'Bearer ' + localStorage['auth_token'] };
      }
    });
    this.apartmentVoteResultDataSource = AspNetData.createStore(dataSourceOptions);

    this.currentUser = this.authService.getCurrentUser();
  }

  ngOnInit() {
    this.currentUser = this.authService.getCurrentUser();
  }

  private loadVote(id: any) {
    if (this.activatedRoute.snapshot.queryParams['msg']) {
      this.toastService.showSuccessToast(this.activatedRoute.snapshot.queryParams['msg']);
    }

    this.voteService.getVoteDetails(id).subscribe(
      data => {
        this.voteDetails = data;
        this.selectedVoteVariant = this.voteDetails.isPassed
          ? this.voteDetails.result.voteChoise
          : this.voteDetails.vote.variants[0].id;
        if (this.voteDetails.isPassed) {
          this.mapVoteResultToChart(this.voteDetails.result);
        }
      },
      error => { this.toastService.showErrorToast(error.message); });
  }

  private mapVoteResultToChart(voreResult: any): void {
    this.voteResultAreas = new Array<VoteResultArea>();
    for (var key in voreResult.voices) {
      if (voreResult.voices.hasOwnProperty(key)) {
        for (var i = 0; i < this.voteDetails.vote.variants.length; i++) {
          if (this.voteDetails.vote.variants[i].id === key) {
            var voiceResult: VoteResultArea = new VoteResultArea();
            voiceResult.answer = this.voteDetails.vote.variants[i].text;
            voiceResult.result = Math.round(voreResult.voices[key] * 100) / 100;
            this.voteResultAreas.push(voiceResult);
          }
        }
      }
    }
  }

  private refreshApartmentVoteResultDataGrid(): void {
    this.apartmentVoteResultDataGrid.instance.refresh();
  }

  public sendVote() {
    this.voteService.sendVote(this.activatedRoute.snapshot.params.id, this.selectedVoteVariant).subscribe(
      data => {
        this.refreshApartmentVoteResultDataGrid();
        this.voteDetails = data;
        if (this.voteDetails.isPassed) {
          this.mapVoteResultToChart(this.voteDetails.result);
        }
      },
      error => { this.toastService.showErrorToast(error.message); });
  }
}
