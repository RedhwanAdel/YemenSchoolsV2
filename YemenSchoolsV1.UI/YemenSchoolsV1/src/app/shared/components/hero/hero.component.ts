import { Component } from '@angular/core';
import { PrimaryButtonComponent } from "../buttons/primary-button/primary-button.component";

@Component({
  selector: 'app-hero',
  standalone: true,
  imports: [PrimaryButtonComponent],
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.css'
})
export class HeroComponent {

}
