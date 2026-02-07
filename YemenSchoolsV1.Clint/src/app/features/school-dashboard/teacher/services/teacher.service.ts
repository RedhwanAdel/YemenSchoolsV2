import { inject, Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '@shared/models/ApiResponse';
import { AccountService } from '@core/services/account.service';
import { CreateTeacherDto, Teacher, UpdateTeacherDto } from '../models/teachers';
import { Pagination } from '@shared/models/Pagination';

@Injectable({
    providedIn: 'root'
})
export class TeacherService {
    baseUrl = environment.apiUrl;
    private http = inject(HttpClient);
    private accountService = inject(AccountService);
    private schoolId = this.accountService.currentUser()?.schoolId;

    getTeachers(role?: string) {
        // Backend typo: GeAlltBySchoolId
        if (this.schoolId) {
            return this.http.get<Pagination<Teacher>>(this.baseUrl + 'Teachers/GeAlltBySchoolId/' + this.schoolId).pipe(
                map(res => res.data)
            );
        }
        return this.http.get<Pagination<Teacher>>(this.baseUrl + 'Teachers').pipe(
            map(res => res.data)
        );
    }

    getTeacherById(id: string) {
        return this.http.get<ApiResponse<Teacher>>(this.baseUrl + 'Teachers/GetTeacherById/' + id).pipe(
            map(res => res.data)
        );
    }

    createTeacher(teacher: CreateTeacherDto) {
        if (this.schoolId) {
            teacher.schoolId = this.schoolId;
        }
        return this.http.post<ApiResponse<string>>(this.baseUrl + 'Teachers', teacher).pipe(
            map(res => res.data)
        );
    }

    updateTeacher(id: string, teacher: UpdateTeacherDto) {
        teacher.id = id;
        if (this.schoolId) {
            teacher.schoolId = this.schoolId;
        }
        return this.http.put<ApiResponse<string>>(this.baseUrl + 'Teachers', teacher).pipe(
            map(res => res.data)
        );
    }

    deleteTeacher(id: string) {
        return this.http.delete<ApiResponse<string>>(this.baseUrl + 'Teachers/' + id).pipe(
            map(res => res.data)
        );
    }

    createTeacherUser(teacherId: string) {
        return this.http.post<ApiResponse<string>>(this.baseUrl + `Teachers/${teacherId}/create-user`, {}).pipe(
            map(res => res.data)
        );
    }
}
