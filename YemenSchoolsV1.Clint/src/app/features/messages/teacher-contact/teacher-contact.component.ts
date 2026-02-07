import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute } from '@angular/router';
import { MessageService } from '@features/messages/services/message.service';
import { Message } from '@features/messages/models/messages';
import { AccountService } from '../../../core/services/account.service';
import { TeacherService } from '@features/school-dashboard/teacher/services/teacher.service';
import { Teacher } from '@features/school-dashboard/teacher/models/teachers';

@Component({
  selector: 'app-teacher-contact',
  standalone: true,
  imports: [
    FormsModule,              // يدعم [(ngModel)]
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    CommonModule
  ],
  templateUrl: './teacher-contact.component.html',
  styleUrl: './teacher-contact.component.scss'
})
export class TeacherContactComponent {
  messageService = inject(MessageService)
  accountService = inject(AccountService)
  teacherService = inject(TeacherService)
  schoolName = 'مدرسة الشروق الأهلية';
  teacher: Teacher | null = null;
  recipientId: string = ''
  senderId?: string = ''
  protected messages = signal<Message[]>([]);
  private route = inject(ActivatedRoute);
  currentUserId: string = '';


  protected messageContent = '';


  ngOnInit(): void {

    const currentUser = this.accountService.currentUser();
    this.currentUserId = currentUser?.id ?? '';
    this.recipientId = this.route.snapshot.paramMap.get('id')!;
    // this.senderId = this.accountService.currentUser()?.id
    // this.teacherService.getTeacherById(this.recipientId).subscribe({
    //   next: res => this.teacher = res
    // })
    // this.loadMessages()
    // // تقدر تجيب الرسائل من API هنا
    // this.teacherService.getTeacherById(this.recipientId).subscribe({
    //   next: res => {
    //     this.teacher = res
    //   }
    // })

    this.route.parent?.paramMap.subscribe({
      next: params => {
        const otherUserId = this.recipientId
        console.log(otherUserId)
        if (!otherUserId) throw new Error('Cannot connect to hub');
        this.messageService.createHubConnection(otherUserId);
      }
    })
  }

  // loadMessages() {

  //   if (this.recipientId) {
  //     this.messageService.getMessageThread(this.recipientId).subscribe({
  //       next: messages => this.messages.set(messages.map(message => ({
  //         ...message,
  //         currentUserSender: message.senderId !== this.recipientId
  //       })))
  //     })
  //   }
  // }



  sendMessage() {
    if (!this.recipientId) return;
    this.messageService.sendMessage(this.recipientId, this.messageContent)?.subscribe({
      next: () => {
        this.messageContent = '';
      }
    })
  }

  sendMessageeeeeeeeeee() {
    // if (!this.newMessage.trim()) return;

    // this.messages.push({
    //   sender: 'student',
    //   text: this.newMessage,
    //   time: new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }),
    // });

    // this.newMessage = '';

    // Scroll لآخر رسالة
    setTimeout(() => {
      const container = document.querySelector('.chat-messages');
      container?.scrollTo({ top: container.scrollHeight, behavior: 'smooth' });
    }, 100);
  }
}
