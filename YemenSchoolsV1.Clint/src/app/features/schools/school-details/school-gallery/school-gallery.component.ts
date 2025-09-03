import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatGridList, MatGridListModule } from '@angular/material/grid-list';

@Component({
  selector: 'app-school-gallery',
  standalone: true,
  imports: [
    MatGridListModule,
    CommonModule
  ],
  templateUrl: './school-gallery.component.html',
  styleUrl: './school-gallery.component.scss'
})
export class SchoolGalleryComponent {
  galleryImages: string[] = [
    'portfolio-1.webp',
    'portfolio-2.webp',
    'portfolio-10.webp',
    'portfolio-11.webp',
    'portfolio-4.webp',
    'portfolio-7.webp',

  ];
}
