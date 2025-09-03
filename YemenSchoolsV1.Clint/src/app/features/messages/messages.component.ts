import { Component, inject, OnInit, signal } from '@angular/core';
import { MessageService } from '../../core/services/message.service';
import { Message } from '../../shared/models/messages/message';
import { Pagination } from '../../shared/models/Pagination';
import { DialogService } from '../../core/services/dialog.service';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { RouterLink } from '@angular/router';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-messages',
  standalone: true,
  imports: [
    MatTabsModule,
    MatTableModule,
    MatPaginatorModule,
    MatIconModule,
    MatButtonModule,
    RouterLink,
    DatePipe
  ],
  templateUrl: './messages.component.html',
  styleUrl: './messages.component.scss'
})
export class MessagesComponent implements OnInit {
  private messageService = inject(MessageService);
  private confirmDialog = inject(DialogService);
  protected container = 'Inbox';
  protected fetchedContainer = 'Inbox';
  protected pageNumber = 1;
  protected pageSize = 10;
  protected paginatedMessages = signal<Pagination<Message> | null>(null);
  displayedColumns = ['user', 'content', 'date', 'actions'];
  containerIndex = 0; // 0 = Inbox, 1 = Outbox

  tabs = [
    { label: 'Inbox', value: 'Inbox' },
    { label: 'Outbox', value: 'Outbox' },
  ]

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages() {
    this.messageService.getMessages(this.container, this.pageNumber, this.pageSize).subscribe({
      next: response => {
        this.paginatedMessages.set(response);
        this.fetchedContainer = this.container;
      }
    })
  }

  async confirmDelete(event: Event, id: string) {
    event.stopPropagation();
    const ok = await this.confirmDialog.confirm('Delet', 'Are you sure you want to delete this message?')
    if (ok) this.deleteMessage(id);
  }

  deleteMessage(id: string) {
    this.messageService.deleteMessage(id).subscribe({
      next: () => {
        const current = this.paginatedMessages();
        if (current?.data) {
          this.paginatedMessages.update(prev => {
            if (!prev) return null;

            const newItems = prev.data.filter(x => x.id !== id) || [];

            return {
              data: newItems,
              pageSize: prev.pageSize,
              currentPage: prev.currentPage,
              totalCount: prev.totalCount,
              totalPages: prev.totalPages,
              meta: prev.meta,
              hasPreviousPage: prev.hasPreviousPage,
              hasNextPage: prev.hasNextPage,
              messages: prev.messages,
              succeeded: prev.succeeded
            }
          })
        }
      }
    })
  }

  get isInbox() {
    return this.fetchedContainer === 'Inbox';
  }

  setContainer(container: string) {
    this.container = container;
    this.pageNumber = 1;
    this.loadMessages();
  }

  onPageChange(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.pageNumber = event.pageIndex + 1;
    this.loadMessages();
  }
  onTabChange(index: number) {
    this.containerIndex = index;
    this.container = index === 0 ? 'Inbox' : 'Outbox';
    this.pageNumber = 1;
    this.loadMessages();
  }

}
