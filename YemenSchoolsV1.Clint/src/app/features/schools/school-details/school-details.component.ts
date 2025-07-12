import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { BrowserModule } from '@angular/platform-browser';
import { MatTabsModule } from '@angular/material/tabs';
import { MatDividerModule } from '@angular/material/divider';
import { MatGridListModule } from '@angular/material/grid-list'; // For gallery
import { MatBadgeModule } from '@angular/material/badge';
import { MatExpansionModule } from '@angular/material/expansion';
import { SchoolOverviewComponent } from "./school-overview/school-overview.component";
import { SchoolAcademicsComponent } from "./school-academics/school-academics.component";
import { SchoolAdmissionsComponent } from "./school-admissions/school-admissions.component";
import { SchoolCampusFacilitiesComponent } from "./school-campus-facilities/school-campus-facilities.component";
import { SchoolNewsEventsComponent } from "./school-news-events/school-news-events.component";
import { SchoolContactUsComponent } from "./school-contact-us/school-contact-us.component"; // <--- NEW!
import { SchoolService } from '../../../core/services/school.service';
import { ActivatedRoute } from '@angular/router';
import { SchoolDetails } from '../../../shared/models/school/schoolDetails';



@Component({
  selector: 'app-school-details',
  standalone: true,
  imports: [
    MatCardModule,
    MatButtonModule,
    MatTabsModule, // Add this
    MatIconModule, // Add this
    MatDividerModule, // Add this
    MatExpansionModule // Add this
    ,
    SchoolOverviewComponent,
    SchoolAcademicsComponent,
    SchoolAdmissionsComponent,
    SchoolCampusFacilitiesComponent,
    SchoolNewsEventsComponent,
    SchoolContactUsComponent
  ],
  templateUrl: './school-details.component.html',
  styleUrl: './school-details.component.scss'
})
export class SchoolDetailsComponent implements OnInit {
  schoolService = inject(SchoolService)
  activatedRoute = inject(ActivatedRoute)
  school?: SchoolDetails


  ngOnInit(): void {
    this.loadSchool();
  }
  loadSchool() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return;
    this.schoolService.getSchoolById(id).subscribe({
      next: school => {
        this.school = school.data
      },
      error: error => console.log(error)
    })

  }
}
