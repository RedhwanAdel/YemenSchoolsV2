import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { AcadmicYearService } from '@features/school-dashboard/year/services/acadmic-year.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogService } from '../../../../core/services/dialog.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { CreateYearDto, YearDto } from '@features/school-dashboard/year/models/AcademicYear';
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
  private destroyRef = inject(DestroyRef);
  private dialogService = inject(DialogService);
  private dialog = inject(MatDialog);
  yearService = inject(AcadmicYearService);
  private accountService = inject(AccountService);
  private snack = inject(SnackbarService);
  years = signal<YearDto[]>([]);

  Columns: TableColumn[] = [
    { key: 'name', header: 'الاسم' },
    { key: 'startDate', header: 'تاريخ البداية', type: 'date' },
    { key: 'endDate', header: 'تاريخ النهاية', type: 'date' },
    { key: 'isCurrentYearDisplay', header: 'الحالة', sortable: false },
  ];

  actions: TableAction[] = [
    { actionKey: 'edit', icon: 'edit', tooltip: 'Edit User', color: 'accent' },
    { actionKey: 'delete', icon: 'delete', tooltip: 'Delete User', color: 'warn' },
    { actionKey: 'set-as-current', icon: 'check_circle', tooltip: 'Set as Current Year', color: 'primary', showCondition: (rowData: YearDto) => !rowData.isCurrentYear }

  ];

  ngOnInit(): void {
    this.loadYears();
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
      case 'set-as-current':
        this.onSetCurrentYear(event.rowData.id);
        break;
    }
  }
  onSetCurrentYear(academicYearId: string): void {
    this.dialogService.confirm(
      'تعيين السنة الحالية',
      `هل أنت متأكد أنك تريد تعيين هذه السنة كسنة دراسية حالية؟ سيؤثر هذا على جميع العمليات المستقبلية.`
    ).then(confirmed => {
      if (confirmed) {
        this.yearService.SetCurrentYear(academicYearId)
          .pipe(takeUntilDestroyed(this.destroyRef))
          .subscribe({
            next: () => {
              this.snack.success('تم تعيين السنة الدراسية الحالية بنجاح!');
              this.loadYears();
            },
            error: (err) => {
              this.snack.error('فشل في تعيين السنة الدراسية الحالية.');
              console.error(err);
            }
          });
      }
    });
  }

  loadYears() {
    this.yearService.getAcademicYears()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (res) => {
          const yearsWithStatus = res.data?.map((year: YearDto) => ({
            ...year,
            isCurrentYearDisplay: year.isCurrentYear ? 'Current' : 'Not Current'
          })) || [];
          this.years.set(yearsWithStatus);
        },
        error: (err) => {
          this.snack.error('Failed to load academic years.');
          console.error(err);
        }
      });
  }
  async openConfirmDialog(id: string, name: string) {
    const confirmed = await this.dialogService.confirm(
      'تأكيد الحذف',
      `هل أنت متأكد أنك تريد حذف السنة الدراسية: ${name}؟`
    );

    if (confirmed) {
      this.yearService.deleteAcademicYear(id)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: () => {
            this.snack.success('تم حذف السنة الدراسية بنجاح!');
            this.loadYears();
          },
          error: (err) => {
            this.snack.error('فشل في حذف السنة الدراسية.');
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


    dialogRef.afterClosed()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(result => {
        if (result) {
          this.loadYears();
        }
      });
  }


}
