import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@microsoft/signalr';

@Component({
  selector: 'app-card-add',
  templateUrl: './card-add.component.html',
  styleUrl: './card-add.component.css'
})
export class CardAddComponent implements OnInit {
  paymentForm: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.initializeForms();
  }

  initializeForms() {
    this.paymentForm = this.fb.group({
      cardHolderName: ['', Validators.required],
      cardNumber: ['', [Validators.required, Validators.pattern(/^\d{16}$/)]],
      expiryDate: ['', Validators.required],
      cvv: ['', [Validators.required, Validators.pattern(/^\d{3}$/)]],
      amount: [null, [Validators.required, Validators.min(1)]]
    });
  }


  submitPayment() {
      const paymentData = this.paymentForm.value;
      // this.http.post('/api/payments/debit-order', paymentData).subscribe(
      //     (response) => console.log('Payment processed:', response),
      //     (error) => console.error('Payment error:', error)
      // );
  }
}