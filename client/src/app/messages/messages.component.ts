import { Component, OnInit } from '@angular/core';
import { MessageApp } from '../_models/message';
import { Pagination } from '../_models/pagination';
import { MessageService } from '../_services/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  messages: MessageApp[];
  pagination: Pagination;
  container = 'Unread';
  pageNumber = 1;
  pageSize = 10;
  loading = false;

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
    this.loadMessages(this.container)
  }

  loadMessages(type: string) {
    this.container = type;
    this.loading = true;
    this.messageService.getMessages(this.pageNumber, this.pageSize, this.container).subscribe(response =>{
      this.messages = response.result;
      this.pagination = response.pagination;
      this.loading = false;
    })
  }

  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.loadMessages(this.container);
  }

  
  deleteMessage(id: number) {
    // this.confirmService.confirm('Confirm delete message', 'This cannot be undone').subscribe(result => {
    //   if (result) {
        this.messageService.deleteMessage(id).subscribe(() => {
          this.messages.splice(this.messages.findIndex(m => m.id === id), 1);
        })
      }
  //   })

  // }

}
