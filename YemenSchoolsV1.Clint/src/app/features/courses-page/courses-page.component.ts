import { Component } from '@angular/core';
import { CourseCardComponent } from "./course-card/course-card.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-courses-page',
  standalone: true,
  imports: [CourseCardComponent, CommonModule],
  templateUrl: './courses-page.component.html',
  styleUrl: './courses-page.component.scss'
})
export class CoursesPageComponent {
  courses = [
    {
      id: 1,
      title: 'مدخل إلى أنجولر',
      instructor: 'أحمد محمد',
      shortDescription: 'تعلم أساسيات إطار العمل Angular خطوة بخطوة.',
      price: 49.99,
      image: 'رياضيات-+-اردوينو_01.png'
    },
    {
      id: 2,
      title: 'إتقان ASP.NET Core',
      instructor: 'سارة عبدالله',
      shortDescription: 'بناء واجهات برمجية وتطبيقات متكاملة باستخدام ASP.NET Core.',
      price: 69.99,
      image: 'سبايك-برايم02.png'
    },
    {
      id: 3,
      title: 'تطوير متكامل Full Stack',
      instructor: 'خالد علي',
      shortDescription: 'أصبح مطور متكامل من خلال مشاريع عملية حقيقية.',
      price: 99.99,
      image: 'دورة-بايثون03.png'
    },
    {
      id: 4,
      title: 'أساسيات البرمجة بلغة بايثون',
      instructor: 'منى سعيد',
      shortDescription: 'ابدأ رحلتك في البرمجة من الصفر مع لغة بايثون.',
      price: 39.99,
      image: 'PICTOBLOX.png'
    },
    {
      id: 5,
      title: 'الروبوتات باستخدام Arduino',
      instructor: 'ياسر حسن',
      shortDescription: 'تعلم كيفية برمجة وبناء روبوتات باستخدام Arduino.',
      price: 59.99,
      image: 'رياضيات-+-اردوينو_01.png'
    },
    {
      id: 6,
      title: 'تصميم المواقع باستخدام HTML & CSS',
      instructor: 'ريم عبدالكريم',
      shortDescription: 'أساسيات بناء مواقع الويب من الصفر باستخدام HTML و CSS.',
      price: 29.99,
      image: 'سبايك-برايم02.png'
    },
    {
      id: 7,
      title: 'مدخل إلى قواعد البيانات SQL',
      instructor: 'محمود إبراهيم',
      shortDescription: 'تعلم كيفية تصميم واستعلام قواعد البيانات باستخدام SQL.',
      price: 34.99,
      image: 'دورة-بايثون03.png'
    },
    {
      id: 8,
      title: 'هندسة البرمجيات للمبتدئين',
      instructor: 'ليلى عمر',
      shortDescription: 'فهم مبادئ هندسة البرمجيات وتصميم الأنظمة.',
      price: 79.99,
      image: 'رياضيات-+-اردوينو_01.png'
    }
  ];

}
