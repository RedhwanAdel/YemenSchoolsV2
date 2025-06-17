import { Component, inject } from '@angular/core';
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
export class RegionListComponent {
  private dialogService = inject(DialogService);
  private dialog = inject(MatDialog);
  // --- Product Table Data ---
  products = [
    { id: 'P001', nameEn: 'Laptop Pro', nameAr: 'Electronics', city: 'Electronics' },
    { id: 'P002', nameEn: 'Gaming Mouse', nameAr: 'Accessories', city: 'Accessories' },
    { id: 'P003', nameEn: 'Mechanical ', nameAr: 'Accessories', city: 'Accessories' },
    { id: 'P001', nameEn: 'Laptop Pro', nameAr: 'Electronics', city: 'Electronics' },
    { id: 'P004', nameEn: '4K Monitor', nameAr: 'Electronics', city: 'Electronics' },
    { id: 'P005', nameEn: 'Webcam HD', nameAr: 'Peripherals', city: 'Peripherals' },
  ];
  productsDataSource: MatTableDataSource<any> = new MatTableDataSource(this.products);

  productColumns: TableColumn[] = [
    { key: 'id', header: 'Product ID', sortable: true },
    { key: 'nameEn', header: 'Product Name En', sortable: true },
    { key: 'nameAr', header: 'Product Name Ar', sortable: true },
    { key: 'city', header: 'City Name ', sortable: true },
  ];

  actions: TableAction[] = [
    { actionKey: 'edit', icon: 'edit', tooltip: 'Edit User', color: 'accent' },
    { actionKey: 'delete', icon: 'delete', tooltip: 'Delete User', color: 'warn' },
  ];

  handleUserAction(event: { actionKey: string; rowData: any }): void {
    console.log(`Action: ${event.actionKey} on User:`, event.rowData);
    // Implement your logic here based on actionKey and rowData
    switch (event.actionKey) {
      case 'view':
        alert(`Viewing user: ${event.rowData.name}`);
        break;
      case 'edit':
        this.openCityDialog(event.rowData)
        break;
      case 'delete':
        this.openConfirmDialog()

        break;
    }
  }

  async openConfirmDialog() {
    const confirmed = await this.dialogService.confirm('Confirm Delet', 'Are you sure you want to delete region');
  }

  openCityDialog(city?: any) {
    const dialogRef = this.dialog.open(RegionFormComponent, {
      width: '400px',
      data: { model: city }
    });

  }
}
