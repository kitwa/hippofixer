import { City } from './city';
import { Country } from './country';
import { Gender } from './gender';
import { Property } from './property'

export interface Member {
  id: number;
  email: string;
  firstName?: string;
  lastName?: string;
  userName?: string;
  fullAddress?: string;
  phone?: string;
  created: Date;
  genderId?: number;
  gender: Gender;
  photo: File;
  photoUrl?: string;
  photoPublicId : string;
  cityId?: number;
  city: City;
  countryId?: number;
  country: Country;
  twitter?: string;
  youtube?: string;
  instagram?: string;
  facebook?: string;

}

