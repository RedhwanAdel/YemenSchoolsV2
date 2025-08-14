import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { CreateStudentDto, Student, StudentList } from '../../shared/models/student/student';
import { AccountService } from './account.service';
import { AcadmicYearService } from './acadmic-year.service';
import { ApiResponse } from '../../shared/models/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  // قم بتغيير هذا الرابط ليتوافق مع رابط الـ API الخاص بك
  private apiUrl = environment.apiUrl;

  private http = inject(HttpClient)
  accountService = inject(AccountService)
  yearService = inject(AcadmicYearService)
  createStudent(studentData: any) {

    return this.http.post(this.apiUrl + 'Student', studentData);
  }


  getStudentProfile(id: string) {
    return this.http.get(`${this.apiUrl}/${id}`);
  }


  removeParentFromStudent(studentId: string, parentId: string) {
    return this.http.delete(`${this.apiUrl}/${studentId}/parents/${parentId}`);
  }

  GetStudentsByYearAndSection(yearId: string, sectionId: string) {
    let params = new HttpParams();

    if (yearId) {
      params = params.append('academicYearId', yearId);
    }
    if (sectionId) {
      params = params.append('sectionId', sectionId);
    }
    return this.http.get<ApiResponse<StudentList[]>>(`${this.apiUrl}Student/by-academic-year-and-section`, { params });

  }
}
