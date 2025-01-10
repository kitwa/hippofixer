import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@microsoft/signalr';
import { CardsService } from 'src/app/_services/cards.service';

@Component({
  selector: 'app-card-add',
  templateUrl: './card-add.component.html',
  styleUrl: './card-add.component.css'
})
export class CardAddComponent implements OnInit {
  addCardForm: FormGroup;
  validationErrors: string[] = [];
  cardTypes: string[] = ['Visa', 'MasterCard'];

  constructor(private fb: FormBuilder, private cardsServices: CardsService) {}

  ngOnInit() {
    this.initializeForms();
  }

  initializeForms() {
    this.addCardForm = this.fb.group({
      cardHolderName: ['', Validators.required],
      cardNumber: ['', [Validators.required, Validators.pattern(/^\d{16}$/)]],
      cardType: ['', Validators.required],
      bankName: ['', Validators.nullValidator],
      expiryDate: ['', Validators.required],
      cvv: ['', [Validators.required, Validators.pattern(/^\d{3}$/)]],
      isDefault:[true, Validators.required]
    });
  }

  addCard() {
    debugger
    this.cardsServices.addCard(this.addCardForm.value).subscribe(res => {
      location.reload();
    }, error => {
      this.validationErrors = error;
    });
  }
}