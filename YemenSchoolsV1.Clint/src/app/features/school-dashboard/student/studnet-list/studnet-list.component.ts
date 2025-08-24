import { Component, inject, signal } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router, RouterLink } from '@angular/router';
import { AccountService } from '../../../../core/services/account.service';
import { DialogService } from '../../../../core/services/dialog.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { TeacherService } from '../../../../core/services/teacher.service';
import { TableColumn, TableComponent } from '../../../../shared/components/table/table.component';
import { Teacher } from '../../../../shared/models/teachers/teacher';
import { Student, StudentListDto } from '../../../../shared/models/student/student';
import { StudentService } from '../../../../core/services/student.service';
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
  private dialogService = inject(DialogService);
  router = inject(Router);
  accountService = inject(AccountService)
  private dialog = inject(MatDialog);
  studentService = inject(StudentService)
  private snack = inject(SnackbarService)
  students = signal<StudentListDto[]>([])

  Columns: TableColumn[] = [
    { key: 'name', header: ' Name ' },
    { key: 'registerNo', header: ' Register No ' },
    { key: 'gradeName', header: ' Class ' },
    { key: 'sectionName', header: ' Section ' }
  ];

  ngOnInit(): void {

    this.loadStudent()
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

  loadStudent() {
    const schoolId = this.accountService.currentUser()?.schoolId
    if (schoolId) {
      this.studentService.getStudentsBySchoolId(schoolId).subscribe({
        next: res => this.students.set(res)
      })
    }
  }

  async openConfirmDialog(id: string, name: string) {
    const confirmed = await this.dialogService.confirm(
      'Confirm Delete',
      `Are you sure you want to delete the city: ${name}?`
    );


  }

}
