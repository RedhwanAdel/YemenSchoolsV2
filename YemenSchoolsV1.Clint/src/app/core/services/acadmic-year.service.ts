import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AcademicYear } from '../../shared/models/AcademicYear/AcademicYear';
import { Pagination } from '../../shared/models/Pagination';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class AcadmicYearService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient)
  acadmicYears = signal<AcademicYear[]>([]);
  accountService = inject(AccountService)

  private schoolId = this.accountService.currentUser()?.schoolId

  getAcademicYears(stageId?: string) {
    let params = new HttpParams();
    if (stageId) {
      params = params.append('stageId', stageId);
    }
    return this.http.get<Pagination<AcademicYear>>(this.baseUrl + 'AcademicYears/GetAllYearsPaged/' + this.schoolId, { params }).subscribe({
      next: res => this.acadmicYears.set(res.data)
    })
  }
  createAcademicYear(academicYear: any) {
    return this.http.post<string>(this.baseUrl + 'AcademicYears', academicYear);
  }

  updateAcademicYear(id: string, academicYear: any) {
    academicYear.id = id
    return this.http.put(this.baseUrl + 'AcademicYears', academicYear);
  }

  deleteAcademicYear(id: string) {
    return this.http.delete(this.baseUrl + 'AcademicYears/' + id);
  }
}
