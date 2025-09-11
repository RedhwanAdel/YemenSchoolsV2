import { CommonModule } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { ReactiveFormsModule, FormControl } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { SectionSubject } from '../../../../shared/models/mark/mark';
import { DailyLog } from '../../../../shared/models/daily-log/daily-log';

@Component({
  selector: 'app-daily-log-list-view',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    ReactiveFormsModule
  ],
  templateUrl: './daily-log-list-view.component.html',
  styleUrl: './daily-log-list-view.component.scss'
})
export class DailyLogListViewComponent {
  logs: DailyLog[] = [];
  filteredLogs: DailyLog[] = [];
  dateFilter = new FormControl();

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { sectionSubject: SectionSubject },
    private dialogRef: MatDialogRef<DailyLogListViewComponent>
  ) { }

  ngOnInit(): void {
    // هنا استبدل البيانات الثابتة بطلب من API لاحقًا
    this.logs = [
      {
        id: '1',
        date: '2025-09-07',
        lessonCovered: 'درس الجذور التربيعية',
        homeworkAssigned: 'تمارين صفحة 12',
        teacherNotes: 'ركزوا على الأمثلة',
        sectionSubjectId: this.data.sectionSubject.id
      },
      {
        id: '2',
        date: '2025-09-06',
        lessonCovered: 'مقدمة في الكسور',
        homeworkAssigned: 'تمارين صفحة 9',
        teacherNotes: '',
        sectionSubjectId: this.data.sectionSubject.id
      }
    ];

    this.filteredLogs = [...this.logs];
  }

  applyDateFilter() {
    if (this.dateFilter.value) {
      const selectedDate = this.dateFilter.value.toISOString().split('T')[0];
      this.filteredLogs = this.logs.filter(log => log.date === selectedDate);
    } else {
      this.filteredLogs = [...this.logs];
    }
  }

  closeDialog() {
    this.dialogRef.close();
  }
}
