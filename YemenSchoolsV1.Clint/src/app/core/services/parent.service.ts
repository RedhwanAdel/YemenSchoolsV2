import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AccountService } from './account.service';
import { ApiResponse } from '../../shared/models/ApiResponse';
import { ParentCheckDto, StudentWithSchoolInfoDto } from '../../shared/models/parent';
import { map } from 'rxjs';
import { SnackbarService } from './snackbar.service';

@Injectable({
  providedIn: 'root'
})
export class ParentService {

  baseUrl = environment.apiUrl;
  private http = inject(HttpClient)
  accountService = inject(AccountService)
  private snack = inject(SnackbarService);

  private schoolId = this.accountService.currentUser()?.schoolId
  private parentId = this.accountService.currentUser()?.entityId


  GetStudentsWithSchoolInfoForParent() {
    if (!this.parentId) {
      this.snack.error('لم يتم ايجاد معرف المستخدم الرجاء تسجيل الدخول')

    }
    return this.http.get<ApiResponse<StudentWithSchoolInfoDto[]>>(`${this.baseUrl}Parents/${this.parentId}/students-with-school-info`)
  }
  checkParentByNationalId(nationalId: string) {
    return this.http.get<ApiResponse<ParentCheckDto>>(`${this.baseUrl}Parents/check-national-id/${nationalId}`).pipe(
      map(res => res.data)
    );
  }
  createParent(parent: any) {

    return this.http.post<ApiResponse<{ message: string, parentId: string }>>(this.baseUrl + 'Parents', parent).pipe(
      map(res => res.data)
    );
  }
}
