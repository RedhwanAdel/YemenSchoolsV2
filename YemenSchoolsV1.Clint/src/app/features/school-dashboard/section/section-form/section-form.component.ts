import { Component, inject, Inject, OnInit } from '@angular/core';
import { FormInputComponent } from "../../../../shared/components/form-input/form-input.component";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { SectionService } from '../../../../core/services/section.service';
import { AcadmicYearService } from '../../../../core/services/acadmic-year.service';

@Component({
  selector: 'app-section-form',
  standalone: true,
  imports: [ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormInputComponent],
  templateUrl: './section-form.component.html',
  styleUrl: './section-form.component.scss'
})
export class SectionFormComponent implements OnInit {
  form!: FormGroup;
  isEdit = false;
  sctionService = inject(SectionService)
  yearService = inject(AcadmicYearService)
  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<SectionFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {
      model?: any,
      gradeId: string,
      yearId: string
    }
  ) { }

  ngOnInit() {
    this.isEdit = !!this.data?.model;

    this.form = this.fb.group({
      name: [this.data?.model?.name || '', Validators.required],
      capacity: [this.data?.model?.capacity || '', Validators.required],

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
      this.sctionService.updateSection(id, year).subscribe({
        next: (res) => {
          this.dialogRef.close(res)
        },
        error: (err) => console.error('Update failed', err)
      });
    } else {
      // إضافة
      this.sctionService.createSection(year).subscribe({
        next: (res) => {
          this.dialogRef.close(res)
        },
        error: (err) => console.error('Add failed', err)
      });
    }
  }

  cancel() {
    this.dialogRef.close(null);
  }
}
