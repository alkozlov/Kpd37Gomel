import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastService } from "../service/toast.service";
import { VoteService } from "../service/vote.service";
import { IVote } from '../vote';

@Component({
  selector: 'app-vote-edit',
  templateUrl: './vote-edit.component.html',
  styleUrls: ['./vote-edit.component.css']
})
export class VoteEditComponent implements OnInit {
  private vote: IVote;

  public voteEditForm: FormGroup;
  public variants: Array<any>;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private toastService: ToastService,
    private voteService: VoteService) {

    this.voteEditForm = new FormGroup({
      'title': new FormControl('', Validators.compose([Validators.required, Validators.minLength(1), Validators.maxLength(200)])),
      'description': new FormControl('', Validators.compose([Validators.required, Validators.minLength(1), Validators.maxLength(500)]))
    });

    this.variants = new Array<any>();
    this.variants.push({ text: 'За' });
    this.variants.push({ text: 'Против' });
    this.variants.push({ text: 'Воздержаться' });

    this.activatedRoute.params.subscribe(params => this.loadVote(params['id']));
  }

  ngOnInit() {
  }

  private loadVote(id: any) {
    this.voteService.getVoteDetails(id).subscribe(
      data => {
        this.vote = data.vote;
        this.voteEditForm.controls['title'].setValue(this.vote.title);
        this.voteEditForm.controls['description'].setValue(this.vote.description);
      },
      error => { this.toastService.showErrorToast(error.message); });
  }

  onSubmit(value: any) {
    if (this.voteEditForm.valid) {
      var vote = { title: value.title, description: value.description };
      this.voteService.updateVote(this.activatedRoute.snapshot.params.id, vote as IVote).subscribe(
        data => { this.toastService.showSuccessToast('Данные успешно обновлены.'); },
        error => { this.toastService.showErrorToast(error.message); });
    }
  }
}
