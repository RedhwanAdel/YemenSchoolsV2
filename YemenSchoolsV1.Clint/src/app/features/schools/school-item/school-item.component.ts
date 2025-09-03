import { Component, inject, input, OnInit, signal } from '@angular/core';
import { SchoolService } from '../../../core/services/school.service';
import { Pagination } from '../../../shared/models/Pagination';
import { SchoolListItem } from '../../../shared/models/school/school';
import { SchoolParams } from '../../../shared/models/school/schoolParams';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-school-item',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    RouterLink,
    CommonModule,

  ],
  templateUrl: './school-item.component.html',
  styleUrl: './school-item.component.scss'
})
export class SchoolItemComponent {
  genderLabels: { [key: string]: string } = {
    Boys: 'بنين',
    Girls: 'بنات',
    Both: 'بنين وبنات'
  };
  schoolType: { [key: string]: string } =
    {
      Private: 'خاص',
      Public: 'عام'
    };
  schoolLevel: { [key: string]: string } =
    {
      None: '',
      Kindergarten: 'روضة',
      Elementary: 'ابتدائي',
      Middle: 'متوسط',
      High: 'ثانوي'
    };
  curriculumType: { [key: string]: string } =
    {
      National: 'وطني',
      International: 'دولي',
      Hybrid: 'مشترك',
      Arabic: 'عربي',
      American: 'أمريكي',
      British: 'بريطاني'
    };

  school = input.required<SchoolListItem>()
  get logo() {
    return this.school().logo
  }
  roundRating(rating: number): number {
    return Math.round(rating);
  }
}
