import { Component, inject, OnInit } from '@angular/core';
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
import { CitiesService } from '../../../../core/services/cities.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';

@Component({
  selector: 'app-city-list',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule, PageWrapperComponent,
    TableComponent
  ],
  templateUrl: './city-list.component.html',
  styleUrl: './city-list.component.scss'
})
export class CityListComponent implements OnInit {

  private dialogService = inject(DialogService);
  private dialog = inject(MatDialog);
  cityService = inject(CitiesService)
  private snack = inject(SnackbarService)

  cityColumns: TableColumn[] = [
    { key: 'nameEn', header: 'city Name En' },
    { key: 'nameAr', header: 'city Name Ar' },
  ];

  actions: TableAction[] = [
    { actionKey: 'edit', icon: 'edit', tooltip: 'Edit User', color: 'accent' },
    { actionKey: 'delete', icon: 'delete', tooltip: 'Delete User', color: 'warn' },
  ];


  ngOnInit(): void {
    this.loadCities()
  }
  handleUserAction(event: { actionKey: string; rowData: any }): void {
    console.log(`Action: ${event.actionKey} on User:`, event.rowData);
    // Implement your logic here based on actionKey and rowData
    switch (event.actionKey) {

      case 'edit':
        this.openCityDialog(event.rowData)
        break;
      case 'delete':
        this.openConfirmDialog(event.rowData.id, event.rowData.nameEn)

        break;
    }
  }

  loadCities() {
    this.cityService.getCites()

  }

  async openConfirmDialog(id: string, name: string) {
    const confirmed = await this.dialogService.confirm(
      'Confirm Delete',
      `Are you sure you want to delete the city: ${name}?`
    );

    if (confirmed) {
      this.cityService.deleteCity(id).subscribe({
        next: () => {
          this.snack.success('city deleted successfully!');
          this.loadCities();
        },
        error: (err) => {
          this.snack.error('Failed to delete city.');
          console.error(err);
        }
      });
    }
  }

  openCityDialog(city?: any) {
    const dialogRef = this.dialog.open(CityFormComponent, {
      width: '400px',
      data: { model: city }
    });

  }

}
