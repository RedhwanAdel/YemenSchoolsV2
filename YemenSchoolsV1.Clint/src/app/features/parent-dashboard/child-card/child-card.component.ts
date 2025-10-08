import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { Student } from '../../../shared/models/student/student';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { StudentWithSchoolInfoDto } from '../../../shared/models/parent';
import { RouterLink } from "@angular/router";
import { ReportsService } from '../../../core/services/reports.service';
import { lastValueFrom } from 'rxjs';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-child-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatProgressBarModule, RouterLink],
  templateUrl: './child-card.component.html',
  styleUrl: './child-card.component.scss'
})
export class ChildCardComponent implements OnInit {
  @Input({ required: true }) child!: StudentWithSchoolInfoDto;
  @Output() openProfile = new EventEmitter<string>();
  @Output() messageTeacher = new EventEmitter<number>();
  reportService = inject(ReportsService)

  loading = false;
  pdfUrl: SafeResourceUrl | null = null;
  sanitizer = inject(DomSanitizer);
  ngOnInit(): void {

    this.child.avg = 95
  }

  getProgressBarColor(): 'primary' | 'accent' | 'warn' {
    if (this.child.avg >= 80) {
      return 'primary'; // أداء ممتاز (أزرق)
    } else if (this.child.avg >= 65) {
      return 'accent'; // أداء جيد (أخضر)
    } else {
      return 'warn'; // يحتاج إلى تحسين (أحمر)
    }
  }
  get imageUrlOrDefault() {

    return this.child?.imageUrl?.trim() ?? '/assets/images/user/avatar-2.jpg';
  }


  async downloadReport() {
    this.loading = true; // (اختياري، لإظهار حالة التحميل)

    try {
      // 1. تحويل الـ Observable إلى Promise وانتظار النتيجة (Blob)
      const blob: Blob = await lastValueFrom(
        this.reportService.downloadReport(this.child.studentId)
      );

      // 2. معالجة الـ Blob للتحميل
      const a = document.createElement('a');
      const objectUrl = URL.createObjectURL(blob);

      a.href = objectUrl;
      a.download = `StudentReport_${this.child.studentId}.pdf`;

      // 3. محاكاة النقر للتحميل
      a.click();

      // 4. تحرير الذاكرة
      URL.revokeObjectURL(objectUrl);

    } catch (error) {
      console.error('فشل في تحميل الملف:', error);
      // يمكنك هنا إظهار رسالة خطأ للمستخدم
    } finally {
      this.loading = false; // إيقاف حالة التحميل
    }
  }
}
