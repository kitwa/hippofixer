import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { MessageApp } from 'src/app/_models/message';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  member: Member;
  galleryImages: Photo[];
  user: User;
  messages: MessageApp[] = [];

  constructor(private accountService: AccountService, private memberService: MembersService, 
      private messageService: MessageService, private route: ActivatedRoute) { 
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }


  ngOnInit(): void {
    this.loadMember();
  }

  loadMessages() {
    this.messageService.getMessageThread(this.user.email).subscribe(messages => {
      this.messages = messages;
      console.log(messages);
    })
  }

  // getImages(): Photo[]{
  //   const imageUrls: any = [];
  //   for(const photo of this.member.photos){
  //     imageUrls.push(photo.url);
  //   }

  //   return imageUrls;
  // }

  loadMember(){
    this.memberService.getMember(this.route.snapshot.params['email']).subscribe(member => {
      this.member = member;
      // this.galleryImages = this.getImages();
    })
  }

}
