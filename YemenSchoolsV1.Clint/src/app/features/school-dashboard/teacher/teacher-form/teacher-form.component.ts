import { Component, inject, input, Input, OnInit } from '@angular/core';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { FormInputComponent } from "../../../../shared/components/form-input/form-input.component";
import { SelectInputComponent } from "../../../../shared/components/select-input/select-input.component";
import { MatCardModule } from '@angular/material/card';
import { TeacherService } from '../../../../core/services/teacher.service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CitiesService } from '../../../../core/services/cities.service';
import { RegionsService } from '../../../../core/services/regions.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { SchoolForUpdate } from '../../../../shared/models/school/school';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { Teacher } from '../../../../shared/models/teachers/teacher';

@Component({
  selector: 'app-teacher-form',
  standalone: true,
  imports: [
    PageWrapperComponent,
    FormInputComponent,
    MatCardModule,
    SelectInputComponent,
    MatIcon,
    ReactiveFormsModule,
    MatButton

  ],
  templateUrl: './teacher-form.component.html',
  styleUrl: './teacher-form.component.scss'
})
export class TeacherFormComponent implements OnInit {


  private fb = inject(FormBuilder)
  private snack = inject(SnackbarService)
  private router = inject(Router)

  teacherService = inject(TeacherService)
  mode = input.required<'add' | 'edit'>();
  @Input() teacherId?: string;

  teacherForm = this.fb.group({
    nameAr: ['', Validators.required],
    nameEn: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phoneNumber: ['', Validators.required],
    address: [''],
    postalCode: [''],
    mainPhone: [''],
    gender: [null as number | null, Validators.required],
    hireDate: [null as string | null, Validators.required],
    specialization: ['', Validators.required],
    employmentStatus: ['', Validators.required], // مثلاً: 'Permanent', 'Contract'
    profilePictureUrl: [''],
  });


  ngOnInit(): void {
    this.initializeForm()
  }


  initializeForm() {
    if (this.mode() === 'edit' && this.teacherId) {
      this.loadTeacherData(this.teacherId);
    }
  }

  loadTeacherData(id: string) {
    this.teacherService.getTeacherById(id).subscribe({
      next: (teacher: Teacher) => {
        this.teacherForm.patchValue({
          nameAr: teacher.name,
          nameEn: teacher.name, // لو عندك nameAr و nameEn عدّلها
          email: teacher.email,
          phoneNumber: teacher.phoneNumber,
          address: teacher.address,
          postalCode: '', // غير موجود بالـ Teacher — أضف إذا لزم الأمر
          mainPhone: '',  // غير موجود بالـ Teacher — أضف إذا لزم الأمر
          gender: teacher.gender,
          hireDate: teacher.hireDate,
          specialization: teacher.specialization,
          employmentStatus: teacher.employmentStatus,
          profilePictureUrl: teacher.profilePictureUrl
        });
      },
      error: (error) => {
        this.snack.error('Failed to load teacher data');
        console.error(error);
      }
    });
  }





  onSumit() {
    if (this.teacherForm.invalid) {
      this.snack.error('Please fill in all required fields.');
      this.teacherForm.markAllAsTouched();
      return;
    }

    const formData = this.teacherForm.value;

    if (this.mode() === 'add') {
      this.teacherService.createTeacher(formData).subscribe({
        next: () => {
          this.snack.success('Teacher created successfully!');
          this.router.navigate(['school-dash-board', 'school-teacher-list']);
        },
        error: (error) => {
          this.snack.error(error.error.Message);
          console.error(error);
        }
      });
    } else if (this.mode() === 'edit' && this.teacherId) {

      this.teacherService.updateTeacher(this.teacherId, formData as SchoolForUpdate).subscribe({
        next: () => {
          this.snack.success('School updated successfully!');
          this.router.navigate(['school-dash-board', 'school-teacher-list']);
        },
        error: (error) => {
          this.snack.error(error.error.Message);
          console.error(error);
        }
      });
    }

  }
  onCancel() {
    this.router.navigate(['school-dash-board', 'school-teacher-list']);
  }
}
