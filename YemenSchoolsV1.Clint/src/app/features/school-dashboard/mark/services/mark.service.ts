import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '@shared/models/ApiResponse';
import { CreateMarksDto, SubjectReportDto } from '../models/mark';

@Injectable({
    providedIn: 'root'
})
export class MarkService {
    private http = inject(HttpClient);
    baseUrl = environment.apiUrl;

    getMarks(sectionSubjectId: string) {
        return this.http.get<ApiResponse<any[]>>(`${this.baseUrl}Mark/section-subject/${sectionSubjectId}`);
    }

    createMarks(marks: CreateMarksDto) {
        return this.http.post(`${this.baseUrl}Mark`, marks);
    }

    addMark(marks: CreateMarksDto) {
        return this.createMarks(marks);
    }

    getTeacherSectionSubjects() {
        return this.http.get<ApiResponse<any[]>>(`${this.baseUrl}SectionSubject/teacher/current`);
    }

    getStudentSubjectsReport(studentId: string) {
        return this.http.get<SubjectReportDto[]>(`${this.baseUrl}Mark/StudentSubjectsReport/${studentId}`);
    }
}
