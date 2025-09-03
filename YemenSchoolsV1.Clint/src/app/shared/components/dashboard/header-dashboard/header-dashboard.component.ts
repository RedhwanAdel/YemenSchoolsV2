import { Component, EventEmitter, inject, OnInit, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AccountService } from '../../../../core/services/account.service';
import { User } from '../../../models/user';

@Component({
  selector: 'app-header-dashboard',
  standalone: true,
  imports: [
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule
  ],
  templateUrl: './header-dashboard.component.html',
  styleUrl: './header-dashboard.component.scss'
})
export class HeaderDashboardComponent implements OnInit {

  @Output() menuToggled = new EventEmitter<void>();
  accountService = inject(AccountService)
  onMenuToggle() {
    this.menuToggled.emit();
  }
  user: User | null = null;
  onLogout() {
    // Implement logout logic here
    console.log('Logout clicked');
    // For a real app, you would typically call an authentication service
  }

  ngOnInit(): void {
    this.user = this.accountService.currentUser()
  }
}
