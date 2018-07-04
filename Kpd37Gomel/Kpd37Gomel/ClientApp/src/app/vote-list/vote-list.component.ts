import { Component, OnInit } from '@angular/core';
import {VoteService} from "../service/vote.service";
import { IVote } from "../vote";
import {ToastService} from "../service/toast.service";

@Component({
  selector: 'app-vote-list',
  templateUrl: './vote-list.component.html',
  styleUrls: ['./vote-list.component.css']
})
export class VoteListComponent implements OnInit {
  public voteList: Array<IVote>;

  constructor(
    private voteService: VoteService,
    private toastService: ToastService) { }

  ngOnInit() {
    this.voteService.getVoteList().subscribe(
      data => { this.voteList = data; },
      error => { this.toastService.showErrorToast(error.message); });
  }

}
