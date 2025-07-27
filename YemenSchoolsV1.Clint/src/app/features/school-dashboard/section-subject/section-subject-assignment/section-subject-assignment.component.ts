import { Component, inject, OnInit, signal } from '@angular/core';
import { PageWrapperComponent } from "../../../../shared/components/page-wrapper/page-wrapper.component";
import { TableAction, TableColumn, TableComponent } from "../../../../shared/components/table/table.component";
import { SectionSubjectService } from '../../../../core/services/section-subject.service';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { DialogService } from '../../../../core/services/dialog.service';
import { SnackbarService } from '../../../../core/services/snackbar.service';
import { Section, SectionSubjectInfoDto } from '../../../../shared/models/section/section';
import { SectionFormComponent } from '../../section/section-form/section-form.component';
import { MatButton } from '@angular/material/button';
import { SectionSubjectAssignmentFormComponent } from '../section-subject-assignment-form/section-subject-assignment-form.component';
import { SectionService } from '../../../../core/services/section.service';

@Component({
  selector: 'app-section-subject-assignment',
  standalone: true,
  imports: [PageWrapperComponent, TableComponent, MatButton],
  templateUrl: './section-subject-assignment.component.html',
  styleUrl: './section-subject-assignment.component.scss'
})
export class SectionSubjectAssignmentComponent implements OnInit {
  private dialogService = inject(DialogService);
  private dialog = inject(MatDialog);
  sectionService = inject(SectionService)
  sectionSubjectService = inject(SectionSubjectService)
  private snack = inject(SnackbarService)
  private route = inject(ActivatedRoute);
  currentSection?: Section;
  sections = signal<SectionSubjectInfoDto[]>([])
  Columns: TableColumn[] = [
    { key: 'subjectName', header: ' Name ', sortable: true },
    { key: 'termName', header: 'termName ', sortable: true },
    { key: 'teacherName', header: 'teacher Name ', sortable: true },
  ];
  actions: TableAction[] = [
    { actionKey: 'edit', icon: 'edit', tooltip: 'Edit User', color: 'accent' },
    { actionKey: 'delete', icon: 'delete', tooltip: 'Delete User', color: 'warn' },
  ];



  ngOnInit(): void {
    this.loadSections()
    const sectionId = this.route.snapshot.paramMap.get('sectionId');
    if (sectionId) {
      this.sectionService.getSectionById(sectionId).subscribe({
        next: res => {
          this.currentSection = res.data;

        }
      })
    }
  }

  loadSections() {
    const sectionId = this.route.snapshot.paramMap.get('sectionId');
    if (!sectionId) return;
    const yearId = 'cc0004c1-7c59-498d-e7df-08ddca96d0a8'
    this.sectionSubjectService.getBySectionId(sectionId).subscribe({
      next: res => {
        this.sections.set(res.data)


      }
    })

  }

  handleUserAction(event: { actionKey: string; rowData: any }): void {
    console.log(`Action: ${event.actionKey} on User:`, event.rowData);
    // Implement your logic here based on actionKey and rowData
    switch (event.actionKey) {

      case 'edit':
        this.openSectionSubjectDialog(event.rowData)
        break;
      case 'delete':
        this.openConfirmDialog(event.rowData.id, event.rowData.name)

        break;
    }
  }

  async openConfirmDialog(id: string, name: string) {
    const confirmed = await this.dialogService.confirm(
      'Confirm Delete',
      `Are you sure you want to delete the section: ${name}?`
    );

    if (confirmed) {
      this.sectionSubjectService.delete(id).subscribe({
        next: () => {
          this.snack.success('section deleted successfully!');
          this.loadSections()
        },
        error: (err) => {
          this.snack.error('Failed to delete section.');
          console.error(err);
        }
      });
    }
  }

  openSectionSubjectDialog(sectionSubject?: SectionSubjectInfoDto) {

    console.log(sectionSubject)
    const dialogRef = this.dialog.open(SectionSubjectAssignmentFormComponent, {
      width: '400px',
      data: {
        model: sectionSubject,
        currentSection: this.currentSection
      }
    });


    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadSections()
      }
    });
  }
}
