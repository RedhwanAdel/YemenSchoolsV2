import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { MatDivider } from '@angular/material/divider';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatListModule, MatNavList } from '@angular/material/list';

@Component({
  selector: 'app-parent-sidebar-dashboard',
  standalone: true,
  imports: [CommonModule, MatListModule, MatIconModule]
  ,
  templateUrl: './parent-sidebar-dashboard.component.html',
  styleUrl: './parent-sidebar-dashboard.component.scss'
})
export class ParentSidebarDashboardComponent {
  @Output() selectSection = new EventEmitter<string>();
  activeSection = 'dashboard';

  navItems = [
    { title: 'لوحة التحكم', section: 'dashboard', icon: 'dashboard' },
    { title: 'أبنائي', section: 'children', icon: 'group' },
    { title: 'التقويم', section: 'calendar', icon: 'calendar_today' },
    { title: 'الرسائل', section: 'messages', icon: 'mail_outline' },
    { title: 'التقارير', section: 'reports', icon: 'assessment' },
    { title: 'المصاريف', section: 'payments', icon: 'credit_card' },
    { title: 'الإعدادات', section: 'settings', icon: 'settings' },
  ];
}
