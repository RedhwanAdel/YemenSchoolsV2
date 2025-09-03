import { Component } from '@angular/core';
import { ParentSidebarDashboardComponent } from "./parent-sidebar-dashboard/parent-sidebar-dashboard.component";
import { ParentHeaderDashboardComponent } from "./parent-header-dashboard/parent-header-dashboard.component";
import { MatSidenavModule } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';
import { Student } from '../../../shared/models/student/student';
import { CommonModule } from '@angular/common';
import { ParentDashboardPageComponent } from "../../../features/parent-dashboard/parent-dashboard-page/parent-dashboard-page.component";

@Component({
  selector: 'app-parent-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    ParentSidebarDashboardComponent,
    ParentHeaderDashboardComponent,
    RouterOutlet
  ],
  templateUrl: './parent-dashboard.component.html',
  styleUrl: './parent-dashboard.component.scss'
})
export class ParentDashboardComponent {
  currentView: 'dashboard' | 'profile' = 'dashboard';
  selectedChild: Student | undefined;

  // Mock data - replace with a real service later


  showProfile(id: string) {
    // this.selectedChild = this.students.find(s => s.id === id);
    // if (this.selectedChild) {
    //   this.currentView = 'profile';
    // }
  }

  toggleDarkMode() {
    document.body.classList.toggle('dark');
  }
}
