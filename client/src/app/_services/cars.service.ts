import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { SearchData } from '../_models/searchData';
import { Country } from '../_models/country';
import { City } from '../_models/city';
import { Car } from '../_models/car';
import { SearchCarData } from '../_models/searchCarData';
import { CarType } from '../_models/carType';
import { CarColor } from '../_models/carColor';


@Injectable({
  providedIn: 'root'
})
export class CarsService {

  baseUrl = environment.apiUrl;
  cars: Car[] = [];
  paginatedResult: PaginatedResult<Car[]> = new PaginatedResult<Car[]>();

  constructor(private http: HttpClient ) {

  } 

  getCars(page? : number, itemsPerPage?: number, location?: string) {

    let params = new HttpParams();

    if(page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('PageSize', itemsPerPage.toString());
      params = params.append('Location', location.toString());
    }

    // if(this.cars.length > 0) return of(this.cars); 
    return this.http.get<Car[]>(this.baseUrl + 'cars', {observe: 'response', params}).pipe(

      map(response => {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedResult;
      })

    );
  }

  searchCars(searchData: SearchCarData, page? : number, itemsPerPage?: number, location?: string) {

    let params = new HttpParams();

    if(page !== null && itemsPerPage !== null) {

      params = params.append('pageNumber', page.toString());
      params = params.append('PageSize', itemsPerPage.toString());
      params = params.append('Location', location.toString());
    }

    return this.http.post<Car[]>(this.baseUrl + 'cars/search-cars', searchData, {observe: 'response', params}).pipe(
      map(response => {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedResult;
      })
    );
  }

  getCar(id: number) {
    const car = this.cars.find(x => x.id === id);
    if(car !== undefined) return of(car);
    return this.http.get<Car>(this.baseUrl + 'cars/' + id);
  }

  updateCar(carId: number, car: Car) {
    return this.http.put(this.baseUrl + 'cars/' + carId, car).pipe(
      map(() => {
        const index = this.cars.indexOf(car);
        this.cars[index] = car;
      })
    );
  }

  addCar(car: Car) {
    return this.http.post<Car>(this.baseUrl + 'cars', car);
  }

  deleteCar(carId: Number) {
    return this.http.put(this.baseUrl + 'cars/' + carId + '/delete', {}).pipe();
  }

  maskAsSoldCar(carId: Number) {
    return this.http.put(this.baseUrl + 'cars/' + carId + '/mask-as-sold', {}).pipe();
  }

  setMainPhoto(carId: Number, photoId: Number) {
    return this.http.put(this.baseUrl + 'cars/' + carId + '/set-main-photo/' + photoId, {});
  }

  deletePhoto(carId: Number, photoId: Number) {
    return this.http.delete(this.baseUrl + 'cars/' + carId + '/delete-photo/' + photoId);
  }

  getCarTypes() {
    return this.http.get<CarType[]>(this.baseUrl + 'cars/car-types');
  }
  
  getCarColors() {
    return this.http.get<CarColor[]>(this.baseUrl + 'cars/car-colors');
  }

  getCountries() {
    return this.http.get<Country[]>(this.baseUrl + 'cars/countries');
  }

  getCities() {
    return this.http.get<City[]>(this.baseUrl + 'cars/cities');
  }

  getAgents() {
    return this.http.get<City[]>(this.baseUrl + 'cars/agents');
  }
}
