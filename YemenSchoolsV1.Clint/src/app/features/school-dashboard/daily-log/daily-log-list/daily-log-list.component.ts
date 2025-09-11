import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { SectionSubject } from '../../../../shared/models/mark/mark';
import { DailyLogCardComponent } from '../daily-log-card/daily-log-card.component';
import { MarkService } from '../../../../core/services/mark.service';

@Component({
  selector: 'app-daily-log-list',
  standalone: true,
  imports: [CommonModule, MatGridListModule, DailyLogCardComponent],
  templateUrl: './daily-log-list.component.html',
  styleUrl: './daily-log-list.component.scss'
})
export class DailyLogListComponent implements OnInit {
  markService = inject(MarkService)
  sectionSubjects: SectionSubject[] = [];


  ngOnInit(): void {
    this.loadTeacherSectionSubjects()
  }
  loadTeacherSectionSubjects(): void {
    this.markService.getTeacherSectionSubjects().subscribe({
      next: (data) => {
        this.sectionSubjects = data;
      },
      error: (err) => console.error('Error loading section subjects', err)
    });
  }
  onAddLog(sectionSubjectId: string) {
    console.log('إضافة سجل جديد لـ', sectionSubjectId);
    // هنا ممكن تعمل توجيه إلى /daily-log/add/:id
  }

  onViewLogs(sectionSubjectId: string) {
    console.log('عرض السجلات لـ', sectionSubjectId);
    // هنا ممكن تعمل توجيه إلى /daily-log/section/:id
  }
}
