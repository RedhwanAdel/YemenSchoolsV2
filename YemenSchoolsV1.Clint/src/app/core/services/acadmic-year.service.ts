import { inject, Injectable, signal, WritableSignal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination } from '../../shared/models/Pagination';
import { AccountService } from './account.service';
import { ApiResponse } from '../../shared/models/ApiResponse';
import { CreateYearDto, YearDto } from '../../shared/models/AcademicYear/AcademicYear';
import { catchError, map, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AcadmicYearService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient)
  accountService = inject(AccountService)
  years = signal<YearDto[]>([])
  currentAcademicYearId: WritableSignal<string | null> = signal(null);

  private schoolId = this.accountService.currentUser()?.schoolId
  setSchoolId(schoolId: string) {
    this.schoolId = schoolId
  }
  getSchoolId() {
    return this.schoolId
  }
  getAcademicYears() {

    return this.http.get<ApiResponse<YearDto[]>>(this.baseUrl + 'AcademicYears/' + this.schoolId).pipe(
      map(res => {
        this.years.set(res.data)
        return res
      })
    )
  }
  GetCurrentYearId() {

    return this.http.get<ApiResponse<string>>(this.baseUrl + 'AcademicYears/' + this.schoolId + '/current-year-id').pipe(
      tap(response => {
        if (response.data && response.data) {
          this.currentAcademicYearId.set(response.data);
          localStorage.setItem('currentAcademicYearId', response.data);
          console.log(`Current academic year fetched from API: ${response.data}`);
        } else {
          console.warn('No current academic year found for this school via API.');
          this.currentAcademicYearId.set(null);
          localStorage.removeItem('currentAcademicYearId');
        }
      }),
      catchError(error => {
        console.error('Error fetching current academic year from API:', error);
        this.currentAcademicYearId.set(null);
        localStorage.removeItem('currentAcademicYearId');
        return throwError(() => new Error('Failed to fetch current academic year from API.'));
      })
    );
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
  SetCurrentYear(academicYearId: string) {
    return this.http.put<ApiResponse<string>>(this.baseUrl + 'AcademicYears/' + this.schoolId + '/set-current/' + academicYearId, academicYearId);
  }

  deleteAcademicYear(id: string) {
    return this.http.delete(this.baseUrl + 'AcademicYears/' + id);
  }
}
