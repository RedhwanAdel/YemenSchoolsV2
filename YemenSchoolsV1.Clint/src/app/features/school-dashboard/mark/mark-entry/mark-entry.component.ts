import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MarkService } from '../../../../core/services/mark.service';
import { SectionSubject, AssessmentType, CreateMarksDto, Student } from '../../../../shared/models/mark/mark';
import { StudentService } from '../../../../core/services/student.service';

@Component({
  selector: 'app-mark-entry',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatSelectModule,
    MatFormFieldModule,
    MatButtonModule,
    MatTableModule,
    MatInputModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './mark-entry.component.html',
  styleUrl: './mark-entry.component.scss'
})
export class MarkEntryComponent {
  sectionSubjects: SectionSubject[] = [];
  selectedSectionSubjectId: string | null = null;
  selectedSectionId: string | null = null;

  assessmentTypes: AssessmentType[] = [
    { value: 'الاختبار الأول', viewValue: 'الاختبار الأول' },
    { value: 'الاختبار الثاني', viewValue: 'الاختبار الثاني' },
    { value: 'الامتحان النهائي', viewValue: 'الامتحان النهائي' },
    { value: 'عمل منزلي', viewValue: 'عمل منزلي' },
    { value: 'الواجب', viewValue: 'الواجب' },
  ];
  selectedAssessmentType: string | null = null;
  maxScore: number | null = null;
  students: Student[] = [];
  studentScores: { [key: string]: number } = {};
  displayedColumns: string[] = ['position', 'name', 'registerNo', 'score'];
  isLoading = false;
  studentService = inject(StudentService)
  constructor(private markService: MarkService) { }

  ngOnInit(): void {
    this.loadTeacherSectionSubjects();
  }

  loadTeacherSectionSubjects(): void {
    this.markService.getTeacherSectionSubjects().subscribe({
      next: (data) => {
        this.sectionSubjects = data;
      },
      error: (err) => console.error('Error loading section subjects', err)
    });
  }

  onSectionSubjectChange(): void {
    if (this.selectedSectionSubjectId) {
      const selected = this.sectionSubjects.find(ss => ss.id === this.selectedSectionSubjectId);
      if (selected) {
        this.selectedSectionId = selected.sectionId;
      }
      if (this.selectedSectionId) {

        this.loadStudents(this.selectedSectionSubjectId, this.selectedSectionId);
      }
    }
  }

  loadStudents(sectionSubjectId: string, sectionId: string): void {
    // يجب أن تحصل على معرف الشعبة من sectionSubjectId
    this.isLoading = true;
    this.studentService.getStudentsBySectionId(sectionId).subscribe({
      next: (data) => {
        this.students = data;
        this.studentScores = {};
        // this.students.forEach(student => this.studentScores[student.id] = 0);
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading students', err);
        this.isLoading = false;
      }
    });
  }

  saveMarks(): void {
    if (!this.selectedSectionSubjectId || !this.selectedAssessmentType || !this.maxScore) {
      alert('Please select a subject and assessment type.');
      return;
    }

    const marksDto: CreateMarksDto = {
      sectionSubjectId: this.selectedSectionSubjectId,
      assessmentType: this.selectedAssessmentType,
      studentScores: this.studentScores,
      maxScore: this.maxScore
    };

    this.isLoading = true;
    this.markService.createMarks(marksDto).subscribe({
      next: (response) => {
        alert(response.message);
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error saving marks', err);
        this.isLoading = false;
        alert('Failed to save marks. Please try again.');
      }
    });
  }
}
