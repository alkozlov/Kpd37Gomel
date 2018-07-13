import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpParams } from '@angular/common/http';
import { VoteService } from "../service/vote.service";
import { IVote } from "../vote";
import { ToastService } from "../service/toast.service";

@Component({
  selector: 'app-vote-create',
  templateUrl: './vote-create.component.html',
  styleUrls: ['./vote-create.component.css']
})
export class VoteCreateComponent implements OnInit {
  public voteCreateForm: FormGroup;
  public variants: Array<any>;
  public loadingVisible: boolean = false;

  constructor(private router: Router, private voteService: VoteService, private toastService: ToastService) {
    this.voteCreateForm = new FormGroup({
      'title': new FormControl('', Validators.compose([Validators.required, Validators.minLength(1), Validators.maxLength(200)])),
      'description': new FormControl('', Validators.compose([Validators.required, Validators.minLength(1), Validators.maxLength(500)]))
    });

    this.variants = new Array<any>();
    this.variants.push({ Text: 'За', SequenceIndex: 0 });
    this.variants.push({ Text: 'Против', SequenceIndex: 1 });
    this.variants.push({ Text: 'Воздержаться', SequenceIndex: 2 });
  }

  ngOnInit() {
  }

  onSubmit(value: any) {
    if (this.voteCreateForm.valid) {
      var vote = { Title: value.title, Description: value.description, UseVoteRate: true, Variants: this.variants };
      this.loadingVisible = true;

      this.voteService.createVote(vote as IVote).subscribe(
        data => {
          this.router.navigate(['votes', data.Id], { queryParams: { msg: 'Опрос успено создан!' } });
        },
        error => {
          this.loadingVisible = false;
          this.toastService.showErrorToast(error.message);
        },
        () => this.loadingVisible = false);
    }
  }
}
