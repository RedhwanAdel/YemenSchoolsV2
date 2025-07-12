import { Component, inject } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AccountService } from '../../core/services/account.service';
import { MatMenuModule } from '@angular/material/menu';
import { MatIcon } from '@angular/material/icon';
import { MatDivider } from '@angular/material/divider';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink,
    MatMenuModule,
    MatIcon,
    RouterLinkActive

  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  accountService = inject(AccountService)
  logout() {
    this.accountService.logout().subscribe({
      next: () => this.accountService.currentUser.set(null)
    })
  }
}
