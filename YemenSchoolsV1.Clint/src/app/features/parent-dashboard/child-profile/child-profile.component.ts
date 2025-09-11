import { Component, inject, Input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatIcon } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { ChildGradesComponent } from "../child-grades/child-grades.component";
import { ChildAttendanceComponent } from "../child-attendance/child-attendance.component";
import { StudentService } from '../../../core/services/student.service';
import { ActivatedRoute } from '@angular/router';
import { SnackbarService } from '../../../core/services/snackbar.service';
import { Student } from '../../../shared/models/student/student';
import { ParentService } from '../../../core/services/parent.service';
import { map } from 'rxjs';
import { StudentWithSchoolInfoDto } from '../../../shared/models/parent';
import { ChildDailyLogComponent } from "../child-daily-log/child-daily-log.component";

@Component({
  selector: 'app-child-profile',
  standalone: true,
  imports: [
    MatCardModule,
    MatTabsModule,
    MatIcon,
    ChildGradesComponent,
    ChildAttendanceComponent,
    ChildDailyLogComponent
  ],
  templateUrl: './child-profile.component.html',
  styleUrl: './child-profile.component.scss'
})
export class ChildProfileComponent {
  parentService = inject(ParentService)
  private route = inject(ActivatedRoute);
  private snackbar = inject(SnackbarService)
  student: StudentWithSchoolInfoDto | null = null;
  ngOnInit(): void {

    const studentId = this.route.snapshot.paramMap.get('studentId');
    if (!studentId) {
      this.snackbar.error('لا يمكن ايجاد معرف الطالب');
      return;
    }
    this.parentService.GetStudentsWithSchoolInfoForParent().pipe(
      map(res => {
        const student = res.data.find(x => x.studentId === studentId)
        return student
      })
    ).subscribe({
      next: res => {
        if (res)
          this.student = res
      }
    })
  }

}
