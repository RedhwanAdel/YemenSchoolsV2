import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
interface Job {
  title: string;
  status: 'متاحة' | 'منتهية';
  description: string;
  details: { icon: string; text: string }[];
  specialization: string;
  companyName: string;
  postTime: string;
}
@Component({
  selector: 'app-jops-list',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatDividerModule,
    MatChipsModule
  ],
  templateUrl: './jops-list.component.html',
  styleUrl: './jops-list.component.scss'
})
export class JopsListComponent {
  jobs: Job[] = [
    {
      title: 'معلم انجليزي',
      status: 'متاحة',
      description:
        'إعلان توظيف تعلن مدرسة بوابة المعرفة العالمية عن حاجتها إلى: معلمين ومعلمات لغ... (نص الإعلان)',
      details: [
        { icon: 'star_border', text: 'غير محدد' },
        { icon: 'school', text: 'التعليم أو التدريس' },
        { icon: 'language', text: 'لغة إنجليزية' },
        { icon: 'location_on', text: 'عدن:المنصورة' },
      ],
      specialization: 'التعليم أو التدريس',
      companyName: 'مدرسة بوابة المعرفة العالمية',
      postTime: 'منذ 2 شهر',
    },
    {
      title: 'معلم رياضيات',
      status: 'متاحة',
      description:
        'إعلان توظيف تعلن مدرسة بوابة المعرفة العالمية عن حاجتها إلى: معلمين ومعلمات لغ... (نص الإعلان)',
      details: [
        { icon: 'star_border', text: 'غير محدد' },
        { icon: 'school', text: 'التعليم أو التدريس' },
        { icon: 'language', text: 'رياضيات' },
        { icon: 'location_on', text: 'عدن: الشيخ عثمان' },
      ],
      specialization: 'التعليم أو التدريس',
      companyName: 'مدرسة بوابة المعرفة العالمية',
      postTime: 'منذ 2 شهر',
    },
    {
      title: 'سائق باص',
      status: 'متاحة',
      description:
        'إعلان توظيف تعلن مدرسة بوابة المعرفة العالمية عن حاجتها إلى: سائق باص (نص الإعلان)',
      details: [
        { icon: 'star_border', text: 'غير محدد' },
        { icon: 'school', text: '  توصيل طلاب' },
        { icon: 'language', text: 'سائق باص' },
        { icon: 'location_on', text: 'عدن:  خورمكسر' },
      ],
      specialization: 'التعليم أو التدريس',
      companyName: 'مدرسة بوابة المعرفة العالمية',
      postTime: 'منذ 2 شهر',
    },
    {
      title: 'معلم انجليزي',
      status: 'متاحة',
      description:
        'إعلان توظيف تعلن مدرسة بوابة المعرفة العالمية عن حاجتها إلى: معلمين ومعلمات لغ... (نص الإعلان)',
      details: [
        { icon: 'star_border', text: 'غير محدد' },
        { icon: 'school', text: 'التعليم أو التدريس' },
        { icon: 'language', text: 'لغة إنجليزية' },
        { icon: 'location_on', text: 'عدن:المنصورة' },
      ],
      specialization: 'التعليم أو التدريس',
      companyName: 'مدرسة بوابة المعرفة العالمية',
      postTime: 'منذ 2 شهر',
    },
    {
      title: 'معلم انجليزي',
      status: 'متاحة',
      description:
        'إعلان توظيف تعلن مدرسة بوابة المعرفة العالمية عن حاجتها إلى: معلمين ومعلمات لغ... (نص الإعلان)',
      details: [
        { icon: 'star_border', text: 'غير محدد' },
        { icon: 'school', text: 'التعليم أو التدريس' },
        { icon: 'language', text: 'لغة إنجليزية' },
        { icon: 'location_on', text: 'عدن:المنصورة' },
      ],
      specialization: 'التعليم أو التدريس',
      companyName: 'مدرسة بوابة المعرفة العالمية',
      postTime: 'منذ 2 شهر',
    },
    {
      title: 'معلم انجليزي',
      status: 'متاحة',
      description:
        'إعلان توظيف تعلن مدرسة بوابة المعرفة العالمية عن حاجتها إلى: معلمين ومعلمات لغ... (نص الإعلان)',
      details: [
        { icon: 'star_border', text: 'غير محدد' },
        { icon: 'school', text: 'التعليم أو التدريس' },
        { icon: 'language', text: 'لغة إنجليزية' },
        { icon: 'location_on', text: 'عدن:المنصورة' },
      ],
      specialization: 'التعليم أو التدريس',
      companyName: 'مدرسة بوابة المعرفة العالمية',
      postTime: 'منذ 2 شهر',
    }
  ];
}
