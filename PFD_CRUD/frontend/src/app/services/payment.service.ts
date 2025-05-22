import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Payment {
  id: number;
  userId: number;
  paymentDate: string;
  amount: number | null;
  paid: boolean;
}

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  private readonly apiUrl = '/api';

  constructor(private http: HttpClient) {}

  getPersonalPayments(): Observable<Payment[]> {
    return this.http.get<Payment[]>(`${this.apiUrl}/user/payments`);
  }

  getUserPayments(userId: number): Observable<Payment[]> {
    return this.http.get<Payment[]>(`${this.apiUrl}/users/${userId}/payments`);
  }

  createPayment(payment: Payment): Observable<Payment> {
    return this.http.post<Payment>(`${this.apiUrl}/payments`, payment);
  }

  updatePayment(payment: Payment): Observable<Payment> {
    return this.http.put<Payment>(`${this.apiUrl}/payments/${payment.id}`, payment);
  }

  deletePayment(paymentId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/payments/${paymentId}`);
  }
}