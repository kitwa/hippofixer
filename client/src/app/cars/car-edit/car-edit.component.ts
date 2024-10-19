import { Component, HostListener, TemplateRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
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
  selector: 'app-car-edit',
  templateUrl: './car-edit.component.html',
  styleUrl: './car-edit.component.css'
})
export class CarEditComponent {
  car: Car;
  editForm: UntypedFormGroup;
  validationErrors: string[] = [];
  user: User;
  modalRef?: BsModalRef;
  message?: string;
  carId = this.route.snapshot.params['id'];
  carTypes: CarType[];
  carColors: CarColor[];
  cities: City[];
  countries: Country[];
  members: Member[];

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  }

  constructor(private acountService: AccountService, private carsService: CarsService, private memberService: MembersService, 
    private toastr: ToastrService, private fb: UntypedFormBuilder, private route: ActivatedRoute, private router: Router, private modalService: BsModalService) {
      this.acountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

    ngOnInit(): void {
      this.loadCar();
      this.getCities();
      this.getCountries();
      this.getCarTypes();
      this.getCarTypes();
      this.getCarColors();
      this.loadMembers();
    }
  
    initializeForm(){
      this.editForm = this.fb.group({
        price: ['', Validators.required],
        carColorId: ['', Validators.required],
        carTypeId: [null, Validators.required],
        description: ['', Validators.required],
        newUsed: [null, Validators.required],
        fuelType: [null, Validators.required],
        transmission: [null, Validators.required],
        mileage: [null, Validators.required],
        year: [null, Validators.required],
        youtubeLink: ['', Validators.nullValidator],
        cityId: [null, Validators.required],
        countryId: [null, Validators.required],
        appUserId: ['', Validators.required],
        agentId: [null, Validators.nullValidator]
  
      })
    }

  loadCar(){ 
    this.carsService.getCar(this.route.snapshot.params['id']).subscribe(res => {
      this.car = res;
      this.initializeForm();
    })
  }

  updateCar(){
    this.carsService.updateCar(this.route.snapshot.params['id'], this.editForm.value).subscribe(() => {
      this.toastr.success("Changes saved successfully");
      // this.editForm.reset(this.Car);
    })
  }

  getCarTypes(){
    this.carsService.getCarTypes().subscribe(res => {
      this.carTypes = res;
    })
  }

  getCarColors(){
    this.carsService.getCarColors().subscribe(res => {
      this.carColors = res;
    })
  }

  getCountries(){
    this.carsService.getCountries().subscribe(countries => {
      this.countries = countries;
    })
  }

  getCities(){
    this.carsService.getCities().subscribe(cities => {
      this.cities = cities;
    })
  }

  loadMembers(){
    this.memberService.getMembers(1, 10).subscribe(response => {
      this.members = response.result;
    })
  }

  openDeleteModal(deleteCar: TemplateRef<void>) {
    this.modalRef = this.modalService.show(deleteCar, { class: 'modal-sm' });
  }

  markAsSoldModal(markAsSoldCar: TemplateRef<void>) {
    this.modalRef = this.modalService.show(markAsSoldCar, { class: 'modal-sm' });
  }
 
  confirmDeleteCar(): void {
    this.carsService.deleteCar(this.carId).subscribe(() => {
      this.modalRef?.hide();
      this.router.navigateByUrl('/cars');
      this.toastr.success("Car successfully deleted.");
    })

  }
 
  declineDeleteCar(): void {
    this.modalRef?.hide();
  }

  confirmMarkAsSoldCar(): void {
    this.carsService.maskAsSoldCar(this.carId).subscribe(() => {
      this.modalRef?.hide();
      this.router.navigateByUrl('/cars/' + this.carId);
      this.toastr.success("Car successfully mark as sold.");
    })

  }
 
  declineMarkAsSoldCar(): void {
    this.modalRef?.hide();
  }

}
