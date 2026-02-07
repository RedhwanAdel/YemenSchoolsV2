import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { SectionSubject } from '@features/school-dashboard/mark/models/mark';
import { MatDialog } from '@angular/material/dialog';
import { AddDailyLogComponent } from '../add-daily-log/add-daily-log.component';
import { DailyLogListViewComponent } from '../daily-log-list-view/daily-log-list-view.component';

@Component({
  selector: 'app-daily-log-card',
  standalone: true,
  imports: [
    CommonModule, MatCardModule, MatButtonModule, MatIconModule
  ],
  templateUrl: './daily-log-card.component.html',
  styleUrl: './daily-log-card.component.scss'
})
export class DailyLogCardComponent {
  dialog = inject(MatDialog)
  @Input() sectionSubject!: SectionSubject;

  @Output() addLog = new EventEmitter<string>();
  @Output() viewLogs = new EventEmitter<string>();

  openAddDailyLog() {
    const dialogRef = this.dialog.open(AddDailyLogComponent, {
      width: '500px',
      data: { sectionSubject: this.sectionSubject }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log('تمت الإضافة:', result);
        // TODO: تحديث الكارد أو إعادة تحميل البيانات إذا لزم
      }
    });
  }
  onViewLogs() {
    this.viewLogs.emit(this.sectionSubject.id);
  }
  openViewDailyLogs() {
    const dialogRef = this.dialog.open(DailyLogListViewComponent, {
      width: '700px',
      data: { sectionSubject: this.sectionSubject }
    });

    dialogRef.afterClosed().subscribe(() => {
      console.log('تم إغلاق عرض السجلات');
    });
  }

}
