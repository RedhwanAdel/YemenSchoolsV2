import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { ChangePasswordDto, UpdateParentProfileDto, User } from '../../shared/models/user';
import { map, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ApiResponse } from '../../shared/models/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient)
  currentUser = signal<User | null>(null)
  isUserLoggedIn() {
    if (this.currentUser()) {
      return true
    }
    return false
  }
  login(value: any) {
    let params = new HttpParams();
    params = params.append('useCookies', true);
    return this.http.post<User>(this.baseUrl + 'Account/login', value, { params })
  }

  register(value: any) {

    return this.http.post<User>(this.baseUrl + 'Account/register', value)
  }
  getUserInfo() {
    return this.http.get<ApiResponse<User>>(this.baseUrl + 'Account/user-info').pipe(
      map(user => {
        this.currentUser.set(user.data)
        return user
      })
    )
  }
  changePassword(model: ChangePasswordDto): Observable<any> {
    return this.http.post(`${this.baseUrl}Account/change-password`, model);
  }
  logout() {
    return this.http.post(this.baseUrl + 'account/logout', {})
  }

  updateProfile(model: UpdateParentProfileDto): Observable<any> {
    return this.http.put(`${this.baseUrl}account/update-profile`, model);
  }
  getProfile() {
    return this.http.get<UpdateParentProfileDto>(`${this.baseUrl}account/profile`, { withCredentials: true });
  }


}
