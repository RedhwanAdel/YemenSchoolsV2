import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '@shared/models/ApiResponse';

@Injectable({
    providedIn: 'root'
})
export class StageService {
    private http = inject(HttpClient);
    baseUrl = environment.apiUrl;

    getStages(schoolId: string) {
        // Backend `GetAll` does not take schoolId in route (likely context based).
        return this.http.get<ApiResponse<any[]>>(`${this.baseUrl}Stages`);
    }

    createStage(stage: any) {
        return this.http.post(`${this.baseUrl}Stages`, stage);
    }

    updateStage(id: string, stage: any) {
        stage.id = id;
        return this.http.put(`${this.baseUrl}Stages`, stage);
    }

    deleteStage(id: string) {
        return this.http.delete(`${this.baseUrl}Stages/${id}`);
    }
}
