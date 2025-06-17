import { Component } from '@angular/core';
import { TitelSectionComponent } from "../../../shared/components/titel-section/titel-section.component";

@Component({
  selector: 'app-pricing-section',
  standalone: true,
  imports: [TitelSectionComponent],
  templateUrl: './pricing-section.component.html',
  styleUrl: './pricing-section.component.css'
})
export class PricingSectionComponent {

}
