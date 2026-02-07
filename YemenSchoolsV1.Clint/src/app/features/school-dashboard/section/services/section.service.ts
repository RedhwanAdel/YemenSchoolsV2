import { inject, Injectable } from '@angular/core';
import { map, throwError } from 'rxjs';
import { CreateSectionDto, Section, SectionsOfYear, UpdateSectionDto } from '../models/section';
import { environment } from '../../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ApiResponse } from '@shared/models/ApiResponse';
import { AcadmicYearService } from '../../year/services/acadmic-year.service';

@Injectable({
    providedIn: 'root'
})
export class SectionService {
    private http = inject(HttpClient);
    private yearService = inject(AcadmicYearService);
    baseUrl = environment.apiUrl;

    /**
     * Get sections by school grade ID for the current academic year
     * @param schoolGradeId The school grade ID
     * @returns Observable of Section array
     */
    getSectionsByGrade(schoolGradeId: string) {
        const academicYearId = this.yearService.currentAcademicYearId();
        if (!academicYearId) {
            return throwError(() => new Error('No current academic year available'));
        }

        const params = new HttpParams()
            .set('academicYearId', academicYearId)
            .set('schoolGradeId', schoolGradeId);

        return this.http.get<ApiResponse<Section[]>>(`${this.baseUrl}Sections/by-academic-year-and-grade`, { params }).pipe(
            map(res => res.data)
        );
    }

    /**
     * Get all sections for a specific academic year
     * @param academicYearId Optional academic year ID (defaults to current year)
     * @returns Observable of SectionsOfYear array
     */
    getSectionsByAcademicYear(academicYearId?: string) {
        const yearId = academicYearId || this.yearService.currentAcademicYearId();
        if (!yearId) {
            return throwError(() => new Error('No academic year available'));
        }

        return this.http.get<ApiResponse<SectionsOfYear[]>>(`${this.baseUrl}Sections/by-academic-year`, {
            params: { academicYearId: yearId }
        }).pipe(
            map(res => res.data)
        );
    }

    /**
     * Get section by ID
     * @param sectionId The section ID
     * @returns Observable of Section
     */
    getSectionById(sectionId: string) {
        return this.http.get<ApiResponse<Section>>(`${this.baseUrl}Sections/${sectionId}`).pipe(
            map(res => res.data)
        );
    }

    /**
     * Get sections assigned to a specific teacher
     * @param teacherId The teacher ID
     * @returns Observable of Section array
     */
    getSectionsByTeacherId(teacherId: string) {
        return this.http.get<ApiResponse<Section[]>>(`${this.baseUrl}Sections/by-teacherId/${teacherId}`).pipe(
            map(res => res.data)
        );
    }

    /**
     * Create a new section
     * @param section Section creation data
     * @returns Observable of created section ID
     */
    createSection(section: CreateSectionDto) {
        return this.http.post<ApiResponse<string>>(`${this.baseUrl}Sections`, section).pipe(
            map(res => res.data)
        );
    }

    /**
     * Update an existing section
     * @param sectionId The section ID to update
     * @param section Section update data
     * @returns Observable of success message
     */
    updateSection(sectionId: string, section: UpdateSectionDto) {
        section.id = sectionId;
        return this.http.put<ApiResponse<string>>(`${this.baseUrl}Sections/${sectionId}`, section).pipe(
            map(res => res.data)
        );
    }

    /**
     * Delete a section
     * @param sectionId The section ID to delete
     * @returns Observable of success message
     */
    deleteSection(sectionId: string) {
        return this.http.delete<ApiResponse<string>>(`${this.baseUrl}Sections/${sectionId}`).pipe(
            map(res => res.data)
        );
    }
}
