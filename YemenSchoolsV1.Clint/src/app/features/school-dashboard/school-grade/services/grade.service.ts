import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '@shared/models/ApiResponse';

@Injectable({
    providedIn: 'root'
})
export class GradeService {
    private http = inject(HttpClient);
    baseUrl = environment.apiUrl;

    // This is often covered by SchoolService for SchoolGrades, but assuming a standalone if used.
    getGrades() {
        return this.http.get<ApiResponse<any[]>>(`${this.baseUrl}Grades`);
    }
}
