import { Component, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { IVote } from '../vote';

@Component({
  selector: 'vote-template',
  templateUrl: './vote-template.component.html',
  styleUrls: ['./vote-template.component.css']
})
export class VoteTemplateComponent {
  private _vote: IVote;

  public voteTemplateForm: FormGroup;
  public variants: Array<any>;

  @Input()
  public set vote(data: IVote) {
    this._vote = data;
    this.voteTemplateForm.controls['title'].setValue(this._vote.title);
    this.voteTemplateForm.controls['description'].setValue(this._vote.description);
    this.variants = this._vote.variants;
  }

  public get vote(): IVote {
    this._vote.title = this.voteTemplateForm.controls['title'].value;
    this._vote.description = this.voteTemplateForm.controls['description'].value;

    return this._vote;
  }

  @Input()
  public get invalid(): boolean {
    return this.voteTemplateForm.invalid;
  }

  constructor() {
    this.voteTemplateForm = new FormGroup({
      'title': new FormControl('', Validators.compose([Validators.required, Validators.minLength(1), Validators.maxLength(200)])),
      'description': new FormControl('', Validators.compose([Validators.required, Validators.minLength(1), Validators.maxLength(500)]))
    });
  }
}
