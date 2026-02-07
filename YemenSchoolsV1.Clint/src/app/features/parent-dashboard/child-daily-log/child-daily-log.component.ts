import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { DomSanitizer } from '@angular/platform-browser';
import { DailyLogService } from '@features/school-dashboard/daily-log/services/daily-log.service';
import { DailyLogDto } from '@features/school-dashboard/daily-log/models/daily-log';
import { ActivatedRoute } from '@angular/router';
import { SnackbarService } from '../../../core/services/snackbar.service';

@Component({
  selector: 'app-child-daily-log',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule,
    MatFormFieldModule,
  ],
  templateUrl: './child-daily-log.component.html',
  styleUrl: './child-daily-log.component.scss'
})
export class ChildDailyLogComponent {
  selectedDate: Date = new Date();
  filteredLogs: DailyLogDto[] = [];

  private route = inject(ActivatedRoute);
  private snackbar = inject(SnackbarService)
  constructor(private dailyLogService: DailyLogService) { }

  ngOnInit(): void {

    this.loadLogs();
  }

  loadLogs(): void {
    const studentId = this.route.snapshot.paramMap.get('studentId');
    if (!studentId) {
      this.snackbar.error('لا يمكن ايجاد معرف الطالب');
      return;
    }
    this.dailyLogService.getStudentDailyLogsForDay(studentId, this.selectedDate)
      .subscribe({
        next: (logs: any) => this.filteredLogs = logs,
        error: (err: any) => {
          console.error('خطأ في تحميل السجلات:', err);
          this.filteredLogs = [];
        }
      });
  }

  onPrevDay(): void {
    this.selectedDate = new Date(this.selectedDate.setDate(this.selectedDate.getDate() - 1));
    this.loadLogs();
  }

  onNextDay(): void {
    this.selectedDate = new Date(this.selectedDate.setDate(this.selectedDate.getDate() + 1));
    this.loadLogs();
  }

  onDateChange(): void {
    this.loadLogs();
  }

  getCardStatusClass(notes: string): string {
    if (!notes) return '';
    if (notes.includes('ممتاز') || notes.includes('متميز') || notes.includes('مميز')) return 'status-excellent';
    if (notes.includes('جيد') || notes.includes('حماس')) return 'status-good';
    if (notes.includes('بحاجة') || notes.includes('ضعيفة')) return 'status-needs-attention';
    return '';
  }

  getCardStatusText(notes: string): string {
    if (!notes) return '';
    if (notes.includes('ممتاز') || notes.includes('متميز') || notes.includes('مميز')) return '🌟 أداء ممتاز';
    if (notes.includes('جيد') || notes.includes('حماس')) return '👍 أداء جيد';
    if (notes.includes('بحاجة') || notes.includes('ضعيفة')) return '⚠️ يحتاج إلى متابعة';
    return '';
  }
}
