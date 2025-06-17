import { Component } from '@angular/core';
import { HeaderComponent } from "../header/header.component";
import { RouterOutlet } from '@angular/router';
import { FooterComponent } from "../footer/footer.component";
import { HomeComponent } from "../../features/home/home.component";

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [HeaderComponent,
    RouterOutlet, FooterComponent, HomeComponent],
  templateUrl: './main-layout.component.html',
  styleUrl: './main-layout.component.scss'
})
export class MainLayoutComponent {

}
