import { Component, inject, Input, OnInit } from '@angular/core';
import { SchoolService } from '../../../core/services/school.service';
import { HttpEventType } from '@angular/common/http';
import { SchoolPhoto } from '../../../shared/models/school/school';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from '../../../core/services/account.service';
interface UploadFile {
  file: File;
  progress: number;
}

@Component({
  selector: 'app-school-photo-upload',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatProgressBarModule,
    MatIconModule
  ],
  templateUrl: './school-photo-upload.component.html',
  styleUrl: './school-photo-upload.component.scss'
})
export class SchoolPhotoUploadComponent implements OnInit {
  @Input() schoolId!: string;
  private route = inject(ActivatedRoute);
  private accountService = inject(AccountService);

  selectedFiles: UploadFile[] = [];
  uploadedPhotos: SchoolPhoto[] = [];

  constructor(private photoService: SchoolService) { }
  ngOnInit(): void {
    this.schoolId = this.accountService.currentUser()?.schoolId!
  }

  onFileSelected(event: any) {
    const files = Array.from(event.target.files) as File[];
    files.forEach(file => {
      this.selectedFiles.push({ file, progress: 0 });
    });
  }
  uploadPhotos() {
    this.selectedFiles.forEach(uploadFile => {
      this.photoService.uploadSchoolPhoto(uploadFile.file, this.schoolId)
        .subscribe({
          next: event => {
            if (event.type === HttpEventType.UploadProgress && event.total) {
              uploadFile.progress = Math.round((event.loaded / event.total) * 100);
            } else if (event.type === HttpEventType.Response) {
              this.uploadedPhotos.push(event.body as SchoolPhoto);
              // إزالة الملف من قائمة الانتظار بعد الانتهاء
              this.selectedFiles = this.selectedFiles.filter(f => f !== uploadFile);
            }
          },
          error: (err) => console.error(err)
        });
    });
  }


  deletePhoto(photo: SchoolPhoto) {
    // هنا يمكن إضافة دالة API لحذف الصورة من الباك
    this.uploadedPhotos = this.uploadedPhotos.filter(p => p.id !== photo.id);
  }
}
