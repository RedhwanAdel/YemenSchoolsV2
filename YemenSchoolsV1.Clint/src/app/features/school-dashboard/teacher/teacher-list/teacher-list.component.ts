import { Component, inject, OnInit, signal } from '@angular/core';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { TeacherService } from '../../../../core/services/teacher.service';
import { MatDialog } from '@angular/material/dialog';
import { AccountService } from '../../../../core/services/account.service';
import { DialogService } from '../../../../core/services/dialog.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { YearDto } from '../../../../shared/models/AcademicYear/AcademicYear';
import { YearFormComponent } from '../../year/year-form/year-form.component';
import { MatButton } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import { Teacher } from '../../../../shared/models/teachers/teacher';

@Component({
  selector: 'app-teacher-list',
  standalone: true,
  imports: [PageWrapperComponent, RouterLink, TableComponent, MatButton],
  templateUrl: './teacher-list.component.html',
  styleUrl: './teacher-list.component.scss'
})
export class TeacherListComponent implements OnInit {
  private dialogService = inject(DialogService);
  router = inject(Router);

  private dialog = inject(MatDialog);
  teacherService = inject(TeacherService)
  private accountService = inject(AccountService)
  private snack = inject(SnackbarService)
  teachers = signal<Teacher[]>([])

  Columns: TableColumn[] = [
    { key: 'name', header: ' Name ' },
    { key: 'email', header: ' email ' },
    { key: 'phoneNumber', header: ' phoneNumber ' },
    { key: 'employmentStatus', header: ' employmentStatus ' },
    { key: 'specialization', header: ' specialization ' },
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
    const schoolId = this.accountService.currentUser()?.schoolId
    if (schoolId) {
      this.teacherService.getTeachers(schoolId).subscribe({
        next: res => this.teachers.set(res.data)
      })
    }
  }

  async openConfirmDialog(id: string, name: string) {
    const confirmed = await this.dialogService.confirm(
      'Confirm Delete',
      `Are you sure you want to delete the city: ${name}?`
    );

    if (confirmed) {
      this.teacherService.deleteTeacher(id).subscribe({
        next: () => {
          this.snack.success('city deleted successfully!');
          this.loadTeacher();
        },
        error: (err) => {
          this.snack.error('Failed to delete city.');
          console.error(err);
        }
      });
    }
  }



}
