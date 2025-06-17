import { Component, inject } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { TableAction, TableColumn, TableComponent } from '../../../../shared/components/table/table.component';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { FormInputComponent } from '../../../../shared/components/form-input/form-input.component';
import { RouterLink } from '@angular/router';
import { DialogService } from '../../../../core/services/dialog.service';
import { CityFormComponent } from '../city-form/city-form.component';
import { MatDialog } from '@angular/material/dialog';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";

@Component({
  selector: 'app-city-list',
  standalone: true,
  imports: [TableComponent,
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule, PageWrapperComponent],
  templateUrl: './city-list.component.html',
  styleUrl: './city-list.component.scss'
})
export class CityListComponent {
  private dialogService = inject(DialogService);
  private dialog = inject(MatDialog);
  // --- Product Table Data ---
  products = [
    { id: 'P001', nameEn: 'Laptop Pro', nameAr: 'Electronics' },
    { id: 'P002', nameEn: 'Gaming Mouse', nameAr: 'Accessories' },
    { id: 'P003', nameEn: 'Mechanical Keyboard', nameAr: 'Accessories' },
    { id: 'P001', nameEn: 'Laptop Pro', nameAr: 'Electronics' },
    { id: 'P004', nameEn: '4K Monitor', nameAr: 'Electronics' },
    { id: 'P005', nameEn: 'Webcam HD', nameAr: 'Peripherals' },
  ];
  productsDataSource: MatTableDataSource<any> = new MatTableDataSource(this.products);

  productColumns: TableColumn[] = [
    { key: 'id', header: 'Product ID', sortable: true },
    { key: 'nameEn', header: 'Product Name En', sortable: true },
    { key: 'nameAr', header: 'Product Name Ar', sortable: true },
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
    const confirmed = await this.dialogService.confirm('Confirm Delet', 'Are you sure you want to delete user');
  }

  openCityDialog(city?: any) {
    const dialogRef = this.dialog.open(CityFormComponent, {
      width: '400px',
      data: { model: city }
    });

  }

}
