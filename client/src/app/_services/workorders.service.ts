import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { City } from '../_models/city';
import { WorkOrder } from '../_models/workorder';


@Injectable({
  providedIn: 'root'
})
export class WorkOrdersService {

  baseUrl = environment.apiUrl;
  workorders: WorkOrder[] = [];
  paginatedResult: PaginatedResult<WorkOrder[]> = new PaginatedResult<WorkOrder[]>();

  constructor(private http: HttpClient ) {

  } 

  getWorkOrders(page? : number, itemsPerPage?: number) {

    let params = new HttpParams();

    if(page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('PageSize', itemsPerPage.toString());
    }

    // if(this.workorders.length > 0) return of(this.workorders); 
    return this.http.get<WorkOrder[]>(this.baseUrl + 'workorders', {observe: 'response', params}).pipe(

      map(response => {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedResult;
      })

    );
  }

  getWorkOrder(id: number) {
    // debugger
    // const workorder = this.workorders.find(x => x.id === id);
    // if(workorder !== undefined) return of(workorder);
    return this.http.get<WorkOrder>(this.baseUrl + 'workorders/' + id);
  }

  completeWorkOrder(workorderId: Number) {
    return this.http.put(this.baseUrl + 'workorders/' + workorderId + '/complete', {}).pipe();
  }

  rejectWorkOrder(workorderId: Number) {
    return this.http.put(this.baseUrl + 'workorders/' + workorderId + '/reject', {}).pipe();
  }
}
