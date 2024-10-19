import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { BlogPost } from 'src/app/_models/blogPost';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { BlogPostService } from 'src/app/_services/blog-post.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-blog-post-edit',
  templateUrl: './blog-post-edit.component.html',
  styleUrl: './blog-post-edit.component.css'
})
export class BlogPostEditComponent {
  blogPost: BlogPost;
  editBlogPostForm: UntypedFormGroup;
  validationErrors: string[] = [];
  user: User;
  acountService: any;
  baseUrl = environment.apiUrl;
  selectedFile: File = null;

  constructor(private blogPostService: BlogPostService,     
    private toastr: ToastrService, private fb: UntypedFormBuilder, private router: Router, private accountService: AccountService,private route: ActivatedRoute) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

   ngOnInit(): void {
    this.loadBlogPost();
    this.initializeForm();
  }

  initializeForm(){
    this.editBlogPostForm = this.fb.group({
      title: ['', Validators.required],
      content: ['', Validators.required],
      appUserId: ['', Validators.required]

    })
  }

  onFileSelected(event: Event) {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      this.selectedFile = fileInput.files[0];
      this.blogPostService.uploadImageBlogPost(this.route.snapshot.params['id'], this.selectedFile).subscribe(blogPost => {
        this.toastr.success("saved successfully");
        window.location.reload();
      });
    }
  }

  
  loadBlogPost(){ 
    this.blogPostService.getBlogPost(this.route.snapshot.params['id']).subscribe(blogPost => {
      this.blogPost = blogPost;
      this.initializeForm();
    })
  }

  updateBlogPost(){
    this.blogPostService.updateBlogPost(this.route.snapshot.params['id'], this.editBlogPostForm.value).subscribe(blogPost => {
      this.toastr.success("updated successfully");
      this.router.navigateByUrl('/blogpost/edit/' + this.route.snapshot.params['id']);
    }, error => {
      this.validationErrors = error;
    });
  }
}
