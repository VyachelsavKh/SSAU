import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PersonalData } from './personal-data.service'
import { IdentityDocument } from './identity-document.service'
import { Address } from './address.service'

export interface User {
  id: number;
  login: string;
  description: PersonalData | null;
  document: IdentityDocument | null;
  address: Address | null;
}

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private readonly apiUrl = '/api/users';

  constructor(private http: HttpClient) {}

  getUser(userId: Number) : Observable<User> {
    return this.http.get<User>(this.apiUrl + '/' + userId);
  }
  
  getUsers() : Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  setRole(userId: number, roleName: string): Observable<any> {
    const url = `${this.apiUrl}/${userId}/roles`;
    const body = { name: roleName };
    return this.http.put(url, body);
  }

  getRoles(userId: number): Observable<string[]> {
    const url = `${this.apiUrl}/${userId}/roles`;

    return this.http.get<string[]>(url);
  }
}
