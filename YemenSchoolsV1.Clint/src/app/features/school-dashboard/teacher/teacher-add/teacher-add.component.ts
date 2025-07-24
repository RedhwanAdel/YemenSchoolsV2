import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { TeacherFormComponent } from "../teacher-form/teacher-form.component";
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-teacher-add',
  standalone: true,
  imports: [TeacherFormComponent],
  templateUrl: './teacher-add.component.html',
  styleUrl: './teacher-add.component.scss'
})
export class TeacherAddComponent {

}
