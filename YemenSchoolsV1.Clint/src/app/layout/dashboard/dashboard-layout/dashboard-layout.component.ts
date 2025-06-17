import { BreakpointObserver } from '@angular/cdk/layout';
import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule, MatSidenav } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { SidebarDashboardComponent } from "../../../shared/components/dashboard/sidebar-dashboard/sidebar-dashboard.component";
import { HeaderDashboardComponent } from "../../../shared/components/dashboard/header-dashboard/header-dashboard.component";
import { StatCardDashboardComponent } from '../../../shared/components/dashboard/stat-card-dashboard/stat-card-dashboard.component';
import { TableAction, TableColumn, TableComponent } from "../../../shared/components/table/table.component";
import { MatTableDataSource } from '@angular/material/table';
import { CityListComponent } from "../../../dashboard/pages/cities/city-list/city-list.component";
import { SchoolListComponent } from "../../../dashboard/pages/schools/school-list/school-list.component";
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-dashboard-layout',
  standalone: true,
  imports: [
    CommonModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatDividerModule,
    MatListModule,
    SidebarDashboardComponent,
    HeaderDashboardComponent,

    RouterOutlet
  ],
  templateUrl: './dashboard-layout.component.html',
  styleUrl: './dashboard-layout.component.scss'
})
export class DashboardLayoutComponent {
  @ViewChild('sidenav') sidenav!: MatSidenav; // Reference to the mat-sidenav

  isSmallScreen = false;

  constructor(private breakpointObserver: BreakpointObserver) {
    this.breakpointObserver
      .observe(['(max-width: 768px)'])
      .subscribe(result => {
        this.isSmallScreen = result.matches;
      });
  }







}
