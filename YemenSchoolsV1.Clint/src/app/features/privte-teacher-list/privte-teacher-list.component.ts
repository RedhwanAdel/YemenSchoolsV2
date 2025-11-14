import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
interface Teacher {
  name: string;
  specialty: string;
  avatarUrl: string;
  rating: number;
  reviewsCount: number;
  features: string[];
}
@Component({
  selector: 'app-privte-teacher-list',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    MatChipsModule,
  ],
  templateUrl: './privte-teacher-list.component.html',
  styleUrl: './privte-teacher-list.component.scss'
})
export class PrivteTeacherListComponent {
  teachers: Teacher[] = [
    {
      name: 'أحمد الهاشمي',
      specialty: 'مدرس رياضيات وفيزياء (ثانوي)',
      avatarUrl: 'assets/img/person/person-m-2.webp',
      rating: 4.9,
      reviewsCount: 120,
      features: ['10 سنوات خبرة', 'تدريس أونلاين / حضوري', 'منهج وزاري'],
    },
    {
      name: 'سارة محمد',
      specialty: 'مدرسة لغة إنجليزية ولغة عربية (ابتدائي)',
      avatarUrl: 'assets/img/person/person-f-3.webp',
      rating: 4.7,
      reviewsCount: 85,
      features: ['5 سنوات خبرة', 'تأسيس لغوي', 'مناسب للأطفال'],
    },
    {
      name: 'خالد الحربي',
      specialty: 'مدرس كيمياء وأحياء (جامعي)',
      avatarUrl: 'assets/img/person/person-m-5.webp',
      rating: 5.0,
      reviewsCount: 210,
      features: ['15 سنة خبرة', 'مستويات متقدمة', 'تحضير للاختبارات'],
    },
    {
      name: 'خالد الحربي',
      specialty: 'مدرس كيمياء وأحياء (جامعي)',
      avatarUrl: 'assets/img/person/person-m-1.webp',
      rating: 5.0,
      reviewsCount: 210,
      features: ['15 سنة خبرة', 'مستويات متقدمة', 'تحضير للاختبارات'],
    },
    {
      name: 'سارة محمد',
      specialty: 'مدرسة لغة إنجليزية ولغة عربية (ابتدائي)',
      avatarUrl: 'assets/img/person/person-f-7.webp',
      rating: 4.7,
      reviewsCount: 85,
      features: ['5 سنوات خبرة', 'تأسيس لغوي', 'مناسب للأطفال'],
    },
    {
      name: 'خالد الحربي',
      specialty: 'مدرس كيمياء وأحياء (جامعي)',
      avatarUrl: 'assets/img/person/person-m-6.webp',
      rating: 5.0,
      reviewsCount: 210,
      features: ['15 سنة خبرة', 'مستويات متقدمة', 'تحضير للاختبارات'],
    },

  ];

  constructor() { }

  ngOnInit(): void { }

  // دالة لحساب عدد النجوم الممتلئة والفارغة
  getStars(rating: number): { full: number; empty: number } {
    const full = Math.floor(rating);
    const hasHalf = rating - full >= 0.5 ? 1 : 0;
    const empty = 5 - full - hasHalf;
    return { full, empty };
  }
}
