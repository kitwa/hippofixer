import { City } from './city';
import { PropertyType } from './propertyType';

export interface SearchData {

    minPrice: number;
    maxPrice: number;
    bathRooms: number;
    bedRooms: number;
    city: City;
    cityId: number;
    propertyType: PropertyType;
    propertyTypeId: number;

}