import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
interface NewsItem {
  title: string;
  excerpt: string;
  date: string;
  imageUrl: string;
}

interface EventItem {
  title: string;
  day: number;
  month: string;
  time: string;
  location: string;
}
@Component({
  selector: 'app-school-news-events',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    MatListModule,
    MatDividerModule,
  ],
  templateUrl: './school-news-events.component.html',
  styleUrl: './school-news-events.component.scss'
})
export class SchoolNewsEventsComponent {
  news: NewsItem[] = [];
  events: EventItem[] = [];

  ngOnInit(): void {
    // بيانات الأخبار الوهمية
    this.news = [
      {
        title: 'طلابنا يتألقون في مسابقة الروبوت الإقليمية',
        excerpt: 'فريقنا للروبوت يفوز بالمركز الأول بعد تصميم روبوت مبتكر لحل مشكلات بيئية.',
        date: '2025/11/15',
        imageUrl: 'assets/images/first-lego-league-junior.jpg',
      },
      {
        title: 'يوم مفتوح للتسجيل في المرحلة الثانوية',
        excerpt: 'ندعو أولياء الأمور لزيارة الأكاديمية والتعرف على المناهج الجديدة والمنح المتاحة.',
        date: '2025/11/10',
        imageUrl: 'assets/images/pngtree-courtyard-of-a-school-building-picture-from-yumi-gyodo-picture-image_2467184.jpg',
      },
      {
        title: 'تخريج دفعة 2025: حفل مهيب وذكريات لا تُنسى',
        excerpt: 'احتفال ضخم لتخريج طلابنا والاحتفاء بإنجازاتهم الأكاديمية والشخصية.',
        date: '2025/11/01',
        imageUrl: 'assets/images/32d9acc6232613053.jpg',
      },
    ];

    // بيانات الفعاليات الوهمية
    this.events = [
      {
        title: 'اجتماع أولياء الأمور الفصلي',
        day: 25,
        month: 'نوفمبر',
        time: '10:00 صباحًا',
        location: 'قاعة المؤتمرات الرئيسية',
      },
      {
        title: 'بطولة الشروق السنوية لكرة القدم',
        day: 5,
        month: 'ديسمبر',
        time: '4:00 مساءً',
        location: 'الملعب الرياضي',
      },
      {
        title: 'معرض الفنون والإبداع الطلابي',
        day: 12,
        month: 'ديسمبر',
        time: '1:00 ظهرًا',
        location: 'استوديو الفنون',
      },
    ];
  }
}
