import { SelectionModel } from '@angular/cdk/collections';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentService } from '@features/school-dashboard/student/services/student.service';
import { StudentListDto } from '@features/school-dashboard/student/models/student';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { SectionService } from '@features/school-dashboard/section/services/section.service';
import { SectionsOfYear } from '@features/school-dashboard/section/models/section';
import { AcadmicYearService } from '@features/school-dashboard/year/services/acadmic-year.service';
import { YearDto } from '@features/school-dashboard/year/models/AcademicYear';

@Component({
  selector: 'app-promote-students',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatTableModule,
    MatCheckboxModule,
    MatButtonModule,
    MatSelectModule,
    MatFormFieldModule,
    MatInputModule
  ],
  templateUrl: './promote-students.component.html',
  styleUrl: './promote-students.component.scss'
})
export class PromoteStudentsComponent {
  private destroyRef = inject(DestroyRef);
  dataSource = new MatTableDataSource<StudentListDto>();
  displayedColumns: string[] = ['select', 'nameAr', 'registerNo'];
  selection = new SelectionModel<StudentListDto>(true, []);
  studentService = inject(StudentService);
  sectionService = inject(SectionService);
  snack = inject(SnackbarService);
  newSections: SectionsOfYear[] = [];
  newSectionCtrl = new FormControl();
  academicYearService = inject(AcadmicYearService);
  academicYears: YearDto[] = [];
  academicYearCtrl = new FormControl();
  currentSectionId?: string;

  constructor(
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.getAcademicYears();
    this.getStudentsAndNewSections();

  }

  getStudentsAndNewSections(): void {
    const sectionId = this.route.snapshot.paramMap.get('sectionId');
    if (!sectionId) {
      this.snack.error('لم يتم التعرف على معرف الشعبة');
      return;
    }
    this.studentService.getStudentsBySectionId(sectionId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (res: StudentListDto[]) => {
          this.dataSource.data = res;
        }
      });
  }

  getAcademicYears(): void {
    this.academicYearService.getAcademicYears()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: res => {
          this.academicYears = res.data;
        },
        error: err => {
          this.snack.error('فشل في جلب السنوات الأكاديمية.');
        }
      });
  }

  onAcademicYearSelected(): void {
    const selectedYearId = this.academicYearCtrl.value;
    if (selectedYearId) {
      this.sectionService.getSectionsByAcademicYear(selectedYearId)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: res => {
            this.newSections = res;
            this.newSectionCtrl.reset();
          },
          error: err => {
            this.snack.error('فشل في جلب الشعب.');
          }
        });
    } else {
      this.newSections = [];
      this.newSectionCtrl.reset();
    }
  }
  isAllSelected(): boolean {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  masterToggle(): void {
    this.isAllSelected() ? this.selection.clear() : this.dataSource.data.forEach(row => this.selection.select(row));
  }

  promoteStudents(): void {
    const selectedStudentIds = this.selection.selected.map(s => s.id);
    const newSectionId = this.newSectionCtrl.value;

    const promotionData = {
      studentIds: selectedStudentIds,
      newSectionId: newSectionId
    };
    this.studentService.promoteStudent(promotionData)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: res => {
          this.snack.success('تمت الترقية بنجاح!');
        },
        error: err => {
          this.snack.error(err.error.message || 'فشلت عملية الترقية.');
        }
      });
  }
}
