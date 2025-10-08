import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {
  private http = inject(HttpClient)
  baseUrl = environment.apiUrl + 'Reports/'

  downloadReport(id: string): Observable<Blob> {
    const url = `${this.baseUrl}student/${id}`;
    return this.http
      .post(url, {}, { responseType: 'blob', observe: 'response' })
      .pipe(
        map((res: HttpResponse<Blob>) => res.body as Blob)
      );
  }

  downloadSchoolReport(id: string): Observable<Blob> {
    const url = `${this.baseUrl}school/${id}`;
    return this.http
      .post(url, {}, { responseType: 'blob', observe: 'response' })
      .pipe(
        map((res: HttpResponse<Blob>) => res.body as Blob)
      );
  }

}
