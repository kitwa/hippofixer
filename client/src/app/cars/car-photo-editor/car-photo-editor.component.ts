import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs';
import { Car } from 'src/app/_models/car';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { CarsService } from 'src/app/_services/cars.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-car-photo-editor',
  templateUrl: './car-photo-editor.component.html',
  styleUrl: './car-photo-editor.component.css'
})
export class CarPhotoEditorComponent {

  @Input() car: Car;
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  user: User;

  constructor(private accountService: AccountService, private carsService: CarsService, private route: ActivatedRoute) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.initializeUploader();
  }


  fileOverBase(e: any){
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'cars/' + this.car.id + '/add-photo',
      authToken: 'Bearer ' + this.user.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if(response) {
        const photo = JSON.parse(response);
        this.car.carPhotos.push(photo);
      }
    }
  }

  setMainPhoto(photo : Photo) {
    this.carsService.setMainPhoto(this.route.snapshot.params['id'], photo.id).subscribe(() => {
      this.car.carPhotos.forEach(p => {
        if(p.isMain) p.isMain = false;
        if(p.id === photo.id) p.isMain = true;
      })
    })
  }

  deletePhoto(photoId : Number) {
    this.carsService.deletePhoto(this.route.snapshot.params['id'], photoId).subscribe(() => {
      this.car.carPhotos = this.car.carPhotos.filter(x => x.id !== photoId);
    })
  }

}

