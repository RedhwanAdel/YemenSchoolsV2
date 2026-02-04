import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CreateDailyLogDto, DailyLogDto } from '../../shared/models/daily-log/daily-log';
import { ApiResponse } from '../../shared/models/ApiResponse';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DailyLogService {


  private apiUrl = environment.apiUrl + 'DailyLog'; // استبدل بعنوان API الخاص بك

  constructor(private http: HttpClient) { }

  getStudentDailyLogsForDay(studentId: string, date: Date) {
    const params = new HttpParams().set('date', date.toISOString());
    return this.http.get<ApiResponse<DailyLogDto[]>>(`${this.apiUrl}/student/${studentId}/daily`, { params }).pipe(
      map(response => response.data))
  }
  // لإرسال الدرجات إلى الـ API
  createDailyLog(dailyLog: any) {
    return this.http.post<any>(`${this.apiUrl}`, dailyLog);
  }
}
