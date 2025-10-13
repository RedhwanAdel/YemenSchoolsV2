import { Component, inject, OnInit, signal } from '@angular/core';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { SchoolService } from '../../../../core/services/school.service';
import { MatDialog } from '@angular/material/dialog';
import { AccountService } from '../../../../core/services/account.service';
import { DialogService } from '../../../../core/services/dialog.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { YearDto } from '../../../../shared/models/AcademicYear/AcademicYear';
import { YearFormComponent } from '../../year/year-form/year-form.component';
import { SchoolGradeWithDetailsDto } from '../../../../shared/models/school/school';
import { Router } from '@angular/router';

@Component({
  selector: 'app-grade-list',
  standalone: true,
  imports: [PageWrapperComponent, TableComponent],
  templateUrl: './grade-list.component.html',
  styleUrl: './grade-list.component.scss'
})
export class GradeListComponent implements OnInit {

  schoolService = inject(SchoolService)
  private accountService = inject(AccountService)
  private router = inject(Router)
  private snack = inject(SnackbarService)
  gradesOfSchool: SchoolGradeWithDetailsDto[] = [];

  Columns: TableColumn[] = [
    { key: 'gradeName', header: 'الصف' },
    { key: 'stageName', header: 'المرحلة' },
  ];

  actions: TableAction[] = [
    { actionKey: 'manage', icon: 'settings', tooltip: 'Manage Sections', color: 'accent' },
  ];

  ngOnInit(): void {

    this.loadGrades()
  }



  handleUserAction(event: { actionKey: string; rowData: any }): void {
    console.log(`Action: ${event.actionKey} on User:`, event.rowData);
    // Implement your logic here based on actionKey and rowData
    switch (event.actionKey) {

      case 'manage':
        this.router.navigate(['/school-dash-board/section-list', event.rowData.id]);

        break;

    }
  }

  loadGrades() {

    this.schoolService.getSchoolGrade().subscribe({
      next: res => this.gradesOfSchool = res
    })

  }


}
