import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { SchoolParams } from '../../shared/models/school/schoolParams';
import { Pagination } from '../../shared/models/Pagination';
import { AssignSubjectsToSchoolGradeDto, CreateSchoolGradeDto, SchoolForUpdate, SchoolGradeSubject, SchoolGradeWithDetailsDto, SchoolListItem, SchoolReportData, StageGradeDto } from '../../shared/models/school/school';
import { SchoolDetails } from '../../shared/models/school/schoolDetails';
import { ApiResponse } from '../../shared/models/ApiResponse';
import { CreateSchoolDto } from '../../shared/models/school/schoolCommand';
import { map } from 'rxjs';
import { Subject } from '../../shared/models/school/subject';
import { AccountService } from './account.service';

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
    return this.http.get<ApiResponse<SchoolDetails>>(this.baseUrl + 'School/' + id)
  }

  getSchoolByIdForUpdate(id: string) {
    return this.http.get<SchoolForUpdate>(this.baseUrl + 'School/GetSchoolByIdForUpdate/' + id)
  }
  createSchool(school: any) {
    return this.http.post<string>(this.baseUrl + 'school', school);
  }
  updateSchoolForAdmin(id: string, schoolData: SchoolForUpdate) {
    schoolData.id = id
    return this.http.put(this.baseUrl + 'School', schoolData);
  }
  deleteSchool(id: string) {
    return this.http.delete(this.baseUrl + 'school/' + id);
  }


  // schoolGrad
  getStageGradesForSchool(schoolId: string) {
    return this.http.get<ApiResponse<StageGradeDto[]>>(`${this.baseUrl}SchoolGrade/${schoolId}`);
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
    return this.http.post<ApiResponse<string>>(`${this.baseUrl}SchoolGrade/sync-stage-grades`, data);
  }

  // دالة لجلب المواد المعينة لصف معين (StageGrade)
  getSubjectsForSchoolGrade(schoolGradeId: string) {
    return this.http.get<ApiResponse<Subject[]>>(this.baseUrl + 'school/' + schoolGradeId + '/subjects').pipe(
      map(res => res.data)
    );
  }

  // دالة لتعيين المواد لصف معين
  assignSubjectsToStageGrade(data: AssignSubjectsToSchoolGradeDto) {
    return this.http.post(this.baseUrl + 'school/assign-grade-subjects', data);
  }


  getSchoolReport(schoolId: string) {
    return this.http.get<ApiResponse<SchoolReportData>>(this.baseUrl + 'School/' + schoolId + '/report');
  }
}
