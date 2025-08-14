import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateAttendanceRequest, UpdateAttendanceRequest } from '../../shared/models/student/student';

@Injectable({
  providedIn: 'root'
})
export class AttendanceService {
  private apiUrl = `${environment.apiUrl}attendance`;

  constructor(private http: HttpClient) { }

  // لإنشاء سجل حضور يومي
  createDailyAttendance(request: CreateAttendanceRequest) {
    return this.http.post<any>(`${this.apiUrl}/daily`, request);
  }

  // لتحديث سجل حضور قائم
  updateDailyAttendance(attendanceId: string, request: UpdateAttendanceRequest): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/daily`, request);
  }


  // لجلب تقرير حضور طالب معين
  getStudentAttendanceReport(studentId: string) {
    // نفترض وجود نقطة نهاية لجلب تقرير الطالب
    return this.http.get<any>(`${this.apiUrl}student-report/${studentId}`);
  }
}
