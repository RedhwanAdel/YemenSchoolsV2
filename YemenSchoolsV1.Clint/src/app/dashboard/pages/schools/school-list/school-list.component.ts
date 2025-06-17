import { Component, inject } from '@angular/core';
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { MatTableDataSource } from '@angular/material/table';
import { async } from 'rxjs';
import { DialogService } from '../../../../core/services/dialog.service';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { MatButtonModule } from '@angular/material/button';

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
  imports: [TableComponent, PageWrapperComponent, MatButtonModule],
  templateUrl: './school-list.component.html',
  styleUrl: './school-list.component.scss'
})
export class SchoolListComponent {

  private dialogService = inject(DialogService);
  users: UserData[] = [
    { id: '1', name: 'Alice Smith', email: 'alice@example.com', role: 'Admin', createdAt: new Date('2023-01-15'), status: 'Active', balance: 1200.50 },
    { id: '2', name: 'Bob Johnson', email: 'bob@example.com', role: 'Editor', createdAt: new Date('2023-02-20'), status: 'Inactive', balance: 50.25 },
    { id: '3', name: 'Charlie Brown', email: 'charlie@example.com', role: 'Viewer', createdAt: new Date('2023-03-10'), status: 'Active', balance: 300.00 },
    { id: '4', name: 'Diana Prince', email: 'diana@example.com', role: 'Admin', createdAt: new Date('2023-04-01'), status: 'Active', balance: 750.75 },
    { id: '5', name: 'Eve Adams', email: 'eve@example.com', role: 'Editor', createdAt: new Date('2023-05-05'), status: 'Active', balance: 200.00 },
    { id: '6', name: 'Frank White', email: 'frank@example.com', role: 'Viewer', createdAt: new Date('2023-06-12'), status: 'Inactive', balance: 150.00 },
    { id: '7', name: 'Grace Lee', email: 'grace@example.com', role: 'Admin', createdAt: new Date('2023-07-22'), status: 'Active', balance: 900.00 },
    { id: '8', name: 'Harry Potter', email: 'harry@example.com', role: 'Viewer', createdAt: new Date('2023-08-30'), status: 'Active', balance: 450.00 },
    { id: '9', name: 'Isabelle Chen', email: 'isabelle@example.com', role: 'Editor', createdAt: new Date('2023-09-19'), status: 'Active', balance: 600.00 },
    { id: '10', name: 'Jack Miller', email: 'jack@example.com', role: 'Admin', createdAt: new Date('2023-10-25'), status: 'Inactive', balance: 80.00 },
    { id: '11', name: 'Kelly Clark', email: 'kelly@example.com', role: 'Viewer', createdAt: new Date('2023-11-01'), status: 'Active', balance: 220.00 },
    { id: '12', name: 'Liam Garcia', email: 'liam@example.com', role: 'Editor', createdAt: new Date('2023-12-15'), status: 'Active', balance: 500.00 },
    { id: '13', name: 'Mia Rodriguez', email: 'mia@example.com', role: 'Admin', createdAt: new Date('2024-01-05'), status: 'Active', balance: 1000.00 },
    { id: '14', name: 'Noah Davis', email: 'noah@example.com', role: 'Viewer', createdAt: new Date('2024-02-14'), status: 'Inactive', balance: 70.00 },
    { id: '15', name: 'Olivia Martinez', email: 'olivia@example.com', role: 'Editor', createdAt: new Date('2024-03-20'), status: 'Active', balance: 350.00 },
  ];
  usersDataSource: MatTableDataSource<UserData> = new MatTableDataSource(this.users);
  userColumns: TableColumn[] = [
    { key: 'id', header: 'ID', sortable: true },
    { key: 'name', header: 'Name', sortable: true },
    { key: 'email', header: 'Email', sortable: true },
    { key: 'role', header: 'Role', sortable: true },
    { key: 'createdAt', header: 'Created At', type: 'date', format: 'MMM d, y', sortable: true },
    { key: 'status', header: 'Status', sortable: true },
    { key: 'balance', header: 'Balance', type: 'currency', format: 'USD', sortable: true },
  ];


  handleUserAction(event: { actionKey: string; rowData: any }): void {
    console.log(`Action: ${event.actionKey} on User:`, event.rowData);
    // Implement your logic here based on actionKey and rowData
    switch (event.actionKey) {
      case 'view':
        alert(`Viewing user: ${event.rowData.name}`);
        break;
      case 'edit':
        alert(`Editing user: ${event.rowData.name}`);
        break;
      case 'delete':
        this.openConfirmDialog()
        // if (confirm(`Are you sure you want to delete user: ${event.rowData.name}?`)) {
        //   alert(`${event.rowData.name} deleted.`);
        // }
        break;
    }
  }

  async openConfirmDialog() {
    const confirmed = await this.dialogService.confirm('Confirm Delet', 'Are you sure you want to delete user');
  }
}
