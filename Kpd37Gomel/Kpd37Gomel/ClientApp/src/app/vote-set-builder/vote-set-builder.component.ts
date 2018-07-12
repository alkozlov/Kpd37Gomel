import { Component, Input } from '@angular/core';
import { Guid } from "guid-typescript";
import { IVote } from '../vote';

@Component({
  selector: 'vote-set-builder',
  templateUrl: './vote-set-builder.component.html',
  styleUrls: ['./vote-set-builder.component.css']
})
export class VoteSetBuilderComponent {
  private _voteSet: Array<IVote>;
  public currentVote: IVote;
  public selectedVotes: Array<IVote>;

  public variants: Array<any>;

  constructor() {
    this._voteSet = new Array<IVote>();

    this.variants = new Array<any>();
    this.variants.push({ text: 'За' });
    this.variants.push({ text: 'Против' });
    this.variants.push({ text: 'Воздержаться' });

    this.currentVote = { Id: null, Title: '', Description: '', Variants: this.variants } as IVote;
  }

  public set voteSet(data: Array<IVote>) {
    this._voteSet = data;
  }

  @Input()
  public get voteSet(): Array<IVote> {
    return this._voteSet;
  }

  public addVote(vote: IVote) {
    if (vote.Id === null) {
      vote.Id = Guid.create().toString();
      this.voteSet.push(vote);
      this.currentVote = { Id: null, Title: '', Description: '', Variants: this.variants } as IVote;
    } else {
      var tmpVote = this.voteSet.find(voteItem => voteItem.Id === vote.Id);
      if (tmpVote) {
        var index = this._voteSet.indexOf(tmpVote);
        this.voteSet[index] = tmpVote;
        this.currentVote = { Id: null, Title: '', Description: '', Variants: this.variants } as IVote;
      }
    }
  }

  public addNewVote(): void {
    this.currentVote = { Id: null, Title: '', Description: '', Variants: this.variants } as IVote;
  }

  public listSelectionChanged = (event) => {
    if (event.addedItems && event.addedItems.length > 0) {
      var targetVoteIndex = this.voteSet.findIndex(vote => vote.Id === event.addedItems[0].id);
      if (targetVoteIndex > -1) {
        this.currentVote = this.voteSet[targetVoteIndex];
      }
    }
  };
}
