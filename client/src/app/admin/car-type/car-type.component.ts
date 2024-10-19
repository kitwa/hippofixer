import { Component, TemplateRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CarType } from 'src/app/_models/carType';
import { AdminService } from 'src/app/_services/admin.service';
import { CarsService } from 'src/app/_services/cars.service';

@Component({
  selector: 'app-car-type',
  templateUrl: './car-type.component.html',
  styleUrl: './car-type.component.css'
})
export class CarTypeComponent {

  carTypes: CarType[];
  addCarTypeForm: UntypedFormGroup;
  modalRef?: BsModalRef;
  
  constructor(private carsService: CarsService, private adminService: AdminService, private fb: UntypedFormBuilder,
    private modalService: BsModalService) { }

  ngOnInit(): void {
    this.getCarTypes();
    this.initializeForm();
  }

  initializeForm(){
    this.addCarTypeForm = this.fb.group({
      model: ['', Validators.required],
      make: ['', Validators.required],

    })
  }

  getCarTypes(){
    this.carsService.getCarTypes().subscribe(res => {
      this.carTypes = res;
    })
  }

  deleteCarType(id: number) {
    this.adminService.deleteCarType(id).subscribe(() => {
        this.carTypes.splice(this.carTypes.findIndex(m => m.id === id), 1);
    })
  }

  addCarType(){
    this.adminService.addCarType(this.addCarTypeForm.value).subscribe(response => {
      this.modalRef?.hide();
      this.getCarTypes();
    }, error => {
    });
  }

  openAddCarTypeModal(addCityModal: TemplateRef<void>) {
    this.modalRef = this.modalService.show(addCityModal, { class: 'modal-sm' });
  }

}
