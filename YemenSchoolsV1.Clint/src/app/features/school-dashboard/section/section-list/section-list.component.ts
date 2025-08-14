import { Component, inject, OnInit, signal } from '@angular/core';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { SectionService } from '../../../../core/services/section.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogService } from '../../../../core/services/dialog.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { MatButton } from '@angular/material/button';
import { ActivatedRoute } from '@angular/router';
import { Section } from '../../../../shared/models/section/section';
import { SectionFormComponent } from '../section-form/section-form.component';
import { AcadmicYearService } from '../../../../core/services/acadmic-year.service';

@Component({
  selector: 'app-section-list',
  standalone: true,
  imports: [PageWrapperComponent, TableComponent, MatButton],
  templateUrl: './section-list.component.html',
  styleUrl: './section-list.component.scss'
})
export class SectionListComponent implements OnInit {
  private dialogService = inject(DialogService);
  private dialog = inject(MatDialog);
  sectionService = inject(SectionService)
  yearService = inject(AcadmicYearService)
  private snack = inject(SnackbarService)
  private route = inject(ActivatedRoute);

  sections = signal<Section[]>([])
  Columns: TableColumn[] = [
    { key: 'name', header: ' Name ', sortable: true },
    { key: 'capacity', header: 'capacity ', sortable: true },
  ];
  actions: TableAction[] = [
    { actionKey: 'edit', icon: 'edit', tooltip: 'Edit User', color: 'accent' },
    { actionKey: 'delete', icon: 'delete', tooltip: 'Delete User', color: 'warn' },
  ];



  ngOnInit(): void {
    this.loadSections()
  }

  loadSections() {
    const gradeId = this.route.snapshot.paramMap.get('id');
    if (!gradeId) return;
    this.sectionService.getSectionsByYearAndGrade(gradeId).subscribe({
      next: res => this.sections.set(res.data)
    })

  }

  handleUserAction(event: { actionKey: string; rowData: any }): void {
    console.log(`Action: ${event.actionKey} on User:`, event.rowData);
    // Implement your logic here based on actionKey and rowData
    switch (event.actionKey) {

      case 'edit':
        this.openSectionDialog(event.rowData)
        break;
      case 'delete':
        this.openConfirmDialog(event.rowData.id, event.rowData.name)

        break;
    }
  }

  async openConfirmDialog(id: string, name: string) {
    const confirmed = await this.dialogService.confirm(
      'Confirm Delete',
      `Are you sure you want to delete the section: ${name}?`
    );

    if (confirmed) {
      this.sectionService.deleteSection(id).subscribe({
        next: () => {
          this.snack.success('section deleted successfully!');
          this.loadSections()
        },
        error: (err) => {
          this.snack.error('Failed to delete section.');
          console.error(err);
        }
      });
    }
  }

  openSectionDialog(section?: any) {
    const gradeId = this.route.snapshot.paramMap.get('id');
    if (!gradeId) return;


    const dialogRef = this.dialog.open(SectionFormComponent, {
      width: '400px',
      data: {
        model: section,
        gradeId: gradeId
      }
    });


    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadSections()
      }
    });
  }
}
