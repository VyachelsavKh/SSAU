import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface IdentityDocument {
  id?: number;
  documentTypeId: number;
  series: string;
  number: string;
  issueDate?: string;
  issuedBy?: string;
}

@Injectable({
  providedIn: 'root',
})
export class IdentityDocumentService {
  private readonly apiUrl = '/api/user/document';

  constructor(private http: HttpClient) {}

  getDocument(): Observable<IdentityDocument> {
    return this.http.get<IdentityDocument>(this.apiUrl);
  }

  updateDocument(data: IdentityDocument): Observable<any> {
    return this.http.put(this.apiUrl, data);
  }
}
