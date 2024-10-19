import { Component, HostListener, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, NgForm, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Property } from 'src/app/_models/property';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { PropertiesService } from 'src/app/_services/properties.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PropertyType } from 'src/app/_models/propertyType';
import { City } from 'src/app/_models/city';
import { Country } from 'src/app/_models/country';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-property-add',
  templateUrl: './property-add.component.html',
  styleUrls: ['./property-add.component.css']
})
export class PropertyAddComponent implements OnInit {

  addForm: UntypedFormGroup;
  validationErrors: string[] = [];
  property: Property;
  user: User;
  propertyTypes: PropertyType[];
  cities: City[];
  countries: Country[];
  members: Member[];
  // @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
  //   if(this.addForm.dirty){
  //     $event.returnValue = true;
  //   }
  // }

  constructor(private acountService: AccountService, private propertyService: PropertiesService, 
    private toastr: ToastrService, private fb: UntypedFormBuilder, private router: Router, private memberService: MembersService) {
    this.acountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

   ngOnInit(): void {
    this.initializeForm();
    this.getPropertyTypes();
    this.getCities();
    this.loadMembers();
  }

  initializeForm(){
    this.addForm = this.fb.group({
      price: ['', Validators.required],
      bathRooms: ['', Validators.required],
      bedRooms: ['', Validators.required],
      garage: ['', Validators.required],
      propertyTypeId: [null, Validators.required],
      description: ['', Validators.required],
      cityId: [null, Validators.required],
      countryId: [null, Validators.required],
      youtubeLink: ['', Validators.nullValidator],
      appUserId: ['', Validators.required],
      agentId: [null, Validators.required]

    })
  }

  addProperty(){
    this.propertyService.addProperty(this.addForm.value).subscribe(property => {
      this.toastr.success("saved successfully");
      this.router.navigateByUrl('/property/edit/' + property.id);
    }, error => {
      this.validationErrors = error;
    });

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

  getAgent(){

  }

  loadMembers(){
    this.memberService.getMembers(1, 10).subscribe(response => {
      this.members = response.result;
    })
  }
}
