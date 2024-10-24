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
import { PropertyListComponent } from './properties/property-list/property-list.component';
import { PropertyEditComponent } from './properties/property-edit/property-edit.component';
import { PropertyCardComponent } from './properties/property-card/property-card.component';
import { PhotoEditorComponent } from './properties/photo-editor/photo-editor.component';
import { PropertyAddComponent } from './properties/property-add/property-add.component';
import { PhotoCarouselComponent } from './properties/photo-carousel/photo-carousel.component';
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
import { PropertyTypeComponent } from './admin/property-type/property-type.component';
import { RouteReuseStrategy } from '@angular/router';
import { CustomRouteReuseStrategy } from './_services/customRouteReuseStrategy';
import { BondRepaymentComponent } from './bonrepayment/bond-repayment/bond-repayment.component';
import { BlogPostListComponent } from './blogpost/blog-post-list/blog-post-list.component';
import { BlogPostAddComponent } from './blogpost/blog-post-add/blog-post-add.component';
import { BlogPostDetailComponent } from './blogpost/blog-post-detail/blog-post-detail.component';
import { BlogPostEditComponent } from './blogpost/blog-post-edit/blog-post-edit.component';
import { BlogPostCardComponent } from './blogpost/blog-post-card/blog-post-card.component';
import { FooterComponent } from './footer/footer.component';
import { CarAddComponent } from './cars/car-add/car-add.component';
import { CarListComponent } from './cars/car-list/car-list.component';
import { CarCardComponent } from './cars/car-card/car-card.component';
import { CarEditComponent } from './cars/car-edit/car-edit.component';
import { CarPhotoEditorComponent } from './cars/car-photo-editor/car-photo-editor.component';
import { CarTypeComponent } from './admin/car-type/car-type.component';
import { CarSearchComponent } from './cars/car-search/car-search.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { SidebarComponent } from './sidebar/sidebar.component';
import { IssueListsComponent } from './issues/issue-lists/issue-lists.component';
import { IssueAddComponent } from './issues/issue-add/issue-add.component';
import { IssueDetailComponent } from './issues/issue-detail/issue-detail.component';

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
    PropertyListComponent,
    PropertyEditComponent,
    PropertyCardComponent,
    PhotoEditorComponent,
    CarPhotoEditorComponent,
    PropertyAddComponent,
    PhotoCarouselComponent,
    LoginComponent,
    MemberMessagesComponent,
    AdminPanelComponent,
    HasRoleDirective,
    UserManagementComponent,
    RolesModalComponent,
    ResetPasswordComponent,
    NewPasswordComponent,
    CityComponent,
    PropertyTypeComponent,
    BondRepaymentComponent,
    BlogPostListComponent,
    BlogPostAddComponent,
    BlogPostDetailComponent,
    BlogPostEditComponent,
    BlogPostCardComponent,
    FooterComponent,
    CarListComponent,
    CarCardComponent,
    CarAddComponent,
    CarEditComponent,
    CarTypeComponent,
    CarSearchComponent,
    SidebarComponent,
    IssueListsComponent,
    IssueAddComponent,
    IssueDetailComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    NgxSpinnerModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: !isDevMode(),
      // Register the ServiceWorker as soon as the application is stable
      // or after 30 seconds (whichever comes first).
      registrationStrategy: 'registerWhenStable:30000'
    })
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
