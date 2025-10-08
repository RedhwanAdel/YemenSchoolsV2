import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-school-dash-overview',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatTableModule,
    BaseChartDirective
  ],
  templateUrl: './school-dash-overview.component.html',
  styleUrl: './school-dash-overview.component.scss'
})
export class SchoolDashOverviewComponent {
  // بيانات واقعية جديدة
  totalStudents = 1500;
  totalTeachers = 180;
  totalSubjects = 25;
  avgStudentsPerClass = 30.0;

  // بيانات رسوم بيانية محسنة
  teachersPerSubjectData = {
    labels: ['الرياضيات', 'العلوم', 'اللغة العربية', 'اللغة الإنجليزية', 'الدراسات الاجتماعية', 'الفنون'],
    datasets: [{
      label: 'عدد المعلمين',
      data: [20, 25, 30, 22, 18, 15],
      backgroundColor: [
        '#3f51b5',
        '#e91e63',
        '#ff9800',
        '#4caf50',
        '#9c27b0',
        '#00bcd4'
      ]
    }]
  };

  studentsPerGradeData = {
    labels: ['الصف الأول', 'الصف الثاني', 'الصف الثالث', 'الصف الرابع', 'الصف الخامس'],
    datasets: [{
      label: 'عدد الطلاب',
      data: [250, 280, 300, 350, 320],
      backgroundColor: '#1976d2'
    }]
  };

  // بيانات تفصيلية لكل صف
  gradesSummary = [
    {
      name: 'الصف الأول',
      totalStudents: 250,
      sectionsCount: 8,
      subjectsCount: 10,
      teachersCount: 15
    },
    {
      name: 'الصف الثاني',
      totalStudents: 280,
      sectionsCount: 9,
      subjectsCount: 11,
      teachersCount: 17
    },
    {
      name: 'الصف الثالث',
      totalStudents: 300,
      sectionsCount: 10,
      subjectsCount: 12,
      teachersCount: 20
    }
  ];

  constructor() { }

  ngOnInit(): void { }
}
