import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AccountService } from './account.service';
import { SectionSubjectInfoDto } from '../../shared/models/section/section';
import { ApiResponse } from '../../shared/models/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class SectionSubjectService {

  baseUrl = environment.apiUrl;
  private http = inject(HttpClient)
  accountService = inject(AccountService)

  private schoolId = this.accountService.currentUser()?.schoolId


  // 3. Get by Section Id
  getBySectionId(sectionId: string) {


    return this.http.get<ApiResponse<SectionSubjectInfoDto[]>>(this.baseUrl + 'SectionSubjects/by-section/' + sectionId);
  }

  // 4. Create new SectionSubject
  create(dto: any) {
    return this.http.post(this.baseUrl + 'SectionSubjects', dto);
  }

  update(id: string, dto: SectionSubjectInfoDto) {
    return this.http.put(this.baseUrl + 'SectionSubjects', dto);
  }

  delete(id: string) {
    return this.http.delete(this.baseUrl + 'SectionSubjects/' + id);
  }
}
