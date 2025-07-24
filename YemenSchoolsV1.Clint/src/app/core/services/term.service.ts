import { inject, Injectable, signal } from '@angular/core';
import { Term } from '../../shared/models/term/term';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Pagination } from '../../shared/models/Pagination';
import { Section } from '../../shared/models/section/section';
import { AccountService } from './account.service';
import { ApiResponse } from '../../shared/models/ApiResponse';

@Injectable({
  providedIn: 'root'
})
export class TermService {


  baseUrl = environment.apiUrl;
  private http = inject(HttpClient)
  accountService = inject(AccountService)

  private schoolId = this.accountService.currentUser()?.schoolId

  getTerms(yaerId: string) {

    return this.http.get<ApiResponse<Term[]>>(this.baseUrl + 'Terms/' + yaerId)
  }

  createTerm(term: any) {
    return this.http.post<string>(this.baseUrl + 'Terms', term);
  }

  updateTerm(id: string, term: any) {
    term.id = id
    return this.http.put(this.baseUrl + 'Terms', term);
  }

  deleteTerm(id: string) {
    return this.http.delete(this.baseUrl + 'Terms/' + id);
  }
}
