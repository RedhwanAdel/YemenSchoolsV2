import { Component, inject } from '@angular/core';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { Router } from '@angular/router';
import { AccountService } from '../../../../core/services/account.service';
import { SectionService } from '../../../../core/services/section.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { Section } from '../../../../shared/models/section/section';

@Component({
  selector: 'app-promote-section-list',
  standalone: true,
  imports: [PageWrapperComponent, TableComponent],
  templateUrl: './promote-section-list.component.html',
  styleUrl: './promote-section-list.component.scss'
})
export class PromoteSectionListComponent {
  sectionService = inject(SectionService)
  private accountService = inject(AccountService)
  private router = inject(Router)
  private snack = inject(SnackbarService)
  sections: Section[] = [];

  Columns: TableColumn[] = [
    { key: 'name', header: ' Name ', sortable: true },
    { key: 'gradeName', header: ' Grade ', sortable: true },
    { key: 'capacity', header: 'capacity ', sortable: true },
    { key: 'classTeacherName', header: 'Teacher Name ', sortable: true },
  ];
  actions: TableAction[] = [
    { actionKey: 'promote', icon: 'upgrade', tooltip: 'promote Students', color: 'accent' },
  ];

  ngOnInit(): void {

    this.loadSections()
  }



  handleUserAction(event: { actionKey: string; rowData: any }): void {
    console.log(`Action: ${event.actionKey} on User:`, event.rowData);
    // Implement your logic here based on actionKey and rowData
    switch (event.actionKey) {

      case 'promote':
        this.router.navigate(['/school-dash-board/promote', event.rowData.id]);

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
