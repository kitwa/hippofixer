import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Invoice } from 'src/app/_models/invoice';
import { WorkOrder } from 'src/app/_models/workorder';
import { InvoicesService } from 'src/app/_services/invoices.service';
import { WorkOrdersService } from 'src/app/_services/workorders.service';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';

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
    this.workorderId = this.route.snapshot.params['workorderId'];
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

  downloadInvoice() {
    const element = document.querySelector('.container');
  
    if (element) {
      // Hide elements you don't want to include
      const removeOnDownload = document.querySelector('.toRemoveOnDownload');
      if (removeOnDownload) removeOnDownload.setAttribute('style', 'display: none !important');
  
      html2canvas(document.querySelector('.container')).then(canvas => {
        // Restore hidden elements
        if (removeOnDownload) removeOnDownload.removeAttribute('style');
  
        const imgData = canvas.toDataURL('image/png');
        const pdf = new jsPDF('p', 'mm', 'a4');
        const imgProps = pdf.getImageProperties(imgData);
        const pdfWidth = pdf.internal.pageSize.getWidth();
        
        // Add padding (e.g., 10mm)
        const padding = 10;
        const contentWidth = pdfWidth - 2 * padding;
        const contentHeight = (imgProps.height * contentWidth) / imgProps.width;

        // Adjust the image to include padding
        pdf.addImage(imgData, 'PNG', padding, padding, contentWidth, contentHeight);
        pdf.save(`Invoice_${this.workorder?.id || 'unknown'}.pdf`);
      }).catch(err => {
        // Ensure hidden elements are restored in case of an error
        if (removeOnDownload) removeOnDownload.removeAttribute('style');
        console.error('Error generating PDF:', err);
      });
    }
  }

  sendEmailInvoice() {
    const element = document.querySelector('.container');
  
    if (element) {
      // Hide elements you don't want to include
      const removeOnDownload = document.querySelector('.toRemoveOnDownload');
      if (removeOnDownload) removeOnDownload.setAttribute('style', 'display: none !important');
  
      html2canvas(document.querySelector('.container')).then(canvas => {
        // Restore hidden elements
        if (removeOnDownload) removeOnDownload.removeAttribute('style');
  
        const imgData = canvas.toDataURL('image/png');
        const pdf = new jsPDF('p', 'mm', 'a4');
        const imgProps = pdf.getImageProperties(imgData);
        const pdfWidth = pdf.internal.pageSize.getWidth();
        
        const padding = 10;
        const contentWidth = pdfWidth - 2 * padding;
        const contentHeight = (imgProps.height * contentWidth) / imgProps.width;
  
        pdf.addImage(imgData, 'PNG', padding, padding, contentWidth, contentHeight);
  
        const pdfBlob = pdf.output('blob'); // Convert PDF to Blob

        const fileName =  `Invoice_${this.invoice?.id || 'unknown'}.pdf`;
        const clientEmail = this.workorder.issue.client.email;
        const invoiceLink =  window.location.href;
  
        this.invoicesService.sendInvoiceEmail(pdfBlob, fileName, clientEmail, invoiceLink, this.invoice).subscribe(response => {
          this.toastr.success("Invoice Sent");
        }, error => {
          console.error('Error emailing invoice:', error);
        });
      });
    }
  }
  
  

  private formatDate(date: Date): string {
    return date.toString().split('T')[0];
  }

}
