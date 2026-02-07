import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatCardModule } from '@angular/material/card';
import { MatIcon } from '@angular/material/icon';
import { SubjectReportDto } from '@features/school-dashboard/mark/models/mark';
import { MarkService } from '@features/school-dashboard/mark/services/mark.service';
import { ActivatedRoute } from '@angular/router';
import { SnackbarService } from '../../../core/services/snackbar.service';

@Component({
  selector: 'app-child-grades',
  standalone: true,
  imports: [
    MatCardModule,
    MatIcon,
    CommonModule
  ],
  templateUrl: './child-grades.component.html',
  styleUrl: './child-grades.component.scss'
})
export class ChildGradesComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  markService = inject(MarkService);
  private route = inject(ActivatedRoute);
  private snackbar = inject(SnackbarService);
  // يمكنك استخدام هذه الخاصية لاستقبال بيانات الطالب من المكون الأب
  @Input() student: any;

  // مصفوفة تحتوي على بيانات جميع المواد
  subjects: SubjectReportDto[] = [];

  // خاصية لتخزين المادة التي تم اختيارها لعرض تفاصيلها
  selectedSubject: any | null = null;

  constructor() { }

  ngOnInit(): void {

    const studentId = this.route.snapshot.paramMap.get('studentId');
    if (!studentId) {
      this.snackbar.error('لا يمكن ايجاد معرف الطالب');
      return;
    }
    this.markService.getStudentSubjectsReport(studentId).subscribe({
      next: (res) => this.subjects = res,
      error: (err) => {
        alert('ffffff')

        console.error(err)
      }
    });
  }

  // هذه الدالة يتم استدعاؤها عند الضغط على بطاقة أي مادة
  showSubjectDetails(subject: any): void {
    this.selectedSubject = subject;
    // يمكنك إضافة منطق آخر هنا، مثل تمرير البيانات إلى مكونات أخرى
  }
}
