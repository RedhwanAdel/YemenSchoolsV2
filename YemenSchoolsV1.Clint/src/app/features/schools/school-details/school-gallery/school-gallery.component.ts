import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MatGridList, MatGridListModule } from '@angular/material/grid-list';
import { SchoolService } from '../../../../core/services/school.service';
import { SchoolPhoto } from '../../../../shared/models/school/school';

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
  @Input() schoolId!: string;
  schoolPhotos: SchoolPhoto[] = [];

  constructor(private photoService: SchoolService) { }

  ngOnInit() {
    this.loadPhotos();
  }

  loadPhotos() {
    this.photoService.getSchoolPhotos(this.schoolId).subscribe({
      next: (photos) => this.schoolPhotos = photos,
      error: (err) => console.error(err)
    });
  }
}
