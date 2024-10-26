import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { City } from 'src/app/_models/city';
import { Issue } from 'src/app/_models/issue';
import { IssueType } from 'src/app/_models/issueType';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { IssuesService } from 'src/app/_services/issues.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-issue-add',
  templateUrl: './issue-add.component.html',
  styleUrl: './issue-add.component.css'
})
export class IssueAddComponent {

  addIssueForm: UntypedFormGroup;
  validationErrors: string[] = [];
  issue: Issue;
  user: User;
  issueTypes: IssueType[];
  selectedFile: File = null;
  cities: City[];

  constructor(private acountService: AccountService, private issuesService: IssuesService, 
    private toastr: ToastrService, private fb: UntypedFormBuilder, private router: Router, private memberService: MembersService) {
    this.acountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

   ngOnInit(): void {
    this.getIssueTypes();
    this.getCities();
  }

  initializeForm(){
    this.addIssueForm = this.fb.group({
      title: ['', Validators.required],
      issueTypeId: [null, Validators.required],
      description: ['', Validators.required],
      clientId: [null, Validators.required],
      cityId: [null, Validators.required],
      contractorId: [null, Validators.required],
      statusId: [1, Validators.required],
      emergency: [false, Validators.required]

    })
  }

  addIssue(){
    this.issuesService.addIssue(this.addIssueForm.value).subscribe(res => {
      if(this.selectedFile != null){
        this.uploadPhoto(res.id);
      }else{
        this.toastr.success("saved successfully");
        this.router.navigateByUrl('/issues');
      }
      // this.router.navigateByUrl('/issue/edit/' + res.id);
    }, error => {
      this.validationErrors = error;
    });

  }

  getIssueTypes(){
    this.issuesService.getIssueTypes().subscribe(issueTypes => {
      this.issueTypes = issueTypes;
    })
  }

  onFileSelected(event: Event) {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      this.selectedFile = fileInput.files[0];
    }
  }

  uploadPhoto(issueId: number){
    this.issuesService.uploadPhoto(issueId, this.selectedFile).subscribe(res => {
      this.toastr.success("saved successfully");
      this.router.navigateByUrl('/issues');
    })
  }

  getCities(){
    this.issuesService.getCities().subscribe(cities => {
      this.cities = cities;
      this.initializeForm();
    })
  }

}
