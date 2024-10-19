import { Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  user: User;
  deferredPrompt: any;
  showInstallButton: boolean = false;

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.checkIfAppCanBeInstalled();
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

    // Listen for the beforeinstallprompt event
    @HostListener('window:beforeinstallprompt', ['$event'])
    onBeforeInstallPrompt(event: Event) {
      event.preventDefault();
      this.deferredPrompt = event;
      this.showInstallButton = true; // Show the install button
    }
  
    // Method to handle the install button click
    installPWA() {
      if (this.deferredPrompt) {
        this.deferredPrompt.prompt();
        this.deferredPrompt.userChoice.then((choiceResult: { outcome: string }) => {
          if (choiceResult.outcome === 'accepted') {
            console.log('User accepted the install prompt');
          } else {
            console.log('User dismissed the install prompt');
          }
          this.deferredPrompt = null; // Reset the deferredPrompt
          this.showInstallButton = false; // Hide the install button
        });
      }
    }
  
    // Check if app is already installed
    checkIfAppCanBeInstalled() {
      if ((window.matchMedia && window.matchMedia('(display-mode: standalone)').matches) || window.navigator['standalone'] === true) {
        this.showInstallButton = false; // App is already installed
      }
    }

}
