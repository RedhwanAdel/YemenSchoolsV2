import { inject, Injectable } from '@angular/core';
import { AccountService } from './account.service';
import { catchError, forkJoin, map, of, switchMap, tap, throwError } from 'rxjs';
import { AcadmicYearService } from './acadmic-year.service';

@Injectable({
  providedIn: 'root'
})
export class InitService {
  private accountService = inject(AccountService);
  private academicYearService = inject(AcadmicYearService);

  init() {
    return forkJoin({
      user: this.accountService.getUserInfo().pipe(
        tap(user => {
          if (user && user.data.schoolId) { // افترض أن UserInfoDto يحتوي على schoolId
            this.academicYearService.setSchoolId(user.data.schoolId);
          }
        }),
        catchError(error => {
          console.warn('⚠️ Failed to fetch user info. Using fallback (null).');
          return of(null);
        })
      )
    }).pipe(
      switchMap(results => {
        if (!this.academicYearService.getSchoolId()) {
          console.warn('School ID not available. Proceeding without academic year.');
          return of({ ...results, currentAcademicYear: null });
        }
        return this.academicYearService.GetCurrentYearId().pipe(
          map(academicYearResult => ({ ...results, currentAcademicYear: academicYearResult })),
          // إذا ألقى GetCurrentYearId() خطأ أو أرجع Observable فارغ
          catchError(error => {
            console.error('Error fetching current academic year, proceeding with null:', error);
            return of({ ...results, currentAcademicYear: null }); // ارجع قيمة افتراضية
          })
        );
      })
    );
  }
}