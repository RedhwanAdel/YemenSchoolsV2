import { CommonModule, DatePipe } from '@angular/common';
import { Component, DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { Student, StudentListDto } from '@features/school-dashboard/student/models/student';
import { AttendanceService } from '@features/school-dashboard/attendance/services/attendance.service';
import { StudentService } from '@features/school-dashboard/student/services/student.service';
import { AcadmicYearService } from '@features/school-dashboard/year/services/acadmic-year.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { MatCardActions, MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { ActivatedRoute } from '@angular/router';
import { SectionService } from '@features/school-dashboard/section/services/section.service';

@Component({
  selector: 'app-daily-attendance',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatDividerModule,
    MatButtonToggleModule,
    MatProgressSpinnerModule,
    DatePipe
  ],
  templateUrl: './daily-attendance.component.html',
  styleUrl: './daily-attendance.component.scss'
})
export class DailyAttendanceComponent {
  private destroyRef = inject(DestroyRef);
  sectionId = '5740ce31-a0b8-4b6e-3b61-08ddccf64cb5'; // TODO: Remove hardcoded value
  students: StudentListDto[] = [];
  studentStatuses: { [id: string]: number } = {};
  displayedColumns: string[] = ['name', 'status'];
  studentService = inject(StudentService);
  yearService = inject(AcadmicYearService);
  private snack = inject(SnackbarService);
  private sectionService = inject(SectionService);
  today = new Date();
  isLoading = true;
  private route = inject(ActivatedRoute);
  sectionName = '';

  // حالات الحضور
  attendanceStatuses = [
    { value: 0, viewValue: 'حاضر' },
    { value: 1, viewValue: 'غائب بدون عذر' },
    { value: 2, viewValue: 'غائب بعذر' },
    { value: 3, viewValue: 'متأخر' },
  ];

  constructor(private attendanceService: AttendanceService) { }

  ngOnInit(): void {
    this.loadSectionStudents();
  }

  loadSectionStudents(): void {
    const yearId = this.yearService.currentAcademicYearId();
    if (!yearId) {
      this.snack.error('لم يتم التعرف على العام الحالي');
      return;
    }
    const sectionId = this.route.snapshot.paramMap.get('teacherId');
    if (!sectionId) {
      this.snack.error('لم يتم التعرف على معرف الشعبة');
      return;
    }
    this.sectionService.getSectionById(sectionId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: res => {
          this.sectionName = res.name;
        }
      });

    this.studentService.getStudentsBySectionId(sectionId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (res: StudentListDto[]) => {
          this.students = res;
          this.initializeStudentStatuses();
          this.isLoading = false;
        }
      });
  }

  initializeStudentStatuses(): void {
    this.students.forEach(student => {
      this.studentStatuses[student.id] = 0; // 0 = حاضر
    });
  }

  submitAttendance(): void {
    const today = new Date().toISOString().slice(0, 10);
    const requestBody = {
      sectionId: this.sectionId,
      date: today,
      studentStatuses: this.studentStatuses
    };

    this.attendanceService.createAttendance(requestBody)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (res: any) => {
          this.snack.success('تم اخذ الحضور بنجاح');
          this.isLoading = false;
        }
      });
  }
}
