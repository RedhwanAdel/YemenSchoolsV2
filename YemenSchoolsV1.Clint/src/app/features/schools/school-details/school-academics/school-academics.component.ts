import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatChipsModule } from '@angular/material/chips'; // For subjects/programs
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
interface MethodologyItem {
  title: string;
  description: string;
}
@Component({
  selector: 'app-school-academics',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatButtonModule,
    MatChipsModule,
    MatExpansionModule,
  ],
  templateUrl: './school-academics.component.html',
  styleUrl: './school-academics.component.scss'
})
export class SchoolAcademicsComponent {
  methodologyItems: MethodologyItem[] = [];

  ngOnInit(): void {
    // بيانات المنهجية لملء الـ accordion
    this.methodologyItems = [
      {
        title: 'التعلم القائم على الاستقصاء',
        description: 'يتم تشجيع الطلاب على طرح الأسئلة، والبحث، واكتساب المعرفة بأنفسهم.',
      },
      {
        title: 'المشاريع التعاونية',
        description: 'تعزز المهام الجماعية مهارات التواصل، وحل المشكلات، والقيادة.',
      },
      {
        title: 'دمج التكنولوجيا',
        description: 'استخدام الأدوات والموارد الرقمية لتعزيز التعلم والتفاعل.',
      },
      {
        title: 'التعليم المتمايز',
        description: 'تكييف أساليب التدريس لتناسب أنماط واحتياجات التعلم المتنوعة.',
      },
    ];
  }
}
