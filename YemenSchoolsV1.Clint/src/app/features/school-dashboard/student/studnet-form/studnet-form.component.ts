import { Component, DestroyRef, inject, input, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { FormInputComponent } from "../../../../shared/components/form-input/form-input.component";
import { SelectInputComponent } from "../../../../shared/components/select-input/select-input.component";
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatRadioModule } from '@angular/material/radio';
import { Router } from '@angular/router';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { StudentService } from '@features/school-dashboard/student/services/student.service';
import { Student, CreateStudentDto } from '@features/school-dashboard/student/models/student';
import { SchoolService } from '@features/schools/services/school.service';
import { SchoolGradeWithDetailsDto } from '@features/schools/models/school';
import { SectionService } from '@features/school-dashboard/section/services/section.service';
import { Section } from '@features/school-dashboard/section/models/section';
import { debounceTime, distinctUntilChanged } from 'rxjs';
import { ParentService } from '@features/parent-dashboard/services/parent.service';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ParentCheckDto } from '@features/parent-dashboard/models/parent';
import { AccountService } from '../../../../core/services/account.service';
import { AcadmicYearService } from '@features/school-dashboard/year/services/acadmic-year.service';

@Component({
  selector: 'app-studnet-form',
  standalone: true,
  imports: [
    CommonModule,
    PageWrapperComponent,
    FormInputComponent,
    SelectInputComponent,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatRadioModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule

  ],
  templateUrl: './studnet-form.component.html',
  styleUrl: './studnet-form.component.scss'
})
export class StudnetFormComponent {
  private destroyRef = inject(DestroyRef);
  private fb = inject(FormBuilder);
  private snack = inject(SnackbarService);
  private router = inject(Router);
  private studentService = inject(StudentService);
  private parentService = inject(ParentService);
  private sectionService = inject(SectionService);
  private schoolService = inject(SchoolService);
  private accountService = inject(AccountService);
  private yearService = inject(AcadmicYearService);

  grades = signal<SchoolGradeWithDetailsDto[]>([]);
  sections = signal<Section[]>([]);

  foundParent: ParentCheckDto | null = null;

  studentForm = this.fb.group({
    registerNo: ['', Validators.required],
    nameAr: ['', Validators.required],
    nameEn: ['', Validators.required],
    phoneNumber: ['', Validators.required],
    address: [''],
    email: ['', [Validators.required, Validators.email]],
    nationality: [''],
    dateOfBirth: [''],
    gender: [null as number | null, Validators.required],
    grade: ['', Validators.required],
    currentSectionId: ['', Validators.required],

    nationalId: ['', Validators.required],
    relationType: [''],

    newParent: this.fb.group({
      nameAr: [''],
      nameEn: [''],
      phoneNumber: [''],
      address: [''],
      jobTitle: [''],
      dateOfBirth: [''],
      nationalId: [''],
      gender: [1],
      email: ['', Validators.email],
      relationType: [''],
    })
  });

  ngOnInit(): void {
    this.loadGrades();
  }

  loadGrades() {
    this.schoolService.getSchoolGrade()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (res) => this.grades.set(res),
      });
  }

  onGradeChange(gradeId: string) {
    this.sectionService.getSectionsByGrade(gradeId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (res: Section[]) => this.sections.set(res),
      });
  }

  searchParent() {
    const nid = this.studentForm.get('nationalId')?.value;
    if (!nid) {
      this.snack.error('الرجاء إدخال الرقم الوطني لولي الأمر');
      return;
    }

    this.parentService.checkParentByNationalId(nid)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((res) => {
        this.foundParent = res.data;
        if (res.data.exists) {
          this.studentForm.get('newParent')?.disable();
        } else {
          this.studentForm.get('newParent')?.enable();
        }
      });
  }

  onSubmit() {
    if (this.studentForm.invalid) {
      this.snack.error('الرجاء تعبئة جميع الحقول المطلوبة.');
      this.studentForm.markAllAsTouched();
      return;
    }
    const schoolId = this.accountService.currentUser()?.schoolId;
    const yearId = this.yearService.currentAcademicYearId();
    if (!schoolId || !yearId) {
      this.snack.error('لا يمكن إضافة طالب بدون مدرسة أو سنة دراسية');
      return;
    }

    const formValue = this.studentForm.value;
    const studentData: CreateStudentDto = {
      registerNo: formValue.registerNo!,
      nameAr: formValue.nameAr!,
      nameEn: formValue.nameEn!,
      phoneNumber: formValue.phoneNumber!,
      address: formValue.address!,
      email: formValue.email!,
      nationality: formValue.nationality!,
      dateOfBirth: formValue.dateOfBirth!,
      gender: formValue.gender!,
      currentSectionId: formValue.currentSectionId!,
      schoolId: schoolId,
      currentAcademicYearId: yearId,
      parents: []
    };

    if (this.foundParent?.exists && this.foundParent.id) {
      studentData.parents.push({
        parentId: this.foundParent.id,
        relationType: formValue.relationType || ''
      });
      this.createStudent(studentData);
    } else {
      const newParentData = this.studentForm.get('newParent')?.value;
      this.parentService.createParent(newParentData)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (parentRes: any) => {
            studentData.parents.push({
              parentId: parentRes.parentId,
              relationType: newParentData?.relationType || ''
            });
            this.createStudent(studentData);
          }
        });
    }
  }
  createStudent(studentData: CreateStudentDto) {
    this.studentService.createStudent(studentData)!
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: res => {
          this.snack.success('نجح إضافة طالب');
          console.log(studentData);
        },
        error: err => {
          this.snack.error('فشل إضافة طالب');
        }
      });
  }

  onCancel() {
    this.router.navigate(['/students']);
  }
}
