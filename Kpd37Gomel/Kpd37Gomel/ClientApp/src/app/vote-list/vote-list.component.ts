import { Component, OnInit } from '@angular/core';
import {VoteService} from "../service/vote.service";
import { IVote } from "../vote";
import { ToastService } from "../service/toast.service";
import { AuthenticationService } from "../service/authentication.service";

import { IUserData } from "../user-data";

@Component({
  selector: 'app-vote-list',
  templateUrl: './vote-list.component.html',
  styleUrls: ['./vote-list.component.css']
})
export class VoteListComponent implements OnInit {
  public voteList: Array<IVote>;
  public currentUser: IUserData;

  constructor(
    private voteService: VoteService,
    private toastService: ToastService,
    private authService: AuthenticationService) {

    this.voteList = new Array<IVote>();
    this.currentUser = this.authService.getCurrentUser();
  }

  ngOnInit() {
    this.currentUser = this.authService.getCurrentUser();
    this.voteService.getVoteList().subscribe(
      data => { this.voteList = data; },
      error => { this.toastService.showErrorToast(error.message); });
  }

}
