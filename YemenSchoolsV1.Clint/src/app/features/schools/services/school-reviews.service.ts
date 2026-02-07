import { Injectable, inject } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SchoolReview } from '@features/schools/models/school';

import { ApiResponse } from '@shared/models/ApiResponse';
import { map } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class SchoolReviewsService {
    private http = inject(HttpClient);
    private baseUrl = environment.apiUrl;

    getReviewsBySchool(schoolId: string): Observable<SchoolReview[]> {
        return this.http.get<ApiResponse<SchoolReview[]>>(`${this.baseUrl}SchoolReviews/school/${schoolId}`).pipe(
            map(response => response.data)
        );
    }

    addOrUpdateReview(review: { schoolId: string; rating: number; comment?: string }): Observable<any> {
        return this.http.post<ApiResponse<any>>(`${this.baseUrl}SchoolReviews`, review).pipe(
            map(response => response.data)
        );
    }

    getAverageRating(schoolId: string): Observable<{ averageRating: number }> {
        return this.http.get<ApiResponse<{ averageRating: number }>>(`${this.baseUrl}SchoolReviews/average/${schoolId}`).pipe(
            map(response => response.data)
        );
    }
}
