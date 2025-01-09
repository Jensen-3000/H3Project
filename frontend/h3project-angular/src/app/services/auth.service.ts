import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { Endpoints } from '../models/endpoints';
import { UserLogin } from '../models/user-login';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  protected baseUrl: string = environment.apiBaseUrl;
  protected endpoint: Endpoints = Endpoints.Auth;

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/${this.endpoint}/token`, { username, password }).pipe(
      tap((response: any) => {
        if (response.token) {
          localStorage.setItem('token', response.token);
        }
      }),
    );
  }

  logout() {
    localStorage.removeItem('token');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
}
