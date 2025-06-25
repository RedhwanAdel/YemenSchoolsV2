import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CitiesService {
  baseUrl = "https://localhost:7220/api/";
  private http = inject(HttpClient)

  getCites() {
    return this.http.get<any>(this.baseUrl + 'cities', { withCredentials: true })
  }
}
