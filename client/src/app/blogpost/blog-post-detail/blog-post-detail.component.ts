import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs';
import { BlogPost } from 'src/app/_models/blogPost';
import { Pagination } from 'src/app/_models/pagination';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { BlogPostService } from 'src/app/_services/blog-post.service';

@Component({
  selector: 'app-blog-post-detail',
  templateUrl: './blog-post-detail.component.html',
  styleUrl: './blog-post-detail.component.css'
})
export class BlogPostDetailComponent {

  user: User;
  blogPost: BlogPost;
  blogPosts: BlogPost[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 5;
  
  constructor(private blogPostService: BlogPostService, private accountService: AccountService, private route: ActivatedRoute) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

   ngOnInit(): void {
    this.loadBlogPost();
    this.getBlogPosts();
  }
   
  loadBlogPost(){ 
    this.blogPostService.getBlogPost(this.route.snapshot.params['id']).subscribe(blogPost => {
      this.blogPost = blogPost;
    })
  }

  getBlogPosts(){
    this.blogPostService.getBlogPosts(this.pageNumber, this.pageSize).subscribe(response => {
      this.blogPosts = response.result;
      this.pagination = response.pagination;
    })
  }

  formatContent(content: string): string {
    return content.split('\n').map(paragraph => `<p>${paragraph}</p>`).join('');
  }
}
