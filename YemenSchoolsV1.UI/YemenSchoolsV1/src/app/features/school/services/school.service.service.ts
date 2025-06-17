import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponse } from '../../../shared/model/api-response';
import { SchoolModel, SchoolPaginationModel } from '../models/school.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SchoolServiceService {

  constructor(private httpClint: HttpClient) { }
  private apiUrl = "https://localhost:7220/api/School"



  getSchools(params: HttpParams): Observable<ApiResponse<SchoolModel[]>> {

    return this.httpClint.get<ApiResponse<SchoolModel[]>>(this.apiUrl, { params });
  }
}
