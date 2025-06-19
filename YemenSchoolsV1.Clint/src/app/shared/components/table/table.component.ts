import { Component, Input, Output, EventEmitter, OnInit, ViewChild } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common'; // DatePipe for date formatting
import { MatTableModule, MatTableDataSource } from '@angular/material/table'; // For MatTableModule
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator'; // For pagination
import { MatSort, MatSortModule } from '@angular/material/sort'; // For sorting
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
  @Input() dataSource: MatTableDataSource<any> = new MatTableDataSource<any>([]);
  @Input() columns: TableColumn[] = [];
  @Input() actions: TableAction[] = [
    { actionKey: 'view', icon: 'visibility', tooltip: 'View Details', color: 'primary' },
    { actionKey: 'edit', icon: 'edit', tooltip: 'Edit User', color: 'accent' },
    { actionKey: 'delete', icon: 'delete', tooltip: 'Delete User', color: 'warn' },
  ];

  @Input() hasActions: boolean = false; // Flag to show/hide actions column

  @Output() actionClicked = new EventEmitter<{ actionKey: string; rowData: any }>();

  displayedColumns: string[] = [];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngOnInit(): void {
    // Determine which columns to display
    this.displayedColumns = this.columns.map(col => col.key);
    if (this.hasActions) {
      this.displayedColumns.push('actions'); // Add actions column if enabled
    }

    // Set up paginator and sort after the data source is initialized
    // Using a timeout to ensure paginator and sort are available (rendered)
    setTimeout(() => {
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;

      // Custom sorting for specific column types if needed (e.g., numbers, dates)
      this.dataSource.sortingDataAccessor = (item, property) => {
        const column = this.columns.find(col => col.key === property);
        if (column && (column.type === 'number' || column.type === 'currency' || column.type === 'date')) {
          return item[property]; // Direct comparison for numbers/dates
        }
        return item[property] ? String(item[property]).toLowerCase() : ''; // Default to string comparison
      };
    }, 0);
  }

  ngAfterViewInit(): void {
    // This is another place to ensure paginator and sort are set,
    // especially if `dataSource` might be set asynchronously after ngOnInit.
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  /**
   * Applies a filter to the table data based on user input.
   * @param event The keyboard event from the filter input.
   */
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage(); // Reset to the first page when filter changes
    }
  }

  /**
   * Handles the click event on an action button.
   * @param actionKey The unique key of the action performed.
   * @param rowData The data object of the row on which the action was performed.
   */
  onActionClick(actionKey: string, rowData: any): void {
    this.actionClicked.emit({ actionKey, rowData });
  }

  /**
   * Custom sorting logic.
   * MatSort usually handles basic sorting, but this method can be extended for complex sorting needs.
   * @param sort The MatSort output event.
   */
  sortData(sort: any): void {
    // MatSort automatically handles sorting data if dataSource.sort is set.
    // This method can be used if you need custom server-side sorting or more complex client-side sorting.
    console.log('Sorting by:', sort.active, 'Direction:', sort.direction);
  }
}
