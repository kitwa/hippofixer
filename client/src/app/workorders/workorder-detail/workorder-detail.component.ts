import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Client } from 'src/app/_models/client';
import { User } from 'src/app/_models/user';
import { WorkOrder } from 'src/app/_models/workorder';
import { WorkOrdersService } from 'src/app/_services/workorders.service';

@Component({
  selector: 'app-workorder-detail',
  templateUrl: './workorder-detail.component.html',
  styleUrl: './workorder-detail.component.css'
})
export class WorkorderDetailComponent {

  workorder: WorkOrder;
  client: Client;
  user: User;

  constructor(private workorderssService: WorkOrdersService, private route: ActivatedRoute, private router: Router, private toastr: ToastrService) {;
   }

  ngOnInit(): void {
    this.getWorkOrder();
  }


  getWorkOrder(){
    this.workorderssService.getWorkOrder(this.route.snapshot.params['id']).subscribe(workorder => {
      this.workorder = workorder;
    })
  }

  completeWorkOrder(){
    this.workorderssService.completeWorkOrder(this.route.snapshot.params['id']).subscribe(workorders => {
      this.toastr.success("This job has been completed", "Workorder Completed");
      window.location.reload();
    })
  }

  rejectWorkOrder(){
    this.workorderssService.rejectWorkOrder(this.route.snapshot.params['id']).subscribe(workorders => {
      this.toastr.warning("Workorder has been rejected", "Workorder Rejected");
      this.router.navigateByUrl('/issues');
    })
  }

  shareIssueDetails() {
    if (navigator.share) {
      navigator.share({
        title: `Workorder Details!`,
        text: `Check this job : ${this.workorder.issue.description}`,
        url: window.location.href,
      }).then(() => {
        console.log('Thanks for sharing!');
      }).catch((error) => {
        console.error('Error sharing', error);
      });
    } 
  }

  getFullUrl(): string {
    const currentUrl = this.router.url; // Get the current URL
    return `${window.location.origin}${currentUrl}`; // Combine with the origin
  }

}