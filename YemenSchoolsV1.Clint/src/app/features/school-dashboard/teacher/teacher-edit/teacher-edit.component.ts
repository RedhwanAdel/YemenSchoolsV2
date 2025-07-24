import { Component, inject, OnInit } from '@angular/core';
import { TeacherFormComponent } from "../teacher-form/teacher-form.component";
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-teacher-edit',
  standalone: true,
  imports: [TeacherFormComponent],
  templateUrl: './teacher-edit.component.html',
  styleUrl: './teacher-edit.component.scss'
})
export class TeacherEditComponent implements OnInit {
  private route = inject(ActivatedRoute)
  teacherId!: string;

  ngOnInit(): void {
    this.teacherId = this.route.snapshot.paramMap.get('id')!;
  }
}
