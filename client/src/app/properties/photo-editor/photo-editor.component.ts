import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs/operators';
import { Photo } from 'src/app/_models/photo';
import { Property } from 'src/app/_models/property';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { PropertiesService } from 'src/app/_services/properties.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() property: Property;
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  user: User;

  constructor(private accountService: AccountService, private propertyService: PropertiesService, private route: ActivatedRoute) { 
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
      url: this.baseUrl + 'properties/' + this.property.id + '/add-photo',
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
        this.property.photos.push(photo);
      }
    }
  }

  setMainPhoto(photo : Photo) {
    this.propertyService.setMainPhoto(this.route.snapshot.params['id'], photo.id).subscribe(() => {
      this.property.photos.forEach(p => {
        if(p.isMain) p.isMain = false;
        if(p.id === photo.id) p.isMain = true;
      })
    })
  }

  deletePhoto(photoId : Number) {
    this.propertyService.deletePhoto(this.route.snapshot.params['id'], photoId).subscribe(() => {
      this.property.photos = this.property.photos.filter(x => x.id !== photoId);
    })
  }

}
