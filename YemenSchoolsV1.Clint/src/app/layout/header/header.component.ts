import { Component, inject } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AccountService } from '../../core/services/account.service';
import { MatMenuModule } from '@angular/material/menu';
import { MatIcon } from '@angular/material/icon';
import { MatDivider } from '@angular/material/divider';
import { UserType } from '../../shared/models/enum/userType';
import { MessagesDropdownComponent } from "../../features/messages/messages-dropdown/messages-dropdown.component";
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink,
    MatMenuModule,
    MatIcon,
    RouterLinkActive,
    MatButtonModule,
    MatMenuModule,

    MessagesDropdownComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  accountService = inject(AccountService)
  userTypes = UserType;

  logout() {
    this.accountService.logout().subscribe({
      next: () => {
        this.accountService.currentUser.set(null)
        window.location.reload()

      }
    })
  }
}
