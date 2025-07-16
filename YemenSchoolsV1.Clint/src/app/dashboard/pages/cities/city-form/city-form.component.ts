import { NgIf } from '@angular/common';
import { Component, inject, Inject, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormInputComponent } from "../../../../shared/components/form-input/form-input.component";
import { CitiesService } from '../../../../core/services/cities.service';

@Component({
  selector: 'app-city-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormInputComponent
  ],
  templateUrl: './city-form.component.html',
  styleUrl: './city-form.component.scss'
})
export class CityFormComponent implements OnInit {
  form!: FormGroup;
  isEdit = false;
  cityService = inject(CitiesService)
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
      imagePath: [null]  // File input
    });


  }


  submit() {
    if (this.form.invalid) return;

    const city = this.form.value;

    if (this.isEdit) {
      const id = this.data.model.id;
      this.cityService.updateCity(id, city).subscribe({
        next: (res) => {
          this.dialogRef.close(res)
          this.cityService.getCites()
        },
        error: (err) => console.error('Update failed', err)
      });
    } else {
      // إضافة
      console.log(city)
      this.cityService.createCity(city).subscribe({
        next: (res) => this.dialogRef.close(res),
        error: (err) => console.error('Add failed', err)
      });
    }
  }

  cancel() {
    this.dialogRef.close(null);
  }
}
