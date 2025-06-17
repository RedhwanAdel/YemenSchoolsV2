import { Component, input } from '@angular/core';
import { RegionModel } from '../../models/region.model';

@Component({
  selector: 'app-region-card',
  standalone: true,
  imports: [],
  templateUrl: './region-card.component.html',
  styleUrl: './region-card.component.css'
})
export class RegionCardComponent {
  region = input.required<RegionModel>()


}
