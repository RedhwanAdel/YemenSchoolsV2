import { Component, inject, OnInit } from '@angular/core';
import { SchoolComponent } from "../../features/school/school.component";
import { RegionComponent } from "../../features/region/region.component";
import { HeroComponent } from "../../shared/components/hero/hero.component";
import { HttpParams } from '@angular/common/http';
import { SchoolServiceService } from '../../features/school/services/school.service.service';
import { SchoolModel } from '../../features/school/models/school.model';
import { SchoolCardComponent } from "../../features/school/components/school-card/school-card.component";
import { MainTitelComponent } from "../../shared/components/main-titel/main-titel.component";
import { ContactComponent } from "../contact/contact.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegionComponent, HeroComponent, SchoolCardComponent, MainTitelComponent, ContactComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  private schoolService = inject(SchoolServiceService);
    schools: SchoolModel[] = [];
  filtterParams = new HttpParams();




  ngOnInit(): void {
this.loadSchools()
  }

  loadSchools(): void {
    this.filtterParams = new HttpParams()
      .set("PageNumber", 1)
      .set("PageSize", 4)

      this.schoolService.getSchools(this.filtterParams).subscribe(res => {
        if (res.succeeded) {
          this.schools = res.data;
        } else {
          console.error("Error:", res.message);
        }
      });
  }
}