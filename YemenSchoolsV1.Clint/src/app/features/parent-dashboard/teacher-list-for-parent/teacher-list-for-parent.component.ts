import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ParentService } from '@features/parent-dashboard/services/parent.service';
import { PageWrapperComponent } from "../../../shared/components/page-wrapper/page-wrapper.component";
import { TableColumn, TableComponent } from "../../../shared/components/table/table.component";
import { Router, RouterLink } from '@angular/router';
import { SnackbarService } from '../../../core/services/snackbar.service';
import { Teacher } from '@features/school-dashboard/teacher/models/teachers';
import { TeacherInfoForParentDto } from '@features/parent-dashboard/models/parent';
import { MatButton } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-teacher-list-for-parent',
  standalone: true,
  imports: [PageWrapperComponent, MatButton, MatCardModule, CommonModule, MatIcon, RouterLink],
  templateUrl: './teacher-list-for-parent.component.html',
  styleUrl: './teacher-list-for-parent.component.scss'
})
export class TeacherListForParentComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  parentService = inject(ParentService);
  teachers = signal<TeacherInfoForParentDto[]>([]);

  ngOnInit(): void {
    this.loadTeachers();
  }

  loadTeachers() {
    this.parentService.GetTeachersForParent()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (res: any) => this.teachers.set(res.data)
      });
  }

  contactTeacher(teacher: TeacherInfoForParentDto) {
    // هنا تفتح صفحة محادثة مع المعلم
    console.log('مراسلة المعلم:', teacher.teacherName);
  }
  trackByTeacherId(index: number, teacher: any): number {
    return teacher.id; // Or a unique identifier for the teacher
  }
}
