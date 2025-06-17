import { Component, input } from '@angular/core';

@Component({
  selector: 'app-titel-section',
  standalone: true,
  imports: [],
  templateUrl: './titel-section.component.html',
  styleUrl: './titel-section.component.scss'
})
export class TitelSectionComponent {
  titel = input.required<string>();
  description = input.required<string>();
}
