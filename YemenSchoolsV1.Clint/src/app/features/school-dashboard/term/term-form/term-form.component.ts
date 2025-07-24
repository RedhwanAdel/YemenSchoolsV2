import { Component, inject, Inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormInputComponent } from '../../../../shared/components/form-input/form-input.component';
import { TermService } from '../../../../core/services/term.service';
import { AcadmicYearService } from '../../../../core/services/acadmic-year.service';
import { CityFormComponent } from '../../../../dashboard/pages/cities/city-form/city-form.component';
import { AccountService } from '../../../../core/services/account.service';
import { YearDto } from '../../../../shared/models/AcademicYear/AcademicYear';
import { SelectInputComponent } from '../../../../shared/components/select-input/select-input.component';

@Component({
  selector: 'app-term-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormInputComponent,
    SelectInputComponent
  ],
  templateUrl: './term-form.component.html',
  styleUrl: './term-form.component.scss'
})
export class TermFormComponent implements OnInit {
  form!: FormGroup;
  isEdit = false;
  yearService = inject(AcadmicYearService)
  termService = inject(TermService)
  private accountService = inject(AccountService)

  years = signal<YearDto[]>([])

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CityFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { model?: any }
  ) {

  }

  ngOnInit() {
    this.loadYears()
    console.log(this.years())

    this.buildForm()
    console.log(this.years())



  }

  buildForm() {
    this.isEdit = !!this.data?.model;

    this.form = this.fb.group({
      name: [this.data?.model?.name || '', Validators.required],
      academicYearId: [this.data?.model?.academicYearId || '', Validators.required],
      startDate: [this.formatDate(this.data?.model?.startDate), Validators.required],
      endDate: [this.formatDate(this.data?.model?.endDate), Validators.required],
    });
  }

  loadYears() {
    const schoolId = this.accountService.currentUser()?.schoolId
    if (schoolId) {
      this.yearService.getAcademicYears(schoolId).subscribe({
        next: res => {
          this.years.set(res.data)

        }
      })
    }
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

    const term = this.form.value;

    if (this.isEdit) {
      const id = this.data.model.id;
      this.termService.updateTerm(id, term).subscribe({
        next: (res) => {
          this.dialogRef.close(res)
        },
        error: (err) => console.error('Update failed', err)
      });
    } else {
      // إضافة
      this.termService.createTerm(term).subscribe({
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
