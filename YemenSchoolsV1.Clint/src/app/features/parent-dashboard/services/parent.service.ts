import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '@shared/models/ApiResponse';
import { StudentWithSchoolInfoDto } from '../models/parent';

@Injectable({
    providedIn: 'root'
})
export class ParentService {
    private http = inject(HttpClient);
    baseUrl = environment.apiUrl;

    GetStudentsWithSchoolInfoForParent() {
        return this.http.get<ApiResponse<StudentWithSchoolInfoDto[]>>(`${this.baseUrl}Parents/${this.getParentId()}/students-with-school-info`);
    }

    getChildrenWithActivities(parentId: string) {
        return this.http.get<ApiResponse<any[]>>(`${this.baseUrl}Parents/children-activities/${parentId}`);
    }

    GetTeachersForParent() {
        return this.http.get<ApiResponse<any[]>>(`${this.baseUrl}Parents/teachers`);
    }

    checkParentByNationalId(nid: string) {
        return this.http.get<ApiResponse<any>>(`${this.baseUrl}Parents/check-national-id/${nid}`);
    }

    createParent(parentData: any) {
        return this.http.post<ApiResponse<any>>(`${this.baseUrl}Parents`, parentData);
    }

    // Helper to get parent ID (assuming stored or available, standardizing to a placeholder or method for now)
    private getParentId(): string {
        // Ideally this comes from a user service or state. For now keeping logic intact but fixing URLs.
        return ''; // Logic to be handled by caller or state
    }
}
