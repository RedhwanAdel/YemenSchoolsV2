import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { TermService } from '@features/school-dashboard/term/services/term.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogService } from '../../../../core/services/dialog.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { MatButton } from '@angular/material/button';
import { AcadmicYearService } from '@features/school-dashboard/year/services/acadmic-year.service';
import { Term } from '@features/school-dashboard/term/models/term';
import { AccountService } from '../../../../core/services/account.service';
import { YearDto } from '@features/school-dashboard/year/models/AcademicYear';
import { MatFormField, MatLabel, MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { TermFormComponent } from '../term-form/term-form.component';

@Component({
  selector: 'app-term-list',
  standalone: true,
  imports: [PageWrapperComponent, TableComponent, MatButton, MatSelectModule, FormsModule],
  templateUrl: './term-list.component.html',
  styleUrl: './term-list.component.scss'
})
export class TermListComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  termService = inject(TermService);
  private dialogService = inject(DialogService);
  private dialog = inject(MatDialog);
  yearService = inject(AcadmicYearService);
  private snack = inject(SnackbarService);
  private accountService = inject(AccountService);
  terms = signal<Term[]>([]);
  Columns: TableColumn[] = [
    { key: 'name', header: 'الاسم' },
    { key: 'academicYearName', header: 'اسم السنة' },
    { key: 'startDate', header: 'تاريخ البداية', type: 'date' },
    { key: 'endDate', header: 'تاريخ النهاية', type: 'date' },
  ];

  actions: TableAction[] = [
    { actionKey: 'edit', icon: 'edit', tooltip: 'Edit User', color: 'accent' },
    { actionKey: 'delete', icon: 'delete', tooltip: 'Delete User', color: 'warn' },
  ];

  ngOnInit(): void {
    this.loadTerms();
  }



  handleUserAction(event: { actionKey: string; rowData: any }): void {
    console.log(`Action: ${event.actionKey} on User:`, event.rowData);
    // Implement your logic here based on actionKey and rowData
    switch (event.actionKey) {

      case 'edit':
        this.opentermDialog(event.rowData)
        break;
      case 'delete':
        this.openConfirmDialog(event.rowData.id, event.rowData.nameAr)

        break;
    }
  }


  loadTerms() {
    const yearId = this.yearService.currentAcademicYearId()!;
    console.log(yearId);

    this.termService.getTerms(yearId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: res => this.terms.set(res)
      });
  }

  async openConfirmDialog(id: string, name: string) {
    const confirmed = await this.dialogService.confirm(
      'تأكيد الحذف',
      `هل أنت متأكد أنك تريد حذف الفصل الدراسي: ${name}؟`
    );

    if (confirmed) {
      this.termService.deleteTerm(id)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: () => {
            this.snack.success('تم حذف الفصل الدراسي بنجاح!');
            this.loadTerms();
          },
          error: (err) => {
            this.snack.error('فشل في حذف الفصل الدراسي.');
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


    dialogRef.afterClosed()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(result => {
        if (result) {
          this.loadTerms();
        }
      });
  }

}

