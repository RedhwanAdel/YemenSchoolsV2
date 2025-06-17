import { Component, inject, OnInit } from '@angular/core';
import { RegionCardComponent } from "./components/region-card/region-card.component";
import { RegionService } from './services/region.service';
import { RegionModel } from './models/region.model';
import { MainTitelComponent } from "../../shared/components/main-titel/main-titel.component";

@Component({
  selector: 'app-region',
  standalone: true,
  imports: [RegionCardComponent, MainTitelComponent],
  templateUrl: './region.component.html',
  styleUrl: './region.component.css'
})
export class RegionComponent implements OnInit {
  private regionService = inject(RegionService);
  regions: RegionModel[] = []

  ngOnInit(): void {

    this.regionService.getAllRegions().subscribe(res => {
      if (res.succeeded) {
        this.regions = res.data.slice(0,4)
       
        console.log(res)
      } else {
        console.log("Error" + res.message)
      }
    })
  }
}
