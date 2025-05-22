import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface PersonalData {
  id?: number;
  lastName: string;
  firstName: string;
  middleName: string;
  birthDate: string;
  gender?: 'MALE' | 'FEMALE';
}

@Injectable({
  providedIn: 'root',
})
export class PersonalDataService {
  private readonly apiUrl = '/api/user/description';

  constructor(private http: HttpClient) {}

  getPersonalData(): Observable<PersonalData> {
    return this.http.get<PersonalData>(this.apiUrl);
  }

  updatePersonalData(data: PersonalData): Observable<any> {
    return this.http.put(this.apiUrl, data);
  }
}
