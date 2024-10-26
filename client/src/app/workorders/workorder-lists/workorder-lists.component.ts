import { Component } from '@angular/core';
import { Pagination } from 'src/app/_models/pagination';
import { WorkOrder } from 'src/app/_models/workorder';
import { WorkOrdersService } from 'src/app/_services/workorders.service';

@Component({
  selector: 'app-workorder-lists',
  templateUrl: './workorder-lists.component.html',
  styleUrl: './workorder-lists.component.css'
})
export class WorkorderListsComponent {


  workorders: WorkOrder[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 10;


  constructor(private workOrdersService: WorkOrdersService) { }

  ngOnInit(): void {
    this.loadWorkorders();
  }

  loadWorkorders(){
    this.workOrdersService.getWorkOrders(this.pageNumber, this.pageSize).subscribe(response => {
      this.workorders = response.result;
      this.pagination = response.pagination;

    })
  }
  pageChanged(event: any){
    this.pageNumber = event.page;
    this.loadWorkorders();
  }

}
