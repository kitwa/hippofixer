import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Car } from 'src/app/_models/car';
import { Pagination } from 'src/app/_models/pagination';
import { CarsService } from 'src/app/_services/cars.service';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrl: './car-list.component.css'
})
export class CarListComponent {

  cars: Car[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 10;
  location: string;

  constructor(private carsService: CarsService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.location = this.route.snapshot.paramMap.get('location');
    this.loadCars();
  }

  loadCars(){
    this.carsService.getCars(this.pageNumber, this.pageSize, this.location).subscribe(response => {
      this.cars = response.result;
      this.pagination = response.pagination;

    })
  }
  pageChanged(event: any){
    this.pageNumber = event.page;
    this.loadCars();
  }

  onCarsFound(cars: Car[]) {
    this.cars = cars;
  }

}

