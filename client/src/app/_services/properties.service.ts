import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Property } from '../_models/property';
import { PaginatedResult } from '../_models/pagination';
import { ActivatedRoute } from '@angular/router';
import { SearchData } from '../_models/searchData';
import { PropertyType } from '../_models/propertyType';
import { Country } from '../_models/country';
import { City } from '../_models/city';


@Injectable({
  providedIn: 'root'
})
export class PropertiesService {

  baseUrl = environment.apiUrl;
  properties: Property[] = [];
  paginatedResult: PaginatedResult<Property[]> = new PaginatedResult<Property[]>();

  constructor(private http: HttpClient ) {

  } 

  getProperties(page? : number, itemsPerPage?: number) {

    let params = new HttpParams();

    if(page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('PageSize', itemsPerPage.toString());
    }

    // if(this.properties.length > 0) return of(this.properties); 
    return this.http.get<Property[]>(this.baseUrl + 'properties', {observe: 'response', params}).pipe(

      map(response => {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedResult;
      })

    );
  }

  searchProperty(searchData: SearchData, page? : number, itemsPerPage?: number) {

    let params = new HttpParams();

    if(page !== null && itemsPerPage !== null) {

      params = params.append('pageNumber', page.toString());
      params = params.append('PageSize', itemsPerPage.toString());
    }

    return this.http.post<Property[]>(this.baseUrl + 'properties/search-properties', searchData, {observe: 'response', params}).pipe(
      map(response => {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedResult;
      })
    );
  }

  getProperty(id: number) {
    const property = this.properties.find(x => x.id === id);
    if(property !== undefined) return of(property);
    return this.http.get<Property>(this.baseUrl + 'properties/' + id);
  }

  updateProperty(propertyId: number, property: Property) {
    return this.http.put(this.baseUrl + 'properties/' + propertyId, property).pipe(
      map(() => {
        const index = this.properties.indexOf(property);
        this.properties[index] = property;
      })
    );
  }

  addProperty(property: Property) {
    return this.http.post<Property>(this.baseUrl + 'properties', property);
  }

  deleteProperty(propertyId: Number) {
    return this.http.put(this.baseUrl + 'properties/' + propertyId + '/delete', {}).pipe();
  }

  maskAsSoldProperty(propertyId: Number) {
    return this.http.put(this.baseUrl + 'properties/' + propertyId + '/mask-as-sold', {}).pipe();
  }

  setMainPhoto(propertyId: Number, photoId: Number) {
    return this.http.put(this.baseUrl + 'properties/' + propertyId + '/set-main-photo/' + photoId, {});
  }

  deletePhoto(propertyId: Number, photoId: Number) {
    return this.http.delete(this.baseUrl + 'properties/' + propertyId + '/delete-photo/' + photoId);
  }

  getPropertyTypes() {
    return this.http.get<PropertyType[]>(this.baseUrl + 'properties/property-types');
  }

  getCountries() {
    return this.http.get<Country[]>(this.baseUrl + 'properties/countries');
  }

  getCities() {
    return this.http.get<City[]>(this.baseUrl + 'properties/cities');
  }

  getAgents() {
    return this.http.get<City[]>(this.baseUrl + 'properties/agents');
  }
}
