import { Component, inject, OnInit, signal } from '@angular/core';
import { SubjectService } from '../../../core/services/subject.service';
import { SchoolService } from '../../../core/services/school.service';
import { Grade } from '../../../shared/models/grade/grade';
import { AssignSubjectsToSchoolGradeDto, SchoolGradeSubject, SchoolGradeSubjectsInit, SchoolGradeWithDetailsDto, StageGradeDto } from '../../../shared/models/school/school';
import { AccountService } from '../../../core/services/account.service';
import { MatTableModule } from '@angular/material/table';
import { MatListModule, MatSelectionList } from '@angular/material/list';
import { FormsModule } from '@angular/forms';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBarModule, MatSnackBar } from '@angular/material/snack-bar';
import { Subject } from '../../../shared/models/school/subject';

@Component({
  selector: 'app-school-subject',
  standalone: true,
  imports: [MatExpansionModule,
    MatListModule,
    MatButtonModule,
    MatIconModule,
    MatSnackBarModule,
    FormsModule, CommonModule],
  templateUrl: './school-subject.component.html',
  styleUrl: './school-subject.component.scss'
})
export class SchoolSubjectComponent implements OnInit {
  // المفتاح سيكون SchoolGradeId بدلاً من StageGradeId
  selectedSubjects: { [key: string]: string[] } = {};
  subjectOfGrade = signal<Subject[]>([]);
  private originalAssignedSubjects: { [key: string]: string[] } = {};
  hasChangesMap: { [key: string]: boolean } = {}; // لتتبع حالة التغيير لكل grade.id

  subjectService = inject(SubjectService);
  schoolService = inject(SchoolService);
  acoountService = inject(AccountService);
  private snackBar = inject(MatSnackBar);

  // gradesOfSchool ستكون من نوع SchoolGradeWithDetailsDto[]
  gradesOfSchool: SchoolGradeWithDetailsDto[] = [];
  ngOnInit(): void {


    this.subjectService.getSubjects().subscribe({
      next: (subjects) => {
        this.subjectOfGrade.set(subjects.data);
      },
      error: (err) => {
        console.error('Error loading all available subjects:', err);
        this.snackBar.open('فشل تحميل قائمة المواد المتاحة.', 'إغلاق', { duration: 3000 });
      }
    });

    this.schoolService.getSchoolGrade().subscribe({
      next: (grades: SchoolGradeWithDetailsDto[]) => {
        this.gradesOfSchool = grades;
        this.gradesOfSchool.forEach((schoolGrade) => {
          this.schoolService.getSubjectsForSchoolGrade(schoolGrade.id).subscribe({
            next: (assignedSubjects: Subject[]) => {
              const subjectIds = assignedSubjects.map((sub) => sub.id);
              this.selectedSubjects[schoolGrade.id] = subjectIds;
              this.originalAssignedSubjects[schoolGrade.id] = [...subjectIds];
              // عند التحميل الأولي، لا توجد تغييرات
              this.hasChangesMap[schoolGrade.id] = false;
            },
            error: (err) => {
              console.error(
                `Error loading subjects for schoolGradeId ${schoolGrade.id}:`,
                err
              );
              this.snackBar.open(
                `فشل تحميل المواد للصف ${schoolGrade.gradeName}.`,
                'إغلاق',
                {
                  duration: 3000,
                }
              );
            },
          });
        });
      },
      error: (err) => {
        console.error('Error loading grades for school:', err);
        this.snackBar.open('فشل تحميل صفوف المدرسة.', 'إغلاق', {
          duration: 3000,
        });
      },
    });
  }

  // ... (بقية الدالة getSelectedSubjectNames) ...

  // دالة جديدة للتحقق من التغييرات وتحديث hasChangesMap
  // يجب استدعاء هذه الدالة عندما تتغير تحديدات المستخدم (مثلاً، عند تغيير حالة checkbox)
  checkForChanges(schoolGradeId: string): void {
    const currentSelected = this.selectedSubjects[schoolGradeId] || [];
    const originalAssigned = this.originalAssignedSubjects[schoolGradeId] || [];

    const sortedCurrent = [...currentSelected].sort();
    const sortedOriginal = [...originalAssigned].sort();

    const hasChanges =
      sortedCurrent.length !== sortedOriginal.length ||
      !sortedCurrent.every((value, index) => value === sortedOriginal[index]);

    this.hasChangesMap[schoolGradeId] = hasChanges;
  }

  onSaveSubjects(schoolGradeId: string) {
    const currentSelectedSubjectIds = this.selectedSubjects[schoolGradeId] || [];

    // نحن نعتمد الآن على hasChangesMap لتعطيل الزر، ولكن من الجيد وجود هذا الفحص أيضًا
    // للتأكد من أننا لا نرسل طلبًا بدون تغييرات حتى لو حدث خطأ ما في تحديث الزر
    if (!this.hasChangesMap[schoolGradeId]) {
      this.snackBar.open('لم يتم إجراء أي تغييرات لحفظها.', 'إغلاق', {
        duration: 3000,
      });
      return;
    }

    const dataToSave: AssignSubjectsToSchoolGradeDto = {
      schoolGradeId: schoolGradeId,
      subjectIds: currentSelectedSubjectIds,
    };

    this.schoolService.assignSubjectsToStageGrade(dataToSave).subscribe({
      next: (res) => {
        console.log(`المواد للصف ${schoolGradeId} تم حفظها بنجاح!`, res);
        this.snackBar.open('تم حفظ المواد بنجاح. ✅', 'إغلاق', {
          duration: 3000,
        });
        // تحديث الحالة الأصلية بعد الحفظ الناجح
        this.originalAssignedSubjects[schoolGradeId] = [...currentSelectedSubjectIds];
        // تحديث hasChangesMap بحيث يصبح الزر معطلًا مرة أخرى
        this.hasChangesMap[schoolGradeId] = false;
      },
      error: (err) => {
        console.error(`خطأ أثناء حفظ المواد للصف ${schoolGradeId}:`, err);
        this.snackBar.open('فشل حفظ المواد. ❌', 'إغلاق', { duration: 3000 });
      },
    });
  }
  getSelectedSubjectNames(schoolGradeId: string): string {
    const selectedIds = this.selectedSubjects[schoolGradeId];
    if (!selectedIds || selectedIds.length === 0) {
      return 'لا يوجد مواد مختارة';
    }
    // تأكد من أن subjectService.subjects() تعيد قيمة الإشارة بشكل صحيح
    const allSubjects = this.subjectOfGrade(); // الوصول إلى قيمة الإشارة
    const selectedNames = allSubjects
      .filter((subject) => selectedIds.includes(subject.id))
      .map((subject) => subject.name);
    return selectedNames.join(', ');
  }
}
