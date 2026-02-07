import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { SectionsOfYear } from '@features/school-dashboard/section/models/section';
import { Router } from '@angular/router';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { SectionService } from '@features/school-dashboard/section/services/section.service';
import { AcadmicYearService } from '@features/school-dashboard/year/services/acadmic-year.service';

@Component({
  selector: 'app-section-subject-list',
  standalone: true,
  imports: [PageWrapperComponent, TableComponent],
  templateUrl: './section-subject-list.component.html',
  styleUrl: './section-subject-list.component.scss'
})
export class SectionSubjectListComponent implements OnInit {

  private router = inject(Router);
  private snack = inject(SnackbarService);
  private destroyRef = inject(DestroyRef);
  sectionService = inject(SectionService);
  yearService = inject(AcadmicYearService);
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
    this.loadSections();
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
    this.sectionService.getSectionsByAcademicYear()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (res: SectionsOfYear[]) => this.sectionsOfYear = res
      });
  }


}
