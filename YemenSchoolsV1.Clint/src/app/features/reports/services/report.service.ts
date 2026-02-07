import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { SchoolReportData } from '@features/schools/models/school';
import { ApiResponse } from '@shared/models/ApiResponse';

@Injectable({
    providedIn: 'root'
})
export class ReportService {
    private http = inject(HttpClient);
    private baseUrl = environment.apiUrl;

    GetSchoolReport(schoolId: string) {
        return this.http.post<ApiResponse<SchoolReportData>>(`${this.baseUrl}Reports/school/${schoolId}`, {});
    }

    downloadSchoolReport(schoolId: string) {
        return this.http.post(`${this.baseUrl}Reports/school/${schoolId}`, {}, { responseType: 'blob' });
    }

    getTeacherReport() {
        // Placeholder if needed
    }
}
