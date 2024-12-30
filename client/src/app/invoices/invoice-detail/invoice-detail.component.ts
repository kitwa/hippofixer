import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Invoice } from 'src/app/_models/invoice';
import { WorkOrder } from 'src/app/_models/workorder';
import { InvoicesService } from 'src/app/_services/invoices.service';
import { WorkOrdersService } from 'src/app/_services/workorders.service';

@Component({
  selector: 'app-invoice-detail',
  templateUrl: './invoice-detail.component.html',
  styleUrl: './invoice-detail.component.css'
})
export class InvoiceDetailComponent {

  invoice: Invoice;
  workorderId: number;
  invoiceDate: string;
  dueDate: string; 
  // invoiceItem: InvoiceItem[] = [];
  workorder: WorkOrder;
  totalAmount: number = 0;
  subTotal: number = 0;

  constructor(private invoicesService: InvoicesService, private route: ActivatedRoute,
    private toastr: ToastrService, private workordersService: WorkOrdersService ) {  
    this.addOrGetInvoice();   
    this.getWorkOrder();
  }

  addOrGetInvoice(){
    this.workorderId = this.route.snapshot.params['id'];
    this.invoicesService.addOrGetInvoice(this.workorderId).subscribe(res => {
      this.invoice = res;
      this.invoiceDate = this.formatDate(res.createdDate);
      this.dueDate = this.formatDate(res.dueDate);
      this.invoice.invoiceItems.forEach(x => {
        this.totalAmount = this.totalAmount + x.price;
        this.subTotal += x.price;
      })
      }, error => {
    });
  }

  shareInvoice() {
    if (navigator.share) {
      navigator.share({
        title: `Invoice!`,
        text: `Shared invoice from hippo fixer`,
        url: window.location.href,
      }).then(() => {
        console.log('Thanks for sharing!');
      }).catch((error) => {
        console.error('Error sharing', error);
      });
    } 
  }

  getWorkOrder(){
    this.workordersService.getWorkOrder(this.route.snapshot.params['id']).subscribe(workorder => {
      this.workorder = workorder;
    })
  }

  private formatDate(date: Date): string {
    return date.toString().split('T')[0];
  }

}
