import { Component, DestroyRef, inject, Inject, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormInputComponent } from "../../../../shared/components/form-input/form-input.component";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { SectionService } from '@features/school-dashboard/section/services/section.service';
import { AcadmicYearService } from '@features/school-dashboard/year/services/acadmic-year.service';
import { TeacherService } from '@features/school-dashboard/teacher/services/teacher.service';
import { AccountService } from '../../../../core/services/account.service';
import { Teacher } from '@features/school-dashboard/teacher/models/teachers';
import { SelectInputComponent } from '../../../../shared/components/select-input/select-input.component';

@Component({
  selector: 'app-section-form',
  standalone: true,
  imports: [ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormInputComponent,
    SelectInputComponent
  ],
  templateUrl: './section-form.component.html',
  styleUrl: './section-form.component.scss'
})
export class SectionFormComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  form!: FormGroup;
  isEdit = false;
  sctionService = inject(SectionService)
  yearService = inject(AcadmicYearService)
  teacherService = inject(TeacherService)
  accountService = inject(AccountService)
  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<SectionFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {
      model?: any,
      gradeId: string,
      yearId: string
    }
  ) { }

  teachers: Teacher[] = []
  ngOnInit() {
    this.loadTeachers()
    this.isEdit = !!this.data?.model;

    this.form = this.fb.group({
      name: [this.data?.model?.name || '', Validators.required],
      capacity: [this.data?.model?.capacity || '', Validators.required],
      classTeacherId: [this.data?.model?.classTeacherId || ''],

    });


  }


  loadTeachers() {
    const schoolId = this.accountService.currentUser()?.schoolId;
    if (!schoolId) return;
    this.teacherService.getTeachers(schoolId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: res => {
          this.teachers = res;
        }
      });
  }
  submit() {
    if (this.form.invalid) return;

    const year = this.form.value;
    year.academicYearId = this.yearService.currentAcademicYearId()

    year.schoolGradeId = this.data.gradeId
    console.log(year)

    if (this.isEdit) {
      const id = this.data.model.id;
      this.sctionService.updateSection(id, year)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (res) => {
            this.dialogRef.close(res);
          },
          error: (err) => console.error('Update failed', err)
        });
    } else {
      // إضافة
      this.sctionService.createSection(year)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (res) => {
            this.dialogRef.close(res);
          },
          error: (err) => console.error('Add failed', err)
        });
    }
  }

  cancel() {
    this.dialogRef.close(null);
  }
}
