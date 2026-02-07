import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '@shared/models/ApiResponse';

@Injectable({
    providedIn: 'root'
})
export class DailyLogService {
    private http = inject(HttpClient);

    createDailyLog(log: any) {
        return this.http.post(`${this.baseUrl}DailyLog`, log);
    }

    getStudentDailyLogsForDay(studentId: string, date: Date) {
        const dateStr = date.toISOString();
        return this.http.get<any[]>(`${this.baseUrl}DailyLog/student/${studentId}/daily`, { params: { date: dateStr } });
    }
    baseUrl = environment.apiUrl;

    getLogs(schoolId: string) {
        // FIXME: Endpoint 'DailyLog/school/{schoolId}' does not exist on backend.
        return this.http.get<ApiResponse<any[]>>(`${this.baseUrl}DailyLog/school/${schoolId}`);
    }

    createLog(log: any) {
        return this.http.post(`${this.baseUrl}DailyLog`, log);
    }

    deleteLog(id: string) {
        return this.http.delete(`${this.baseUrl}DailyLog/${id}`);
    }
}
