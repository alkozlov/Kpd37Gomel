import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from "../service/authentication.service";
import { ILoginModel, LoginModel } from "../login-model";

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private router: Router,
    private authService: AuthenticationService) {
    this.loginForm = new FormGroup({
      'lastName': new FormControl('', Validators.compose([Validators.required, Validators.maxLength(150)])),
      'firstName': new FormControl('', Validators.compose([Validators.required, Validators.maxLength(150)])),
      'middleName': new FormControl('', Validators.compose([Validators.required, Validators.maxLength(150)]))
    });
  }

  ngOnInit() {
  }

  onSubmit(value: any) {
    if (this.loginForm.valid) {
      var loginModel: ILoginModel = new LoginModel(value.firstName, value.middleName, value.lastName);
      this.authService.login(loginModel);
    }
  }
}
