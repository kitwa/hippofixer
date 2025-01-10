import { NgModule, isDevMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { SharedModule } from './_modules/shared.module';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { LoginComponent } from './login/login.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { MemberMessagesComponent } from './members/member-messages/member-messages.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { RolesModalComponent } from './modals/roles-modal/roles-modal.component';
import { ResetPasswordComponent } from './resetpassword/reset-password/reset-password.component';
import { NewPasswordComponent } from './resetpassword/new-password/new-password.component';
import { CityComponent } from './admin/city/city.component';
import { RouteReuseStrategy } from '@angular/router';
import { CustomRouteReuseStrategy } from './_services/customRouteReuseStrategy';
import { BlogPostListComponent } from './blogpost/blog-post-list/blog-post-list.component';
import { BlogPostAddComponent } from './blogpost/blog-post-add/blog-post-add.component';
import { BlogPostDetailComponent } from './blogpost/blog-post-detail/blog-post-detail.component';
import { BlogPostEditComponent } from './blogpost/blog-post-edit/blog-post-edit.component';
import { BlogPostCardComponent } from './blogpost/blog-post-card/blog-post-card.component';
import { FooterComponent } from './footer/footer.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { IssueListsComponent } from './issues/issue-lists/issue-lists.component';
import { IssueAddComponent } from './issues/issue-add/issue-add.component';
import { IssueDetailComponent } from './issues/issue-detail/issue-detail.component';
import { WorkorderListsComponent } from './workorders/workorder-lists/workorder-lists.component';
import { WorkorderDetailComponent } from './workorders/workorder-detail/workorder-detail.component';
import { IssueAddNoaccountComponent } from './issues/issue-add-noaccount/issue-add-noaccount.component';
import { InvoiceDetailComponent } from './invoices/invoice-detail/invoice-detail.component';
import { InvoiceListsComponent } from './invoices/invoice-lists/invoice-lists.component';
import { InvoiceAddComponent } from './invoices/invoice-add/invoice-add.component';
import { SupportComponent } from './support/support.component';
import { CardAddComponent } from './payment/card/card-add/card-add.component';
import { CardListComponent } from './payment/card/card-list/card-list.component';
import { RateAddComponent } from './rate/rate-add/rate-add.component';
import { RateDetailComponent } from './rate/rate-detail/rate-detail.component';
import { RateListComponent } from './rate/rate-list/rate-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    MemberDetailComponent,
    ListsComponent,
    MessagesComponent,
    TestErrorsComponent,
    NotFoundComponent,
    ServerErrorComponent,
    MemberCardComponent,
    MemberEditComponent,
    TextInputComponent,
    LoginComponent,
    MemberMessagesComponent,
    AdminPanelComponent,
    HasRoleDirective,
    UserManagementComponent,
    RolesModalComponent,
    ResetPasswordComponent,
    NewPasswordComponent,
    CityComponent,
    BlogPostListComponent,
    BlogPostAddComponent,
    BlogPostDetailComponent,
    BlogPostEditComponent,
    BlogPostCardComponent,
    FooterComponent,
    SidebarComponent,
    IssueListsComponent,
    IssueAddComponent,
    IssueDetailComponent,
    IssueAddNoaccountComponent,
    WorkorderListsComponent,
    WorkorderDetailComponent,
    InvoiceDetailComponent,
    InvoiceListsComponent,
    InvoiceAddComponent,
    SupportComponent,
    CardAddComponent,
    CardListComponent,
    RateAddComponent,
    RateDetailComponent,
    RateListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    NgxSpinnerModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
    {provide: RouteReuseStrategy, useClass: CustomRouteReuseStrategy}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
