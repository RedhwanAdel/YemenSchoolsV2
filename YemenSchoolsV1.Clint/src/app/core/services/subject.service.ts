import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '../../shared/models/ApiResponse';
import { Subject } from '../../shared/models/school/subject';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {
  baseUrl = environment.apiUrl
  private http = inject(HttpClient)
  subjects = signal<Subject[]>([]);

  getSubjects() {
    return this.http.get<ApiResponse<Subject[]>>(this.baseUrl + 'Subjects').pipe(
      map(res => {
        this.subjects.set(res.data)
        return res;
      })
    )
  }
}
