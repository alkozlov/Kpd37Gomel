import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { FooterComponent } from './shared/layout/footer/footer.component';
import { HeaderComponent } from './shared/layout/header/header.component';
import { SidebarComponent } from './shared/layout/sidebar/sidebar.component';
import { ButtonsComponent } from './shared/layout/header/buttons/buttons.component';
import { CollapseComponent } from './shared/layout/header/collapse/collapse.component';
import { SearchComponent } from './shared/layout/header/search/search.component';
import { NavComponent } from './shared/layout/header/nav/nav.component';
import { NotificationsComponent } from './shared/layout/header/nav/notifications/notifications.component';
import { ProfileComponent } from './shared/layout/header/nav/profile/profile.component';
import { CustomizeComponent } from './shared/layout/sidebar/customize/customize.component';
import { SidebarHeaderComponent } from './shared/layout/sidebar/sidebar-header/sidebar-header.component';
import { MetisMenuDirective } from './directives/metis-menu.directive';

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HeaderComponent,
    SidebarComponent,
    ButtonsComponent,
    CollapseComponent,
    SearchComponent,
    NavComponent,
    NotificationsComponent,
    ProfileComponent,
    CustomizeComponent,
    SidebarHeaderComponent,
    MetisMenuDirective
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
