import { Component, DestroyRef, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { CommonModule, CurrencyPipe } from '@angular/common';
import html2pdf from 'html2pdf.js';
import { MatButtonModule } from '@angular/material/button';
import { SubjectReportDto } from '@features/school-dashboard/mark/models/mark';
import { MarkService } from '@features/school-dashboard/mark/services/mark.service';
import { ActivatedRoute } from '@angular/router';
import { SnackbarService } from '../../../core/services/snackbar.service';

@Component({
  selector: 'app-child-report',
  standalone: true,
  imports: [
    MatCardModule,
    MatTableModule,
    MatButtonModule,
    CommonModule

  ],
  templateUrl: './child-report.component.html',
  styleUrl: './child-report.component.scss'
})
export class ChildReportComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  @ViewChild('reportContent', { static: false }) reportContent!: ElementRef;
  markService = inject(MarkService)
  private route = inject(ActivatedRoute);
  snackbar = inject(SnackbarService)

  studentId: string | null = null; // تختار الطالب الحالي (ممكن تجيبه من route param)
  subjects: any[] = [];


  ngOnInit(): void {

    this.loadReport();
  }

  loadReport() {
    this.studentId = this.route.snapshot.paramMap.get('studentId');
    if (!this.studentId) {
      this.snackbar.error('لا يمكن ايجاد معرف الطالب');
      return;
    }
    this.markService.getStudentSubjectsReport(this.studentId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (data: SubjectReportDto[]) => {
          this.subjects = data.map(subj => {
            const firstExam = subj.details.grades.find(g => g.type === 'الاختبار الأول')?.percentage || '-';
            const secondExam = subj.details.grades.find(g => g.type === 'الاختبار الثاني')?.percentage || '-';

            return {
              subject: subj.name,
              firstSemester: firstExam,
              secondSemester: secondExam,
              finalGrade: subj.grade // ممكن تخليها subj.score إذا تبغى رقم
            };
          });
        },
        error: err => {
          console.error('Error loading report:', err);
        }
      });
  }

  downloadPDF() {
    const element = this.reportContent.nativeElement;
    const options = {
      margin: 0.3,
      filename: 'report-card.pdf',
      image: { type: 'jpeg' as const, quality: 0.98 },
      html2canvas: { scale: 2 },
      jsPDF: { unit: 'in' as const, format: 'letter' as const, orientation: 'portrait' as const }
    };

    html2pdf().from(element).set(options).save();
  }
}
