import { inject, Injectable } from '@angular/core';
import { map } from 'rxjs';
import { CreateSectionSubjectDto, SectionSubject, SectionSubjectUpdateDto } from '../models/section-subject';
import { environment } from '../../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '@shared/models/ApiResponse';

import { SectionSubjectInfoDto } from '../../section/models/section';

@Injectable({
    providedIn: 'root'
})
export class SectionSubjectService {
    private http = inject(HttpClient);
    baseUrl = environment.apiUrl;

    getSubjectsBySection(sectionId: string) {
        return this.http.get<ApiResponse<SectionSubjectInfoDto[]>>(`${this.baseUrl}SectionSubjects/by-section/${sectionId}`).pipe(
            map(res => res.data)
        );
    }

    delete(id: string) {
        return this.http.delete<ApiResponse<string>>(`${this.baseUrl}SectionSubjects/${id}`).pipe(
            map(res => res.data)
        );
    }

    create(data: CreateSectionSubjectDto) {
        return this.http.post<ApiResponse<string>>(`${this.baseUrl}SectionSubjects`, data).pipe(
            map(res => res.data)
        );
    }

    update(id: string, data: SectionSubjectUpdateDto) {
        return this.http.put<ApiResponse<string>>(`${this.baseUrl}SectionSubjects/${id}`, data).pipe(
            map(res => res.data)
        );
    }

    // Add more methods if needed based on typical patterns
}
