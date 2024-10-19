import { CarColor } from './carColor';
import { CarType } from './carType';
import { City } from './city';
import { Country } from './country';
import { Member } from './member';
import {Photo} from './photo';

export interface Car {
    id: number;
    reference: string;
    appUserId?: number;
    price: number;
    newUsed: string;
    transmission: string;
    fuelType: string;
    youtubeLink: string;
    mileage: number;
    year: number;
    estimatedDeliveryDate: Date;
    description: string;
    carPhotos: Photo[];
    cityId: number;
    city: City;
    carColorId: number;
    carColor: CarColor;
    carTypeId: number;
    carType: CarType;
    country: Country;
    countryId: number;
    appUser: Member;
    agent: Member;
    agentId: number;
    created: Date;
    sold: boolean;
    deleted: boolean;
}