import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserRolesService {
  private apiUrl = 'api/users';

  constructor(private http: HttpClient) {}

  setRole(userId: number, roleName: string): Observable<any> {
    const url = `${this.apiUrl}/${userId}/setRole`;
    const body = { name: roleName };
    return this.http.put(url, body);
  }
}