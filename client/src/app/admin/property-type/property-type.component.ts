import { Component, OnInit, TemplateRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PropertyType } from 'src/app/_models/propertyType';
import { AdminService } from 'src/app/_services/admin.service';
import { PropertiesService } from 'src/app/_services/properties.service';

@Component({
  selector: 'app-property-type',
  templateUrl: './property-type.component.html',
  styleUrls: ['./property-type.component.css']
})
export class PropertyTypeComponent implements OnInit {
  
  propertyTypes: PropertyType[];
  addPropertyTypeForm: UntypedFormGroup;
  modalRef?: BsModalRef;
  
  constructor(private propertyService: PropertiesService, private adminService: AdminService, private fb: UntypedFormBuilder,
    private modalService: BsModalService) { }

  ngOnInit(): void {
    this.getPropertyTypes();
    this.initializeForm();
  }

  initializeForm(){
    this.addPropertyTypeForm = this.fb.group({
      name: ['', Validators.required],

    })
  }

  getPropertyTypes(){
    this.propertyService.getPropertyTypes().subscribe(propertyTypes => {
      this.propertyTypes = propertyTypes;
    })
  }

  deletePropertyType(id: number) {
    this.adminService.deletePropertyType(id).subscribe(() => {
        this.propertyTypes.splice(this.propertyTypes.findIndex(m => m.id === id), 1);
    })
  }

  addPropertyType(){
    this.adminService.addPropertyType(this.addPropertyTypeForm.value).subscribe(response => {
      this.modalRef?.hide();
      this.getPropertyTypes();
    }, error => {
    });
  }

  openAddPropertyTypeModal(addCityModal: TemplateRef<void>) {
    this.modalRef = this.modalService.show(addCityModal, { class: 'modal-sm' });
  }

}