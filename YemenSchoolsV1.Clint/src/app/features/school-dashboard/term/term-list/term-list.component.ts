import { Component, inject, OnInit } from '@angular/core';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { TermService } from '../../../../core/services/term.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogService } from '../../../../core/services/dialog.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-term-list',
  standalone: true,
  imports: [PageWrapperComponent, TableComponent, MatButton],
  templateUrl: './term-list.component.html',
  styleUrl: './term-list.component.scss'
})
export class TermListComponent implements OnInit {
  private dialogService = inject(DialogService);
  private dialog = inject(MatDialog);
  termService = inject(TermService)
  private snack = inject(SnackbarService)

  Columns: TableColumn[] = [
    { key: 'id', header: ' ID', sortable: true },
    { key: 'name', header: ' Name ', sortable: true },
    { key: 'academicYearName', header: 'Academic Year Name ', sortable: true },
    { key: 'startDate', header: 'Start Date', sortable: true, type: 'date' },
    { key: 'endDate', header: 'Endt Date ', sortable: true, type: 'date' },
  ];
  actions: TableAction[] = [
    { actionKey: 'edit', icon: 'edit', tooltip: 'Edit User', color: 'accent' },
    { actionKey: 'delete', icon: 'delete', tooltip: 'Delete User', color: 'warn' },
  ];



  ngOnInit(): void {
    this.termService.getTerms()
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
      `Are you sure you want to delete the term: ${name}?`
    );

    if (confirmed) {
      this.termService.deleteTerm(id).subscribe({
        next: () => {
          this.snack.success('term deleted successfully!');
          this.termService.getTerms();
        },
        error: (err) => {
          this.snack.error('Failed to delete term.');
          console.error(err);
        }
      });
    }
  }

  openRegionDialog(city?: any) {


  }
}
