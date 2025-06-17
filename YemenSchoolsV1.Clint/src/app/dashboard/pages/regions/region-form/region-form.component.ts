import { Component, Inject } from '@angular/core';
import { FormInputComponent } from "../../../../shared/components/form-input/form-input.component";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CityFormComponent } from '../../cities/city-form/city-form.component';
import { SelectInputComponent } from "../../../../shared/components/select-input/select-input.component";

@Component({
  selector: 'app-region-form',
  standalone: true,
  imports: [ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormInputComponent, SelectInputComponent],
  templateUrl: './region-form.component.html',
  styleUrl: './region-form.component.scss'
})
export class RegionFormComponent {
  form!: FormGroup;
  isEdit = false;
  options = ['aden', 'sana', 'aben']
  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CityFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { model?: any }
  ) { }

  ngOnInit() {
    this.isEdit = !!this.data?.model;

    this.form = this.fb.group({
      nameAr: [this.data?.model?.nameAr || '', Validators.required],
      nameEn: [this.data?.model?.nameEn || '', Validators.required],
      city: [this.data?.model?.city || '', Validators.required],
      imagePath: [null]  // File input
    });


  }


  submit() {
    if (this.form.invalid) return;
    this.dialogRef.close(this.form.value);
  }

  cancel() {
    this.dialogRef.close(null);
  }
}
