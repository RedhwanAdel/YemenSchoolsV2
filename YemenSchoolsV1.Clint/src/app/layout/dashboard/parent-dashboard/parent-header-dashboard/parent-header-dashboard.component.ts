import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { MatButton, MatButtonModule, MatFabButton } from '@angular/material/button';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatMenu } from '@angular/material/menu';
import { MatToolbar, MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-parent-header-dashboard',
  standalone: true,
  imports: [
    CommonModule, MatToolbarModule, MatIconModule, MatButtonModule
  ],
  templateUrl: './parent-header-dashboard.component.html',
  styleUrl: './parent-header-dashboard.component.scss'
})
export class ParentHeaderDashboardComponent {
  @Output() modeToggle = new EventEmitter<void>();
  @Output() notifications = new EventEmitter<void>();
  isDarkMode = false;

  toggleMode() {
    this.isDarkMode = !this.isDarkMode;
  }
}
