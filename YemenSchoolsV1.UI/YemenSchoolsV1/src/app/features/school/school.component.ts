import { Component, inject, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { SchoolModel, SchoolPaginationModel } from './models/school.model';
import { SchoolServiceService } from './services/school.service.service';
import { SchoolCardComponent } from "./components/school-card/school-card.component";
import { RegionService } from '../region/services/region.service';
import { RegionModel } from '../region/models/region.model';
import { DropDownListComponent } from "../../shared/components/drop-down-list/drop-down-list.component";
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-school',
  standalone: true,
  imports: [SchoolCardComponent, DropDownListComponent],
  templateUrl: './school.component.html',
  styleUrl: './school.component.css'
})
export class SchoolComponent implements OnInit {
  params: SchoolPaginationModel = {
    PageNumber: 1,
    PageSize: 12,
    OrderBy: 0,
    Search: '',
    CityId: '',
    RegionId: ''
  };

  schools: SchoolModel[] = [];
  regions: RegionModel[] = [];
  filtterParams = new HttpParams();

  private schoolService = inject(SchoolServiceService);
  private regionService = inject(RegionService);

  ngOnInit(): void {
    this.loadRegions(); // تحميل المناطق
    this.loadSchools(); // تحميل المدارس
  }

  // تحميل قائمة المناطق
  loadRegions(): void {
    this.regionService.getAllRegions().subscribe(res => {
      this.regions = res.data;
    });
  }

  // تحميل المدارس بناءً على المعايير
  loadSchools(): void {
    this.filtterParams = new HttpParams()
      .set("PageNumber", this.params.PageNumber)
      .set("PageSize", this.params.PageSize)
      .set("OrderBy", this.params.OrderBy)
      .set("Search", this.params.Search)
      .set("CityId", this.params.CityId)
      .set("RegionId", this.params.RegionId);

    this.schoolService.getSchools(this.filtterParams).subscribe(res => {
      if (res.succeeded) {
        this.schools = res.data;
      } else {
        console.error("Error:", res.message);
      }
    });
  }


  orderby:any[] =[
    {id:2, name:"city" },{id:3,name:"region"}
  ]
  // استدعاء عند تغيير المنطقة
  onFilter(regionId: string): void {
    console.log(regionId)
    console.log("regionId")
    this.params.RegionId = regionId;
    this.loadSchools(); // إعادة تحميل المدارس بعد تغيير المنطقة
  }
  onorder(orderby:string){
    this.params.OrderBy= +orderby
    this.loadSchools();
  }
  trackById(index: number, school: SchoolModel): string {
    return school.id;
  }
}
