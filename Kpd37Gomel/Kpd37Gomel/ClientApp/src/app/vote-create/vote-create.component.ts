import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { VoteService } from "../service/vote.service";
import { Vote } from "../vote";
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
    this.variants.push({ text: 'За' });
    this.variants.push({ text: 'Против' });
    this.variants.push({ text: 'Воздержаться' });
  }

  ngOnInit() {
  }

  onSubmit(value: any) {
    if (this.voteCreateForm.valid) {
      var vote = { title: value.title, description: value.description, useVoteRate: true, variants: this.variants };
      this.loadingVisible = true;
      this.voteService.createVote(vote as Vote).subscribe(
        data => { this.router.navigate(['votes', data.id], { queryParams: { msg: 'Опрос успено создан!' } }); },
        error => { this.toastService.showErrorToast(error.message); },
        () => { this.loadingVisible = false; });
    }
  }
}
