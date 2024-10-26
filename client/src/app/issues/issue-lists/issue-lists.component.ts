import { Component } from '@angular/core';
import { take } from 'rxjs';
import { Issue } from 'src/app/_models/issue';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { IssuesService } from 'src/app/_services/issues.service';

@Component({
  selector: 'app-issue-lists',
  templateUrl: './issue-lists.component.html',
  styleUrl: './issue-lists.component.css'
})
export class IssueListsComponent {

  user: User;
  issues: Issue[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 10;


  constructor(private accountService: AccountService, private issuesService: IssuesService) {     
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user)
  }
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