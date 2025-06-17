import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../../shared/model/api-response';
import { RegionModel } from '../models/region.model';

@Injectable({
  providedIn: 'root'
})
export class RegionService {
  private apiUrl = "https://localhost:7220/api/Regions"
  constructor(private httpClint: HttpClient) { }
  getAllRegions(): Observable<ApiResponse<RegionModel[]>> {
    return this.httpClint.get<ApiResponse<RegionModel[]>>(this.apiUrl);
  }
}


