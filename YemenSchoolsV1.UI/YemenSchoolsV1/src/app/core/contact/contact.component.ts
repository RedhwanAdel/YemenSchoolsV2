import { Component } from '@angular/core';
import { MainTitelComponent } from "../../shared/components/main-titel/main-titel.component";

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [MainTitelComponent],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent {

}
