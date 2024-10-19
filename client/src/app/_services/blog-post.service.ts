import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { BlogPost } from '../_models/blogPost';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  baseUrl = environment.apiUrl;
  blogPosts: BlogPost[] = [];
  paginatedResult: PaginatedResult<BlogPost[]> = new PaginatedResult<BlogPost[]>();

  constructor(private http: HttpClient ) {

  } 

  getBlogPosts(page? : number, itemsPerPage?: number) {

    let params = new HttpParams();

    if(page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('PageSize', itemsPerPage.toString());
    }

    return this.http.get<BlogPost[]>(this.baseUrl + 'blogPosts', {observe: 'response', params}).pipe(

      map(response => {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedResult;
      })
    );
  }

  getBlogPost(id: number) {
    const Blog = this.blogPosts.find(x => x.id === id);
    if(Blog !== undefined) return of(Blog);
    return this.http.get<BlogPost>(this.baseUrl + 'blogPosts/' + id);
  }

  addBlogPost(Blog: BlogPost) {
    return this.http.post<BlogPost>(this.baseUrl + 'blogPosts', Blog);
  }

  updateBlogPost(blogPostId: number, blogPost: BlogPost) {
    return this.http.put(this.baseUrl + 'blogPosts/' + blogPostId, blogPost).pipe(
      map(() => {
        const index = this.blogPosts.indexOf(blogPost);
        this.blogPosts[index] = blogPost;
      })
    );
  }

  uploadImageBlogPost(blogPostId: number, image: File) {
    const formData = new FormData();
    formData.append('file', image);
    return this.http.post<BlogPost>(this.baseUrl + 'blogPosts/' + blogPostId + '/add-image', formData);
  }

  deleteBlogPost(blogPostId: Number, photoId: Number) {
    return this.http.delete(this.baseUrl + 'blogPosts/' + blogPostId + '/delete');
  }
}
