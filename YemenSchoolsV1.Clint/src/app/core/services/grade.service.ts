import { inject, Injectable, signal } from '@angular/core';
import { Grade } from '../../shared/models/grade/grade';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Pagination } from '../../shared/models/Pagination';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class GradeService {

  baseUrl = environment.apiUrl;
  private http = inject(HttpClient)
  grades = signal<Grade[]>([]);
  accountService = inject(AccountService)

  private schoolId = this.accountService.currentUser()?.schoolId

  getGrades(termId?: string) {

    let params = new HttpParams();
    if (termId) {
      params = params.append('termId', termId);
    }
    return this.http.get<Pagination<Grade>>(this.baseUrl + 'Grades/GetAllGradesPaged/' + this.schoolId, { params }).subscribe({
      next: res => this.grades.set(res.data)
    })
  }

  createGrade(grade: any) {
    return this.http.post<string>(this.baseUrl + 'Grades', grade);
  }

  updateGrade(id: string, grade: any) {
    grade.id = id
    return this.http.put(this.baseUrl + 'Grades', grade);
  }

  deleteGrade(id: string) {
    return this.http.delete(this.baseUrl + 'Grades/' + id);
  }
}
