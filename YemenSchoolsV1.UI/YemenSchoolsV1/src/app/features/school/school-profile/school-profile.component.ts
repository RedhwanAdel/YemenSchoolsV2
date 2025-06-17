import { Component } from '@angular/core';

interface School {
  id: number;
  name: string;
  address: string;
  phone: string;
  email: string;
  description: string;
}
@Component({
  selector: 'app-school-profile',
  standalone: true,
  imports: [],
  templateUrl: './school-profile.component.html',
  styleUrl: './school-profile.component.css'
})
export class SchoolProfileComponent {
  schools: School[] = [
    {
      id: 1,
      name: 'Greenwood High',
      address: '123 Main St, Springfield',
      phone: '+1 234 567 890',
      email: 'info@greenwoodhigh.com',
      description: 'A leading school in academics and sports.',
    },
    {
      id: 2,
      name: 'Riverside Academy',
      address: '456 Elm St, Rivertown',
      phone: '+1 345 678 901',
      email: 'contact@riversideacademy.edu',
      description: 'Focused on holistic education and innovation.',
    },
    {
      id: 3,
      name: 'Sunrise Public School',
      address: '789 Oak St, Sunville',
      phone: '+1 456 789 012',
      email: 'admin@sunriseps.org',
      description: 'Committed to excellence in education.',
    },
  ];

  selectedSchool: School | null = null;

  // Function to select a school
  selectSchool(school: School) {
    this.selectedSchool = school;
  }
}
