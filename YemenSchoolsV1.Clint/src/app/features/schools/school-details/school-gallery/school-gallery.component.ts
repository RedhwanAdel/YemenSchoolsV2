import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, Input } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatGridList, MatGridListModule } from '@angular/material/grid-list';
import { SchoolService } from '@features/schools/services/school.service';
import { SchoolPhoto } from '@features/schools/models/school';

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
  private destroyRef = inject(DestroyRef);
  schoolPhotos: SchoolPhoto[] = [];

  constructor(private photoService: SchoolService) { }

  ngOnInit() {
    this.loadPhotos();
  }

  loadPhotos() {
    this.photoService.getSchoolPhotos(this.schoolId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (photos) => this.schoolPhotos = photos,
        error: (err) => console.error(err)
      });
  }
}
