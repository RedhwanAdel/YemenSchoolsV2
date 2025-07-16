import { Component, inject, Inject, OnInit } from '@angular/core';
import { FormInputComponent } from "../../../../shared/components/form-input/form-input.component";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CityFormComponent } from '../../cities/city-form/city-form.component';
import { SelectInputComponent } from "../../../../shared/components/select-input/select-input.component";
import { CitiesService } from '../../../../core/services/cities.service';
import { RegionsService } from '../../../../core/services/regions.service';

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
export class RegionFormComponent implements OnInit {
  form!: FormGroup;
  isEdit = false;
  cityService = inject(CitiesService)
  regionService = inject(RegionsService)

  options = ['aden', 'sana', 'aben']
  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CityFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { model?: any }
  ) { }

  ngOnInit() {
    this.initializeForm()


  }

  initializeForm() {
    this.cityService.getCites()
    this.isEdit = !!this.data?.model;

    this.form = this.fb.group({
      nameAr: [this.data?.model?.nameAr || '', Validators.required],
      nameEn: [this.data?.model?.nameEn || '', Validators.required],
      cityId: [this.data?.model?.cityId || '', Validators.required],
      imagePath: [null]
    });


  }
  submit() {
    if (this.form.invalid) return;

    const region = this.form.value;

    if (this.isEdit) {
      const id = this.data.model.id;
      this.regionService.updateRegion(id, region).subscribe({
        next: (res) => {
          this.dialogRef.close(res)
          this.regionService.getRegions()
        },
        error: (err) => console.error('Update failed', err)
      });
    } else {
      // إضافة
      this.regionService.createRegion(region).subscribe({
        next: (res) => {
          this.regionService.getRegions()
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
