import { Component, inject, OnInit, signal } from '@angular/core';
import { TitelSectionComponent } from "../../shared/components/titel-section/titel-section.component";
import { SchoolItemComponent } from "./school-item/school-item.component";
import { SchoolService } from '../../core/services/school.service';
import { SchoolListItem } from '../../shared/models/school/school';
import { SchoolOrdering, SchoolParams } from '../../shared/models/school/schoolParams';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Pagination } from '../../shared/models/Pagination';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { MatFormField, MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { CitiesService } from '../../core/services/cities.service';
import { RegionsService } from '../../core/services/regions.service';
import { Region } from '../../shared/models/region/region';
import { MainBtnDirective } from '../../shared/directives/main-btn.directive';
import { MatButton } from '@angular/material/button';
import { MatOption, MatSelect, MatSelectChange } from '@angular/material/select';

@Component({
  selector: 'app-schools',
  standalone: true,
  imports: [TitelSectionComponent,
    SchoolItemComponent,
    MatIconModule,
    FormsModule,
    MatPaginator,
    MatMenu,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger,
    MatFormFieldModule,
    MatLabel,
    MatInput,
    MatButton,
    MatSelect,
    MatOption
  ],
  templateUrl: './schools.component.html',
  styleUrl: './schools.component.scss'
})
export class SchoolsComponent implements OnInit {
  private schoolService = inject(SchoolService)
  citiesService = inject(CitiesService)
  regionService = inject(RegionsService)
  schools?: Pagination<SchoolListItem>
  sortOptions = [
    { name: 'Alphabetical', value: SchoolOrdering.Name },
    { name: 'City', value: SchoolOrdering.City },
    { name: 'Region', value: SchoolOrdering.Region },
  ]
  sortDirectionOptions = [
    { name: 'Ascending', value: 'asc' },
    { name: 'Descending', value: 'desc' },
  ];


  schoolTypes = [
    { value: 0, name: 'Private' },
    { value: 1, name: 'Public' },
    { value: 2, name: 'International' }
  ];

  genders = [
    { value: 0, name: 'Boys' },
    { value: 1, name: 'Girls' },
    { value: 2, name: 'Both' }
  ];

  schoolLevels = [
    { value: 1, name: 'Kindergarten' },
    { value: 2, name: 'Elementary' },
    { value: 4, name: 'Middle' },
    { value: 8, name: 'High' }
  ];
  selectedLevels: number[] = [];


  schoolParams = new SchoolParams();
  pageSizeOptions = [5, 10, 15, 20]
  regions = signal<Region[]>([]);


  ngOnInit(): void {
    this.initialiseSchool()
  }

  initialiseSchool() {
    this.getSchools();
    this.citiesService.getCites();
  }
  getSchools() {
    this.schoolService.getSchools(this.schoolParams).subscribe({
      next: res => this.schools = res,
      error: error => console.log(error)
    })
  }

  onCityChange(event: MatSelectChange) {
    const cityId = event.value;

    if (!cityId) return;

    this.schoolParams.cityId = cityId;
    this.schoolParams.regionId = '';

    this.regionService.getRegionsByCity(cityId).subscribe({
      next: (res) => this.regions.set(res.data),
      error: (err) => console.error(err)
    });
  }


  applyFilters(): void {

    this.schoolParams.pageNumber = 1;
    this.getSchools();
  }

  handlePageEvent(event: PageEvent) {
    this.schoolParams.pageNumber = event.pageIndex + 1;
    this.schoolParams.pageSize = event.pageSize;
    this.getSchools();
  }
  onSearchChange() {
    this.schoolParams.pageNumber = 1;
    this.getSchools();
  }

  onSortChange(value: number) {
    this.schoolParams.orderBy = value as SchoolOrdering;
    this.getSchools();
  }
  onSortDirectionChange(value: string) {
    this.schoolParams.sortDirection = value as 'asc' | 'desc';
    this.getSchools();
  }


  onLevelsChange() {
    this.schoolParams.levels = this.selectedLevels.reduce((acc, val) => acc | val, 0);
  }
}
