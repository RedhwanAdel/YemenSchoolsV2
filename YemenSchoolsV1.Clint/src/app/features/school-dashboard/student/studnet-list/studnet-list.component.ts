import { Component, DestroyRef, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatDialog } from '@angular/material/dialog';
import { Router, RouterLink } from '@angular/router';
import { AccountService } from '../../../../core/services/account.service';
import { DialogService } from '../../../../core/services/dialog.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { TeacherService } from '@features/school-dashboard/teacher/services/teacher.service';
import { TableColumn, TableComponent } from '../../../../shared/components/table/table.component';
import { Teacher } from '@features/school-dashboard/teacher/models/teachers';
import { Student, StudentListDto } from '@features/school-dashboard/student/models/student';
import { StudentService } from '@features/school-dashboard/student/services/student.service';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-studnet-list',
  standalone: true,
  imports: [PageWrapperComponent, TableComponent, MatButton, RouterLink],
  templateUrl: './studnet-list.component.html',
  styleUrl: './studnet-list.component.scss'
})
export class StudnetListComponent {
  private destroyRef = inject(DestroyRef);
  private dialogService = inject(DialogService);
  router = inject(Router);
  accountService = inject(AccountService);
  private dialog = inject(MatDialog);
  studentService = inject(StudentService);
  private snack = inject(SnackbarService);
  students = signal<StudentListDto[]>([]);

  Columns: TableColumn[] = [
    { key: 'name', header: 'الاسم' },
    { key: 'registerNo', header: 'رقم التسجيل' },
    { key: 'gradeName', header: 'الصف' },
    { key: 'sectionName', header: 'الشعبة' }
  ];

  ngOnInit(): void {

    this.loadStudent()
  }



  handleUserAction(event: { actionKey: string; rowData: any }): void {
    console.log(`Action: ${event.actionKey} on User:`, event.rowData);
    // Implement your logic here based on actionKey and rowData
    switch (event.actionKey) {
      case 'view':
        this.router.navigate(['/school-dash-board/school-student-detail', event.rowData.id]);
        break;
      case 'edit':
        this.router.navigate(['/school-dash-board/school-teacher-edit', event.rowData.id]);
        break;
      case 'delete':
        this.openConfirmDialog(event.rowData.id, event.rowData.name)

        break;
    }
  }

  loadStudent() {
    const schoolId = this.accountService.currentUser()?.schoolId;
    if (schoolId) {
      this.studentService.getStudentsBySchoolId(schoolId)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (res: StudentListDto[]) => this.students.set(res)
        });
    }
  }

  async openConfirmDialog(id: string, name: string) {
    const confirmed = await this.dialogService.confirm(
      'تأكيد الحذف',
      `هل أنت متأكد أنك تريد حذف الطالب: ${name}؟`
    );
    if (confirmed) {
      this.studentService.deleteStudent(id)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: () => {
            this.snack.success('تم حذف الطالب بنجاح!');
            this.loadStudent();
          },
          error: (err) => {
            this.snack.error('فشل في حذف الطالب.');
            console.error(err);
          }
        });
    }
  }

}
