import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Router } from '@angular/router';
import { FormInputComponent } from "../../../shared/components/form-input/form-input.component";
import { AccountService } from '../../../core/services/account.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatIconModule,
    MatCheckboxModule,
    FormInputComponent
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  hidePassword = true; // For toggling password visibility
  private destroyRef = inject(DestroyRef);
  private fb = inject(FormBuilder)
  private accountService = inject(AccountService)
  private router = inject(Router)

  loginForm = this.fb.group({
    userName: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required, Validators.minLength(6)]),
  });


  onSubmit(): void {
    if (this.loginForm.valid) {
      this.accountService.login(this.loginForm.value)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: () => {
            this.accountService.getUserInfo()
              .pipe(takeUntilDestroyed(this.destroyRef))
              .subscribe();
            setTimeout(() => {
              window.location.reload();

            }, 500);
            this.router.navigateByUrl('/');
          }
        })
    } else {
      console.log('Login form is invalid.');
      this.loginForm.markAllAsTouched();
    }
  }

  togglePasswordVisibility(): void {
    this.hidePassword = !this.hidePassword;
  }
}
