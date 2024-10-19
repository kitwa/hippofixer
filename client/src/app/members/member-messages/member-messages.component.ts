import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MessageApp } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
  @ViewChild('messageForm') messageForm: NgForm;
  @Input() email: string;
  @Input() messages: MessageApp[];
  messageContent: string;
  loading = false;

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
    this.getMessageThread();
  }

  getMessageThread() {
    this.loading = true;
    this.messageService.getMessageThread(this.email).subscribe(messages => {
      this.messages = messages;
      this.loading = false;
    })
  }

  sendMessage() {
    this.messageService.sendMessage(this.email, this.messageContent).subscribe(message => {
      this.messages.push(message);
      this.messageForm.reset();
    })
  }

}
