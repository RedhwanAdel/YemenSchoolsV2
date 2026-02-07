import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { TeacherService } from '@features/school-dashboard/teacher/services/teacher.service';
import { MatDialog } from '@angular/material/dialog';
import { AccountService } from '../../../../core/services/account.service';
import { DialogService } from '../../../../core/services/dialog.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { YearDto } from '@features/school-dashboard/year/models/AcademicYear';
import { MatFormField, MatLabel, MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { TeacherFormComponent } from '../teacher-form/teacher-form.component';
import { MatButton } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import { Teacher } from '../models/teachers';

@Component({
  selector: 'app-teacher-list',
  standalone: true,
  imports: [PageWrapperComponent, RouterLink, TableComponent, MatButton],
  templateUrl: './teacher-list.component.html',
  styleUrl: './teacher-list.component.scss'
})
export class TeacherListComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  private dialogService = inject(DialogService);
  router = inject(Router);
  private dialog = inject(MatDialog);
  teacherService = inject(TeacherService);
  private accountService = inject(AccountService);
  private snack = inject(SnackbarService);
  teachers = signal<Teacher[]>([]);

  Columns: TableColumn[] = [
    { key: 'name', header: 'الاسم' },
    { key: 'email', header: 'البريد الإلكتروني' },
    { key: 'phoneNumber', header: 'رقم الهاتف' },
    { key: 'employmentStatus', header: 'حالة التوظيف' },
    { key: 'specialization', header: 'التخصص' },
  ];

  ngOnInit(): void {

    this.loadTeacher()
  }



  handleUserAction(event: { actionKey: string; rowData: any }): void {
    console.log(`Action: ${event.actionKey} on User:`, event.rowData);
    // Implement your logic here based on actionKey and rowData
    switch (event.actionKey) {
      case 'view':
        this.router.navigate(['/school-dash-board/school-teacher-detail', event.rowData.id]);
        break;
      case 'edit':
        this.router.navigate(['/school-dash-board/school-teacher-edit', event.rowData.id]);
        break;
      case 'delete':
        this.openConfirmDialog(event.rowData.id, event.rowData.name)

        break;
    }
  }

  loadTeacher() {
    const schoolId = this.accountService.currentUser()?.schoolId;
    if (schoolId) {
      this.teacherService.getTeachers(schoolId)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: res => this.teachers.set(res)
        });
    }
  }

  async openConfirmDialog(id: string, name: string) {
    const confirmed = await this.dialogService.confirm(
      'تأكيد الحذف',
      `هل أنت متأكد أنك تريد حذف المعلم: ${name}؟`
    );

    if (confirmed) {
      this.teacherService.deleteTeacher(id)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: () => {
            this.snack.success('تم حذف المعلم بنجاح!');
            this.loadTeacher();
          },
          error: (err) => {
            this.snack.error('فشل في حذف المعلم.');
            console.error(err);
          }
        });
    }
  }


}
