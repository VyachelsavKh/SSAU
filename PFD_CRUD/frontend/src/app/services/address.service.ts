import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Address {
  houseNumber: number;
  cityId: number;
  streetId: number;
  apartmentNumber: number;
}

@Injectable({
  providedIn: 'root',
})
export class AddressService {
  private readonly apiUrl = '/api/user/address';

  constructor(private http: HttpClient) {}

  getAddress(): Observable<Address> {
    return this.http.get<Address>(this.apiUrl);
  }

  updateAddress(data: Address): Observable<any> {
    return this.http.put(this.apiUrl, data);
  }
}
