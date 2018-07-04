import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {ToastService} from "../service/toast.service";
import {VoteService} from "../service/vote.service";
import {VoteResultArea} from "../vote-result-area";

@Component({
  selector: 'app-vote',
  templateUrl: './vote.component.html',
  styleUrls: ['./vote.component.css']
})
export class VoteComponent implements OnInit {
  public voteDetails: any;
  public voteResultAreas: Array<VoteResultArea>;
  private selectedVoteVariant: any;

  constructor(
    private activatedRoute: ActivatedRoute,
    private toastService: ToastService,
    private voteService: VoteService) {

    this.activatedRoute.params.subscribe(params => this.loadVote(params['id']));
    this.voteDetails = {
      isPassed: false,
      vote: {
        title: '',
        description: '',
        variants: []
      },
      result: null
    };
  }

  ngOnInit() {
  }

  private loadVote(id: any) {
    if (this.activatedRoute.snapshot.queryParams['msg']) {
      this.toastService.showSuccessToast(this.activatedRoute.snapshot.queryParams['msg']);
    }

    this.voteService.getVoteDetails(id).subscribe(
      data => {
        this.voteDetails = data;
        this.selectedVoteVariant = this.voteDetails.vote.variants[0];
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

  public onSelectionChange(voteVariant): void {
    this.selectedVoteVariant = Object.assign({}, this.selectedVoteVariant, voteVariant);
  }

  public sendVote() {
    this.voteService.sendVote(this.activatedRoute.snapshot.params.id, this.selectedVoteVariant).subscribe(
      data => {
        this.voteDetails = data;
        if (this.voteDetails.isPassed) {
          this.mapVoteResultToChart(this.voteDetails.result);
        }
      },
      error => { this.toastService.showErrorToast(error.message); });
  }
}
