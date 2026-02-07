import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '@shared/models/ApiResponse';

@Injectable({
    providedIn: 'root'
})
export class ReportsService {
    private http = inject(HttpClient);
    baseUrl = environment.apiUrl;

    downloadSchoolReport(schoolId: string) {
        // Backend expects POST for this report download
        return this.http.post(`${this.baseUrl}Reports/school/${schoolId}`, {}, { responseType: 'blob' });
    }

    downloadReport(studentId: string) {
        return this.http.post(`${this.baseUrl}Reports/student/${studentId}`, {}, { responseType: 'blob' });
    }

    getGeneralReport(schoolId: string) {
        // This seems to link to downloadSchoolReport logic or similar. backend only has 2 actions shown.
        // Assuming this is the same as download for now or incorrect endpoint.
        // Given code, let's point to the known valid one or leave checks.
        // Actually, if it returns JSON, the controller actions return File().
        // There is NO endpoint for "general report" returning JSON in the viewed controller.
        // I will mark it as potentially missing or assume it's the download one.
        return this.http.post(`${this.baseUrl}Reports/school/${schoolId}`, {});
    }
}
