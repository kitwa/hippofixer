import { Component } from '@angular/core';
import { Issue } from 'src/app/_models/issue';
import { Pagination } from 'src/app/_models/pagination';
import { IssuesService } from 'src/app/_services/issues.service';

@Component({
  selector: 'app-issue-lists',
  templateUrl: './issue-lists.component.html',
  styleUrl: './issue-lists.component.css'
})
export class IssueListsComponent {

  issues: Issue[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 10;


  constructor(private issuesService: IssuesService) { }

  ngOnInit(): void {
    this.loadIssues();
  }

  loadIssues(){
    this.issuesService.getIssues(this.pageNumber, this.pageSize).subscribe(response => {
      this.issues = response.result;
      this.pagination = response.pagination;

    })
  }
  pageChanged(event: any){
    this.pageNumber = event.page;
    this.loadIssues();
  }

}