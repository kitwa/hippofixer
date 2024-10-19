import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BondRepaymentService {

  constructor() { }

  calculateRepayment(principal: number, annualRate: number, years: number): number {
    const monthlyRate = annualRate / 12 / 100;
    const numberOfPayments = years * 12;
    return (principal * monthlyRate) / (1 - Math.pow(1 + monthlyRate, -numberOfPayments));
  }
}
