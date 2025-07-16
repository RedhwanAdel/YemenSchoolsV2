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

  getRegions() {
    return this.http.get<Pagination<Region>>(this.baseUrl + 'Regions').subscribe({
      next: res => this.regions.set(res.data)
    })
  }

  getRegionsByCity(cityId: string) {
    return this.http.get<Pagination<Region>>(this.baseUrl + 'Regions/GetAllRegionsByCityID/' + cityId).pipe(
      map(res => {
        this.regions.set(res.data);
        return res
      })
    )
  }


  createRegion(region: any) {
    return this.http.post<string>(this.baseUrl + 'Regions', region);
  }

  updateRegion(id: string, region: any) {
    region.id = id
    return this.http.put(this.baseUrl + 'Regions', region);
  }

  deleteRegion(id: string) {
    return this.http.delete(this.baseUrl + 'Regions/' + id);
  }
}
