import { Component, inject, OnInit } from '@angular/core';
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { MatTableDataSource } from '@angular/material/table';
import { async } from 'rxjs';
import { DialogService } from '../../../../core/services/dialog.service';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import { SchoolService } from '../../../../core/services/school.service';
import { SchoolParams } from '../../../../shared/models/school/schoolParams';
import { SchoolListItem } from '../../../../shared/models/school/school';
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

  private snack = inject(SnackbarService)

  router = inject(Router);
  private dialogService = inject(DialogService);
  private schoolService = inject(SchoolService)
  schools: SchoolListItem[] = [];
  totalItems = 0;

  schoolColumns: TableColumn[] = [
    { key: 'name', header: 'Name', sortable: true },
    { key: 'city', header: 'City' },
    { key: 'region', header: 'Region' },
    { key: 'mainPhone', header: 'Phone ' },
    { key: 'schoolType', header: 'SchoolType' },
    { key: 'schoolLevel', header: 'SchoolLevel' },
    { key: 'genderType', header: 'GenderType' },
  ];
  schoolParams = new SchoolParams()
  ngOnInit(): void {
    this.loadSchools()
  }
  loadSchools() {
    this.schoolService.getSchools(this.schoolParams).subscribe({
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
      'Confirm Delete',
      `Are you sure you want to delete the school: ${name}?`
    );

    if (confirmed) {
      this.schoolService.deleteSchool(id).subscribe({
        next: () => {
          this.snack.success('School deleted successfully!');
          this.loadSchools();
        },
        error: (err) => {
          this.snack.error('Failed to delete school.');
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
