import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { SchoolService } from '@features/schools/services/school.service';
import { AccountService } from '../../../core/services/account.service';
import { SelectionModel } from '@angular/cdk/collections';
import { StageGradeDto, CreateSchoolGradeDto } from '@features/schools/models/school';
import { MatIcon } from '@angular/material/icon';
import { MatCard, MatCardModule } from '@angular/material/card';
import { SnackbarService } from '../../../core/services/snackbar.service';

@Component({
  selector: 'app-school-grade',
  standalone: true,
  imports: [FormsModule, CommonModule, MatTableModule, MatCheckboxModule, MatButtonModule, MatIcon, MatCardModule],
  templateUrl: './school-grade.component.html',
  styleUrl: './school-grade.component.scss'
})
export class SchoolGradeComponent implements OnInit {
  schoolService = inject(SchoolService);
  acoountService = inject(AccountService);
  snackbarService = inject(SnackbarService)

  // Data source for MatTable
  stageGrades = new MatTableDataSource<StageGradeDto>([]);
  // Columns to be displayed in the table
  displayedColumns: string[] = ['select', 'stageName', 'gradeName'];
  // SelectionModel to manage selected rows
  selection = new SelectionModel<StageGradeDto>(true, []);

  ngOnInit(): void {
    this.loadStageGrades();
  }

  loadStageGrades(): void {
    const schoolId = this.acoountService.currentUser()?.schoolId;
    if (!schoolId) {
      this.snackbarService.error('School ID not found. Cannot load stage grades.')
      return;
    }

    this.schoolService.getStageGradesForSchool(schoolId).subscribe({
      next: (res) => {
        this.stageGrades.data = res; // Set the data for the table

        // Pre-select items based on isSelected property
        this.stageGrades.data.forEach((row) => {
          if (row.isSelected) {
            this.selection.select(row);
          }
        });
      },
      error: (err) => {
        this.snackbarService.error('Error loading stage grades:')

      },
    });
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.stageGrades.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.stageGrades.data.forEach((row) => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: StageGradeDto): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.stageName + ' ' + row.gradeName
      }`;
  }

  saveSettings(): void {
    const schoolId = this.acoountService.currentUser()?.schoolId;
    if (!schoolId) {
      this.snackbarService.error('School ID not found. Cannot save settings.')

      return;
    }

    // Get the IDs of the selected stage grades
    const selectedStageGradeIds = this.selection.selected.map(
      (sg) => sg.stageGradeId
    );

    const createDto: CreateSchoolGradeDto = {
      schoolId: schoolId,
      stageGradeIds: selectedStageGradeIds,
    };

    this.schoolService.syncStageGrades(createDto).subscribe({
      next: (res) => {
        this.snackbarService.success(res)

        console.log('Settings saved successfully!', res);
        // Optionally, show a success message to the user
      },
      error: (err) => {
        console.error('Error saving settings:', err);
        // Handle error, e.g., show an error message
      },
    });
  }

  // If you implement the export functionality
  // exportToGoogleSheets(): void {
  //   console.log('Exporting to Google Sheets...');
  //   // Implement your logic to export data
  // }
}
