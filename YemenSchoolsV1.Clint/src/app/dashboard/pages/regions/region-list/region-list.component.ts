import { Component, inject, OnInit } from '@angular/core';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { RegionFormComponent } from '../region-form/region-form.component';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { DialogService } from '../../../../core/services/dialog.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { RegionsService } from '../../../../core/services/regions.service';
import { CitiesService } from '../../../../core/services/cities.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';

@Component({
  selector: 'app-region-list',
  standalone: true,
  imports: [PageWrapperComponent, CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule, TableComponent],
  templateUrl: './region-list.component.html',
  styleUrl: './region-list.component.scss'
})
export class RegionListComponent implements OnInit {

  private dialogService = inject(DialogService);
  private dialog = inject(MatDialog);
  regionService = inject(RegionsService)
  private snack = inject(SnackbarService)

  regionColumns: TableColumn[] = [
    { key: 'nameEn', header: 'الاسم بالإنجليزية', sortable: true },
    { key: 'nameAr', header: 'الاسم بالعربية', sortable: true },
    { key: 'cityName', header: 'اسم المدينة', sortable: true },
    { key: 'countSchools', header: 'عدد المدارس', sortable: true },
  ];


  actions: TableAction[] = [
    { actionKey: 'edit', icon: 'edit', tooltip: 'Edit User', color: 'accent' },
    { actionKey: 'delete', icon: 'delete', tooltip: 'Delete User', color: 'warn' },
  ];




  ngOnInit(): void {
    this.loadRegions()
  }
  loadRegions() {
    this.regionService.getRegions()

  }


  handleUserAction(event: { actionKey: string; rowData: any }): void {
    console.log(`Action: ${event.actionKey} on User:`, event.rowData);
    // Implement your logic here based on actionKey and rowData
    switch (event.actionKey) {

      case 'edit':
        this.openRegionDialog(event.rowData)
        break;
      case 'delete':
        this.openConfirmDialog(event.rowData.id, event.rowData.nameAr)

        break;
    }
  }

  async openConfirmDialog(id: string, name: string) {
    const confirmed = await this.dialogService.confirm(
      'تأكيد الحذف',
      `هل أنت متأكد أنك تريد حذف المنطقة: ${name}؟`
    );

    if (confirmed) {
      this.regionService.deleteRegion(id).subscribe({
        next: () => {
          this.snack.success('تم حذف المنطقة بنجاح!');
          this.loadRegions();
        },
        error: (err) => {
          this.snack.error('فشل في حذف المنطقة.');
          console.error(err);
        }
      });
    }
  }

  openRegionDialog(city?: any) {
    const dialogRef = this.dialog.open(RegionFormComponent, {
      width: '400px',
      data: { model: city }
    });

  }
}
