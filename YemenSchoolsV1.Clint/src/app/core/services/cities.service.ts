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
    return this.http.get<Pagination<City>>(this.baseUrl + 'Cities').subscribe({
      next: res => this.cities.set(res.data)
    })
  }
  createCity(city: any) {
    return this.http.post<string>(this.baseUrl + 'Cities', city);
  }

  updateCity(id: string, city: any) {
    city.id = id
    return this.http.put(this.baseUrl + 'Cities', city);
  }

  deleteCity(id: string) {
    return this.http.delete(this.baseUrl + 'Cities/' + id);
  }
}
