import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from "../service/authentication.service";
import { ToastService } from "../service/toast.service";
import { ILoginModel, LoginModel } from "../login-model";

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  public loginForm: FormGroup;
  public loadingVisible: boolean = false;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthenticationService,
    private toastService: ToastService) {

    this.loginForm = new FormGroup({
      'lastName': new FormControl('', Validators.compose([Validators.required, Validators.maxLength(150)])),
      'firstName': new FormControl('', Validators.compose([Validators.required, Validators.maxLength(150)])),
      'middleName': new FormControl('', Validators.compose([Validators.required, Validators.maxLength(150)])),
      'apartmentNumber': new FormControl('', Validators.compose([Validators.required, Validators.maxLength(5)]))
    });
  }

  ngOnInit() {
  }

  public onSubmit(value: any) {
    if (this.loginForm.valid) {
      var loginModel: ILoginModel = new LoginModel(value.firstName, value.middleName, value.lastName, value.apartmentNumber);
      this.loadingVisible = true;
      this.authService.login(loginModel).subscribe(
        data => {
          localStorage['auth_token'] = (data as any).token;
          localStorage['tenant'] = JSON.stringify((data as any).tenantTiny);
          if (this.activatedRoute.snapshot.queryParams['redirectUrl']) {
            this.router.navigate([this.activatedRoute.snapshot.queryParams['redirectUrl']]);
          } else {
            this.router.navigate(['/']);
          }
        },
        error => {
          this.handleHttpErrorResponse(error);
        },
        () => { this.loadingVisible = false; });
    }
  }

  private handleHttpErrorResponse(error: HttpErrorResponse) {
    if (error.status === 401) {
      //this.toastService.showErrorToast('Неверные данные.');
    } else {
      this.toastService.showErrorToast(error.error.message);
    }
  }
}
