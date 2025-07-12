import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { SchoolParams } from '../../shared/models/school/schoolParams';
import { Pagination } from '../../shared/models/Pagination';
import { SchoolListItem } from '../../shared/models/school/school';
import { SchoolDetails } from '../../shared/models/school/schoolDetails';
import { ApiResponse } from '../../shared/models/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class SchoolService {
  baseUrl = environment.apiUrl
  private http = inject(HttpClient)
  getSchools(schoolParams: SchoolParams) {
    let params = new HttpParams();

    if (schoolParams.search) {
      params = params.append('search', schoolParams.search);
    }

    if (schoolParams.orderBy !== undefined && schoolParams.orderBy !== null) {
      params = params.append('orderBy', schoolParams.orderBy.toString());
    }

    if (schoolParams.cityId) {
      params = params.append('cityId', schoolParams.cityId);
    }

    if (schoolParams.regionId) {
      params = params.append('regionId', schoolParams.regionId);
    }

    if (schoolParams.gender)
      params = params.append('gender', schoolParams.gender.toString());

    if (schoolParams.type)
      params = params.append('type', schoolParams.type.toString());

    if (schoolParams.levels) {
      params = params.append('levels', schoolParams.levels.toString());
    }
    params = params.append('pageNumber', schoolParams.pageNumber.toString());
    params = params.append('pageSize', schoolParams.pageSize.toString());
    params = params.append('sortDirection', schoolParams.sortDirection)

    return this.http.get<Pagination<SchoolListItem>>(this.baseUrl + 'School', { params })
  }
  getSchoolById(id: string) {
    return this.http.get<ApiResponse<SchoolDetails>>(this.baseUrl + 'School/' + id)
  }
}
