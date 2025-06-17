import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Router } from '@angular/router';
import { FormInputComponent } from "../../../shared/components/form-input/form-input.component";

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
  loginForm: FormGroup;
  hidePassword = true; // For toggling password visibility

  constructor(private router: Router) { // Inject Router if needed
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
      rememberMe: new FormControl(false) // For "Remember me" checkbox
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      console.log('Login Form Submitted!', this.loginForm.value);
      // Here you would typically call an authentication service
      // e.g., this.authService.login(this.loginForm.value.email, this.loginForm.value.password).subscribe(...)

      // Example navigation after successful login (replace with your actual logic)
      // this.router.navigate(['/dashboard']);
    } else {
      console.log('Login form is invalid.');
      // Optionally, mark all fields as touched to display validation errors
      this.loginForm.markAllAsTouched();
    }
  }

  togglePasswordVisibility(): void {
    this.hidePassword = !this.hidePassword;
  }
}
