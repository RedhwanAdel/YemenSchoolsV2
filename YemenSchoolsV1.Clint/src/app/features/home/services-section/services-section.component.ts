import { Component } from '@angular/core';
import { TitelSectionComponent } from "../../../shared/components/titel-section/titel-section.component";
import { ServiceSectionItemComponent } from "./service-section-item/service-section-item.component";

@Component({
  selector: 'app-services-section',
  standalone: true,
  imports: [TitelSectionComponent, ServiceSectionItemComponent],
  templateUrl: './services-section.component.html',
  styleUrl: './services-section.component.css'
})
export class ServicesSectionComponent {

}
