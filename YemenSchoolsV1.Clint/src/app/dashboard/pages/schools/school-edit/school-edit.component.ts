import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { FormInputComponent } from '../../../../shared/components/form-input/form-input.component';

@Component({
  selector: 'app-school-edit',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    PageWrapperComponent,
    FormInputComponent
  ],
  templateUrl: './school-edit.component.html',
  styleUrl: './school-edit.component.scss'
})
export class SchoolEditComponent implements OnInit {
  @Output() schoolAdded = new EventEmitter<any>();
  @Output() cancelAdd = new EventEmitter<void>();

  schoolForm!: FormGroup;

  constructor(private fb: FormBuilder) { }

  ngOnInit() {

    this.schoolForm = this.fb.group({
      id: [''],
      name: ['', Validators.required],
      address: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      zipCode: ['', Validators.required],
      phone: ['', [Validators.required, Validators.pattern(/^(\+?\d{1,4})?\s?-?\d{7,15}$/)]],
      email: ['', [Validators.required, Validators.email]],
      principal: ['', Validators.required],
      foundedYear: [new Date().getFullYear(), [
        Validators.required,
        Validators.min(1800),
        Validators.max(new Date().getFullYear())
      ]],
      studentsCount: [0, [Validators.required, Validators.min(0)]],
      website: ['', Validators.pattern(/^(https?:\/\/)?([\w\d\-]+\.)+\w{2,}(\/.+)?$/)]
    });
  }

}
