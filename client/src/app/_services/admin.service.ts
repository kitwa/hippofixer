import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { Stats } from '../_models/stats';
import { PropertyType } from '../_models/propertyType';
import { City } from '../_models/city';
import { CarType } from '../_models/carType';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http : HttpClient) { }

  getUsersWithRoles() {
    return this.http.get<Partial<User[]>>(this.baseUrl + 'admin/users-with-roles');
  }

  updateUserRoles(email: string, roles: string[]) {
    return this.http.post(this.baseUrl + 'admin/edit-roles/' + email + '?roles=' + roles, {})
  }

  getStats() {
    return this.http.get<Stats>(this.baseUrl + 'admin/stats');
  }

  addCity(city: City) {
    return this.http.post<City>(this.baseUrl + 'admin/create-city', city);
  }

  deleteCity(id: number) {
    return this.http.delete(this.baseUrl + 'admin/delete-city/' + id);
  }

  addPropertyType(propertyType: PropertyType) {
    return this.http.post<PropertyType>(this.baseUrl + 'admin/create-property-type', propertyType);
  }
  
  deletePropertyType(id: number) {
    return this.http.delete(this.baseUrl + 'admin/delete-property-type/' + id);
  }

  addCarType(carType: CarType) {
    return this.http.post<CarType>(this.baseUrl + 'admin/create-car-type', carType);
  }

  deleteCarType(id: number) {
    return this.http.delete(this.baseUrl + 'admin/delete-car-type/' + id);
  }
}
