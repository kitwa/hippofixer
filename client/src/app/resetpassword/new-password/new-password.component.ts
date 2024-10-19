import { Component, OnInit } from '@angular/core';
import { AbstractControl, UntypedFormBuilder, UntypedFormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-new-password',
  templateUrl: './new-password.component.html',
  styleUrls: ['./new-password.component.css']
})
export class NewPasswordComponent implements OnInit {
  
  newPasswordForm: UntypedFormGroup;
  validationErrors: any[] = [];
  email: string;
  resetToken: string;


  constructor(private accountService: AccountService,  private toastr: ToastrService, private fb: UntypedFormBuilder, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.email = params['email'];
      this.resetToken = params['resetToken'].replace(/\s/g, '+');
    });
    this.initializeForm();
  }

  initializeForm(){
    this.newPasswordForm = this.fb.group({
      email: ['', Validators.required],
      resetToken: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]]
    },
    {
      validators: this.matchValidator('password', 'confirmPassword')
    })
  }

  newPassword() {
     this.accountService.newPassword(this.newPasswordForm.value).subscribe(response => {
      this.router.navigateByUrl('/');
    }, error => {
      this.validationErrors = error.error;
    });
  }

  matchValidator(controlName: string, matchingControlName: string): ValidatorFn {
    return (abstractControl: AbstractControl) => {
        const control = abstractControl.get(controlName);
        const matchingControl = abstractControl.get(matchingControlName);

        if (matchingControl!.errors && !matchingControl!.errors?.['confirmedValidator']) {
            return null;
        }

        if (control!.value !== matchingControl!.value) {
          const error = { confirmedValidator: 'Passwords do not match.' };
          matchingControl!.setErrors(error);
          return error;
        } else {
          matchingControl!.setErrors(null);
          return null;
        }
    }
  }
}