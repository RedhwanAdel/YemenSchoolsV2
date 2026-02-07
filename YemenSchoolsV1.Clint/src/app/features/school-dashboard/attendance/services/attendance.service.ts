import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '@shared/models/ApiResponse';

@Injectable({
    providedIn: 'root'
})
export class AttendanceService {
    private http = inject(HttpClient);
    baseUrl = environment.apiUrl;

    getAttendance(classId: string, date: string) {
        return this.http.get<ApiResponse<any[]>>(`${this.baseUrl}Attendance/class/${classId}?date=${date}`);
    }

    createAttendance(attendanceData: any) {
        return this.http.post(`${this.baseUrl}Attendance`, attendanceData);
    }

    getStudentAttendanceReportByDate(studentId: string, startDate: string, endDate: string) {
        return this.http.get<any[]>(`${this.baseUrl}Attendance/student/${studentId}/report?startDate=${startDate}&endDate=${endDate}`);
    }
}
