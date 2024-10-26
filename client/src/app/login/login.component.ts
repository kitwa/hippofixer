import { Component, Input, OnInit, Output, EventEmitter} from '@angular/core';
import { AbstractControl, UntypedFormBuilder, FormControl, UntypedFormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  loginForm: UntypedFormGroup;
  validationErrors: string[] = [];

  constructor(public accountService: AccountService,  private toastr: ToastrService, private fb: UntypedFormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.loginForm = this.fb.group({
      email: ['', Validators.required],
      password: ['', [Validators.required]],

    })
  }

  login(){
    this.accountService.login(this.loginForm.value).subscribe(response => {
      this.router.navigateByUrl('/issues');
    }, error => {
      this.validationErrors = error.error;
    });
  }

}
