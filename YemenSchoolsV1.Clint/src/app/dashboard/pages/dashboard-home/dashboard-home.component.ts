import { Component } from '@angular/core';
import { StatCardDashboardComponent } from "../../../shared/components/dashboard/stat-card-dashboard/stat-card-dashboard.component";

@Component({
  selector: 'app-dashboard-home',
  standalone: true,
  imports: [StatCardDashboardComponent],
  templateUrl: './dashboard-home.component.html',
  styleUrl: './dashboard-home.component.scss'
})
export class DashboardHomeComponent {

}
