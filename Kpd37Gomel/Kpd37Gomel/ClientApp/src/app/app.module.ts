import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import {
  DxDataGridModule,
  DxTemplateModule,
  DxToastModule,
  DxPieChartModule,
  DxRadioGroupModule,
  DxListModule,
  DxLoadIndicatorModule,
  DxLoadPanelModule
} from 'devextreme-angular';

import { AuthGuard } from "./guard/auth.guard";

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { VoteListComponent } from './vote-list/vote-list.component';
import { VoteComponent } from './vote/vote.component';
import { VoteCreateComponent } from './vote-create/vote-create.component';
import { VoteEditComponent } from './vote-edit/vote-edit.component';
import { VoteTemplateComponent } from './vote-template/vote-template.component';
import { VoteSetBuilderComponent } from './vote-set-builder/vote-set-builder.component';
import { VoteSetCreateComponent } from './vote-set-create/vote-set-create.component';
import { FlatsListComponent } from './flats-list/flats-list.component';
import { TenantsListComponent } from './tenants-list/tenants-list.component';

import { VoteService } from './service/vote.service';
import { ToastService } from './service/toast.service';
import { AuthenticationService } from './service/authentication.service';
import { NavigationMenuService } from './service/navigation-menu.service';

import { JwtInterceptor } from './auth/jwt.interceptor';

import { locale, loadMessages } from 'devextreme/localization';
import 'devextreme-intl';
import * as messagesRu from "devextreme/localization/messages/ru.json";

loadMessages(messagesRu);
locale("ru-Ru");

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginFormComponent,
    VoteListComponent,
    VoteComponent,
    FlatsListComponent,
    VoteCreateComponent,
    TenantsListComponent,
    VoteEditComponent,
    VoteTemplateComponent,
    VoteSetBuilderComponent,
    VoteSetCreateComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    DxDataGridModule,
    DxTemplateModule,
    DxToastModule,
    DxPieChartModule,
    DxRadioGroupModule,
    DxListModule,
    DxLoadIndicatorModule,
    DxLoadPanelModule,
    RouterModule.forRoot([
      {
        path: '', component: HomeComponent, canActivate: [AuthGuard],
        children: [
          { path: '', redirectTo: 'votes', pathMatch: 'full' },
          { path: 'votes', component: VoteListComponent, pathMatch: 'full' },
          { path: 'votes/:id', component: VoteComponent, pathMatch: 'full' },
          { path: 'flats', component: FlatsListComponent, pathMatch: 'full' },
          { path: 'tenants', component: TenantsListComponent, pathMatch: 'full' },
          { path: 'vote-create', component: VoteCreateComponent, pathMatch: 'full' },
          { path: 'votes/:id/edit', component: VoteEditComponent, pathMatch: 'full' },
          { path: 'questionnaire', component: VoteSetCreateComponent, pathMatch: 'full' }
        ]
      },
      { path: 'login', component: LoginFormComponent, pathMatch: 'full', canActivate: [AuthGuard] }
    ])
  ],
  providers: [AuthGuard, VoteService, ToastService, AuthenticationService, NavigationMenuService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
