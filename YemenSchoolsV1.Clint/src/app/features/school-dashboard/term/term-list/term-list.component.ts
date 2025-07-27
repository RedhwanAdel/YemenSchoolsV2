import { Component, inject, OnInit, signal } from '@angular/core';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { TermService } from '../../../../core/services/term.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogService } from '../../../../core/services/dialog.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { MatButton } from '@angular/material/button';
import { AcadmicYearService } from '../../../../core/services/acadmic-year.service';
import { Term } from '../../../../shared/models/term/term';
import { AccountService } from '../../../../core/services/account.service';
import { YearDto } from '../../../../shared/models/AcademicYear/AcademicYear';
import { MatFormField, MatLabel, MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { TermFormComponent } from '../term-form/term-form.component';

@Component({
  selector: 'app-term-list',
  standalone: true,
  imports: [PageWrapperComponent, TableComponent, MatButton, MatSelectModule, MatLabel, MatFormField, FormsModule],
  templateUrl: './term-list.component.html',
  styleUrl: './term-list.component.scss'
})
export class TermListComponent implements OnInit {
  termService = inject(TermService)
  private dialogService = inject(DialogService);
  private dialog = inject(MatDialog);
  yearService = inject(AcadmicYearService)
  private snack = inject(SnackbarService)
  private accountService = inject(AccountService)
  terms = signal<Term[]>([])
  Columns: TableColumn[] = [
    { key: 'name', header: ' Name ' },
    { key: 'academicYearName', header: ' Year Name ' },
    { key: 'startDate', header: ' start Date ', type: 'date' },
    { key: 'endDate', header: ' end Date ', type: 'date' },
  ];

  actions: TableAction[] = [
    { actionKey: 'edit', icon: 'edit', tooltip: 'Edit User', color: 'accent' },
    { actionKey: 'delete', icon: 'delete', tooltip: 'Delete User', color: 'warn' },
  ];

  ngOnInit(): void {
    this.loadTerms()
  }



  handleUserAction(event: { actionKey: string; rowData: any }): void {
    console.log(`Action: ${event.actionKey} on User:`, event.rowData);
    // Implement your logic here based on actionKey and rowData
    switch (event.actionKey) {

      case 'edit':
        this.opentermDialog(event.rowData)
        break;
      case 'delete':
        this.openConfirmDialog(event.rowData.id, event.rowData.nameEn)

        break;
    }
  }


  loadTerms() {
    const yearId = this.yearService.currentAcademicYearId()!
    console.log(yearId)

    this.termService.getTerms(yearId).subscribe({
      next: res => this.terms.set(res.data)
    })

  }

  async openConfirmDialog(id: string, name: string) {
    const confirmed = await this.dialogService.confirm(
      'Confirm Delete',
      `Are you sure you want to delete the city: ${name}?`
    );

    if (confirmed) {
      this.termService.deleteTerm(id).subscribe({
        next: () => {
          this.snack.success('city deleted successfully!');
          this.loadTerms();
        },
        error: (err) => {
          this.snack.error('Failed to delete city.');
          console.error(err);
        }
      });
    }
  }

  opentermDialog(term?: any) {

    const dialogRef = this.dialog.open(TermFormComponent, {
      width: '400px',
      data: { model: term }
    });


    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadTerms()
      }
    });
  }

}

