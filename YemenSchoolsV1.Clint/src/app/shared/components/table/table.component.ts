import { Component, Input, Output, EventEmitter, OnInit, ViewChild } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common'; // DatePipe for date formatting
import { MatTableModule, MatTableDataSource } from '@angular/material/table'; // For MatTableModule
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator'; // For pagination
import { MatSort, MatSortModule, Sort } from '@angular/material/sort'; // For sorting
import { MatIconModule } from '@angular/material/icon'; // For MatIcon
import { MatButtonModule } from '@angular/material/button'; // For MatButton
import { MatFormFieldModule } from '@angular/material/form-field'; // For search input
import { MatInputModule } from '@angular/material/input'; // For search input
import { MatCard } from '@angular/material/card';
import { MatTooltipModule } from '@angular/material/tooltip';
export interface TableColumn {
  key: string;       // Property key in the data object
  header: string;    // Header text to display
  type?: 'text' | 'date' | 'number' | 'currency' | 'action'; // Type of data/column
  format?: string;   // Optional format string for date/currency pipes
  sortable?: boolean; // Whether the column is sortable
}

// Interface to define the structure of an action button
export interface TableAction {
  actionKey: string; // Unique key for the action (e.g., 'edit', 'delete')
  icon: string;      // Material icon name (e.g., 'edit', 'visibility', 'delete')
  tooltip: string;   // Tooltip text for the button
  color?: string;    // Optional color for the button (e.g., 'primary', 'accent', 'warn')
}

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [
    CommonModule,
    MatTooltipModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatIconModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    DatePipe // Include DatePipe for date formatting
  ],
  templateUrl: './table.component.html',
  styleUrl: './table.component.scss'
})
export class TableComponent implements OnInit {
  @Input() dataRow: any[] = [];
  @Input() columns: TableColumn[] = [];
  @Input() actions: TableAction[] = [
    { actionKey: 'view', icon: 'visibility', tooltip: 'View Details', color: 'primary' },
    { actionKey: 'edit', icon: 'edit', tooltip: 'Edit User', color: 'accent' },
    { actionKey: 'delete', icon: 'delete', tooltip: 'Delete User', color: 'warn' },
  ];
  @Input() totalItems: number = 0;
  @Input() pageSize: number = 5;
  @Input() pageIndex: number = 0;
  @Input() hasActions: boolean = false;
  @Input() usePaginator: boolean = true;
  @Input() allowSerch: boolean = true;

  @Output() actionClicked = new EventEmitter<{ actionKey: string; rowData: any }>();
  @Output() pageChange = new EventEmitter<PageEvent>();
  @Output() sortChange = new EventEmitter<Sort>();
  filteredData: any[] = [];

  displayedColumns: string[] = [];

  ngOnInit(): void {

    this.displayedColumns = this.columns.map(col => col.key);

    if (this.hasActions) this.displayedColumns.push('actions');
  }

  onPageChange(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.pageChange.emit(event);
  }
  ngOnChanges(): void {
    this.filteredData = this.dataRow;
  }

  sortData(event: Sort): void {
    this.sortChange.emit(event);
  }

  onActionClick(actionKey: string, rowData: any): void {
    this.actionClicked.emit({ actionKey, rowData });
  }

  applyFilter(event: Event): void {
    const value = (event.target as HTMLInputElement).value.trim().toLowerCase();

    if (!value) {
      // لو البحث فاضي، رجع كل البيانات الأصلية
      this.filteredData = this.dataRow;
    } else {
      this.filteredData = this.dataRow.filter(item =>
        Object.values(item).some(val =>
          String(val).toLowerCase().includes(value)
        )
      );
    }

  }


}
