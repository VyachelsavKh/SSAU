import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaymentService, Payment } from '../../services/payment.service';

@Component({
  selector: 'app-payments',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './my-payments.component.html',
  styleUrl: './my-payments.component.css'
})
export class MyPaymentsComponent {
  payments: Payment[] = [];
  sortField: keyof Payment = 'paymentDate';
  sortDirection: 'asc' | 'desc' = 'desc';

  constructor(private paymentService: PaymentService) {
    this.loadPayments();
  }

  loadPayments() {
    this.paymentService.getPersonalPayments().subscribe({
      next: (data) => {
        this.payments = data;
        this.sortPayments();
      },
      error: (err) => console.error('Ошибка загрузки выплат', err),
    });
  }

  sortPayments() {
    this.payments.sort((a, b) => {
      const aValue = a[this.sortField];
      const bValue = b[this.sortField];

      if (aValue == null) return 1;
      if (bValue == null) return -1;

      if (aValue < bValue) return this.sortDirection === 'asc' ? -1 : 1;
      if (aValue > bValue) return this.sortDirection === 'asc' ? 1 : -1;
      return 0;
    });
  }

  toggleSort(field: keyof Payment) {
    if (this.sortField === field) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortField = field;
      this.sortDirection = 'asc';
    }
    this.sortPayments();
  }

  isSortedAsc(field: keyof Payment): boolean {
    return this.sortField === field && this.sortDirection === 'asc';
  }

  isSortedDesc(field: keyof Payment): boolean {
    return this.sortField === field && this.sortDirection === 'desc';
  }
}