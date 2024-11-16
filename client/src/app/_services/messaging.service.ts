import { Injectable } from '@angular/core';
import { AngularFireMessaging } from '@angular/fire/compat/messaging';
import { take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MessagingService {

  currentMessage = this.afMessaging.messages;

  constructor(private afMessaging: AngularFireMessaging) {}

  requestPermission() {
    this.afMessaging.requestToken
      .pipe(take(1))
      .subscribe({
        next: (token) => {
          console.log('FCM Token:', token);
          // You might want to save the token to the backend for future use
        },
        error: (error) => {
          console.error('Permission denied', error);
        }
      });
  }

  receiveMessage() {
    this.afMessaging.messages
      .pipe(take(1))
      .subscribe((message) => {
        console.log('Message received:', message);
        // Customize how you display or handle notifications here
      });
  }
}