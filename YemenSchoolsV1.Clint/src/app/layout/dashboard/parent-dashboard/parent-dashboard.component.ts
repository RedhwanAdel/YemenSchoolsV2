import { Component } from '@angular/core';
import { ParentSidebarDashboardComponent } from "./parent-sidebar-dashboard/parent-sidebar-dashboard.component";
import { ParentHeaderDashboardComponent } from "./parent-header-dashboard/parent-header-dashboard.component";
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';
import { Student } from '../../../shared/models/student/student';
import { CommonModule } from '@angular/common';
import { ParentDashboardPageComponent } from "../../../features/parent-dashboard/parent-dashboard-page/parent-dashboard-page.component";

@Component({
  selector: 'app-parent-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    ParentSidebarDashboardComponent,
    ParentHeaderDashboardComponent,
    ParentDashboardPageComponent
  ],
  templateUrl: './parent-dashboard.component.html',
  styleUrl: './parent-dashboard.component.scss'
})
export class ParentDashboardComponent {
  currentView: 'dashboard' | 'profile' = 'dashboard';
  selectedChild: Student | undefined;

  // Mock data - replace with a real service later
  private students: Student[] = [
    {
      id: 1,
      name: 'رانيا أحمد',
      school: 'مدرسة النهضة',
      grade: 'الصف الخامس - الشعبة أ',
      avg: 75,
      last: 85,
      avatar: 'https://i.pravatar.cc/100?img=47',
      attendance: { present: 90, absent: 3, late: 2 },
      timetable: [
        ['السبت', 'رياضيات', '08:00'],
        ['الأحد', 'لغة عربية', '09:00'],
      ],
      grades: [
        { subject: 'الرياضيات', exam: 88, homework: 80, final: 84 },
        { subject: 'اللغة العربية', exam: 92, homework: 90, final: 91 },
      ],
    },
    {
      id: 2,
      name: 'خالد صالح',
      school: 'مدرسة الإبداع',
      grade: 'الصف الثالث - الشعبة ب',
      avg: 85,
      last: 92,
      avatar: 'https://i.pravatar.cc/100?img=48',
      attendance: { present: 95, absent: 1, late: 0 },
      timetable: [
        ['السبت', 'علوم', '08:00'],
        ['الأحد', 'رياضيات', '09:00'],
      ],
      grades: [{ subject: 'العلوم', exam: 75, homework: 80, final: 78 }],
    },
    {
      id: 3,
      name: 'لارا حسن',
      school: 'مدرسة الشروق',
      grade: 'الصف السادس - الشعبة أ',
      avg: 60,
      last: 70,
      avatar: 'https://i.pravatar.cc/100?img=49',
      attendance: { present: 85, absent: 5, late: 4 },
      timetable: [['الاثنين', 'انشطة', '11:00']],
      grades: [{ subject: 'العلوم', exam: 70, homework: 65, final: 68 }],
    },
  ];

  showProfile(id: number) {
    this.selectedChild = this.students.find(s => s.id === id);
    if (this.selectedChild) {
      this.currentView = 'profile';
    }
  }

  toggleDarkMode() {
    document.body.classList.toggle('dark');
  }
}
