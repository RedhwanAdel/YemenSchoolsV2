import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-school-sidebar-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatListModule,
    MatIconModule,
    MatDividerModule,
    MatButtonModule,
    RouterLink
  ],
  templateUrl: './school-sidebar-dashboard.component.html',
  styleUrl: './school-sidebar-dashboard.component.scss'
})
export class SchoolSidebarDashboardComponent {
  @Input() isMobileView: boolean = false; // Input to control close button visibility
  @Output() closeSidebar = new EventEmitter<void>(); // Event emitter for closing
}
