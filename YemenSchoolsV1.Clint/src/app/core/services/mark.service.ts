import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CreateMarksDto, SectionSubject, Student } from '../../shared/models/mark/mark';

@Injectable({
  providedIn: 'root'
})
export class MarkService {

  private apiUrl = environment.apiUrl + 'Mark'; // استبدل بعنوان API الخاص بك

  constructor(private http: HttpClient) { }

  // لجلب الشعب والمواد التي يدرسها المعلم
  getTeacherSectionSubjects() {
    // يجب أن تكون هذه النقطة في الـ API موجودة وتجلب البيانات بناءً على هوية المعلم
    return this.http.get<SectionSubject[]>(this.apiUrl + '/section-subjects');
  }


  // لإرسال الدرجات إلى الـ API
  createMarks(marksDto: CreateMarksDto) {
    return this.http.post<any>(`${this.apiUrl}/create`, marksDto);
  }
}
