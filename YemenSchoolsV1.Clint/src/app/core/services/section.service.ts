import { inject, Injectable, signal } from '@angular/core';
import { Section, SectionsOfYear } from '../../shared/models/section/section';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination } from '../../shared/models/Pagination';
import { AccountService } from './account.service';
import { ApiResponse } from '../../shared/models/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class SectionService {

  baseUrl = environment.apiUrl;
  private http = inject(HttpClient)
  sections = signal<Section[]>([]);
  accountService = inject(AccountService)

  private schoolId = this.accountService.currentUser()?.schoolId

  getSectionsByYearAndGrade(academicYearId: string, schoolGradeId: string) {
    let params = new HttpParams();
    params = params.append('academicYearId', academicYearId);
    params = params.append('schoolGradeId', schoolGradeId);

    return this.http.get<ApiResponse<Section[]>>(this.baseUrl + 'Sections/by-academic-year-and-grade', { params })
  }

  getSectionById(id: string) {
    return this.http.get<ApiResponse<Section>>(this.baseUrl + 'Sections/' + id)

  }
  getSectionsForSpcificYear(academicYearId: string) {
    let params = new HttpParams();
    params = params.append('academicYearId', academicYearId);

    return this.http.get<ApiResponse<SectionsOfYear[]>>(this.baseUrl + 'Sections/by-academic-year', { params })
  }


  createSection(section: any) {
    return this.http.post<string>(this.baseUrl + 'Sections', section);
  }

  updateSection(id: string, section: any) {
    section.id = id
    return this.http.put(this.baseUrl + 'Sections/' + id, section);
  }

  deleteSection(id: string) {
    return this.http.delete(this.baseUrl + 'Sections/' + id);
  }
}
