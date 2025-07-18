import { inject, Injectable, signal } from '@angular/core';
import { Term } from '../../shared/models/term/term';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Pagination } from '../../shared/models/Pagination';
import { Section } from '../../shared/models/section/section';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class TermService {


  baseUrl = environment.apiUrl;
  private http = inject(HttpClient)
  terms = signal<Term[]>([]);
  accountService = inject(AccountService)

  private schoolId = this.accountService.currentUser()?.schoolId

  getTerms(yaerId?: string) {
    let params = new HttpParams();
    if (yaerId) {
      params = params.append('acadmicYearId', yaerId);
    }
    return this.http.get<Pagination<Term>>(this.baseUrl + 'Terms/GetAllTermsPaged/' + this.schoolId, { params }).subscribe({
      next: res => this.terms.set(res.data)
    })
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
