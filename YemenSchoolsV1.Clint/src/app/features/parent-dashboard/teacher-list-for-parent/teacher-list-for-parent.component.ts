import { Component, inject, OnInit, signal } from '@angular/core';
import { ParentService } from '../../../core/services/parent.service';
import { PageWrapperComponent } from "../../../shared/components/page-wrapper/page-wrapper.component";
import { TableColumn, TableComponent } from "../../../shared/components/table/table.component";
import { Router, RouterLink } from '@angular/router';
import { SnackbarService } from '../../../core/services/snackbar.service';
import { Teacher } from '../../../shared/models/teachers/teacher';
import { TeacherInfoForParentDto } from '../../../shared/models/parent';
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
  parentService = inject(ParentService);
  teachers = signal<TeacherInfoForParentDto[]>([]);

  ngOnInit(): void {
    this.loadTeachers();
  }

  loadTeachers() {
    this.parentService.GetTeachersForParent().subscribe({
      next: res => this.teachers.set(res.data)
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
