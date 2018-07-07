import { Component, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { IVote } from '../vote';

@Component({
  selector: 'vote-template',
  templateUrl: './vote-template.component.html',
  styleUrls: ['./vote-template.component.css']
})
export class VoteTemplateComponent {
  public voteTemplateForm: FormGroup;
  public variants: Array<any>;

  @Input()
  public set vote(data: IVote) {
    this.voteTemplateForm.controls['title'].setValue(data.title);
    this.voteTemplateForm.controls['description'].setValue(data.description);
    this.variants = this.variants;
  }

  public get vote(): IVote {
    return {
      title: this.voteTemplateForm.controls['title'].value,
      description: this.voteTemplateForm.controls['description'].value,
      variants: this.variants
    } as IVote;
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

    this.variants = new Array<any>();
    this.variants.push({ text: 'За' });
    this.variants.push({ text: 'Против' });
    this.variants.push({ text: 'Воздержаться' });
  }
}
