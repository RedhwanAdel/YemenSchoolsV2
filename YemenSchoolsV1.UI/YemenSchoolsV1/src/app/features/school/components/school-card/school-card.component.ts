import { Component, input } from '@angular/core';
import { SchoolModel } from '../../models/school.model';

@Component({
  selector: 'app-school-card',
  standalone: true,
  imports: [],
  templateUrl: './school-card.component.html',
  styleUrl: './school-card.component.css'
})
export class SchoolCardComponent {
school = input.required<SchoolModel>();
}
