import { Component, OnInit } from '@angular/core';
import { VoteService } from "../service/vote.service";
import { IVote } from "../vote";
import { ToastService } from "../service/toast.service";
import { AuthenticationService } from "../service/authentication.service";

import { confirm } from 'devextreme/ui/dialog';

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

  public deleteVote(vote: IVote): void {
    var confirmMessage = 'Удалить голосование "' + vote.title + '"?';
    var result = confirm(confirmMessage, 'Удаление голосования');
    result.done(dialogResult => {
      if (dialogResult) {
        this.voteService.deleteVote(vote.id).subscribe(
          data => {
            var index = this.voteList.indexOf(vote, 0);
            if (index > -1) {
              this.voteList.splice(index, 1);
            }
            this.toastService.showSuccessToast('Голосование успешно удалено.');
          },
          error => { this.toastService.showErrorToast(error.error.message); }
        );
      }
    });
  }
}
