import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();
  @ViewChild('autoShownModal', { static: false }) autoShownModal?: ModalDirective;
  isModalShown = false;
  resetPasswordForm: UntypedFormGroup;
  validationErrors: string[] = [];

  constructor(public accountService: AccountService,  private toastr: ToastrService, private fb: UntypedFormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.resetPasswordForm = this.fb.group({
      email: ['', Validators.required],
    })
  }

  resetpassword(){
    this.accountService.resetPassword(this.resetPasswordForm.value).subscribe(response => {
      this.showModal();
    }, error => {
      this.validationErrors = error.error;
    });
  }

  showModal(): void {
    this.isModalShown = true;
  }
 
  hideModal(): void {
    this.autoShownModal?.hide();
  }
 
  onHidden(): void {
    this.isModalShown = false;
  }

}