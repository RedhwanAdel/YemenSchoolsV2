import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams, HttpEvent } from '@angular/common/http';
import { SchoolParams } from '@features/schools/models/schoolParams';
import { Pagination } from '@shared/models/Pagination';
import { AssignSubjectsToSchoolGradeDto, CreateSchoolGradeDto, SchoolForUpdate, SchoolGradeSubject, SchoolGradeWithDetailsDto, SchoolListItem, SchoolPhoto, SchoolReportData, StageGradeDto } from '@features/schools/models/school';
import { SchoolDetails } from '@features/schools/models/schoolDetails';
import { ApiResponse } from '@shared/models/ApiResponse';
import { CreateSchoolDto } from '@features/schools/models/schoolCommand';
import { map, Observable } from 'rxjs';
import { Subject } from '@features/school-dashboard/school-subject/models/subject';
import { AccountService } from '@core/services/account.service';

@Injectable({
  providedIn: 'root'
})
export class SchoolService {
  baseUrl = environment.apiUrl
  private http = inject(HttpClient)
  accountService = inject(AccountService)
  private schoolId = this.accountService.currentUser()?.schoolId

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
    if (schoolParams.CurriculumType)
      params = params.append('CurriculumType', schoolParams.CurriculumType.toString());

    if (schoolParams.levels) {
      params = params.append('levels', schoolParams.levels.toString());
    }
    params = params.append('pageNumber', schoolParams.pageNumber.toString());
    params = params.append('pageSize', schoolParams.pageSize.toString());
    params = params.append('sortDirection', schoolParams.sortDirection)

    return this.http.get<Pagination<SchoolListItem>>(this.baseUrl + 'School', { params })
  }
  getSchoolById(id: string) {
    return this.http.get<ApiResponse<SchoolDetails>>(this.baseUrl + 'School/' + id).pipe(
      map(response => response.data)
    )
  }

  getSchoolByIdForUpdate(id: string) {
    return this.http.get<ApiResponse<SchoolForUpdate>>(this.baseUrl + 'School/GetSchoolByIdForUpdate/' + id).pipe(
      map(response => response.data)
    )
  }
  createSchool(school: CreateSchoolDto) {
    return this.http.post<ApiResponse<string>>(this.baseUrl + 'school', school).pipe(
      map(response => response.data)
    );
  }
  updateSchoolForAdmin(id: string, schoolData: SchoolForUpdate) {
    schoolData.id = id
    return this.http.put<ApiResponse<any>>(this.baseUrl + 'School', schoolData).pipe(
      map(response => response.data)
    );
  }
  deleteSchool(id: string) {
    return this.http.delete<ApiResponse<any>>(this.baseUrl + 'school/' + id).pipe(
      map(response => response.data)
    );
  }


  // schoolGrad
  // schoolGrad
  getStageGradesForSchool(schoolId: string) {
    return this.http.get<ApiResponse<StageGradeDto[]>>(`${this.baseUrl}SchoolGrade/${schoolId}`).pipe(
      map(res => res.data)
    );
  }

  getSelectedStageGradesForSchool(schoolId: string) {
    return this.http.get<ApiResponse<StageGradeDto[]>>(`${this.baseUrl}SchoolGrade/${schoolId}`).pipe(
      map(res => res.data.filter(sg => sg.isSelected))
    );
  }

  getSchoolGrade() {

    return this.http.get<ApiResponse<SchoolGradeWithDetailsDto[]>>(`${this.baseUrl}SchoolGrade/grade/${this.schoolId}`).pipe(
      map(res => res.data)
    );
  }

  syncStageGrades(data: CreateSchoolGradeDto) {
    return this.http.post<ApiResponse<string>>(`${this.baseUrl}SchoolGrade/sync-stage-grades`, data).pipe(
      map(res => res.data)
    );
  }

  // دالة لجلب المواد المعينة لصف معين (StageGrade)
  getSubjectsForSchoolGrade(schoolGradeId: string) {
    return this.http.get<ApiResponse<Subject[]>>(this.baseUrl + 'school/' + schoolGradeId + '/subjects').pipe(
      map(res => res.data)
    );
  }

  // دالة لتعيين المواد لصف معين
  assignSubjectsToStageGrade(data: AssignSubjectsToSchoolGradeDto) {
    return this.http.post<ApiResponse<any>>(this.baseUrl + 'school/assign-grade-subjects', data).pipe(
      map(res => res.data)
    );
  }


  getSchoolReport(schoolId: string) {
    return this.http.get<ApiResponse<SchoolReportData>>(this.baseUrl + 'School/' + schoolId + '/report').pipe(
      map(res => res.data)
    );
  }


  uploadSchoolPhoto(file: File, schoolId: string): Observable<HttpEvent<SchoolPhoto>> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<SchoolPhoto>(`${this.baseUrl}school/${schoolId}/upload`, formData, {
      reportProgress: true,
      observe: 'events'
    });
  }

  getSchoolPhotos(schoolId: string): Observable<SchoolPhoto[]> {
    return this.http.get<ApiResponse<SchoolPhoto[]>>(`${this.baseUrl}school/${schoolId}/photos`).pipe(
      map(response => response.data)
    );
  }

}
