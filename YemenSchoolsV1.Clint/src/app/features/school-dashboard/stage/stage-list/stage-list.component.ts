import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";

import { MatDialog } from '@angular/material/dialog';
import { DialogService } from '../../../../core/services/dialog.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { MatCardModule } from '@angular/material/card';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTableModule } from '@angular/material/table';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-stage-list',
  standalone: true,
  imports: [FormsModule, CommonModule, MatTableModule, MatCheckboxModule, MatButtonModule],
  templateUrl: './stage-list.component.html',
  styleUrl: './stage-list.component.scss'
})
export class StageListComponent {
  stageGrades: any[] = [
    { id: '1', stageName: 'حضانة', gradeName: 'روضة أولى', description: 'أطفال من عمر 3 إلى 4 سنوات' },
    { id: '2', stageName: 'حضانة', gradeName: 'روضة ثانية', description: 'أطفال من عمر 4 إلى 5 سنوات' },
    { id: '3', stageName: 'ابتدائي', gradeName: 'الصف الأول' },
    { id: '4', stageName: 'ابتدائي', gradeName: 'الصف الثاني' },
    { id: '5', stageName: 'ابتدائي', gradeName: 'الصف الثالث' },
    { id: '6', stageName: 'ابتدائي', gradeName: 'الصف الرابع' },
    { id: '7', stageName: 'ابتدائي', gradeName: 'الصف الخامس' },
    { id: '8', stageName: 'ابتدائي', gradeName: 'الصف السادس' },
    { id: '9', stageName: 'إعدادي', gradeName: 'الصف السابع' },
    { id: '10', stageName: 'إعدادي', gradeName: 'الصف الثامن' },
    { id: '11', stageName: 'إعدادي', gradeName: 'الصف التاسع' },
    { id: '12', stageName: 'ثانوي', gradeName: 'الصف العاشر' },
    { id: '13', stageName: 'ثانوي', gradeName: 'الصف الحادي عشر' },
    { id: '14', stageName: 'ثانوي', gradeName: 'الصف الثاني عشر' },
  ];;
  @Output() selectionChanged = new EventEmitter<any[]>();

  columns = ['select', 'stage', 'grade', 'description'];
  selectionMap: Record<string, boolean> = {};

  submitSelection() {
    const selected = this.stageGrades.filter(g => this.selectionMap[g.id]);
    console.log(selected)
    this.selectionChanged.emit(selected);
  }

}
