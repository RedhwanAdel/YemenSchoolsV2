import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { FooterComponent } from "./layout/footer/footer.component";
import { HomeComponent } from "./features/home/home.component";
import { SchoolDetailsComponent } from "./features/schools/school-details/school-details.component";
import { DashboardLayoutComponent } from "./layout/dashboard/dashboard-layout/dashboard-layout.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, FooterComponent, HomeComponent, SchoolDetailsComponent, DashboardLayoutComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'YemenSchoolsV1.Clint';
}
