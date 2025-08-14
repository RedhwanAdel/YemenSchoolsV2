import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { Student } from '../../../shared/models/student/student';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { CalendarComponent } from "../calendar/calendar.component";
import { ChildCardComponent } from "../child-card/child-card.component";
import { ParentService } from '../../../core/services/parent.service';
import { StudentWithSchoolInfoDto } from '../../../shared/models/parent';

@Component({
  selector: 'app-parent-dashboard-page',
  standalone: true,
  imports: [
    CommonModule, MatCardModule,
    CalendarComponent,
    ChildCardComponent
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
  // students: Student[] = [
  //   {
  //     id: 1,
  //     name: 'رانيا أحمد',
  //     school: 'مدرسة النهضة',
  //     grade: 'الصف الخامس - الشعبة أ',
  //     avg: 75,
  //     last: 85,
  //     avatar: 'https://i.pravatar.cc/100?img=47',
  //     attendance: { present: 90, absent: 3, late: 2 },
  //     timetable: [
  //       ['السبت', 'رياضيات', '08:00'],
  //       ['الأحد', 'لغة عربية', '09:00'],
  //     ],
  //     grades: [
  //       { subject: 'الرياضيات', exam: 88, homework: 80, final: 84 },
  //       { subject: 'اللغة العربية', exam: 92, homework: 90, final: 91 },
  //     ],
  //   },
  //   {
  //     id: 2,
  //     name: 'خالد صالح',
  //     school: 'مدرسة الإبداع',
  //     grade: 'الصف الثالث - الشعبة ب',
  //     avg: 85,
  //     last: 92,
  //     avatar: 'https://i.pravatar.cc/100?img=48',
  //     attendance: { present: 95, absent: 1, late: 0 },
  //     timetable: [
  //       ['السبت', 'علوم', '08:00'],
  //       ['الأحد', 'رياضيات', '09:00'],
  //     ],
  //     grades: [{ subject: 'العلوم', exam: 75, homework: 80, final: 78 }],
  //   },
  //   {
  //     id: 3,
  //     name: 'لارا حسن',
  //     school: 'مدرسة الشروق',
  //     grade: 'الصف السادس - الشعبة أ',
  //     avg: 60,
  //     last: 70,
  //     avatar: 'https://i.pravatar.cc/100?img=49',
  //     attendance: { present: 85, absent: 5, late: 4 },
  //     timetable: [['الاثنين', 'انشطة', '11:00']],
  //     grades: [{ subject: 'العلوم', exam: 70, homework: 65, final: 68 }],
  //   },
  // ];
}
