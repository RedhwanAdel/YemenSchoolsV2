import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { MatTableDataSource } from '@angular/material/table';
import { async } from 'rxjs';
import { DialogService } from '../../../../core/services/dialog.service';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import { SchoolService } from '@features/schools/services/school.service';
import { SchoolParams } from '@features/schools/models/schoolParams';
import { SchoolListItem } from '@features/schools/models/school';
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { SnackbarService } from '../../../../core/services/snackbar.service';

export interface UserData {
  id: string;
  name: string;
  email: string;
  role: string;
  createdAt: Date;
  status: string;
  balance: number;
}

@Component({
  selector: 'app-school-list',
  standalone: true,
  imports: [TableComponent, PageWrapperComponent, MatButtonModule, RouterLink],
  templateUrl: './school-list.component.html',
  styleUrl: './school-list.component.scss'
})
export class SchoolListComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  private snack = inject(SnackbarService)

  router = inject(Router);
  private dialogService = inject(DialogService);
  private schoolService = inject(SchoolService)
  schools: SchoolListItem[] = [];
  totalItems = 0;

  schoolColumns: TableColumn[] = [
    { key: 'name', header: 'الاسم', sortable: true },
    { key: 'city', header: 'المدينة' },
    { key: 'region', header: 'المنطقة' },
    { key: 'mainPhone', header: 'رقم الهاتف' },
    { key: 'schoolType', header: 'نوع المدرسة' },
    { key: 'schoolLevel', header: 'المرحلة الدراسية' },
    { key: 'genderType', header: 'نوع الجنس' },
  ];

  schoolParams = new SchoolParams()
  ngOnInit(): void {
    this.loadSchools()
  }
  loadSchools() {
    this.schoolService.getSchools(this.schoolParams)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: response => {
          if (response.data) {
            this.schools = response.data;
            this.totalItems = response.totalCount
          }
        }
      })
  }
  handleUserAction(event: { actionKey: string; rowData: any }): void {
    console.log(`Action: ${event.actionKey} on User:`, event.rowData);
    // Implement your logic here based on actionKey and rowData
    switch (event.actionKey) {
      case 'view':
        this.router.navigate(['/dash-board/schools-detail', event.rowData.id]);
        break;
      case 'edit':
        this.router.navigate(['/dash-board/schools-edit', event.rowData.id]);
        break;
      case 'delete':
        this.openConfirmDialog(event.rowData.id, event.rowData.name)

        break;
    }
  }
  async openConfirmDialog(id: string, name: string) {
    const confirmed = await this.dialogService.confirm(
      'تأكيد الحذف',
      `هل أنت متأكد أنك تريد حذف المدرسة: ${name}؟`
    );

    if (confirmed) {
      this.schoolService.deleteSchool(id)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: () => {
            this.snack.success('تم حذف المدرسة بنجاح!');
            this.loadSchools();
          },
          error: (err) => {
            this.snack.error('فشل في حذف المدرسة.');
            console.error(err);
          }
        });
    }
  }

  onPageChange(event: PageEvent) {
    this.schoolParams.pageNumber = event.pageIndex + 1;
    this.schoolParams.pageSize = event.pageSize;
    this.loadSchools();
  }

  onSortChange(sort: Sort): void {
    this.schoolParams.orderBy = 1;
    this.schoolParams.sortDirection = sort.direction as 'asc' | 'desc';
    this.loadSchools(); // إعادة تحميل المدارس بالترتيب الجديد
  }
}
