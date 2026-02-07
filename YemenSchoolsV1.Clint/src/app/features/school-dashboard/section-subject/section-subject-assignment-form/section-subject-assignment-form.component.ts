import { Component, DestroyRef, inject, Inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { SelectInputComponent } from "../../../../shared/components/select-input/select-input.component";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { TeacherService } from '@features/school-dashboard/teacher/services/teacher.service';
import { Teacher } from '@features/school-dashboard/teacher/models/teachers';
import { AccountService } from '../../../../core/services/account.service';
import { TermService } from '@features/school-dashboard/term/services/term.service';
import { Term } from '@features/school-dashboard/term/models/term';
import { SchoolService } from '@features/schools/services/school.service';
import { Subject } from '@features/schools/models/school';
import { ActivatedRoute } from '@angular/router';
import { SectionSubjectService } from '@features/school-dashboard/section-subject/services/section-subject.service';
import { Section } from '@features/school-dashboard/section/models/section';
import { AcadmicYearService } from '@features/school-dashboard/year/services/acadmic-year.service';

@Component({
  selector: 'app-section-subject-assignment-form',
  standalone: true,
  imports: [ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,

    SelectInputComponent],
  templateUrl: './section-subject-assignment-form.component.html',
  styleUrl: './section-subject-assignment-form.component.scss'
})
export class SectionSubjectAssignmentFormComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  form!: FormGroup;
  isEdit = false;
  private route = inject(ActivatedRoute);

  teacherService = inject(TeacherService)
  yearService = inject(AcadmicYearService)
  termService = inject(TermService)
  schoolService = inject(SchoolService)
  sectionSubjectService = inject(SectionSubjectService)
  private accountService = inject(AccountService)

  teachrs = signal<Teacher[]>([])
  terms = signal<Term[]>([])
  subjects = signal<Subject[]>([])

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<SectionSubjectAssignmentFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { model?: any, currentSection: Section }
  ) {

  }

  ngOnInit() {
    console.log(this.data.model)
    this.loadSubjects()
    this.loadTeachrs()
    this.loadTerms()

    this.buildForm()



  }

  buildForm() {
    this.isEdit = !!this.data?.model;

    this.form = this.fb.group({
      gradeSubjectId: [this.data?.model?.gradeSubjectId || '', Validators.required],
      termId: [this.data?.model?.termId || '', Validators.required],
      teacherId: [this.data?.model?.teacherId || '', Validators.required],

    });
  }

  loadTeachrs() {
    const schoolId = this.accountService.currentUser()?.schoolId
    if (schoolId) {
      this.teacherService.getTeachers(schoolId)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: res => {
            this.teachrs.set(res);
          }
        });
    }
  }
  loadTerms() {
    const yearId = this.yearService.currentAcademicYearId()!;
    this.termService.getTerms(yearId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: res => {
          this.terms.set(res);
        }
      });

  }

  loadSubjects() {
    const gradeId = this.data.currentSection.schoolGradeId;
    if (!gradeId) return;
    this.schoolService.getSubjectsForSchoolGrade(gradeId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: res => {
          this.subjects.set(res);
        }
      });

  }

  submit() {
    if (this.form.invalid) return;

    const sectionSubject = this.form.value;
    const sectionId = this.data.currentSection.id

    sectionSubject.sectionId = sectionId;

    if (this.isEdit) {
      const id = this.data.model.id;
      this.sectionSubjectService.update(id, sectionSubject)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (res: any) => {
            this.dialogRef.close(res);
          },
          error: (err: any) => console.error('Update failed', err)
        });
    } else {
      // إضافة
      this.sectionSubjectService.create(sectionSubject)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (res: any) => {
            this.dialogRef.close(res);
          },
          error: (err: any) => console.error('Add failed', err)
        });
    }
  }

  cancel() {
    this.dialogRef.close(null);
  }
}
