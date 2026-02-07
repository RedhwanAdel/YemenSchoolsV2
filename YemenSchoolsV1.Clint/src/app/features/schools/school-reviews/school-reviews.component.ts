import { Component, DestroyRef, inject, Input } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { SchoolReviewsService } from '@features/schools/services/school-reviews.service';
import { SchoolReview } from '@features/schools/models/school';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { AccountService } from '../../../core/services/account.service';

@Component({
  selector: 'app-school-reviews',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule
  ],
  templateUrl: './school-reviews.component.html',
  styleUrl: './school-reviews.component.scss'
})
export class SchoolReviewsComponent {
  @Input() schoolId!: string;
  private destroyRef = inject(DestroyRef);
  private accountService = inject(AccountService)
  reviews: SchoolReview[] = [];
  averageRating: number = 0;
  isLoggedIn: boolean = false; // يجب تحديث هذه القيمة بناءً على حالة المستخدم

  newRating: number = 0;
  newComment: string = '';

  constructor(private reviewService: SchoolReviewsService) { }

  ngOnInit(): void {

    this.isLoggedIn = this.accountService.isUserLoggedIn(); // مثال على طريقة التحقق
    this.loadReviews();
    if (this.isLoggedIn) {
      this.loadUserReview();
    }
  }

  loadReviews(): void {
    this.reviewService.getReviewsBySchool(this.schoolId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((data: SchoolReview[]) => this.reviews = data);
    this.reviewService.getAverageRating(this.schoolId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((res: any) => this.averageRating = res.averageRating);
  }

  submitReview(): void {
    // إيجاد تقييم المستخدم الحالي إن وجد
    const currentUserReview = this.reviews.find((r: SchoolReview) => r.userId === this.accountService.currentUser()?.id);

    const review = {
      schoolId: this.schoolId,
      rating: this.newRating,
      comment: this.newComment || currentUserReview?.comment // إذا التعليق الجديد فارغ استخدم التعليق القديم
    };

    this.reviewService.addOrUpdateReview(review)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(() => {
        // لا نمسح الفورم، فقط إعادة تحميل التقييمات
        this.loadReviews();
      });
  }

  loadUserReview(): void {
    this.reviewService.getReviewsBySchool(this.schoolId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((data: SchoolReview[]) => {
        const userReview = data.find((r: SchoolReview) => r.userId === this.accountService.currentUser()?.id);
        if (userReview) {
          this.newRating = userReview.rating;
          this.newComment = userReview.comment || '';
        }
      });
  }
}
