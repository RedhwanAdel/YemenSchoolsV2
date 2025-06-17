import { AfterViewInit, Component, CUSTOM_ELEMENTS_SCHEMA, ElementRef, ViewChild } from '@angular/core';
import { TitelSectionComponent } from "../../../shared/components/titel-section/titel-section.component";

@Component({
  selector: 'app-testimonials-section',
  standalone: true,
  imports: [TitelSectionComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  templateUrl: './testimonials-section.component.html',
  styleUrl: './testimonials-section.component.css'
})
export class TestimonialsSectionComponent {


}
