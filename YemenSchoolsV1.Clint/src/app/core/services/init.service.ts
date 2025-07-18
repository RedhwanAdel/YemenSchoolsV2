import { inject, Injectable } from '@angular/core';
import { AccountService } from './account.service';
import { catchError, forkJoin, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InitService {
  private accountService = inject(AccountService);

  init() {
    return forkJoin({
      user: this.accountService.getUserInfo().pipe(
        catchError(error => {
          console.warn('⚠️ Failed to fetch user info. Using fallback (null).');
          return of(null); // أو بيانات وهمية حسب رغبتك
        })
      )
    })
  }
}
