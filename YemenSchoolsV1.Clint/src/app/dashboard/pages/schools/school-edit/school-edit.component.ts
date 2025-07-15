import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { FormInputComponent } from '../../../../shared/components/form-input/form-input.component';
import { SchoolFormComponent } from "../school-form/school-form.component";
import { ActivatedRoute } from '@angular/router';

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
    SchoolFormComponent
  ],
  templateUrl: './school-edit.component.html',
  styleUrl: './school-edit.component.scss'
})
export class SchoolEditComponent implements OnInit {
  private route = inject(ActivatedRoute)
  schoolId!: string;

  ngOnInit(): void {
    this.schoolId = this.route.snapshot.paramMap.get('id')!;
  }
}
