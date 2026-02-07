import { Component, inject, OnInit } from '@angular/core';
import { SelectInputComponent } from "../select-input/select-input.component";
import { AcadmicYearService } from '@features/school-dashboard/year/services/acadmic-year.service';
import { GradeService } from '@features/school-dashboard/school-grade/services/grade.service';
import { SectionService } from '@features/school-dashboard/section/services/section.service';
import { StageService } from '@features/school-dashboard/stage/services/stage.service';
import { TermService } from '@features/school-dashboard/term/services/term.service';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatOption } from '@angular/material/core';
import { SelectParams } from '../../models/selectParams';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-school-levels-selector',
  standalone: true,
  imports: [MatFormField, MatOption, MatLabel, FormsModule, MatSelectModule],
  templateUrl: './school-levels-selector.component.html',
  styleUrl: './school-levels-selector.component.scss'
})
export class SchoolLevelsSelectorComponent implements OnInit {
  private fb = inject(FormBuilder)

  stageService = inject(StageService)
  acadmicYearService = inject(AcadmicYearService)
  termService = inject(TermService)
  gradeService = inject(GradeService)
  sectionService = inject(SectionService)

  params = new SelectParams();

  ngOnInit(): void {
    this.stageService.getStages();
  }

  onStageChange() {
    this.acadmicYearService.getAcademicYears(this.params.stageId)
  }
  onYearChange() {
    this.termService.getTerms(this.params.yearId)

  }
  onTermChange() {
    this.gradeService.getGrades(this.params.termId)

  }
  onGradeChange() {
    this.sectionService.getSections(this.params.gradeId)

  }
  onSectionChange() {
    console.log(this.params)
  }
}
