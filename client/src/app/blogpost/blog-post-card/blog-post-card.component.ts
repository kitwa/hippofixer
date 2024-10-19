import { Component, Input } from '@angular/core';
import { take } from 'rxjs';
import { BlogPost } from 'src/app/_models/blogPost';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-blog-post-card',
  templateUrl: './blog-post-card.component.html',
  styleUrl: './blog-post-card.component.css'
})
export class BlogPostCardComponent {
  user: User;
  @Input() blogPost : BlogPost;

  constructor(private acountService: AccountService) {
    this.acountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }
}
