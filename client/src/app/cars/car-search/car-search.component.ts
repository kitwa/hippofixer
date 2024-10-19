import { Component, EventEmitter, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Car } from 'src/app/_models/car';
import { CarColor } from 'src/app/_models/carColor';
import { CarType } from 'src/app/_models/carType';
import { City } from 'src/app/_models/city';
import { Pagination } from 'src/app/_models/pagination';
import { CarsService } from 'src/app/_services/cars.service';

@Component({
  selector: 'app-car-search',
  templateUrl: './car-search.component.html',
  styleUrl: './car-search.component.css'
})
export class CarSearchComponent {

  @Output() carsFound: EventEmitter<Car[]> = new EventEmitter<Car[]>();
  searchForm: UntypedFormGroup;
  validationErrors: string[] = [];
  cars: Car[];
  location: string;
  locationFullName: string = " à Dubai";
  moreOptions: boolean = false;
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 10;
  carTypes: CarType[];
  carColors: CarColor[];
  cities: City[];

  constructor( private carsService: CarsService, private fb: UntypedFormBuilder, private route: ActivatedRoute) {
   }

    ngOnInit(): void {
      this.location = this.route.snapshot.paramMap.get('location');
      if(this.location == 'rdc'){
        this.locationFullName = 'en R.D.Congo'
      }
      this.getCities();
      this.getCarTypes();
      this.getCarColors();
      this.initializeForm();
    }
  
    initializeForm(){
      this.searchForm = this.fb.group({
        minPrice: [0, Validators.nullValidator],
        maxPrice: [0, Validators.nullValidator],
        carColorId: [0, Validators.nullValidator],
        carTypeId: [0, Validators.nullValidator],
        newUsed: [null, Validators.nullValidator],
        transmission: [null, Validators.nullValidator],
        maxMileage: [0, Validators.nullValidator],
        minYear: [0, Validators.nullValidator],
        cityId: [0, Validators.nullValidator],
      })
    }

    getCars(){
      this.carsService.getCars(this.pageNumber, this.pageSize, this.location).subscribe(response => {
        this.cars = response.result;
        this.pagination = response.pagination;
  
      })
    }
    pageChanged(event: any){
      this.pageNumber = event.page;
      this.searchCars();
    }

    searchCars(){
      this.carsService.searchCars(this.searchForm.value, this.pageNumber, this.pageSize, this.location).subscribe(response => {
  
        this.cars = response.result;
        if(response.result.length > 1){
          this.pagination = response.pagination;
        }
        this.carsFound.emit(this.cars);
      }, error => {
        this.validationErrors = error;
      });
    }

    showMoreOptions(){
      this.moreOptions = true;
    }

    showLessOptions(){
      this.moreOptions = false;
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

  getCities(){
    this.carsService.getCities().subscribe(cities => {
      this.cities = cities;
    })
  }

}
