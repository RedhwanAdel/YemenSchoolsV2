import { Component, input } from '@angular/core';

@Component({
  selector: 'app-service-section-item',
  standalone: true,
  imports: [],
  templateUrl: './service-section-item.component.html',
  styleUrl: './service-section-item.component.css'
})
export class ServiceSectionItemComponent {
  icon = input.required<string>();
  titel = input.required<string>();
  description = input.required<string>();
}
