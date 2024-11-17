import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { take } from 'rxjs';
import { User } from '../_models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  user: User;

  constructor(private accountService: AccountService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user)
  }

  fullText = 'Find Your Contractor and Fixe Your Issue, Just a Call Away!';
  displayedText = '';
  typingSpeed = 100; // Adjust speed in milliseconds
  isTyping = true;  

  ngOnInit(): void {
    this.typeWriterEffect();
  }

  typeWriterEffect() {
    let index = 0;

    const typeWriterInterval = setInterval(() => {
      if (index < this.fullText.length) {
        this.displayedText += this.fullText.charAt(index);
        index++;
      } else {
        clearInterval(typeWriterInterval);
        this.isTyping = false; 
      }
    }, this.typingSpeed);
  }

}
