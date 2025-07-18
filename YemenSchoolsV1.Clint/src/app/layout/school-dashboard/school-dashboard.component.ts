import { BreakpointObserver } from '@angular/cdk/layout';
import { Component, ViewChild } from '@angular/core';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { HeaderDashboardComponent } from "../../shared/components/dashboard/header-dashboard/header-dashboard.component";
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { SchoolSidebarDashboardComponent } from "./school-sidebar-dashboard/school-sidebar-dashboard.component";

@Component({
  selector: 'app-school-dashboard',
  standalone: true,
  imports: [HeaderDashboardComponent,
    CommonModule,
    RouterOutlet,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatDividerModule,
    MatListModule, SchoolSidebarDashboardComponent],
  templateUrl: './school-dashboard.component.html',
  styleUrl: './school-dashboard.component.scss'
})
export class SchoolDashboardComponent {
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
