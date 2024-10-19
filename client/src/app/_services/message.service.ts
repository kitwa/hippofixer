import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { MessageApp } from '../_models/message';
import { getPaginatedResult, getPaginationHeaders } from '../_services/paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }


  getMessages(pageNumber, pageSize, container) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('Container', container);
    return getPaginatedResult<MessageApp[]>(this.baseUrl + 'messages', params, this.http);
  }

  getMessageThread(email: string) {
    return this.http.get<MessageApp[]>(this.baseUrl + 'messages/thread/' + email);
  }


  sendMessage(email: string, content: string) {
    return this.http.post<MessageApp>(this.baseUrl + 'messages', {recipientEmail: email, content});
  }

  // async sendMessage(email: string, content: string) {
  //   return this.hubConnection.invoke('SendMessage', {recipientUsername: username, content})
  //     .catch(error => console.log(error));
  // }

  deleteMessage(id: number) {
    return this.http.delete(this.baseUrl + 'messages/' + id);
  }
}
