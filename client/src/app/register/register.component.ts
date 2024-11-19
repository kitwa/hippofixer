import { Component, Input, OnInit, Output, EventEmitter} from '@angular/core';
import { AbstractControl, UntypedFormBuilder, FormControl, UntypedFormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';
import { City } from '../_models/city';

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
  cities: City[];


  constructor(private accountService: AccountService,  private toastr: ToastrService, private fb: UntypedFormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.getCities();
  }

  initializeForm(){
    this.registerForm = this.fb.group({
      email: ['',[
        Validators.required,
        Validators.email
      ]],
      phone: [null,         [
        Validators.required,
        Validators.pattern('^[0-9]{10}$')
      ]],
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]],
      cityId: [null, Validators.required],
      cityName: ['', Validators.required]
    },
    {
      validators: this.matchValidator('password', 'confirmPassword')
    })

    this.registerForm.get('cityName')?.valueChanges.subscribe(value => {
      const selectedCity = this.cities.find(city => city.name === value);
      this.registerForm.patchValue({ cityId: selectedCity ? selectedCity.id : null });
    });

  }

  register() {
     this.accountService.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('/member/edit');
    }, error => {
      this.validationErrors = error.error;
    });
  }

  getCities(){
    this.accountService.getCities().subscribe(cities => {
      this.cities = cities;
      this.initializeForm();
    })
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
