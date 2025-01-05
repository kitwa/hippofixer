import { Component, TemplateRef, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { City } from 'src/app/_models/city';
import { Issue } from 'src/app/_models/issue';
import { IssueType } from 'src/app/_models/issueType';
import { IssuesService } from 'src/app/_services/issues.service';

@Component({
  selector: 'app-issue-add-noaccount',
  templateUrl: './issue-add-noaccount.component.html',
  styleUrls: ['./issue-add-noaccount.component.css']
})
export class IssueAddNoaccountComponent {

  addIssueForm: UntypedFormGroup;
  validationErrors: string[] = [];
  issue: Issue;
  issueTypes: IssueType[];
  selectedFile: File = null;
  cities: City[];
  isModalShown = false;
  @ViewChild('autoShownModal', { static: false }) autoShownModal?: ModalDirective;

  constructor(
    private issuesService: IssuesService, 
    private toastr: ToastrService, 
    private fb: UntypedFormBuilder, 
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getIssueTypes();
    this.getCities();
  }

  initializeForm() {
    this.addIssueForm = this.fb.group({
      issueTypeId: [null, Validators.required],
      description: ['', Validators.required],
      cityId: [1, Validators.required],
      statusId: [1, Validators.required],
      emergency: [false, Validators.required],
      clientPhone: [
        null,
        [
          Validators.required,
          Validators.pattern('^[0-9]{10}$') // Pattern for a 10-digit phone number
        ]
      ],
      clientEmail: [
        '',
        [
          Validators.required,
          Validators.email // Validator for email format
        ]
      ],
      clientName: ['', Validators.required],
      cityName: ['', Validators.required]
    });

    // Listen for changes to cityName and update cityId
    this.addIssueForm.get('cityName')?.valueChanges.subscribe(value => {
      const selectedCity = this.cities.find(city => city.name === value);
      this.addIssueForm.patchValue({ cityId: selectedCity ? selectedCity.id : null });
    });
  }


  addIssue() {
    this.issuesService.addIssue(this.addIssueForm.value).subscribe(
      res => {
        if (this.selectedFile != null) {
          this.uploadPhoto(res.id);
        }
        this.showModal();
      },
      error => {
        this.validationErrors = error;
      }
    );
  }

  getIssueTypes() {
    this.issuesService.getIssueTypes().subscribe(issueTypes => {
      this.issueTypes = issueTypes;
    });
  }

  onFileSelected(event: Event) {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      this.selectedFile = fileInput.files[0];
    }
  }

  uploadPhoto(issueId: number) {
    this.issuesService.uploadPhoto(issueId, this.selectedFile).subscribe(() => {
      this.toastr.success("Saved successfully");
    });
  }

  getCities() {
    this.issuesService.getCities().subscribe(cities => {
      this.cities = cities;
      this.initializeForm();
    });
  }

  showModal(): void {
    this.isModalShown = true;
  }

  hideModal(): void {
    this.autoShownModal?.hide();
    this.router.navigateByUrl('/');
  }

  onHidden(): void {
    this.isModalShown = false;
  }
}
