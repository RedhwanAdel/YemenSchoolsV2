import { Component, inject, OnInit } from '@angular/core';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { SectionService } from '../../../../core/services/section.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogService } from '../../../../core/services/dialog.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { MatButton } from '@angular/material/button';

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
  private snack = inject(SnackbarService)

  Columns: TableColumn[] = [
    { key: 'id', header: ' ID', sortable: true },
    { key: 'name', header: ' Name ', sortable: true },
    { key: 'gradeName', header: 'Grade Name ', sortable: true },
    { key: 'roomNumber', header: 'Room Number', sortable: true },
  ];
  actions: TableAction[] = [
    { actionKey: 'edit', icon: 'edit', tooltip: 'Edit User', color: 'accent' },
    { actionKey: 'delete', icon: 'delete', tooltip: 'Delete User', color: 'warn' },
  ];



  ngOnInit(): void {
    this.sectionService.getSections()
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
      `Are you sure you want to delete the section: ${name}?`
    );

    if (confirmed) {
      this.sectionService.deleteSection(id).subscribe({
        next: () => {
          this.snack.success('section deleted successfully!');
          this.sectionService.getSections();
        },
        error: (err) => {
          this.snack.error('Failed to delete section.');
          console.error(err);
        }
      });
    }
  }

  openRegionDialog(city?: any) {


  }
}
