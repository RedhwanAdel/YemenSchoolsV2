import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { Student } from '../../../shared/models/student/student';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { StudentWithSchoolInfoDto } from '../../../shared/models/parent';

@Component({
  selector: 'app-child-card',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatProgressBarModule],
  templateUrl: './child-card.component.html',
  styleUrl: './child-card.component.scss'
})
export class ChildCardComponent implements OnInit {
  @Input({ required: true }) child!: StudentWithSchoolInfoDto;
  @Output() openProfile = new EventEmitter<string>();
  @Output() messageTeacher = new EventEmitter<number>();

  ngOnInit(): void {

    this.child.avg = 95
  }
  getProgressBarColor(): 'primary' | 'accent' | 'warn' {
    if (this.child.avg >= 80) {
      return 'primary'; // أداء ممتاز (أزرق)
    } else if (this.child.avg >= 65) {
      return 'accent'; // أداء جيد (أخضر)
    } else {
      return 'warn'; // يحتاج إلى تحسين (أحمر)
    }
  }
  get imageUrlOrDefault() {

    return this.child?.imageUrl?.trim() ?? '/assets/images/user/avatar-2.jpg';
  }
}
