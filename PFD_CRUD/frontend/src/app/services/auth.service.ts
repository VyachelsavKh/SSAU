import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

interface AuthRequest {
  login: string;
  password: string;
}

interface AuthResponse {
  roles: string[];
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly apiUrl = '/api';
  private static readonly storageKey = 'auth_data';

  constructor(private http: HttpClient) {}

  login(credentials: AuthRequest): Observable<AuthResponse> {
    return new Observable((observer) => {
      this.http.post<AuthResponse>(`${this.apiUrl}/login`, credentials).subscribe({
        next: (res) => {
          this.saveCredentials(credentials.login, credentials.password, res.roles);
          observer.next(res);
          observer.complete();
        },
        error: (err) => observer.error(err),
      });
    });
  }

  register(credentials: AuthRequest): Observable<AuthResponse> {
    return new Observable((observer) => {
      this.http.post<AuthResponse>(`${this.apiUrl}/register`, credentials).subscribe({
        next: (res) => {
          this.saveCredentials(credentials.login, credentials.password, res.roles);
          observer.next(res);
          observer.complete();
        },
        error: (err) => observer.error(err),
      });
    });
  }

  private saveCredentials(login: string, password: string, roles: string[]): void {
    const data = { login, password, roles };
    localStorage.setItem(AuthService.storageKey, JSON.stringify(data));
  }

  getCredentials(): { login: string; password: string; roles: string[] } | null {
    const raw = localStorage.getItem(AuthService.storageKey);
    if (!raw) return null;
    try {
      return JSON.parse(raw);
    } catch {
      return null;
    }
  }

  getRoles(): string[] {
    return this.getCredentials()?.roles ?? [];
  }

  getAuthorizationHeader(): string | null {
    const creds = this.getCredentials();
    if (!creds) return null;
    const token = btoa(`${creds.login}:${creds.password}`);
    return `Basic ${token}`;
  }

  logout(): void {
    localStorage.removeItem(AuthService.storageKey);
  }
}