import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ActivatedRoute } from '@angular/router';
import { TeacherService } from '@features/school-dashboard/teacher/services/teacher.service';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { PageWrapperComponent } from '../../../../shared/components/page-wrapper/page-wrapper.component';
import { Teacher } from '../models/teachers';

@Component({
  selector: 'app-teacher-detail',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatDividerModule,
    PageWrapperComponent
  ],
  templateUrl: './teacher-detail.component.html',
  styleUrl: './teacher-detail.component.scss'
})
export class TeacherDetailComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  teacher?: Teacher;
  private route = inject(ActivatedRoute);
  private teacherService = inject(TeacherService);
  teacherId!: string;

  ngOnInit(): void {
    this.teacherId = this.route.snapshot.paramMap.get('id')!;
    this.teacherService.getTeacherById(this.teacherId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (response) => (this.teacher = response),
      });
  }
}
