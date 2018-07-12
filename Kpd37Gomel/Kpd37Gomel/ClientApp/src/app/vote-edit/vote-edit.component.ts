import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpParams } from '@angular/common/http';
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
  public loadingVisible: boolean = false;

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
    this.variants.push({ text: 'За', SequenceIndex: 0 });
    this.variants.push({ text: 'Против', SequenceIndex: 1 });
    this.variants.push({ text: 'Воздержаться', SequenceIndex: 2 });

    this.activatedRoute.params.subscribe(params => this.loadVote(params['id']));
  }

  ngOnInit() {
  }

  private loadVote(id: any) {
    this.loadingVisible = true;

    var params: HttpParams = new HttpParams();
    params = params.append('$select', ['Id', 'Title', 'Description', 'CreateDateUtc'].join(','));
    params = params.append('$expand', 'Variants($orderby=SequenceIndex)');

    this.voteService.getVoteById(id, params)
      .subscribe(
        data => {
          this.vote = data;
          this.voteEditForm.controls['title'].setValue(this.vote.Title);
          this.voteEditForm.controls['description'].setValue(this.vote.Description);
        },
        error => {
          this.loadingVisible = false;
          this.toastService.showErrorToast(error);
        },
        () => this.loadingVisible = false
      );
  }

  onSubmit(value: any) {
    if (this.voteEditForm.valid) {
      var vote = { Title: value.title, Description: value.description };
      this.loadingVisible = true;

      var params: HttpParams = new HttpParams();
      params = params.append('$select', ['Id', 'Title', 'Description', 'CreateDateUtc'].join(','));

      this.voteService.updateVote(this.activatedRoute.snapshot.params.id, vote as IVote, params)
        .subscribe(
          data => this.toastService.showSuccessToast('Данные успешно обновлены.'),
          error => {
            this.loadingVisible = false;
            this.toastService.showErrorToast(error);
          },
          () => this.loadingVisible = false
        );
    }
  }
}
