import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../../shared/models/user';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = "https://localhost:7220/api/";
  private http = inject(HttpClient)
  currentUser = signal<User | null>(null)

  login(value: any) {
    let params = new HttpParams();
    params = params.append('useCookies', true);
    return this.http.post<User>(this.baseUrl + 'login', value, { params })
  }

  register(value: any) {

    return this.http.post<User>(this.baseUrl + 'Account/register', value)
  }
  getUserInfo() {
    return this.http.get<User>(this.baseUrl + 'Account/user-info').pipe(
      map(user => {
        this.currentUser.set(user)
        return user
      })
    )
  }

  logout() {
    return this.http.post(this.baseUrl + 'account/logout', {})
  }
}
