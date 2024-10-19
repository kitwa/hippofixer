import { Component } from '@angular/core';
import { BlogPost } from 'src/app/_models/blogPost';
import { Pagination } from 'src/app/_models/pagination';
import { BlogPostService } from 'src/app/_services/blog-post.service';

@Component({
  selector: 'app-blog-post-list',
  templateUrl: './blog-post-list.component.html',
  styleUrl: './blog-post-list.component.css'
})
export class BlogPostListComponent {
  blogPosts: BlogPost[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 10;


  constructor(private blogPostService: BlogPostService) { }

  ngOnInit(): void {
    this.getBlogPosts();
  }

  getBlogPosts(){
    this.blogPostService.getBlogPosts(this.pageNumber, this.pageSize).subscribe(response => {
      this.blogPosts = response.result;
      this.pagination = response.pagination;
    })
  }
  pageChanged(event: any){
    this.pageNumber = event.page;
    this.getBlogPosts();
  }

}
