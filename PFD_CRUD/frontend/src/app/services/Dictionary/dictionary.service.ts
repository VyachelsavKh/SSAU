import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface DictionaryDTO {
  id?: number;
  name: string;
}

export abstract class DictionaryService<T> {
  constructor(
    protected http: HttpClient,
    protected apiUrl: string
  ) {}

  create(item: T): Observable<any> {
    return this.http.post(this.apiUrl, item);
  }

  getAll(search?: string): Observable<any[]> {
    const params = search ? { params: { search } } : {};
    return this.http.get<any[]>(this.apiUrl, params);
  }

  get(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  update(id: number, item: T): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, item);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  deleteCascade(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}/cascade`);
  }
}