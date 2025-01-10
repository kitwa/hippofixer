import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Invoice } from 'src/app/_models/invoice';
import { InvoiceItem } from 'src/app/_models/invoiceItem';
import { WorkOrder } from 'src/app/_models/workorder';
import { InvoicesService } from 'src/app/_services/invoices.service';
import { WorkOrdersService } from 'src/app/_services/workorders.service';

@Component({
  selector: 'app-invoice-add',
  templateUrl: './invoice-add.component.html',
  styleUrl: './invoice-add.component.css'
})

export class InvoiceAddComponent {

  addInvoiceItemForm: UntypedFormGroup;
  validationErrors: string[] = [];
  invoice: Invoice;
  workorderId: number;
  invoiceDate: string;
  dueDate: string; 
  invoiceItem: InvoiceItem[] = [];
  workorder: WorkOrder;
  totalAmount: number = 0;
  subTotal: number = 0;

  constructor(private invoicesService: InvoicesService, private route: ActivatedRoute,   
    private fb: UntypedFormBuilder, private toastr: ToastrService, private workordersService: WorkOrdersService ) {  
    this.initializeAddInvoiceItemForm();
    this.addOrGetInvoice();   
    this.getWorkOrder();
  }

  addOrGetInvoice(){
    this.workorderId = this.route.snapshot.params['workorderId'];
    this.invoicesService.addOrGetInvoice(this.workorderId).subscribe(res => {
      this.invoice = res;
      res.createdDate ? this.invoiceDate = this.formatDate(res.createdDate) : this.invoiceDate = null;
      res.dueDate ? this.dueDate = this.formatDate(res.dueDate) : this.dueDate = null;
      this.invoice.invoiceItems.forEach(x => {
        this.totalAmount = this.totalAmount + x.price;
        this.subTotal += x.price;
      })
      }, error => {
    });
  }

  // getInvoice(invoiceId: number){ 
  //   this.invoicesService.getInvoice(invoiceId).subscribe(invoice => {
  //     this.invoice = invoice;
  //   })
  // }

  updateInvoiceDate() {
    this.invoicesService.updateInvoiceDate(this.invoice.id, this.invoiceDate).subscribe(res => {
      this.toastr.success("Invoice date updated");
    }, error => {
      this.toastr.error("An error occured!");
    });
  }

  updateDueDate() {
    this.invoicesService.updateDueDate(this.invoice.id, this.dueDate).subscribe(res => {
      this.toastr.success("Due date updated");
    }, error => {
      this.toastr.error("An error occured!");
    });
  }

  initializeAddInvoiceItemForm(){
    this.addInvoiceItemForm = this.fb.group({
      description: ['', Validators.required],
      quantity: [null, Validators.required],
      price: [null, Validators.required]
    })
  }

  addInvoiceItem(){
    this.invoicesService.addInvoiceItem(this.invoice.id, this.addInvoiceItemForm.value).subscribe(res => {
      location.reload();
    }, error => {
      this.validationErrors = error;
    });
  }

  getWorkOrder(){
    this.workordersService.getWorkOrder(this.route.snapshot.params['workorderId']).subscribe(workorder => {
      this.workorder = workorder;
    })
  }

  private formatDate(date: Date): string {
    return date.toString().split('T')[0];
  }

}
