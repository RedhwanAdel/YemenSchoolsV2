import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { PageWrapperComponent } from '../../../../shared/components/page-wrapper/page-wrapper.component';
import { StudentById } from '@features/school-dashboard/student/models/student';
import { ActivatedRoute } from '@angular/router';
import { TeacherService } from '@features/school-dashboard/teacher/services/teacher.service';
import { StudentService } from '@features/school-dashboard/student/services/student.service';

@Component({
  selector: 'app-student-detail',
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
  templateUrl: './student-detail.component.html',
  styleUrl: './student-detail.component.scss'
})
export class StudentDetailComponent {
  private destroyRef = inject(DestroyRef);
  student?: StudentById;
  private route = inject(ActivatedRoute);
  private studentService = inject(StudentService);
  studentId!: string;

  ngOnInit(): void {
    this.studentId = this.route.snapshot.paramMap.get('id')!;
    this.studentService.getStudentProfile(this.studentId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (stu) => (this.student = stu),
      });
  }
}
