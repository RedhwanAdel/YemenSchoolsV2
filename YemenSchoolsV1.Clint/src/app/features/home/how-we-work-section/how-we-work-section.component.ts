import { Component } from '@angular/core';
import { TitelSectionComponent } from "../../../shared/components/titel-section/titel-section.component";
import { HowWeWorkItemComponent } from "./how-we-work-item/how-we-work-item.component";

@Component({
  selector: 'app-how-we-work-section',
  standalone: true,
  imports: [TitelSectionComponent, HowWeWorkItemComponent],
  templateUrl: './how-we-work-section.component.html',
  styleUrl: './how-we-work-section.component.css'
})
export class HowWeWorkSectionComponent {

}
