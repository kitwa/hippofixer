import { Component, Input, OnInit, Output, EventEmitter} from '@angular/core';
import { AbstractControl, UntypedFormBuilder, FormControl, UntypedFormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
 // @Input() usersFromHomeComponent: any; //get data from parent
  @Output() cancelRegister = new EventEmitter();
  registerForm: UntypedFormGroup;
  validationErrors: any[] = [];


  constructor(private accountService: AccountService,  private toastr: ToastrService, private fb: UntypedFormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.registerForm = this.fb.group({
      email: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]]
    },
    {
      validators: this.matchValidator('password', 'confirmPassword')
    })
  }

  register() {
     this.accountService.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('/');
    }, error => {
      this.validationErrors = error.error;
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
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
