import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-school-contact-us',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatDividerModule,
    FormsModule // Import FormsModule for template-driven forms
  ],
  templateUrl: './school-contact-us.component.html',
  styleUrl: './school-contact-us.component.scss'
})
export class SchoolContactUsComponent {
  contactForm = {
    name: '',
    email: '',
    subject: '',
    message: ''
  };

  onSubmit(): void {
    // In a real application, you would send this data to a backend service
    console.log('Contact Form Submitted:', this.contactForm);
    alert('Thank you for your message! We will get back to you soon.');
    // Reset form
    this.contactForm = { name: '', email: '', subject: '', message: '' };
  }
}
