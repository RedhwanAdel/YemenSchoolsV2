import { Component, inject, OnInit } from '@angular/core';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { AcadmicYearService } from '../../../../core/services/acadmic-year.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogService } from '../../../../core/services/dialog.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { MatButton } from '@angular/material/button';
import { SchoolLevelsSelectorComponent } from "../../../../shared/components/school-levels-selector/school-levels-selector.component";

@Component({
  selector: 'app-acadmic-year-list',
  standalone: true,
  imports: [PageWrapperComponent, TableComponent, MatButton, SchoolLevelsSelectorComponent],
  templateUrl: './acadmic-year-list.component.html',
  styleUrl: './acadmic-year-list.component.scss'
})
export class AcadmicYearListComponent implements OnInit {
  private dialogService = inject(DialogService);
  private dialog = inject(MatDialog);
  acadmicYearService = inject(AcadmicYearService)
  private snack = inject(SnackbarService)

  Columns: TableColumn[] = [
    { key: 'id', header: ' ID', sortable: true },
    { key: 'name', header: ' Name ', sortable: true },
    { key: 'stageName', header: 'Stage Name ', sortable: true },
    { key: 'startDate', header: 'Start Date', sortable: true, type: 'date' },
    { key: 'endDate', header: 'Endt Date ', sortable: true, type: 'date' },

  ];
  actions: TableAction[] = [
    { actionKey: 'edit', icon: 'edit', tooltip: 'Edit User', color: 'accent' },
    { actionKey: 'delete', icon: 'delete', tooltip: 'Delete User', color: 'warn' },
  ];



  ngOnInit(): void {
    this.acadmicYearService.getAcademicYears()
  }



  handleUserAction(event: { actionKey: string; rowData: any }): void {
    console.log(`Action: ${event.actionKey} on User:`, event.rowData);
    // Implement your logic here based on actionKey and rowData
    switch (event.actionKey) {

      case 'edit':
        this.openRegionDialog(event.rowData)
        break;
      case 'delete':
        this.openConfirmDialog(event.rowData.id, event.rowData.name)

        break;
    }
  }

  async openConfirmDialog(id: string, name: string) {
    const confirmed = await this.dialogService.confirm(
      'Confirm Delete',
      `Are you sure you want to delete the Year: ${name}?`
    );

    if (confirmed) {
      this.acadmicYearService.deleteAcademicYear(id).subscribe({
        next: () => {
          this.snack.success('Year deleted successfully!');
          this.acadmicYearService.getAcademicYears();
        },
        error: (err) => {
          this.snack.error('Failed to delete Year.');
          console.error(err);
        }
      });
    }
  }

  openRegionDialog(city?: any) {


  }

}
