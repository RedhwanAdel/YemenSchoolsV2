import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { YearDto, CreateYearDto } from '../../shared/models/AcademicYear/AcademicYear';
import { ApiResponse } from '../../shared/models/ApiResponse';
import { AccountService } from './account.service';
import { Teacher } from '../../shared/models/teachers/teacher';

@Injectable({
  providedIn: 'root'
})
export class TeacherService {

  baseUrl = environment.apiUrl;
  private http = inject(HttpClient)
  accountService = inject(AccountService)

  private schoolId = this.accountService.currentUser()?.schoolId

  getTeachers(schoolId: string) {

    return this.http.get<ApiResponse<Teacher[]>>(this.baseUrl + 'Teachers/GeAlltBySchoolId/' + this.schoolId)
  }

  getTeacherById(id: string) {
    return this.http.get<ApiResponse<Teacher>>(this.baseUrl + 'Teachers/GetTeacherById/' + id).pipe(
      map(res => res.data)
    )
  }

  createTeacher(teacher: any) {
    if (this.schoolId) {

      teacher.schoolId = this.schoolId
    }
    return this.http.post<string>(this.baseUrl + 'Teachers', teacher);
  }

  updateTeacher(id: string, teacher: any) {
    teacher.id = id
    if (this.schoolId) {

      teacher.schoolId = this.schoolId
    }
    return this.http.put(this.baseUrl + 'Teachers', teacher);
  }

  deleteTeacher(id: string) {
    return this.http.delete(this.baseUrl + 'Teachers/' + id);
  }
}
