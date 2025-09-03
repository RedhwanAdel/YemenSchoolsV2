import { Component, inject, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { StudentReport } from '../../../shared/models/reports/studentReport';
import { StudentWithSchoolInfoDto } from '../../../shared/models/parent';
import { SubjectReportDto } from '../../../shared/models/mark/mark';
import { ParentService } from '../../../core/services/parent.service';
import { MarkService } from '../../../core/services/mark.service';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { MatDivider, MatDividerModule } from '@angular/material/divider';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';

@Component({
  selector: 'app-student-mark-report',
  standalone: true,
  imports: [
    MatCardModule,
    MatTableModule,
    MatExpansionModule,
    MatMenuModule,
    MatButtonModule,
    MatDividerModule,
    CommonModule
  ],
  templateUrl: './student-mark-report.component.html',
  styleUrl: './student-mark-report.component.scss'
})
export class StudentMarkReportComponent implements OnInit {

  parentService = inject(ParentService)
  markService = inject(MarkService)
  studentsWithSchoolInfo: StudentWithSchoolInfoDto[] = [];
  reports: StudentReport[] = [];
  displayedColumns: string[] = ['name', 'score', 'grade', 'details'];

  ngOnInit(): void {
    this.loadReports()
  }
  loadReports() {
    this.parentService.GetStudentsWithSchoolInfoForParent().subscribe(res => {
      this.studentsWithSchoolInfo = res.data;

      this.studentsWithSchoolInfo.forEach(student => {
        this.markService.getStudentSubjectsReport(student.studentId).subscribe(subjects => {

          const totalScore = subjects.reduce((sum, sub) => sum + sub.score, 0);

          const maxTotal = subjects.reduce((sum, sub) => {
            // مجموع درجات كل اختبار في المادة
            const subMax = sub.details.grades.reduce((s, d) => s + d.total, 0);
            return sum + subMax;
          }, 0);

          const percentage = ((totalScore / maxTotal) * 100).toFixed(0) + '%';

          const report: StudentReport = {
            student: {
              studentId: student.studentId,
              name: student.studentName,
              school: student.schoolName!,
              grade: student.className!,
              section: student.sectionName!
            },
            subjects: subjects,
            final: {
              total: totalScore,
              maxTotal: maxTotal,
              percentage: percentage,
              grade: this.getGradeFromPercentage(parseFloat(percentage))
            }
          };

          this.reports.push(report);
        });
      });
    });
  }

  getGradeFromPercentage(percentage: number): string {
    if (percentage >= 90) return 'ممتاز';
    if (percentage >= 75) return 'جيد جدًا';
    if (percentage >= 60) return 'جيد';
    if (percentage >= 50) return 'مقبول';
    return 'ضعيف';
  }

}
