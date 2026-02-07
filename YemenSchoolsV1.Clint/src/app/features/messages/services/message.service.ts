import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ApiResponse } from '@shared/models/ApiResponse';
import { Pagination } from '@shared/models/Pagination';
import { map } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class MessageService {
    private http = inject(HttpClient);
    baseUrl = environment.apiUrl;

    getMessages(container: string, pageNumber: number, pageSize: number) {
        let params = new HttpParams();
        params = params.append('Container', container);
        params = params.append('PageNumber', pageNumber.toString());
        params = params.append('PageSize', pageSize.toString());
        return this.http.get<Pagination<any>>(this.baseUrl + 'Messages', { params });
    }

    sendMessage(recipientId: string, content: string) {
        return this.http.post<ApiResponse<any>>(`${this.baseUrl}Messages`, { recipientId, content }).pipe(
            map(response => response.data)
        );
    }

    createHubConnection(userId: string) {
        console.log('Hub connection not implemented yet for user', userId);
    }

    deleteMessage(id: string) {
        return this.http.delete<ApiResponse<any>>(`${this.baseUrl}Messages/${id}`).pipe(
            map(response => response.data)
        );
    }
}
