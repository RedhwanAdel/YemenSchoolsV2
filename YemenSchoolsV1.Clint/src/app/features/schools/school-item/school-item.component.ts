import { Component, inject, input, OnInit, signal } from '@angular/core';
import { SchoolService } from '../../../core/services/school.service';
import { Pagination } from '../../../shared/models/Pagination';
import { SchoolListItem } from '../../../shared/models/school/school';
import { SchoolParams } from '../../../shared/models/school/schoolParams';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-school-item',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    RouterLink
  ],
  templateUrl: './school-item.component.html',
  styleUrl: './school-item.component.scss'
})
export class SchoolItemComponent {
  genderLabels: { [key: string]: string } = {
    Boys: 'Boys',
    Girls: 'Girls',
    Both: 'Boys & Girls'
  };

  school = input.required<SchoolListItem>()
  get logo() {
    return this.school().logo
  }
}
