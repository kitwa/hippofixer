import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { Invoice } from '../_models/invoice';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map, of } from 'rxjs';
import { InvoiceItem } from '../_models/invoiceItem';

@Injectable({
  providedIn: 'root'
})

export class InvoicesService {

  constructor(private http: HttpClient) { }

  baseUrl = environment.apiUrl;
  invoices: Invoice[] = [];
  paginatedResult: PaginatedResult<Invoice[]> = new PaginatedResult<Invoice[]>();

  getInvoices(page? : number, itemsPerPage?: number) {

    let params = new HttpParams();

    if(page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('PageSize', itemsPerPage.toString());
    }

    return this.http.get<Invoice[]>(this.baseUrl + 'invoices/get-invoices', {observe: 'response', params}).pipe(

      map(response => {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedResult;
      })

    );
  }

  getInvoice(id: number) {
    const invoice = this.invoices.find(x => x.id === id);
    if(invoice !== undefined) return of(invoice);
    return this.http.get<Invoice>(this.baseUrl + 'invoices/' + id);
  }

  updateInvoice(invoiceId: number, invoice: Invoice) {
    return this.http.put(this.baseUrl + 'invoices/' + invoiceId, invoice).pipe(
      map(() => {
        const index = this.invoices.indexOf(invoice);
        this.invoices[index] = invoice;
      })
    );
  }

  addOrGetInvoice(workorderId: number) {
    return this.http.post<Invoice>(this.baseUrl + `invoices/${workorderId}/add-get-invoice`, {});
  }

  deleteInvoice(invoiceId: Number) {
    return this.http.put(this.baseUrl + 'invoices/' + invoiceId + '/delete', {}).pipe();
  }

  updateInvoiceDate(invoiceId: number, invoiceDate: string){
    return this.http.put(this.baseUrl + 'invoices/' + invoiceId + '/update-invoice-date/' + invoiceDate, {}).pipe();
  }

  updateDueDate(invoiceId: number, dueDate: string){
    return this.http.put(this.baseUrl + 'invoices/' + invoiceId + '/update-due-date/' + dueDate, {}).pipe();
  }

  addInvoiceItem(invoiceId: number, invoiceItem: InvoiceItem) {
    return this.http.post<InvoiceItem>(this.baseUrl + 'invoices/' + invoiceId + '/add-invoice-item', invoiceItem).pipe();
  }

  sendInvoiceEmail(file, fileName: string, email: string, invoiceLink: string, invoice: Invoice){

    const formData = new FormData();
    formData.append('file', file);
    formData.append('fileName', fileName);
    formData.append('email', email);
    formData.append('invoiceLink', invoiceLink);
    formData.append('invoiceId', invoice.id.toString());
    formData.append('invoiceLink', invoiceLink);
    formData.append('dueDate', invoice.dueDate?.toString());
    formData.append('createdDate', invoice.createdDate.toString());

    return this.http.post<any>(this.baseUrl + 'invoices/send-email-invoice', formData).pipe();
  }
}

