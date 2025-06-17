import { Component, input } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-main-titel',
  standalone: true,
  imports: [],
  templateUrl: './main-titel.component.html',
  styleUrl: './main-titel.component.css'
})
export class MainTitelComponent {
titel = input.required<string>();
}
