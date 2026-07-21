import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { loginRequest, RegisterRequest, TokenResponse } from '../models/auth.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'https://localhost:7220/api/Auth';

  constructor(private http: HttpClient) {}

  register(request: RegisterRequest): Observable<TokenResponse> {
    return this.http.post<TokenResponse>(`${this.apiUrl}/register`, request);
  }

  login(request: loginRequest): Observable<TokenResponse> {
    return this.http.post<TokenResponse>(`${this.apiUrl}/login`, request);
  }

  saveToken(response: TokenResponse): void {
    localStorage.setItem('token', response.token);
    localStorage.setItem('email', response.email);
    localStorage.setItem('fullName', response.fullname);
    localStorage.setItem('role', response.role);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getRole(): string | null {
    return localStorage.getItem('role');
  }

  getFullName(): string | null {
    return localStorage.getItem('fullName');
  }

  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('email');
    localStorage.removeItem('fullName');
    localStorage.removeItem('role');
  }
}
