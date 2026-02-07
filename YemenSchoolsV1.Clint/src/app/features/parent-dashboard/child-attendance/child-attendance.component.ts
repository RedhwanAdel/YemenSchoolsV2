import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatDivider } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { finalize } from 'rxjs';
import { AttendanceService } from '@features/school-dashboard/attendance/services/attendance.service';
import { AttendanceDetailDto } from '@features/school-dashboard/attendance/models/attendance';
import { ActivatedRoute } from '@angular/router';
import { SnackbarService } from '../../../core/services/snackbar.service';
import { MatSpinner } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-child-attendance',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatDivider,
    MatFormFieldModule,
    MatSelectModule,
    FormsModule,
  ],
  templateUrl: './child-attendance.component.html',
  styleUrl: './child-attendance.component.scss'
})
export class ChildAttendanceComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  private route = inject(ActivatedRoute);
  private snackbar = inject(SnackbarService);
  studentName = 'أحمد محمد';
  selectedMonth = new Date().getMonth();
  selectedYear = new Date().getFullYear(); // إضافة متغير للسنة

  availableMonths = [
    { name: 'يناير', value: 1 },
    { name: 'فبراير', value: 2 },
    { name: 'مارس', value: 3 },
    { name: 'أبريل', value: 4 },
    { name: 'مايو', value: 5 },
    { name: 'يونيو', value: 6 },
    { name: 'يوليو', value: 7 },
    { name: 'أغسطس', value: 8 },
    { name: 'سبتمبر', value: 9 },
    { name: 'أكتوبر', value: 10 },
    { name: 'نوفمبر', value: 11 },
    { name: 'ديسمبر', value: 12 },
  ];

  calendarDays: any[] = [];
  isLoading = false; // حالة للتحميل
  errorMessage: string | null = null; // رسالة خطأ

  // حقن الخدمة في constructor
  constructor(private attendanceService: AttendanceService) { }

  ngOnInit(): void {

    this.selectedMonth = new Date().getMonth() + 1;
    this.loadAttendanceData();
  }

  // دالة تُستدعى عند تغيير الشهر
  onMonthChange(): void {
    this.loadAttendanceData();
  }

  // دالة تحميل البيانات
  loadAttendanceData(): void {
    if (!this.selectedMonth || !this.selectedYear) {
      this.errorMessage = 'الرجاء اختيار شهر وسنة.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = null;
    const studentId = this.route.snapshot.paramMap.get('studentId');
    if (!studentId) {
      this.snackbar.error('لا يمكن ايجاد معرف الطالب');
      return;
    }
    // استدعاء الخدمة الحقيقية
    this.attendanceService.getStudentAttendanceReportByDate(
      studentId,
      this.selectedYear.toString(),
      this.selectedMonth.toString()
    )
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        finalize(() => this.isLoading = false)
      )
      .subscribe({
        next: (response: AttendanceDetailDto[]) => {
          // تحويل بيانات الـ API إلى تنسيق التقويم
          if (this.selectedMonth)
            this.calendarDays = this.mapAttendanceDataToCalendar(response, this.selectedYear, this.selectedMonth);
        },
        error: (err: any) => {
          // التعامل مع الأخطاء
          this.errorMessage = 'حدث خطأ أثناء تحميل البيانات. الرجاء المحاولة لاحقًا.';
          console.error(err);
        }
      });
  }

  // دالة جديدة لتحويل بيانات الـ API إلى تنسيق التقويم
  private mapAttendanceDataToCalendar(data: AttendanceDetailDto[], year: number, month: number): any[] {
    const daysInMonth = new Date(year, month, 0).getDate();
    const calendar = [];
    const attendanceMap = new Map<number, AttendanceDetailDto>();

    // إنشاء خريطة (Map) لتسهيل البحث عن كل يوم
    data.forEach(item => {
      const day = new Date(item.createdAt).getDate();
      attendanceMap.set(day, item);
    });

    for (let i = 1; i <= daysInMonth; i++) {
      const attendanceRecord = attendanceMap.get(i);

      // إذا كان هناك سجل لهذا اليوم
      if (attendanceRecord) {
        calendar.push({
          date: i,
          status: this.getStatusText(attendanceRecord.status),
          notes: attendanceRecord.notes
        });
      } else {
        // إذا لم يكن هناك سجل، افترض أنه عطلة أو فارغ
        calendar.push({
          date: i,
          status: 'holiday' // أو null حسب منطقك
        });
      }
    }
    return calendar;
  }

  // دالة لتحويل الحالة الرقمية إلى نص
  private getStatusText(statusNumber: number): string {
    switch (statusNumber) {
      case 0: return 'present';
      case 1: return 'absent';
      case 2: return 'excused'; // افترضنا أن 2 هي حالة الغياب بعذر
      case 3: return 'late';
      default: return 'holiday';
    }
  }

  // الدالة القديمة (تبقى كما هي)
  getStatusClass(status: string): string {
    switch (status) {
      case 'present': return 'status-present';
      case 'absent': return 'status-absent';
      case 'excused': return 'status-excused';
      case 'late': return 'status-late';
      case 'holiday': return 'status-holiday'; // إضافة حالة العطلة
      default: return '';
    }
  }
}
