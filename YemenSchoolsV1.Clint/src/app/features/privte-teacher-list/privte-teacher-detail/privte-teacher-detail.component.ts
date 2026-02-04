import { CommonModule, DecimalPipe } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatTabsModule } from '@angular/material/tabs';
export interface Teacher {
  id: number;
  name: string;
  specialty: string;
  avatarUrl: string;
  rating: number;
  reviewsCount: number;
  features: string[];
  // تفاصيل الصفحة الجديدة:
  bio: string;
  certifications: Certification[];
  topics: Topic[];
  reviews: Review[];
}
export interface Certification {
  title: string;
  institution: string;
  year: number;
}

export interface Topic {
  name: string;
  level: string;
  description: string;
}

export interface Review {
  user: string;
  stars: number;
  date: string;
  comment: string;
}
@Component({
  selector: 'app-privte-teacher-detail',
  standalone: true,
  imports: [
    CommonModule,
    DecimalPipe,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule,
    MatTabsModule,
    MatListModule,
    MatExpansionModule,
  ],
  templateUrl: './privte-teacher-detail.component.html',
  styleUrl: './privte-teacher-detail.component.scss'
})
export class PrivteTeacherDetailComponent {
  teacher!: Teacher; // سنقوم بتهيئة هذا الكائن في ngOnInit

  ngOnInit(): void {
    // 🚀 تهيئة البيانات الثابتة (Mock Data) هنا
    this.teacher = {
      id: 1,
      name: 'أحمد علي العباسي',
      specialty: 'مدرّس لغة عربية وخبير في النحو',
      avatarUrl: 'assets/img/person/person-m-2.webp', // تأكد من وجود صورة
      rating: 4.8,
      reviewsCount: 155,
      features: ['خبرة 10 سنوات', 'مناهج تفاعلية', 'مستوى متقدم'],

      // بيانات قسم تفاصيل المعلم
      bio: 'أنا أحمد العباسي، شغوف بتدريس اللغة العربية وآدابها، خاصة النحو والصرف. أهدف إلى تبسيط القواعد المعقدة وتقديمها بطريقة ممتعة وتفاعلية، مما يمكن الطلاب من إتقان اللغة والتعبير السليم. لدي خبرة واسعة في إعداد الطلاب لاختبارات القبول الجامعية والدورات المتقدمة في الأدب العربي.',
      certifications: [
        { title: 'ماجستير في النحو والصرف', institution: 'جامعة القاهرة', year: 2018 },
        { title: 'دبلوم تدريس اللغة العربية لغير الناطقين بها', institution: 'مركز تعليم اللغات', year: 2020 }
      ],
      topics: [
        { name: 'القواعد الأساسية للنحو', level: 'مبتدئ/متوسط', description: 'شرح مفصل للجملة الاسمية والفعلية، الفاعل والمفعول به، والحروف الناسخة.' },
        { name: 'الصرف والمشتقات', level: 'متوسط', description: 'دراسة أوزان الأفعال والأسماء، والتعرف على المشتقات كاسم الفاعل والمفعول.' },
        { name: 'التحليل الأدبي المتقدم', level: 'متقدم', description: 'تحليل نصوص من الشعر الجاهلي والعباسي مع التركيز على البلاغة والمعاني.' }
      ],
      reviews: [
        { user: 'فاطمة م.', stars: 5, date: '10 نوفمبر 2025', comment: 'أفضل مدرس لغة عربية تعاملت معه! شرح مبسط وممتاز.' },
        { user: 'خالد س.', stars: 4, date: '01 نوفمبر 2025', comment: 'ممتاز في النحو، لكن يحتاج إلى مزيد من التركيز على مهارات التعبير.' }
      ]
    };
  }

  /**
   * دالة مساعدة لحساب النجوم
   */
  getStars(rating: number) {
    const fullStars = Math.floor(rating);
    const emptyStars = 5 - fullStars;
    return { full: fullStars, empty: emptyStars };
  }
}
