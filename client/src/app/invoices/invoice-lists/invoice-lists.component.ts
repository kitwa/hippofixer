import { Component } from '@angular/core';
import { take } from 'rxjs';
import { Invoice } from 'src/app/_models/invoice';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { InvoicesService } from 'src/app/_services/invoices.service';

@Component({
  selector: 'app-invoice-lists',
  templateUrl: './invoice-lists.component.html',
  styleUrl: './invoice-lists.component.css'
})
export class InvoiceListsComponent {

  user: User;
  invoices: Invoice[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 10;

  constructor(private accountService: AccountService, private invoicesService: InvoicesService) {     
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user)
  }

  ngOnInit(): void {
    this.getInvoices();
  }

  getInvoices(){
    this.invoicesService.getInvoices(this.pageNumber, this.pageSize).subscribe(response => {
      this.invoices = response.result;
      this.pagination = response.pagination;
    })
  }

  pageChanged(event: any){
    this.pageNumber = event.page;
    this.getInvoices();
  }
  

}
