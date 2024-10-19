import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  loadingRequestCount = 0;
  showSpinner = false;

  constructor() {}

  loading() {
    this.loadingRequestCount++;
    this.showSpinner = true;
  }

  idle() {

    this.loadingRequestCount--;
    if(this.loadingRequestCount <=0) {
      this.showSpinner = false;
    }
  }
}
