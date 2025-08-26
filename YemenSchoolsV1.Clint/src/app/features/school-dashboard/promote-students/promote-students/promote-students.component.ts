import { SelectionModel } from '@angular/cdk/collections';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentService } from '../../../../core/services/student.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { SectionService } from '../../../../core/services/section.service';
import { SectionsOfYear } from '../../../../shared/models/section/section';
import { AcadmicYearService } from '../../../../core/services/acadmic-year.service';
import { YearDto } from '../../../../shared/models/AcademicYear/AcademicYear';

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
  dataSource = new MatTableDataSource<any>();
  displayedColumns: string[] = ['select', 'nameAr', 'registerNo'];
  selection = new SelectionModel<any>(true, []);
  studentService = inject(StudentService)
  sectionService = inject(SectionService)
  snack = inject(SnackbarService)
  newSections: SectionsOfYear[] = [];
  newSectionCtrl = new FormControl();
  academicYearService = inject(AcadmicYearService); // يجب أن يكون لديك خدمة للسنوات الأكاديمية
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
    // جلب الطلاب في الشعبة الحالية
    const sectionId = this.route.snapshot.paramMap.get('sectionId');
    if (!sectionId) {
      this.snack.error('لم يتم التعرف على معرف الشعبة ')
      return;
    }
    this.studentService.getStudentsBySectionId(sectionId).subscribe({
      next: res => {
        this.dataSource.data = res
      }
    })

  }

  getAcademicYears(): void {
    this.academicYearService.getAcademicYears().subscribe({
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
      // جلب الشعب المرتبطة بالسنة المختارة
      this.sectionService.getSectionsForSpcificYear(selectedYearId).subscribe({
        next: res => {
          this.newSections = res.data;
          this.newSectionCtrl.reset(); // إعادة تعيين الشعبة المختارة
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
    this.studentService.promoteStudent(promotionData).subscribe({
      next: res => {
        this.snack.success('تمت الترقية بنجاح!')
      }, error: err => {
        this.snack.error(err.error.message || 'فشلت عملية الترقية.')
      }
    })

  }
}
