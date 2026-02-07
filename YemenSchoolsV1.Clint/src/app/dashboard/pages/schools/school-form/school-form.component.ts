import { Component, DestroyRef, inject, Input, input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatCardActions } from '@angular/material/card';
import { MatIcon } from '@angular/material/icon';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { FormInputComponent } from "../../../../shared/components/form-input/form-input.component";
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { SelectInputComponent } from "../../../../shared/components/select-input/select-input.component";
import { CitiesService } from '../../../../core/services/cities.service';
import { RegionsService } from '../../../../core/services/regions.service';
import { SchoolService } from '@features/schools/services/school.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { CreateSchoolDto } from '@features/schools/models/schoolCommand';
import { Router } from '@angular/router';
import { SchoolForUpdate } from '@features/schools/models/school';
type CreateSchoolForm = {
  [K in keyof CreateSchoolDto]: FormControl<CreateSchoolDto[K]>;
};

@Component({
  selector: 'app-school-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatCardActions,
    MatIcon,
    PageWrapperComponent,
    MatButton,
    FormInputComponent,
    SelectInputComponent
  ],
  templateUrl: './school-form.component.html',
  styleUrl: './school-form.component.scss'
})
export class SchoolFormComponent implements OnInit {
  private destroyRef = inject(DestroyRef);

  private fb = inject(FormBuilder)
  private snack = inject(SnackbarService)
  private router = inject(Router)

  citeService = inject(CitiesService)
  regionService = inject(RegionsService)
  schoolService = inject(SchoolService)
  mode = input.required<'add' | 'edit'>();
  @Input() schoolId?: string;

  schoolForm = this.fb.group({
    nameAr: ['', Validators.required],
    nameEn: ['', Validators.required],
    addressAr: [''],
    addressEn: [''],
    postalCode: [''],
    mainPhone: [''],
    email: ['', [Validators.email]],
    schoolType: [0],
    genderType: [0],
    curriculumType: [0],
    schoolLevel: [0],
    cityId: ['', Validators.required],
    regionId: ['', Validators.required]
  });


  ngOnInit(): void {
    this.initializeForm()
  }


  initializeForm() {
    this.citeService.getCites()
    if (this.mode() === 'edit' && this.schoolId) {
      this.loadSchoolData(this.schoolId);
    }
  }
  onCitiesChange(value: any) {
    this.regionService.getRegionsByCity(value)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe();
  }
  loadSchoolData(id: string) {
    this.schoolService.getSchoolByIdForUpdate(id)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (school) => {
          this.schoolForm.patchValue({
            nameAr: school.nameAr,
            nameEn: school.nameEn,
            addressAr: school.addressAr,
            addressEn: school.addressEn,
            postalCode: school.postalCode,
            mainPhone: school.mainPhone,
            email: school.email,
            schoolType: school.schoolType,
            genderType: school.genderType,
            curriculumType: school.curriculumType,
            schoolLevel: school.schoolLevel,
            cityId: school.cityId,
            regionId: school.regionId,
          });
          this.regionService.getRegionsByCity(school.cityId)
            .pipe(takeUntilDestroyed(this.destroyRef))
            .subscribe();
        },
        error: (error) => {
          this.snack.error('Failed to load school data');
          console.error(error);
        }
      });
  }




  onSubmit() {
    if (this.schoolForm.invalid) {
      this.snack.error('Please fill in all required fields.');
      this.schoolForm.markAllAsTouched();
      return;
    }

    const formData = this.schoolForm.value;

    if (this.mode() === 'add') {
      this.schoolService.createSchool(formData as CreateSchoolDto)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: () => {
            this.snack.success('School created successfully!');
            this.router.navigate(['dash-board', 'schools']);
          },
          error: (error) => {
            this.snack.error(error.error.Message);
            console.error(error);
          }
        });
    } else if (this.mode() === 'edit' && this.schoolId) {

      this.schoolService.updateSchoolForAdmin(this.schoolId, formData as SchoolForUpdate)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: () => {
            this.snack.success('School updated successfully!');
            this.router.navigate(['dash-board', 'schools']);
          },
          error: (error) => {
            this.snack.error(error.error.Message);
            console.error(error);
          }
        });
    }

  }
  onCancel() {
    this.router.navigate(['dash-board', 'schools']);
  }
}
