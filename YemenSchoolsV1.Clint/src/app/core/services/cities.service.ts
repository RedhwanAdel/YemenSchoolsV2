import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CitiesService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient)

  getCites() {
    return this.http.get<any>(this.baseUrl + 'cities', { withCredentials: true })
  }
}
