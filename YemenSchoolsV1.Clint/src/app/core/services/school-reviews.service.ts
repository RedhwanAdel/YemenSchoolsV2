import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SchoolReview } from '../../shared/models/school/school';

@Injectable({
  providedIn: 'root'
})
export class SchoolReviewsService {

  private apiUrl = environment.apiUrl + 'SchoolReviews/';

  constructor(private http: HttpClient) { }

  getReviewsBySchool(schoolId: string): Observable<SchoolReview[]> {
    return this.http.get<SchoolReview[]>(`${this.apiUrl}school/${schoolId}`);
  }

  getAverageRating(schoolId: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}average/${schoolId}`);
  }

  addOrUpdateReview(review: { schoolId: string; rating: number; comment?: string }): Observable<SchoolReview> {
    return this.http.post<SchoolReview>(`${this.apiUrl}`, review);
  }


  updateReview(id: string, review: { rating: number; comment?: string }): Observable<SchoolReview> {
    return this.http.put<SchoolReview>(`${this.apiUrl}${id}`, review);
  }

  deleteReview(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}${id}`);
  }
}
