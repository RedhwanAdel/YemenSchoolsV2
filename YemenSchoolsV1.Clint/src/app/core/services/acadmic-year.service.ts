import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination } from '../../shared/models/Pagination';
import { AccountService } from './account.service';
import { ApiResponse } from '../../shared/models/ApiResponse';
import { CreateYearDto, YearDto } from '../../shared/models/AcademicYear/AcademicYear';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AcadmicYearService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient)
  accountService = inject(AccountService)
  years = signal<YearDto[]>([])

  private schoolId = this.accountService.currentUser()?.schoolId

  getAcademicYears(schoolId: string) {

    return this.http.get<ApiResponse<YearDto[]>>(this.baseUrl + 'AcademicYears/' + this.schoolId).pipe(
      map(res => {
        this.years.set(res.data)
        return res
      })
    )
  }
  createAcademicYear(academicYear: CreateYearDto) {
    if (this.schoolId) {

      academicYear.schoolId = this.schoolId
    }
    return this.http.post<string>(this.baseUrl + 'AcademicYears', academicYear);
  }

  updateAcademicYear(id: string, academicYear: any) {
    academicYear.id = id
    if (this.schoolId) {

      academicYear.schoolId = this.schoolId
    }
    return this.http.put(this.baseUrl + 'AcademicYears', academicYear);
  }

  deleteAcademicYear(id: string) {
    return this.http.delete(this.baseUrl + 'AcademicYears/' + id);
  }
}
