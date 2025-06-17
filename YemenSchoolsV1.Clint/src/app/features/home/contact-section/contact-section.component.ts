import { Component } from '@angular/core';
import { TitelSectionComponent } from "../../../shared/components/titel-section/titel-section.component";

@Component({
  selector: 'app-contact-section',
  standalone: true,
  imports: [TitelSectionComponent],
  templateUrl: './contact-section.component.html',
  styleUrl: './contact-section.component.css'
})
export class ContactSectionComponent {

}
