import { Component, inject, OnInit } from '@angular/core';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { StageService } from '../../../../core/services/stage.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogService } from '../../../../core/services/dialog.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { MatCardModule } from '@angular/material/card';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-stage-list',
  standalone: true,
  imports: [PageWrapperComponent, MatButton, TableComponent],
  templateUrl: './stage-list.component.html',
  styleUrl: './stage-list.component.scss'
})
export class StageListComponent implements OnInit {
  private dialogService = inject(DialogService);
  private dialog = inject(MatDialog);
  stageService = inject(StageService)
  private snack = inject(SnackbarService)

  stageColumns: TableColumn[] = [
    { key: 'id', header: ' ID', sortable: true },
    { key: 'name', header: ' Name ', sortable: true },
    { key: 'schoolName', header: 'School Name ', sortable: true },
  ];
  actions: TableAction[] = [
    { actionKey: 'edit', icon: 'edit', tooltip: 'Edit User', color: 'accent' },
    { actionKey: 'delete', icon: 'delete', tooltip: 'Delete User', color: 'warn' },
  ];



  ngOnInit(): void {
    this.stageService.getStages()
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
      `Are you sure you want to delete the stage: ${name}?`
    );

    if (confirmed) {
      this.stageService.deleteStage(id).subscribe({
        next: () => {
          this.snack.success('stage deleted successfully!');
          this.stageService.getStages();
        },
        error: (err) => {
          this.snack.error('Failed to delete stage.');
          console.error(err);
        }
      });
    }
  }

  openRegionDialog(city?: any) {


  }

}
