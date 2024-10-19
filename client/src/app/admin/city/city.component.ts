import { Component, OnInit, TemplateRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { City } from 'src/app/_models/city';
import { AdminService } from 'src/app/_services/admin.service';
import { PropertiesService } from 'src/app/_services/properties.service';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})
export class CityComponent implements OnInit {
  cities: City[];
  addCityForm: UntypedFormGroup;
  modalRef?: BsModalRef;
  constructor(private propertyService: PropertiesService, private adminService: AdminService, private fb: UntypedFormBuilder,
    private modalService: BsModalService) { }

  ngOnInit(): void {
    this.getCities();
    this.initializeForm();
  }

  initializeForm(){
    this.addCityForm = this.fb.group({
      name: ['', Validators.required],

    })
  }

  getCities(){
    this.propertyService.getCities().subscribe(cities => {
      this.cities = cities;
    })
  }

  deleteCity(id: number) {
    this.adminService.deleteCity(id).subscribe(() => {
        this.cities.splice(this.cities.findIndex(m => m.id === id), 1);
    })
  }

  addCity(){
    this.adminService.addCity(this.addCityForm.value).subscribe(response => {
      this.modalRef?.hide();
      this.getCities();
    }, error => {
    });
  }

  openAddCityModal(addCityModal: TemplateRef<void>) {
    this.modalRef = this.modalService.show(addCityModal, { class: 'modal-sm' });
  }

}
