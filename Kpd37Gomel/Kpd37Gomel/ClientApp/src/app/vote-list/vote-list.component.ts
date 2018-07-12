import { Component, OnInit } from '@angular/core';
import { VoteService } from "../service/vote.service";
import { ToastService } from "../service/toast.service";
import { AuthenticationService } from "../service/authentication.service";
import { HttpParams } from '@angular/common/http';
import { confirm } from 'devextreme/ui/dialog';
import { IUserData } from "../user-data";
import { IVote } from "../vote";

@Component({
  selector: 'app-vote-list',
  templateUrl: './vote-list.component.html',
  styleUrls: ['./vote-list.component.css']
})
export class VoteListComponent implements OnInit {
  public voteList: Array<IVote>;
  public currentUser: IUserData;
  public loadingVisible: boolean = false;

  constructor(
    private voteService: VoteService,
    private toastService: ToastService,
    private authService: AuthenticationService) {

    this.voteList = new Array<IVote>();
    this.currentUser = this.authService.getCurrentUser();

    
  }

  ngOnInit() {
    this.loadingVisible = true;
    this.currentUser = this.authService.getCurrentUser();

    var params: HttpParams = new HttpParams();
    params = params.append('$select', ['Id', 'Title', 'Description', 'CreateDateUtc'].join(','));
    params = params.append('$orderby', 'CreateDateUtc desc');

    this.voteService.getVotes(params).subscribe(
      data => {
        this.loadingVisible = false;
        this.voteList = data;
      },
      error => {
        this.loadingVisible = false;
        this.toastService.showErrorToast(error);
      });
  }
  
  public deleteVote(vote: IVote): void {
    var confirmMessage = 'Удалить голосование "' + vote.Title + '"?';
    var result = confirm(confirmMessage, 'Удаление голосования');
    result.done(dialogResult => {
      if (dialogResult) {
        this.loadingVisible = true;
        this.voteService.deleteVote(vote.Id).subscribe(
          data => {
            var index = this.voteList.indexOf(vote, 0);
            if (index > -1) {
              this.voteList.splice(index, 1);
            }
            this.toastService.showSuccessToast('Голосование успешно удалено.');
          },
          error => this.toastService.showErrorToast(error),
          () => { this.loadingVisible = false; }
        );
      }
    });
  }
}
