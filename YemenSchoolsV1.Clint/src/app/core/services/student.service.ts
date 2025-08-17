import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { CreateStudentDto, StudentList } from '../../shared/models/student/student';
import { AccountService } from './account.service';
import { AcadmicYearService } from './acadmic-year.service';
import { ApiResponse } from '../../shared/models/ApiResponse';
import { Student } from '../../shared/models/mark/mark';
import { map } from 'rxjs';

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

  // لجلب الطلاب في شعبة معينة
  getStudentsBySectionId(sectionId: string) {
    return this.http.get<ApiResponse<Student[]>>(`${this.apiUrl}Student/by-section/${sectionId}`).pipe(
      map(res => res.data)
    );
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
