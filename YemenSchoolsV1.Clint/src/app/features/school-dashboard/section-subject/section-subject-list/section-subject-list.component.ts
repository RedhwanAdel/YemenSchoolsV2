import { Component, inject, OnInit } from '@angular/core';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { SectionsOfYear } from '../../../../shared/models/section/section';
import { Router } from '@angular/router';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { SectionService } from '../../../../core/services/section.service';
import { AcadmicYearService } from '../../../../core/services/acadmic-year.service';

@Component({
  selector: 'app-section-subject-list',
  standalone: true,
  imports: [PageWrapperComponent, TableComponent],
  templateUrl: './section-subject-list.component.html',
  styleUrl: './section-subject-list.component.scss'
})
export class SectionSubjectListComponent implements OnInit {

  private router = inject(Router)
  private snack = inject(SnackbarService)
  sectionService = inject(SectionService)
  yearService = inject(AcadmicYearService)
  sectionsOfYear: SectionsOfYear[] = [];

  Columns: TableColumn[] = [
    { key: 'sectionName', header: 'اسم الشعبة' },
    { key: 'gradeName', header: 'اسم الصف' },
    { key: 'subjectCount', header: 'عدد المواد' },
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
        this.router.navigate(['/school-dash-board/section-subject-assignment', event.rowData.sectionId]);

        break;

    }
  }

  loadSections() {
    const yearId = this.yearService.currentAcademicYearId()!

    this.sectionService.getSectionsForSpcificYear(yearId).subscribe({
      next: res => this.sectionsOfYear = res.data
    })

  }


}
