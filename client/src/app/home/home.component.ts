import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { take } from 'rxjs';
import { User } from '../_models/user';
import { MessagingService } from '../_services/messaging.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  user: User;

  constructor(private accountService: AccountService, private messagingService: MessagingService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user)
  }

  fullText = 'Find Your Contractors and Fixe Your Issue, Just a Call Away!';
  displayedText = '';
  typingSpeed = 100; // Adjust speed in milliseconds
  isTyping = true;  

  ngOnInit(): void {
    this.typeWriterEffect();
    this.messagingService.requestPermission();
    this.messagingService.receiveMessage();
  }

  typeWriterEffect() {
    let index = 0;

    const typeWriterInterval = setInterval(() => {
      if (index < this.fullText.length) {
        this.displayedText += this.fullText.charAt(index);
        index++;
      } else {
        clearInterval(typeWriterInterval);
        this.isTyping = false; // Set to false when typing is complete
      }
    }, this.typingSpeed);
  }

}
