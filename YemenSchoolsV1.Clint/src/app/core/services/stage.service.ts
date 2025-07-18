import { inject, Injectable, signal } from '@angular/core';
import { Stage } from '../../shared/models/stage/stage';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Pagination } from '../../shared/models/Pagination';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class StageService {

  baseUrl = environment.apiUrl;
  private http = inject(HttpClient)
  stages = signal<Stage[]>([]);
  accountService = inject(AccountService)

  private schoolId = this.accountService.currentUser()?.schoolId

  getStages() {
    return this.http.get<Pagination<Stage>>(this.baseUrl + 'Stages/GetAllStagesPaged/' + this.schoolId).subscribe({
      next: res => this.stages.set(res.data)
    })
  }

  createStage(stage: any) {
    return this.http.post<string>(this.baseUrl + 'Stages', stage);
  }

  updateStage(id: string, stage: any) {
    stage.id = id
    return this.http.put(this.baseUrl + 'Stages', stage);
  }

  deleteStage(id: string) {
    return this.http.delete(this.baseUrl + 'Stages/' + id);
  }
}
