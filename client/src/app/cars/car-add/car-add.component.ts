import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Car } from 'src/app/_models/car';
import { CarColor } from 'src/app/_models/carColor';
import { CarType } from 'src/app/_models/carType';
import { City } from 'src/app/_models/city';
import { Country } from 'src/app/_models/country';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { CarsService } from 'src/app/_services/cars.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-car-add',
  templateUrl: './car-add.component.html',
  styleUrl: './car-add.component.css'
})
export class CarAddComponent {

  addForm: UntypedFormGroup;
  validationErrors: string[] = [];
  car: Car;
  user: User;
  carTypes: CarType[];
  carColors: CarColor[];
  cities: City[];
  countries: Country[];
  members: Member[];
  // @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
  //   if(this.addForm.dirty){
  //     $event.returnValue = true;
  //   }
  // }

  constructor(private acountService: AccountService, private carService: CarsService, 
    private toastr: ToastrService, private fb: UntypedFormBuilder, private router: Router, private memberService: MembersService) {
    this.acountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

   ngOnInit(): void {
    this.initializeForm();
    this.getCarTypes();
    this.getCarColors();
    this.getCities();
    this.getCountries();
    this.loadMembers();
  }

  initializeForm(){
    this.addForm = this.fb.group({
      price: ['', Validators.required],
      carColorId: ['', Validators.required],
      carTypeId: [null, Validators.required],
      description: ['', Validators.required],
      newUsed: [null, Validators.required],
      transmission: [null, Validators.required],
      fuelType: [null, Validators.required],
      mileage: [null, Validators.required],
      year: [null, Validators.required],
      cityId: [null, Validators.required],
      countryId: [null, Validators.required],
      appUserId: ['', Validators.required],
      agentId: [null, Validators.nullValidator]

    })
  }

  addCar(){
    this.carService.addCar(this.addForm.value).subscribe(car => {
      this.toastr.success("saved successfully");
      this.router.navigateByUrl('/car/edit/' + car.id);
    }, error => {
      this.validationErrors = error;
    });

  }

  getCountries(){
    this.carService.getCountries().subscribe(res => {
      this.countries = res;
    })
  }

  getCities(){
    this.carService.getCities().subscribe(res => {
      this.cities = res;
    })
  }

  getCarColors(){
    this.carService.getCarColors().subscribe(res => {
      this.carColors = res;
    })
  }

  getCarTypes(){
    this.carService.getCarTypes().subscribe(carTypes => {
      this.carTypes = carTypes;
    })
  }

  loadMembers(){
    this.memberService.getMembers(1, 10).subscribe(response => {
      this.members = response.result;
    })
  }
}