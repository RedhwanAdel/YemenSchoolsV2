import { inject, Injectable } from '@angular/core';
import { map } from 'rxjs';
import { CreateTermDto, Term, TermDto, UpdateTermDto } from '../models/term';
import { environment } from '../../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '@shared/models/ApiResponse';

@Injectable({
    providedIn: 'root'
})
export class TermService {
    private http = inject(HttpClient)
    baseUrl = environment.apiUrl;

    getTerms(academicYearId: string) {
        return this.http.get<ApiResponse<Term[]>>(`${this.baseUrl}Terms/${academicYearId}`).pipe(
            map(res => res.data)
        );
    }

    createTerm(term: CreateTermDto) {
        return this.http.post<ApiResponse<string>>(`${this.baseUrl}Terms`, term).pipe(
            map(res => res.data)
        );
    }

    updateTerm(id: string, term: UpdateTermDto) {
        term.id = id;
        return this.http.put<ApiResponse<string>>(`${this.baseUrl}Terms`, term).pipe(
            map(res => res.data)
        );
    }

    deleteTerm(id: string) {
        return this.http.delete<ApiResponse<string>>(`${this.baseUrl}Terms/${id}`).pipe(
            map(res => res.data)
        );
    }
}
