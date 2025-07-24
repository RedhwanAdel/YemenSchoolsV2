import { Component, inject, OnInit, signal } from '@angular/core';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { AcadmicYearService } from '../../../../core/services/acadmic-year.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogService } from '../../../../core/services/dialog.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { YearDto } from '../../../../shared/models/AcademicYear/AcademicYear';
import { AccountService } from '../../../../core/services/account.service';
import { MatButton } from '@angular/material/button';
import { YearFormComponent } from '../year-form/year-form.component';

@Component({
  selector: 'app-year-list',
  standalone: true,
  imports: [PageWrapperComponent, TableComponent, MatButton],
  templateUrl: './year-list.component.html',
  styleUrl: './year-list.component.scss'
})
export class YearListComponent implements OnInit {
  private dialogService = inject(DialogService);
  private dialog = inject(MatDialog);
  yearService = inject(AcadmicYearService)
  private accountService = inject(AccountService)
  private snack = inject(SnackbarService)
  years = signal<YearDto[]>([])

  Columns: TableColumn[] = [
    { key: 'name', header: ' Name ' },
    { key: 'startDate', header: ' start Date ', type: 'date' },
    { key: 'endDate', header: ' end Date ', type: 'date' },
  ];

  actions: TableAction[] = [
    { actionKey: 'edit', icon: 'edit', tooltip: 'Edit User', color: 'accent' },
    { actionKey: 'delete', icon: 'delete', tooltip: 'Delete User', color: 'warn' },
  ];

  ngOnInit(): void {

    this.loadYears()
  }



  handleUserAction(event: { actionKey: string; rowData: any }): void {
    console.log(`Action: ${event.actionKey} on User:`, event.rowData);
    // Implement your logic here based on actionKey and rowData
    switch (event.actionKey) {

      case 'edit':
        this.openYearDialog(event.rowData)
        break;
      case 'delete':
        this.openConfirmDialog(event.rowData.id, event.rowData.name)

        break;
    }
  }

  loadYears() {
    const schoolId = this.accountService.currentUser()?.schoolId
    if (schoolId) {
      this.yearService.getAcademicYears(schoolId).subscribe({
        next: res => this.years.set(res.data)
      })
    }
  }

  async openConfirmDialog(id: string, name: string) {
    const confirmed = await this.dialogService.confirm(
      'Confirm Delete',
      `Are you sure you want to delete the city: ${name}?`
    );

    if (confirmed) {
      this.yearService.deleteAcademicYear(id).subscribe({
        next: () => {
          this.snack.success('city deleted successfully!');
          this.loadYears();
        },
        error: (err) => {
          this.snack.error('Failed to delete city.');
          console.error(err);
        }
      });
    }
  }

  openYearDialog(year?: any) {
    const dialogRef = this.dialog.open(YearFormComponent, {
      width: '400px',
      data: { model: year }
    });


    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadYears()
      }
    });
  }


}
