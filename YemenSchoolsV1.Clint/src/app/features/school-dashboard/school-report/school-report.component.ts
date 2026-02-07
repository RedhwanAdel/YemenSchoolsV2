import { Component, computed, DestroyRef, inject, OnInit, signal, WritableSignal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { Observable, map, catchError, of, tap, Subscription, lastValueFrom } from 'rxjs';
import { SchoolReportData } from '@features/schools/models/school';
import { SchoolService } from '@features/schools/services/school.service';
import { HttpClientModule } from '@angular/common/http';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserModule, DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { AccountService } from '../../../core/services/account.service';
import { CommonModule } from '@angular/common';
import { ReportsService } from '@features/reports/services/reports.service';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-school-report',
  standalone: true,
  imports: [
    MatCardModule,
    MatIconModule,
    MatToolbarModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    CommonModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './school-report.component.html',
  styleUrl: './school-report.component.scss'
})
export class SchoolReportComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  private accountService = inject(AccountService);
  private schoolReportService = inject(SchoolService);
  private snackBar = inject(MatSnackBar);
  reportService = inject(ReportsService)
  loading = false;
  pdfUrl: SafeResourceUrl | null = null;
  sanitizer = inject(DomSanitizer);
  reportData: WritableSignal<SchoolReportData | null> = signal(null);
  isLoading: WritableSignal<boolean> = signal(true);
  errorMessage: WritableSignal<string | null> = signal(null);

  private dataSubscription: Subscription | null = null;

  // إصلاح مشكلة Type 'number' can't be used to index type
  readonly schoolTypeMap = computed(() => ({
    0: 'حكومي',
    1: 'خاص'
  }) as { [key: number]: string });

  readonly genderTypeMap = computed(() => ({
    0: 'بنين',
    1: 'بنات',
    2: 'مختلط'
  }) as { [key: number]: string });

  readonly schoolLevelMap = computed(() => ({
    0: 'ابتدائي',
    1: 'إعدادي',
    2: 'ثانوي',
    3: 'متعدد المراحل'
  }) as { [key: number]: string });

  ngOnInit(): void {
    const schoolId = this.accountService.currentUser()?.schoolId;

    if (!schoolId) {
      this.errorMessage.set('معرف المدرسة غير متوفر لعرض التقرير.');
      this.isLoading.set(false);
      this.reportData.set(null);
      return; // توقف هنا إذا لم يكن هناك schoolId
    }

    // --- الجزء المبسّط للغاية كما طلبته ---
    this.isLoading.set(true); // ابدأ التحميل
    this.errorMessage.set(null); // مسح أي رسائل خطأ سابقة

    this.schoolReportService.getSchoolReport(schoolId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (res) => {
          if (res) {
            this.reportData.set(res); // تحديث الـ Signal مباشرة بالبيانات
            this.errorMessage.set(null); // مسح أي خطأ سابق إذا نجح الطلب
          } else {
            // Fallback if data is null/undefined
            const msg = 'فشل جلب بيانات التقرير.';
            this.errorMessage.set(msg);
            this.reportData.set(null);
            this.snackBar.open(msg, 'إغلاق', { duration: 5000 });
          }
          this.isLoading.set(false);
        },
        error: (err) => {
          // التعامل مع أخطاء الشبكة أو الأخطاء غير المتوقعة
          console.error('Error fetching school report:', err);
          const msg = 'حدث خطأ أثناء الاتصال بالخادم. يرجى المحاولة مرة أخرى.';
          this.errorMessage.set(msg);
          this.reportData.set(null); // مسح البيانات إذا حدث خطأ
          this.snackBar.open(msg, 'إغلاق', { duration: 5000 });
          this.isLoading.set(false); // توقف التحميل عند الخطأ
        },
        complete: () => {
          // هذا الجزء سينفذ بعد next أو error، ويمكن أن يكون فارغًا إذا تم التعامل مع كل شيء
          // في next و error، أو للتأكد من توقف التحميل إذا لم يتم ذلك في next/error
          if (this.isLoading()) { // للتأكد فقط إذا لم يتم ضبطه بالفعل
            this.isLoading.set(false);
          }
        }
      });
    // --- نهاية الجزء المبسّط للغاية ---
  }

  getSchoolType(type: number): string {
    return this.schoolTypeMap()[type] || 'غير معروف';
  }

  getGenderType(type: number): string {
    return this.genderTypeMap()[type] || 'غير معروف';
  }

  getSchoolLevel(level: number): string {
    return this.schoolLevelMap()[level] || 'غير معروف';
  }


  async downloadReport() {
    this.loading = true; // (اختياري، لإظهار حالة التحميل)

    try {
      // 1. تحويل الـ Observable إلى Promise وانتظار النتيجة (Blob)
      const blob: Blob = await lastValueFrom(
        this.reportService.downloadSchoolReport(this.accountService.currentUser()?.schoolId || '')
      );

      // 2. معالجة الـ Blob للتحميل
      const a = document.createElement('a');
      const objectUrl = URL.createObjectURL(blob);

      a.href = objectUrl;
      a.download = `SchoolReport_${this.reportData()?.nameEn}.pdf`;

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
