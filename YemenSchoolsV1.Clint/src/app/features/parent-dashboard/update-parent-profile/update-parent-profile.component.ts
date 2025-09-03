import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';

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
  profileForm: FormGroup;
  passwordForm: FormGroup;
  profileImageUrl: string = 'assets/default-avatar.png'; // صورة افتراضية

  constructor(private fb: FormBuilder) {
    // النموذج الشامل للبيانات الشخصية
    this.profileForm = this.fb.group({
      name: ['', Validators.required],
      imageUrl: [''], // هذا الحقل سيتم تحديثه في منطق تحميل الصورة
      phoneNumber: ['', Validators.required],
      address: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
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
    // TODO: في هذه النقطة، قم بتحميل بيانات ولي الأمر من API
    // على سبيل المثال:
    // this.apiService.getProfileData().subscribe(data => {
    //   this.profileForm.patchValue(data.user);
    //   this.profileForm.patchValue(data.parent);
    // });
  }

  // حفظ بيانات الملف الشخصي
  onSaveProfile() {
    if (this.profileForm.valid) {
      console.log('✅ بيانات محدثة:', this.profileForm.value);
      // TODO: استدعاء API لتحديث البيانات
      // يمكنك إرسال البيانات بشكل منفصل إلى endpoint لتحديث User و Parents
      const userData = {
        name: this.profileForm.value.name,
        imageUrl: this.profileForm.value.imageUrl,
      };
      const parentData = {
        phoneNumber: this.profileForm.value.phoneNumber,
        address: this.profileForm.value.address,
        email: this.profileForm.value.email,
        jobTitle: this.profileForm.value.jobTitle,
      };
      // هنا يمكنك استدعاء خدمات منفصلة
      // this.apiService.updateUser(userData);
      // this.apiService.updateParent(parentData);
    }
  }

  // تغيير كلمة المرور
  onChangePassword() {
    if (this.passwordForm.valid) {
      if (this.passwordForm.value.newPassword !== this.passwordForm.value.confirmPassword) {
        alert('❌ كلمة المرور الجديدة غير متطابقة');
        return;
      }
      console.log('🔑 تغيير كلمة المرور:', this.passwordForm.value);
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
