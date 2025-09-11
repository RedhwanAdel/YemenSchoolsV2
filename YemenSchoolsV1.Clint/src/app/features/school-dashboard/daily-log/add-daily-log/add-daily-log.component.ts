import { CommonModule } from '@angular/common';
import { Component, inject, Inject, Input } from '@angular/core';
import { ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { SectionSubject } from '../../../../shared/models/mark/mark';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { DailyLogService } from '../../../../core/services/daily-log.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';

@Component({
  selector: 'app-add-daily-log',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatDialogModule
  ],
  templateUrl: './add-daily-log.component.html',
  styleUrl: './add-daily-log.component.scss'
})
export class AddDailyLogComponent {
  dailyLogForm: FormGroup;
  dailyLogService = inject(DailyLogService)
  snack = inject(SnackbarService)
  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<AddDailyLogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { sectionSubject: SectionSubject }
  ) {
    this.dailyLogForm = this.fb.group({
      date: [new Date(), Validators.required],
      lessonCovered: ['', Validators.required],
      homeworkAssigned: ['', Validators.required],
      teacherNotes: ['']
    });
  }

  onSubmit() {
    console.log(this.data.sectionSubject.sectionName)
    if (this.dailyLogForm.valid) {
      const payload = {
        ...this.dailyLogForm.value,
        sectionSubjectId: this.data.sectionSubject.id
      };
      this.dailyLogService.createDailyLog(payload).subscribe({
        next: res => {
          this.snack.success('تم انشاء السجل بنجاح')
        }
      })

      this.dialogRef.close(payload); // إغلاق الـ Dialog وإرجاع البيانات
    }
  }

  onCancel() {
    this.dialogRef.close(); // إغلاق الـ Dialog بدون حفظ
  }

}
