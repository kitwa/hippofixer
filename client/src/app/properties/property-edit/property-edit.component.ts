import { Component, HostListener, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, NgForm, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Property } from 'src/app/_models/property';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { PropertiesService } from 'src/app/_services/properties.service';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PropertyType } from 'src/app/_models/propertyType';
import { City } from 'src/app/_models/city';
import { Country } from 'src/app/_models/country';

@Component({
  selector: 'app-property-edit',
  templateUrl: './property-edit.component.html',
  styleUrls: ['./property-edit.component.css']
})
export class PropertyEditComponent implements OnInit {
  // @ViewChild('editForm') editForm: NgForm;
  property: Property;
  editForm: UntypedFormGroup;
  validationErrors: string[] = [];
  user: User;
  modalRef?: BsModalRef;
  message?: string;
  propertyId = this.route.snapshot.params['id'];
  propertyTypes: PropertyType[];
  cities: City[];
  countries: Country[];

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  }

  constructor(private acountService: AccountService, private propertyService: PropertiesService, 
    private toastr: ToastrService, private fb: UntypedFormBuilder, private route: ActivatedRoute, private router: Router, private modalService: BsModalService) {
      this.acountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

    ngOnInit(): void {
      this.loadProperty();
      this.getCities();
      this.getPropertyTypes();
    }
  
    initializeForm(){
      this.editForm = this.fb.group({
        price: ['', Validators.required],
        bathRooms: ['', Validators.required],
        bedRooms: ['', Validators.required],
        garage: ['', Validators.required],
        propertyTypeId: [null, Validators.required],
        description: ['', Validators.required],
        cityId: [null, Validators.required],
        countryId: [null, Validators.required],
        youtubeLink: ['', Validators.nullValidator],
        appUserId: ['', Validators.required]
  
      })
    }

  loadProperty(){ 
    this.propertyService.getProperty(this.route.snapshot.params['id']).subscribe(property => {
      this.property = property;
      this.initializeForm();
    })
  }

  updateProperty(){
    this.propertyService.updateProperty(this.route.snapshot.params['id'], this.editForm.value).subscribe(() => {
      this.toastr.success("Changes saved successfully");
      // this.editForm.reset(this.property);
    })
  }

  getPropertyTypes(){
    this.propertyService.getPropertyTypes().subscribe(propertyTypes => {
      this.propertyTypes = propertyTypes;
    })
  }

  getCountries(){
    this.propertyService.getCountries().subscribe(countries => {
      this.countries = countries;
    })
  }

  getCities(){
    this.propertyService.getCities().subscribe(cities => {
      this.cities = cities;
    })
  }

  openDeleteModal(deleteProperty: TemplateRef<void>) {
    this.modalRef = this.modalService.show(deleteProperty, { class: 'modal-sm' });
  }

  markAsSoldModal(markAsSoldProperty: TemplateRef<void>) {
    this.modalRef = this.modalService.show(markAsSoldProperty, { class: 'modal-sm' });
  }
 
  confirmDeleteProperty(): void {
    this.propertyService.deleteProperty(this.propertyId).subscribe(() => {
      this.modalRef?.hide();
      this.router.navigateByUrl('/properties');
      this.toastr.success("Property successfully deleted.");
    })

  }
 
  declineDeleteProperty(): void {
    this.modalRef?.hide();
  }

  confirmMarkAsSoldProperty(): void {
    this.propertyService.maskAsSoldProperty(this.propertyId).subscribe(() => {
      this.modalRef?.hide();
      this.router.navigateByUrl('/properties/' + this.propertyId);
      this.toastr.success("Property successfully mark as sold.");
    })

  }
 
  declineMarkAsSoldProperty(): void {
    this.modalRef?.hide();
  }

}
