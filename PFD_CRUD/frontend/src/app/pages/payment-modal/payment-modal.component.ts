import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Payment } from '../../services/payment.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-payment-modal',
  templateUrl: './payment-modal.component.html',
  styleUrls: ['./payment-modal.component.css'],
  imports: [
    FormsModule,
    CommonModule
  ],
})
export class PaymentModalComponent {
  @Input() payment!: Payment;
  @Output() close = new EventEmitter<void>();
  @Output() save = new EventEmitter<Payment>();

  originalPayment: Payment = this.payment;

  ngOnChanges(): void {
    if (this.payment) {
      this.originalPayment = { ...this.payment };
    }
  }

  restorePayment(): void {
    if (this.originalPayment) {
      this.payment = { ...this.originalPayment };
    }
  }

  savePayment(): void {
    if (this.payment) {
      this.save.emit(this.payment);
    }
  }
}