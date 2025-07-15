import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { City } from '../../shared/models/city/city';
import { Pagination } from '../../shared/models/Pagination';
import { Region } from '../../shared/models/region/region';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegionsService {

  baseUrl = environment.apiUrl;
  private http = inject(HttpClient)
  regions = signal<Region[]>([]);

  getRegionsByCity(cityId: string) {
    return this.http.get<Pagination<Region>>(this.baseUrl + 'Regions/GetAllRegionsByCityID/' + cityId).pipe(
      map(res => {
        this.regions.set(res.data);
        return res
      })
    )
  }
}
