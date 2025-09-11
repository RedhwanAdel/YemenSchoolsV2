import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CreateDailyLogDto, DailyLogDto } from '../../shared/models/daily-log/daily-log';

@Injectable({
  providedIn: 'root'
})
export class DailyLogService {


  private apiUrl = environment.apiUrl + 'DailyLog'; // استبدل بعنوان API الخاص بك

  constructor(private http: HttpClient) { }

  getStudentDailyLogsForDay(studentId: string, date: Date) {
    const params = new HttpParams().set('date', date.toISOString());
    return this.http.get<DailyLogDto[]>(`${this.apiUrl}/student/${studentId}/daily`, { params });
  }
  // لإرسال الدرجات إلى الـ API
  createDailyLog(dailyLog: any) {
    return this.http.post<any>(`${this.apiUrl}`, dailyLog);
  }
}
