import { Component, OnInit } from '@angular/core';
import { Pagination } from 'src/app/_models/pagination';
import { WorkOrder } from 'src/app/_models/workorder';
import { WorkOrdersService } from 'src/app/_services/workorders.service';

@Component({
  selector: 'app-workorder-lists',
  templateUrl: './workorder-lists.component.html',
  styleUrls: ['./workorder-lists.component.css']
})
export class WorkorderListsComponent implements OnInit {
  workorders: WorkOrder[] = [];
  pagination: Pagination;
  hasInProgressWorkorders: Boolean = false;
  hasCompletedWorkorders: Boolean = false;
  hasRejectedWorkorders: Boolean = false;
  inProgressWorkorders: WorkOrder[] = [];
  completedWorkorders: WorkOrder[] = [];
  rejectedWorkorders: WorkOrder[] = [];
  pageNumber = 1;
  pageSize = 10;

  constructor(private workOrdersService: WorkOrdersService) {}

  ngOnInit(): void {
    this.loadWorkorders();
  }

  loadWorkorders(): void {
    this.workOrdersService.getWorkOrders(this.pageNumber, this.pageSize).subscribe((response) => {
      this.workorders = response.result;
      this.pagination = response.pagination;

      this.inProgressWorkorders = this.workorders.filter(w => w.status.identifier === 'InProgress');
      this.completedWorkorders = this.workorders.filter(w => w.status.identifier === 'Completed');
      this.rejectedWorkorders = this.workorders.filter(w => w.status.identifier === 'Rejected');

      this.hasInProgressWorkorders = this.inProgressWorkorders.length > 0;
      this.hasCompletedWorkorders = this.completedWorkorders.length > 0;
      this.hasRejectedWorkorders = this.rejectedWorkorders.length > 0;
    });
  }

  pageChanged(event: any): void {
    this.pageNumber = event.page;
    this.loadWorkorders();
  }
}
