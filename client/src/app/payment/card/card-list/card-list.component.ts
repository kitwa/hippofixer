import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-card-list',
  templateUrl: './card-list.component.html',
  styleUrl: './card-list.component.css'
})
export class CardListComponent implements OnInit {

  isModalShown = false;
  @ViewChild('autoShownModal', { static: false }) autoShownModal?: ModalDirective;


  constructor(
    private toastr: ToastrService, 
    private router: Router
  ) { }

  ngOnInit() {
  }

  showModal(): void {
    debugger
    this.isModalShown = true;
  }

  hideModal(): void {
    this.autoShownModal?.hide();
    this.router.navigateByUrl('/');
  }

  onHidden(): void {
    this.isModalShown = false;
  }

}
