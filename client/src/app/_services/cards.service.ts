import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Card } from '../_models/card';
import { PaginatedResult } from '../_models/pagination';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CardsService {

  constructor(private http: HttpClient) { }

  baseUrl = environment.apiUrl;
  cards: Card[] = [];
  paginatedResult: PaginatedResult<Card[]> = new PaginatedResult<Card[]>();

  getCards(page? : number, itemsPerPage?: number) {

    let params = new HttpParams();

    if(page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('PageSize', itemsPerPage.toString());
    }

    return this.http.get<Card[]>(this.baseUrl + 'cards/get-cards', {observe: 'response', params}).pipe(

      map(response => {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedResult;
      })

    );
  }

  getCard(id: number) {
    const invoice = this.cards.find(x => x.id === id);
    if(invoice !== undefined) return of(invoice);
    return this.http.get<Card>(this.baseUrl + 'cards/' + id);
  }

  addCard(card: Card) {
    debugger
    return this.http.post<Card>(this.baseUrl + 'cards/add-card', card).pipe();
  }

  deleteCard(invoiceId: Number) {
    return this.http.put(this.baseUrl + 'cards/' + invoiceId + '/delete', {}).pipe();
  }

}