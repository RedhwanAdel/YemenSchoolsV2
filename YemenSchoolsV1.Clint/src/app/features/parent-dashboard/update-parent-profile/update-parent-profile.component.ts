import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';
import { AccountService } from '../../../core/services/account.service';
import { ChangePasswordDto } from '../../../shared/models/user';
import { SnackbarService } from '../../../core/services/snackbar.service';

@Component({
  selector: 'app-update-parent-profile',
  standalone: true,
  imports: [
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatTabsModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule
  ],
  templateUrl: './update-parent-profile.component.html',
  styleUrl: './update-parent-profile.component.scss'
})
export class UpdateParentProfileComponent {
  accountService = inject(AccountService)
  snackService = inject(SnackbarService)
  profileForm: FormGroup;
  passwordForm: FormGroup;
  profileImageUrl: string = 'assets/default-avatar.png'; // صورة افتراضية

  constructor(private fb: FormBuilder) {
    // النموذج الشامل للبيانات الشخصية
    this.profileForm = this.fb.group({
      name: ['',],
      imageUrl: [''], // هذا الحقل سيتم تحديثه في منطق تحميل الصورة
      phoneNumber: ['',],
      address: ['',],
      email: ['', [, Validators.email]],
      jobTitle: [''],
    });

    // النموذج الخاص بكلمة المرور
    this.passwordForm = this.fb.group({
      oldPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.accountService.getProfile().subscribe({
      next: (data) => {
        this.profileForm.patchValue({
          name: data.name,
          phoneNumber: data.phoneNumber,
          email: data.email,
          address: data.address,
          jobTitle: data.jobTitle,
          imageUrl: data.imageUrl
        });

        // إذا فيه صورة محفوظة في البروفايل
        if (data.imageUrl) {
          this.profileImageUrl = data.imageUrl;
        }
      },
      error: () => {
        this.snackService.error('❌ فشل تحميل بيانات الملف الشخصي');
      }
    });
  }

  // حفظ بيانات الملف الشخصي
  onSaveProfile() {
    if (this.profileForm.valid) {
      const profileData = {
        name: this.profileForm.value.name,
        imageUrl: this.profileForm.value.imageUrl,
        phoneNumber: this.profileForm.value.phoneNumber,
        address: this.profileForm.value.address,
        email: this.profileForm.value.email,
        jobTitle: this.profileForm.value.jobTitle,
      };

      this.accountService.updateProfile(profileData).subscribe({
        next: res => {
          this.snackService.success(res.message);
        },
        error: err => {
          this.snackService.error('❌ فشل تحديث البيانات');
        }
      });
    }
  }

  // تغيير كلمة المرور
  onChangePassword() {
    if (this.passwordForm.valid) {
      if (this.passwordForm.value.newPassword !== this.passwordForm.value.confirmPassword) {
        this.snackService.error('❌ كلمة المرور الجديدة غير متطابقة')

        return;
      }
      console.log('🔑 تغيير كلمة المرور:', this.passwordForm.value);
      const passwordModel: ChangePasswordDto = {
        newPassword: this.passwordForm.value.newPassword,
        currentPassword: this.passwordForm.value.oldPassword
      }
      this.accountService.changePassword(passwordModel).subscribe({
        next: res => {
          this.snackService.success(res.message)
        }
      })
      // TODO: استدعاء API لتغيير كلمة المرور
    }
  }

  // اختيار ملف الصورة
  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.profileImageUrl = reader.result as string;
        // قم بتعيين قيمة الصورة في النموذج
        this.profileForm.get('imageUrl')?.setValue(reader.result);
      };
      reader.readAsDataURL(file);
    }
  }
}
