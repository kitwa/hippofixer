import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FileUploadModule } from 'ng2-file-upload';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TimeagoModule } from 'ngx-timeago';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    PaginationModule.forRoot(),
    FileUploadModule,
    ModalModule.forRoot(),
    TimeagoModule.forRoot()
  ],
  exports: [
    ToastrModule,
    PaginationModule,
    FileUploadModule,
    ModalModule,
    TimeagoModule
  ]
})
export class SharedModule { }
