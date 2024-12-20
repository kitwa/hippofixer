import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { City } from 'src/app/_models/city';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  editMemberForm: UntypedFormGroup;
  @ViewChild('editForm') editForm: NgForm;
  member: Member;
  user: User;
  selectedFile: File = null;
  cities: City[];
  memberCityName: string;

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  }

  constructor(private acountService: AccountService, private memberService: MembersService, private fb: UntypedFormBuilder, 
    private toastr: ToastrService, private route: ActivatedRoute) {
    this.acountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
   }

  ngOnInit(): void {
    this.loadMember();
  }

  initializeForm(){
    this.editMemberForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.nullValidator],
      cityId: [1, Validators.nullValidator],
      genderId: [1, Validators.nullValidator],
      fullAddress: ['', Validators.nullValidator],
      facebook: ['', Validators.nullValidator],
      instagram: ['', Validators.nullValidator],
      twitter: ['', Validators.nullValidator],
      youtube: ['', Validators.nullValidator],
      phone: [
        null,
        [
          Validators.required,
          Validators.pattern('^[0-9]{10}$') // Pattern for a 10-digit phone number
        ]
      ],
      cityName: ['', Validators.nullValidator]
    });

    this.editMemberForm.get('cityName')?.valueChanges.subscribe(value => {
      const selectedCity = this.cities.find(city => city.name === value);
      this.editMemberForm.patchValue({ cityId: selectedCity ? selectedCity.id : null });
    });
  }

  onFileSelected(event: Event) {
    const fileInput = event.target as HTMLInputElement;
    if (fileInput.files && fileInput.files.length > 0) {
      this.selectedFile = fileInput.files[0];
      this.memberService.uploadProfilePhoto(this.user.id, this.selectedFile).subscribe(blogPost => {
        this.toastr.success("saved successfully");
        window.location.reload();
      });
    }
  }

  loadMember(){
    this.memberService.getMember(this.user.email).subscribe(member => {
      this.member = member;
      this.getCities();
    })
  }

  updateMember(){

    this.memberService.updateMember(this.member).subscribe(() => {
      this.toastr.success("Changes saved successfully");
      this.editForm.reset(this.member);
    })

  }

  editMember() {
    this.memberService.updateMember(this.editMemberForm.value).subscribe(() => {
      this.toastr.success("Changes saved successfully");
    });
  }

  getCities() {
    this.memberService.getCities().subscribe(cities => {
      this.cities = cities;
      this.memberCityName = this.cities.find(x => x.id == this.member.cityId)?.name;
      this.initializeForm();
    });
  }

}
