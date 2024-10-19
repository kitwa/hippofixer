import { Component, Input } from '@angular/core';
import { Car } from 'src/app/_models/car';
import { Photo } from 'src/app/_models/photo';

@Component({
  selector: 'app-car-card',
  templateUrl: './car-card.component.html',
  styleUrl: './car-card.component.css'
})

export class CarCardComponent {

  @Input() car : Car;
  mainPhoto: Photo;

  constructor() { }

  ngOnInit(): void {
    this.mainPhoto = this.car.carPhotos.find(x => x.isMain);
  }

}

