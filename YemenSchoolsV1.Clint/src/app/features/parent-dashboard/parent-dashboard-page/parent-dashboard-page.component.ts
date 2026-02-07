import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { Student } from '@features/school-dashboard/student/models/student';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { CalendarComponent } from "../calendar/calendar.component";
import { ChildCardComponent } from "../child-card/child-card.component";
import { ParentService } from '@features/parent-dashboard/services/parent.service';
import { StudentWithSchoolInfoDto } from '@features/parent-dashboard/models/parent';
import { RouterOutlet } from "../../../../../node_modules/@angular/router/index";

@Component({
  selector: 'app-parent-dashboard-page',
  standalone: true,
  imports: [
    CommonModule, MatCardModule,
    CalendarComponent,
    ChildCardComponent,
  ],
  templateUrl: './parent-dashboard-page.component.html',
  styleUrl: './parent-dashboard-page.component.scss'
})
export class ParentDashboardPageComponent implements OnInit {
  @Output() openChildProfile = new EventEmitter<string>();
  @Output() sendMessage = new EventEmitter<number>();

  parentService = inject(ParentService)
  students: StudentWithSchoolInfoDto[] = []

  ngOnInit(): void {
    this.parentService.GetStudentsWithSchoolInfoForParent().subscribe({
      next: res => {
        this.students = res.data
        console.log(this.students)
      }
    })
  }

}
