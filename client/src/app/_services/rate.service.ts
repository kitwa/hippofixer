import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RateService {
  private baseUrl = 'https://api.example.com/ratings';

  constructor(private http: HttpClient) {}

  addRate(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}`, data);
  }
}