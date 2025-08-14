import { Component, inject } from '@angular/core';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { Router } from '@angular/router';
import { AccountService } from '../../../../core/services/account.service';
import { SchoolService } from '../../../../core/services/school.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { SchoolGradeWithDetailsDto } from '../../../../shared/models/school/school';
import { SectionService } from '../../../../core/services/section.service';
import { Section } from '../../../../shared/models/section/section';

@Component({
  selector: 'app-section-list-by-teacher-id',
  standalone: true,
  imports: [PageWrapperComponent, TableComponent],
  templateUrl: './section-list-by-teacher-id.component.html',
  styleUrl: './section-list-by-teacher-id.component.scss'
})
export class SectionListByTeacherIdComponent {
  sectionService = inject(SectionService)
  private accountService = inject(AccountService)
  private router = inject(Router)
  private snack = inject(SnackbarService)
  sections: Section[] = [];

  Columns: TableColumn[] = [
    { key: 'name', header: ' Name ', sortable: true },
    { key: 'capacity', header: 'capacity ', sortable: true },
    { key: 'classTeacherName', header: 'Teacher Name ', sortable: true },
  ];
  actions: TableAction[] = [
    { actionKey: 'manage', icon: 'settings', tooltip: 'Manage Sections', color: 'accent' },
  ];

  ngOnInit(): void {

    this.loadSections()
  }



  handleUserAction(event: { actionKey: string; rowData: any }): void {
    console.log(`Action: ${event.actionKey} on User:`, event.rowData);
    // Implement your logic here based on actionKey and rowData
    switch (event.actionKey) {

      case 'manage':
        this.router.navigate(['/school-dash-board/attendance', event.rowData.id]);

        break;

    }
  }

  loadSections() {
    const userType = this.accountService.currentUser()?.userType
    if (userType === 'Teacher') {
      const teacherId = this.accountService.currentUser()?.entityId
      if (!teacherId) return;
      this.sectionService.getSectionsByTeacherId(teacherId).subscribe({
        next: res => this.sections = res.data
      })
    }



  }

}
