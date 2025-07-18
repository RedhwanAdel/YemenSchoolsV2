import { inject, Injectable, signal } from '@angular/core';
import { Section } from '../../shared/models/section/section';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination } from '../../shared/models/Pagination';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class SectionService {

  baseUrl = environment.apiUrl;
  private http = inject(HttpClient)
  sections = signal<Section[]>([]);
  accountService = inject(AccountService)

  private schoolId = this.accountService.currentUser()?.schoolId

  getSections(gradeId?: string) {
    let params = new HttpParams();
    if (gradeId) {
      params = params.append('gradeId', gradeId);
    }
    return this.http.get<Pagination<Section>>(this.baseUrl + 'Sections/GetAllSectiosPaged/' + this.schoolId, { params }).subscribe({
      next: res => this.sections.set(res.data)
    })
  }

  createSection(section: any) {
    return this.http.post<string>(this.baseUrl + 'Sections', section);
  }

  updateSection(id: string, section: any) {
    section.id = id
    return this.http.put(this.baseUrl + 'Sections', section);
  }

  deleteSection(id: string) {
    return this.http.delete(this.baseUrl + 'Sections/' + id);
  }
}
