import { Component, Input } from '@angular/core';
import { IVote } from '../vote';

@Component({
  selector: 'vote-set-builder',
  templateUrl: './vote-set-builder.component.html',
  styleUrls: ['./vote-set-builder.component.css']
})
export class VoteSetBuilderComponent {
  private _voteSet: Array<IVote>;
  private _selectedVote: IVote;
  public selectedVotes: Array<IVote>;

  constructor() {
    this._voteSet = new Array<IVote>();
    this._selectedVote = { title: '', description: '' } as IVote;
  }

  public set voteSet(data: Array<IVote>) {
    this._voteSet = data;
  }

  @Input()
  public get voteSet(): Array<IVote> {
    return this._voteSet;
  }

  public set selectedVote(data: IVote) {
    this._selectedVote = data;
  }

  public get selectedVote(): IVote {
    return this._selectedVote;
  }

  public addVote(vote: IVote) {
    this._voteSet.push(vote);
  }

  public onVoteSelected(event: any) {
    this.selectedVote = event.addedItems[0];
  }
}
