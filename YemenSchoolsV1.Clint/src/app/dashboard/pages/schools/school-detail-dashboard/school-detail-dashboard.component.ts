import { CommonModule } from '@angular/common';
import { Component, inject, Input, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatListModule } from '@angular/material/list';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { SchoolForUpdate } from '../../../../shared/models/school/school';
import { SchoolService } from '../../../../core/services/school.service';

interface School {
  id: string;
  name: string;
  address: string;
  city: string;
  state: string;
  zipCode: string;
  phone: string;
  email: string;
  principal: string;
  foundedYear: number;
  studentsCount: number;
  website: string;
}
@Component({
  selector: 'app-school-detail-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatDividerModule,
    PageWrapperComponent
  ],
  templateUrl: './school-detail-dashboard.component.html',
  styleUrl: './school-detail-dashboard.component.scss'
})
export class SchoolDetailDashboardComponent {
  school?: SchoolForUpdate;
  private route = inject(ActivatedRoute)
  schoolId!: string;

  schoolService = inject(SchoolService)
  ngOnInit(): void {
    this.schoolId = this.route.snapshot.paramMap.get('id')!;

    this.schoolService.getSchoolByIdForUpdate(this.schoolId).subscribe({
      next: school => this.school = school
    })

  }

}
