import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { PageWrapperComponent } from '../../../../shared/components/page-wrapper/page-wrapper.component';
import { StudentById } from '../../../../shared/models/student/student';
import { ActivatedRoute } from '@angular/router';
import { TeacherService } from '../../../../core/services/teacher.service';
import { StudentService } from '../../../../core/services/student.service';

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
  student?: StudentById;
  private route = inject(ActivatedRoute);
  private studentService = inject(StudentService);
  studentId!: string;

  ngOnInit(): void {
    this.studentId = this.route.snapshot.paramMap.get('id')!;
    this.studentService.getStudentProfile(this.studentId).subscribe({
      next: (stu) => (this.student = stu.data),
    });
  }
}
