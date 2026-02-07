import { DatePipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { RouterLink } from '@angular/router';
import { MessageService } from '@features/messages/services/message.service';
import { Message } from '@features/messages/models/messages';

@Component({
  selector: 'app-messages-dropdown',
  standalone: true,
  imports: [
    MatTabsModule,
    MatTableModule,
    MatIconModule,
    DatePipe,
    MatButtonModule,
    RouterLink
  ],
  templateUrl: './messages-dropdown.component.html',
  styleUrl: './messages-dropdown.component.scss'
})
export class MessagesDropdownComponent {
  private messageService = inject(MessageService);
  protected container = 'Inbox';
  protected messages = signal<Message[] | null>(null);
  displayedColumns = ['user', 'content', 'date'];
  containerIndex = 0;

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages() {
    this.messageService.getMessages(this.container, 1, 5).subscribe({
      next: response => {
        this.messages.set(response.data);
      }
    });
  }

  onTabChange(index: number) {
    this.containerIndex = index;
    this.container = index === 0 ? 'Inbox' : 'Outbox';
    this.loadMessages();
  }

  get isInbox() {
    return this.container === 'Inbox';
  }
}
