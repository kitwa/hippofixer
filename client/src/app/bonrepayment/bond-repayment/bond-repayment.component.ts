import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BondRepaymentService } from 'src/app/_services/bond-repayment.service';

@Component({
  selector: 'app-bond-repayment',
  // standalone: true,
  // imports: [RouterModule, CommonModule],
  templateUrl: './bond-repayment.component.html',
  styleUrl: './bond-repayment.component.css'
})
export class BondRepaymentComponent  implements OnInit {
  bondForm: UntypedFormGroup;
  monthlyRepayment: number | null = null;

  constructor(
    private fb: UntypedFormBuilder,
    private bondRepaymentService: BondRepaymentService
  ) {
    this.bondForm = this.fb.group({
      principal: [null, [Validators.required, Validators.min(0)]],
      deposit: [null, [Validators.required, Validators.min(0)]],
      annualRate: [null, [Validators.required, Validators.min(0), Validators.max(100)]],
      years: [null, [Validators.required, Validators.min(1)]],
    });
  }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  onSubmit() {
    if (this.bondForm.valid) {
      const { principal, deposit, annualRate, years } = this.bondForm.value;
      const netPrincipal = principal - deposit;
      this.monthlyRepayment = this.bondRepaymentService.calculateRepayment(netPrincipal, annualRate, years);
    }
  }
}
