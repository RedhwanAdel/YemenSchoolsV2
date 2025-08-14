import { Component, inject, input, signal } from '@angular/core';
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
import { StudentService } from '../../../../core/services/student.service';
import { Student } from '../../../../shared/models/student/student';
import { SchoolService } from '../../../../core/services/school.service';
import { SchoolGradeWithDetailsDto } from '../../../../shared/models/school/school';
import { SectionService } from '../../../../core/services/section.service';
import { Section } from '../../../../shared/models/section/section';
import { debounceTime, distinctUntilChanged } from 'rxjs';
import { ParentService } from '../../../../core/services/parent.service';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ParentCheckDto } from '../../../../shared/models/parent';
import { AccountService } from '../../../../core/services/account.service';
import { AcadmicYearService } from '../../../../core/services/acadmic-year.service';
export interface ParentSearchDto {
  id: string;
  name: string;
  phoneNumber: string;
  // يمكنك إضافة خصائص أخرى حسب الحاجة
}
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
    this.schoolService.getSchoolGrade().subscribe({
      next: (res) => this.grades.set(res),
    });
  }

  onGradeChange(gradeId: string) {
    this.sectionService.getSectionsByYearAndGrade(gradeId).subscribe({
      next: (res) => this.sections.set(res.data),
    });
  }

  searchParent() {
    const nid = this.studentForm.get('nationalId')?.value;
    if (!nid) {
      this.snack.error('الرجاء إدخال الرقم الوطني لولي الأمر');
      return;
    }

    this.parentService.checkParentByNationalId(nid).subscribe(res => {
      this.foundParent = res;
      if (res.exists) {
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
    const studentData: any = {
      registerNo: formValue.registerNo,
      nameAr: formValue.nameAr,
      nameEn: formValue.nameEn,
      phoneNumber: formValue.phoneNumber,
      address: formValue.address,
      email: formValue.email,
      nationality: formValue.nationality,
      dateOfBirth: formValue.dateOfBirth,
      gender: formValue.gender,
      currentSectionId: formValue.currentSectionId,
      schoolId: schoolId,
      currentAcademicYearId: yearId,
      parents: []
    };

    if (this.foundParent?.exists) {
      studentData.parents.push({
        parentId: this.foundParent.id,
        relationType: formValue.relationType
      });

      this.createStudent(studentData)
    } else {
      const newParentData = this.studentForm.get('newParent')?.value;
      this.parentService.createParent(newParentData).subscribe({
        next: (parentRes) => {
          studentData.parents.push({
            parentId: parentRes.parentId,
            relationType: newParentData?.relationType
          });
          this.createStudent(studentData);

        }
      })


    }





  }
  createStudent(studentData: any) {
    this.studentService.createStudent(studentData)!.subscribe({
      next: res => {
        this.snack.success('نجح إضافة طالب')
        console.log(studentData)
      },
      error: err => {
        this.snack.error('فشل إضافة طالب')
      }
    });
  }

  onCancel() {
    this.router.navigate(['/students']);
  }
}
