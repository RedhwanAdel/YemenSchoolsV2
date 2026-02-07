import { Component, DestroyRef, EventEmitter, inject, Inject, OnInit, Output } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormInputComponent } from "../../../../shared/components/form-input/form-input.component";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AcadmicYearService } from '@features/school-dashboard/year/services/acadmic-year.service';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { CityFormComponent } from '../../../../dashboard/pages/cities/city-form/city-form.component';
import { AccountService } from '../../../../core/services/account.service';
import { SelectInputComponent } from "../../../../shared/components/select-input/select-input.component";
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-year-form',
  standalone: true,
  imports: [ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormInputComponent],
  templateUrl: './year-form.component.html',
  styleUrl: './year-form.component.scss'
})
export class YearFormComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  form!: FormGroup;
  isEdit = false;
  yearService = inject(AcadmicYearService)
  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CityFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { model?: any }
  ) { }

  ngOnInit() {
    this.isEdit = !!this.data?.model;

    this.form = this.fb.group({
      name: [this.data?.model?.name || '', Validators.required],
      startDate: [this.formatDate(this.data?.model?.startDate), Validators.required],
      endDate: [this.formatDate(this.data?.model?.endDate), Validators.required],
    });


  }

  formatDate(dateString: string | Date | undefined): string | null {
    if (!dateString) return null;

    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = (`0${date.getMonth() + 1}`).slice(-2); // شهر يبدأ من 0
    const day = (`0${date.getDate()}`).slice(-2);

    return `${year}-${month}-${day}`;
  }

  submit() {
    if (this.form.invalid) return;

    const year = this.form.value;

    if (this.isEdit) {
      const id = this.data.model.id;
      this.yearService.updateAcademicYear(id, year)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (res) => {
            this.dialogRef.close(res);
          },
          error: (err) => console.error('Update failed', err)
        });
    } else {
      // إضافة
      this.yearService.createAcademicYear(year)
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
