import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { BlogPost } from 'src/app/_models/blogPost';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { BlogPostService } from 'src/app/_services/blog-post.service';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-blog-post-add',
  templateUrl: './blog-post-add.component.html',
  styleUrl: './blog-post-add.component.css'
})
export class BlogPostAddComponent {
  blogPost: BlogPost;
  addBlogPostForm: UntypedFormGroup;
  validationErrors: string[] = [];
  user: User;
  acountService: any;
  baseUrl = environment.apiUrl;
  selectedFile: File = null;

  constructor(private blogPostService: BlogPostService,     
    private toastr: ToastrService, private fb: UntypedFormBuilder, private router: Router, private accountService: AccountService,) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

   ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.addBlogPostForm = this.fb.group({
      title: ['', Validators.required],
      content: ['', Validators.required],
      appUserId: ['', Validators.required]

    })
  }

  onFileSelected(event: Event) {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      this.selectedFile = fileInput.files[0];
    }
  }

  addBlogPost(){
    this.blogPostService.addBlogPost(this.addBlogPostForm.value).subscribe(blogPost => {
      this.toastr.success("saved successfully");
      this.router.navigateByUrl('/blogpost/edit/' + blogPost.id);
    }, error => {
      this.validationErrors = error;
    });

  }
}
