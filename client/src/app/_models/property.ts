import { City } from './city';
import { Country } from './country';
import { Member } from './member';
import {Photo} from './photo';
import { PropertyType } from './propertyType';

export interface Property {
    id: number;
    reference: string;
    appUserId?: number;
    price: number;
    bathRooms: number;
    bedRooms: number;
    garage: number;
    propertyType: PropertyType;
    propertyTypeId: number;
    description: string;
    youtubeLink?: string;
    photos: Photo[];
    city: City;
    cityId: number;
    country: Country;
    countryId: number;
    appUser: Member;
    agent: Member;
    agentId: number;
    created: Date;
    sold: boolean;
    deleted: boolean;
}