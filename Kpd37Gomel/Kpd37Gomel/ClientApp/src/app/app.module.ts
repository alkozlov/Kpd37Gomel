import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AuthGuard } from "./guard/auth.guard";

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginFormComponent } from './login-form/login-form.component';

import { VoteListComponent } from './vote-list/vote-list.component';
import { VoteComponent } from './vote/vote.component';
import { VoteCreateComponent } from './vote-create/vote-create.component';
import { FlatsListComponent } from './flats-list/flats-list.component';
import { TenantsListComponent } from './tenants-list/tenants-list.component';

import { DxDataGridModule, DxTemplateModule, DxToastModule, DxPieChartModule, DxRadioGroupModule } from 'devextreme-angular';
import { VoteService } from './service/vote.service';
import { ToastService } from './service/toast.service';
import { AuthenticationService } from './service/authentication.service';
import { NavigationMenuService } from "./service/navigation-menu.service";

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
    TenantsListComponent
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
    RouterModule.forRoot([
      {
        path: '', component: HomeComponent, canActivate: [AuthGuard],
        children: [
          { path: '', redirectTo: 'votes', pathMatch: 'full' },
          { path: 'votes', component: VoteListComponent, pathMatch: 'full' },
          { path: 'votes/:id', component: VoteComponent, pathMatch: 'full' },
          { path: 'flats', component: FlatsListComponent, pathMatch: 'full' },
          { path: 'tenants', component: TenantsListComponent, pathMatch: 'full' },
          { path: 'vote-create', component: VoteCreateComponent, pathMatch: 'full' }
        ]
      },
      { path: 'login', component: LoginFormComponent, pathMatch: 'full', canActivate: [AuthGuard] }
    ])
  ],
  providers: [AuthGuard, VoteService, ToastService, AuthenticationService, NavigationMenuService],
  bootstrap: [AppComponent]
})
export class AppModule { }
