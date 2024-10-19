import { CarType } from './carType';
import { City } from './city';

export interface SearchCarData {

    minPrice: number;
    maxPrice: number;
    maxMileage: number;
    minYear: number;
    cityId: number;
    carColorId: number;
    carTypeId: number;
    transmission: string;
    newUsed: string;
}