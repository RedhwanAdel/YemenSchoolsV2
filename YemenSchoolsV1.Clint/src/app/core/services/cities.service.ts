import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { City } from '../../shared/models/city/city';
import { Pagination } from '../../shared/models/Pagination';

@Injectable({
  providedIn: 'root'
})
export class CitiesService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient)
  cities = signal<City[]>([]);

  getCites() {
    return this.http.get<Pagination<City>>(this.baseUrl + 'cities').subscribe({
      next: res => this.cities.set(res.data)
    })
  }
  createCity(city: any) {
    return this.http.post<string>(this.baseUrl + 'cities', city);
  }

  updateCity(id: string, city: any) {
    city.id = id
    return this.http.put(this.baseUrl + 'cities', city);
  }

  deleteCity(id: string) {
    return this.http.delete(this.baseUrl + 'cities/' + id);
  }
}
