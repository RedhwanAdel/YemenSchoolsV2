import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatListModule } from '@angular/material/list';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";

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
  @Input() school: School | undefined; // Input property to receive school data

  ngOnInit(): void {
    // You can add initialization logic here if needed
    // For demonstration, if no school data is passed, we can populate a dummy one
    if (!this.school) {
      this.school = {
        id: 'SCH001',
        name: 'Harmony Public School',
        address: '123 Education Lane',
        city: 'Metropolis',
        state: 'NY',
        zipCode: '10001',
        phone: '(555) 123-4567',
        email: 'info@harmonyschool.edu',
        principal: 'Dr. Eleanor Vance',
        foundedYear: 1998,
        studentsCount: 750,
        website: 'https://www.harmonyschool.edu'
      };
    }
  }

  // --- Action methods (can be expanded) ---

  onEdit(): void {
    console.log('Edit School:', this.school?.name);
    // Implement navigation to an edit form or open a dialog
    alert('Edit functionality not yet implemented for ' + this.school?.name);
  }

  onDelete(): void {
    console.log('Delete School:', this.school?.name);
    // Implement confirmation dialog and deletion logic
    const confirmDelete = confirm(`Are you sure you want to delete ${this.school?.name}?`);
    if (confirmDelete) {
      alert('Delete functionality not yet implemented.');
    }
  }

  onContact(): void {
    console.log('Contact School:', this.school?.name);
    // Implement logic to open email client or show contact details
    alert('Contact functionality not yet implemented.');
  }
}
