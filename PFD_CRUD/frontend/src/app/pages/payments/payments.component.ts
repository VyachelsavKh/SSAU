import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PaymentService, Payment } from '../../services/payment.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PaymentModalComponent } from '../payment-modal/payment-modal.component'

@Component({
  selector: 'app-payments',
  imports: [
    FormsModule,
    CommonModule,
    PaymentModalComponent
  ],
  templateUrl: './payments.component.html',
  styleUrls: ['./payments.component.css']
})
export class PaymentsComponent implements OnInit {
  userId!: number;
  payments: Payment[] = [];
  selectedPayment: Payment = { id: 0, userId: this.userId, paymentDate: '', amount: null, paid: false };
  showModal = false;

  constructor(
    private paymentService: PaymentService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.userId = +params['id'];
      this.loadPayments();
    });
  }

  loadPayments(): void {
    this.paymentService.getUserPayments(this.userId).subscribe(payments => {
      this.payments = payments;
    });
  }

  openAddPaymentModal(): void {
    this.selectedPayment = { id: 0, userId: this.userId, paymentDate: '', amount: null, paid: false };
    this.showModal = true;
  }

  openEditPaymentModal(payment: Payment): void {
    this.selectedPayment = { ...payment };
    this.showModal = true;
  }

  deletePayment(paymentId: number): void {
    this.paymentService.deletePayment(paymentId).subscribe(() => {
      this.loadPayments();
      alert('Выплата удалена');
    });
  }

  closeModal(): void {
    this.showModal = false;
  }

  savePayment(payment: Payment): void {
    if (payment.id === 0) {
      this.paymentService.createPayment(payment).subscribe(() => {
        this.loadPayments();
        this.closeModal();
      });
    } else {
      this.paymentService.updatePayment(payment).subscribe(() => {
        this.loadPayments();
        this.closeModal();
      });
    }
  }

  restorePayment(): void {
    if (this.selectedPayment) {
      this.selectedPayment = { ...this.selectedPayment };
    }
  }
  
  sortField: keyof Payment = 'paymentDate';
  sortDirection: 'asc' | 'desc' = 'desc';
  
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
