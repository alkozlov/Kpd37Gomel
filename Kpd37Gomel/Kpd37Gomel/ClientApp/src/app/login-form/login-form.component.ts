import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';

import { AuthenticationService } from "../service/authentication.service";
import { LoaderOverlayService } from "../service/loader-overlay.service";
import { LoaderOverlayRef } from "../service/loader-overlay-ref";
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
    private toastService: ToastService,
    private loaderOverlayService: LoaderOverlayService) {

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
      let loaderOverlayRef: LoaderOverlayRef = this.loaderOverlayService.open();

      this.authService.login(loginModel).subscribe(
        data => {
          // Hide loader overlay
          loaderOverlayRef && loaderOverlayRef.close();

          localStorage['auth_token'] = (data as any).token;
          localStorage['tenant'] = JSON.stringify((data as any).tenantTiny);

          if (this.activatedRoute.snapshot.queryParams['redirectUrl']) {
            this.router.navigate([this.activatedRoute.snapshot.queryParams['redirectUrl']]);
          } else {
            this.router.navigate(['/']);
          }
        },
        error => {
          // Hide loader overlay
          loaderOverlayRef && loaderOverlayRef.close();
          this.handleHttpErrorResponse(error);
        });
    }
  }

  private handleHttpErrorResponse(error: HttpErrorResponse) {
    if (error.status === 401) {
      
    } else {
      this.toastService.showErrorToast(error.error.message);
    }
  }

  public getErrorMessage(formControl: FormControl): string {
    return formControl.hasError('required') ? 'Это поле обязательное.' :
      formControl.hasError('maxlength') ? 'Допустимая длиня поля ' + formControl.getError('maxlength').requiredLength + ' символов.' :
      'Непредвиденное исключение.';
  }

  public getErrorIcon(formControl: FormControl): string {
    if (!formControl.touched) {
      return 'sentiment_satisfied';
    }

    return formControl.valid ? 'sentiment_satisfied' : 'sentiment_dissatisfied';
  }
}
