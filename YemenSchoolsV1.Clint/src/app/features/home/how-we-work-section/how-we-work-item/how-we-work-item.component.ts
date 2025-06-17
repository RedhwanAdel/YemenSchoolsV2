import { Component, input } from '@angular/core';

@Component({
  selector: 'app-how-we-work-item',
  standalone: true,
  imports: [],
  templateUrl: './how-we-work-item.component.html',
  styleUrl: './how-we-work-item.component.css'
})
export class HowWeWorkItemComponent {
  num = input.required<string>();
  icon = input.required<string>();
  titel = input.required<string>();
  description = input.required<string>();
}
