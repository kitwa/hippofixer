import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { take } from 'rxjs';
import { Car } from 'src/app/_models/car';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { CarsService } from 'src/app/_services/cars.service';

@Component({
  standalone: true,
  selector: 'app-car-detail',
  templateUrl: './car-detail.component.html',
  styleUrl: './car-detail.component.css',
  imports: [GalleryModule, RouterModule, CommonModule]
})
export class CarDetailComponent {
  car: Car;
  member: Member;
  user: User;
  youtubeVideo: SafeResourceUrl;
  images: GalleryItem[] = [];

  constructor(private carsService: CarsService, private route: ActivatedRoute, 
      private accountService: AccountService, private sanitizer: DomSanitizer, private router: Router ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  ngOnInit(): void {
    this.loadCar();
  }

  getImages(): GalleryItem[]{
    const imageUrls: any = [];
    for(const photo of this.car.carPhotos){
      this.images.push( new ImageItem({src: photo.url, thumb: photo.url}));
    }

    return imageUrls;
  }

  loadCar(){
    
    this.carsService.getCar(this.route.snapshot.params['id']).subscribe(res => {
      debugger
      this.car = res;
      this.youtubeVideo = this.sanitizer.bypassSecurityTrustResourceUrl(res.youtubeLink);
      this.getImages();
      this.member = this.car.appUser;
    })
  }

  shareCarDetails() {
    if (navigator.share) {
      navigator.share({
        title: `Découvrez cette voiture!`,
        text: `Jetez un œil à cette voiture : ${this.car.description}`,
        url: window.location.href,
      }).then(() => {
        console.log('Thanks for sharing!');
      }).catch((error) => {
        console.error('Error sharing', error);
      });
    } 
  }

  getFullUrl(): string {
    const currentUrl = this.router.url; // Get the current URL
    return `${window.location.origin}${currentUrl}`; // Combine with the origin
  }
}