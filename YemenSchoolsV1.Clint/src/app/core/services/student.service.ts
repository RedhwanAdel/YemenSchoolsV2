import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { CreateStudentDto } from '../../shared/models/student/student';
import { AccountService } from './account.service';
import { AcadmicYearService } from './acadmic-year.service';

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

  /**
   * يجلب ملف الطالب مع تفاصيل أولياء أموره.
   * @param id معرف الطالب (Guid).
   */
  getStudentProfile(id: string) {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  /**
   * يقوم بتحديث ملف الطالب.
   * @param id معرف الطالب (Guid).
   * @param dto البيانات المراد تحديثها.
   */
  // updateStudentProfile(id: string, dto: StudentUpdateDto):  {
  //   return this.http.put(`${this.apiUrl}/${id}`, dto);
  // }

  /**
   * يضيف ولي أمر إلى طالب موجود.
   * @param studentId معرف الطالب.
   * @param parentId معرف ولي الأمر.
   * @param relationType نوع العلاقة (مثال: 'أب', 'أم').
   */
  // addParentToStudent(studentId: string, parentId: string, relationType: string): Observable<any> {
  //   const params = new HttpParams().set('relationType', relationType);
  //   return this.http.post(`${this.apiUrl}/${studentId}/parents/${parentId}`, null, { params });
  // }

  /**
   * يزيل ولي أمر من طالب.
   * @param studentId معرف الطالب.
   * @param parentId معرف ولي الأمر.
   */
  removeParentFromStudent(studentId: string, parentId: string) {
    return this.http.delete(`${this.apiUrl}/${studentId}/parents/${parentId}`);
  }
}
