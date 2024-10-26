import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Client } from 'src/app/_models/client';
import { Issue } from 'src/app/_models/issue';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { IssuesService } from 'src/app/_services/issues.service';

@Component({
  selector: 'app-issue-detail',
  templateUrl: './issue-detail.component.html',
  styleUrl: './issue-detail.component.css'
})
export class IssueDetailComponent {

  issue: Issue;
  client: Client;
  user: User;

  constructor(private issuesService: IssuesService, private route: ActivatedRoute, 
      private accountService: AccountService, private router: Router) {;
   }

  ngOnInit(): void {
    this.loadIssue();
  }


  loadIssue(){
    this.issuesService.getIssue(this.route.snapshot.params['id']).subscribe(issue => {
      this.issue = issue;
    })
  }

  acceptIsse(){
    this.issuesService.acceptIssue(this.route.snapshot.params['id']).subscribe(issue => {
      this.router.navigateByUrl('/workorders');
    })
  }
  shareIssueDetails() {
    if (navigator.share) {
      navigator.share({
        title: `Découvrez ce bien!`,
        text: `Check this job : ${this.issue.description}`,
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
