import { map } from 'rxjs';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination } from '@shared/models/Pagination';
import { ApiResponse } from '@shared/models/ApiResponse';
import { CreateStudentDto, PromoteStudentsDto, StudentListDto, StudentParams, StudentProfileDto, UpdateStudentProfileDto } from '../models/student';

@Injectable({
    providedIn: 'root'
})
export class StudentService {
    baseUrl = environment.apiUrl;
    private http = inject(HttpClient);

    getAllStudents(studentParams: StudentParams) {
        let params = new HttpParams();
        if (studentParams.search) params = params.append('search', studentParams.search);
        params = params.append('pageNumber', studentParams.pageNumber.toString());
        params = params.append('pageSize', studentParams.pageSize.toString());
        if (studentParams.className) params = params.append('ClassName', studentParams.className);
        if (studentParams.sectionName) params = params.append('SectionName', studentParams.sectionName);
        if (studentParams.academicYear) params = params.append('AcademicYear', studentParams.academicYear);

        return this.http.get<Pagination<StudentListDto>>(this.baseUrl + 'Student', { params });
    }

    getStudentById(id: string) {
        return this.http.get<ApiResponse<StudentProfileDto>>(this.baseUrl + 'Student/' + id).pipe(
            map(res => res.data)
        );
    }

    getStudentProfile(id: string) {
        return this.getStudentById(id);
    }

    createStudent(studentData: CreateStudentDto) {
        return this.http.post<ApiResponse<string>>(this.baseUrl + 'Student', studentData).pipe(
            map(res => res.data)
        );
    }

    updateStudent(id: string, studentData: UpdateStudentProfileDto) {
        studentData.id = id;
        return this.http.put<ApiResponse<string>>(this.baseUrl + 'Student/' + id, studentData).pipe(
            map(res => res.data)
        );
    }

    deleteStudent(id: string) {
        return this.http.delete<ApiResponse<string>>(this.baseUrl + 'Student/' + id).pipe(
            map(res => res.data)
        );
    }

    getStudentsBySchoolId(schoolId: string) {
        return this.http.get<Pagination<StudentListDto>>(this.baseUrl + 'Student/student-by-school/' + schoolId).pipe(
            // Pagination object is usually { data: [], totalCount: ... } and NOT wrapped in ApiResponse<T> generically unless specified.
            // Looking at Response.cs vs PaginatedResponse.cs:
            // PaginatedResponse<T> : Response<T> { Data: T (which is List?), PageNumber... }
            // If PaginatedResponse inherits Response, then it usually has 'data' property.
            // On frontend 'Pagination<T>' interface usually matches this structure directly.
            // So returning it directly is correct.
            map((response: any) => response.data)
        );
    }

    promoteStudent(studentData: PromoteStudentsDto) {
        return this.http.post<ApiResponse<string>>(this.baseUrl + 'Student/promote', studentData).pipe(
            map(res => res.data)
        );
    }

    getStudentsBySectionId(sectionId: string) {
        return this.http.get<Pagination<StudentListDto>>(this.baseUrl + 'Student/by-section/' + sectionId).pipe(
            map((response: any) => response.data)
        );
    }
}
