import { inject, Injectable } from '@angular/core';
import { map } from 'rxjs';
import { CreateSubjectDto, UpdateSubjectDto } from '../models/subject';
import { environment } from '../../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '@shared/models/ApiResponse';
import { Subject } from '../models/subject';

@Injectable({
    providedIn: 'root'
})
export class SubjectService {
    private http = inject(HttpClient);
    baseUrl = environment.apiUrl;

    getSubjects(schoolId: string) {
        // Backend 'GetAll' likely uses user context or needs adjustment.
        // Assuming 'Subjects' root GET is sufficient or correct based on current controller.
        return this.http.get<ApiResponse<Subject[]>>(`${this.baseUrl}Subjects`).pipe(
            map(res => res.data)
        );
    }

    createSubject(subject: CreateSubjectDto) {
        return this.http.post<ApiResponse<string>>(`${this.baseUrl}Subjects`, subject).pipe(
            map(res => res.data)
        );
    }

    updateSubject(id: string, subject: UpdateSubjectDto) {
        subject.id = id;
        return this.http.put<ApiResponse<string>>(`${this.baseUrl}Subjects`, subject).pipe(
            map(res => res.data)
        );
    }

    deleteSubject(id: string) {
        return this.http.delete<ApiResponse<string>>(`${this.baseUrl}Subjects/${id}`).pipe(
            map(res => res.data)
        );
    }
}
