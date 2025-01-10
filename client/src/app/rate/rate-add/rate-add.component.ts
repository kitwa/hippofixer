import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RateService } from 'src/app/_services/rate.service';

@Component({
  selector: 'app-rate-add',
  templateUrl: './rate-add.component.html',
  styleUrl: './rate-add.component.css'
})
export class RateAddComponent {
  addRateForm: FormGroup;
  rate = 0; // Current selected rate
  stars = [1, 2, 3, 4, 5]; // Star array
  validationErrors: string[] = [];

  constructor(private fb: FormBuilder, private ratesService: RateService) {}

  ngOnInit() {
    this.initializeForm();
  }

  initializeForm() {
    this.addRateForm = this.fb.group({
      comment: ['', Validators.required],
      rate: [this.rate, Validators.required]
    });
  }

  setRating(value: number): void {
    this.rate = value;
    this.addRateForm.patchValue({ rate: this.rate });
  }

  addRate() {
    if (this.addRateForm.valid) {
      debugger
      this.ratesService.addRate(this.addRateForm.value).subscribe(
        res => {
          console.log('Rating submitted successfully:', res);
          alert('Thank you for your feedback!');
        },
        error => {
          console.error('Error submitting rate:', error);
          this.validationErrors = error;
        }
      );
    } else {
      console.error('Form is invalid');
    }
  }
}
