import { Component } from '@angular/core';
import { StudnetFormComponent } from "../studnet-form/studnet-form.component";

@Component({
  selector: 'app-student-add',
  standalone: true,
  imports: [StudnetFormComponent],
  templateUrl: './student-add.component.html',
  styleUrl: './student-add.component.scss'
})
export class StudentAddComponent {

}
