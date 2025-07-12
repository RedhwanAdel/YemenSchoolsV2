import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';

@Component({
  selector: 'app-school-news-events',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule,
    MatDividerModule,
    MatListModule
  ],
  templateUrl: './school-news-events.component.html',
  styleUrl: './school-news-events.component.scss'
})
export class SchoolNewsEventsComponent {
  newsArticles = [
    {
      title: 'Sunrise Academy Wins National Robotics Competition',
      date: 'June 25, 2025',
      summary: 'Our robotics team secured first place in the annual National Robotics Challenge, showcasing exceptional innovation and teamwork. Congratulations to all participants!',
      image: 'https://via.placeholder.com/400x250/3f51b5/ffffff?text=Robotics+Win'
    },
    {
      title: 'Annual Arts Festival Showcases Student Talent',
      date: 'May 18, 2025',
      summary: 'The campus came alive with creativity during our Annual Arts Festival, featuring captivating performances and stunning artworks by students from all grades.',
      image: 'https://via.placeholder.com/400x250/FFC107/000000?text=Arts+Festival'
    },
    {
      title: 'Community Service Day: Making a Difference',
      date: 'April 10, 2025',
      summary: 'Students and faculty dedicated a day to various community service projects, reinforcing our commitment to social responsibility and empathy.',
      image: 'https://via.placeholder.com/400x250/28A745/ffffff?text=Community+Service'
    }
  ];

  upcomingEvents = [
    {
      name: 'Parent-Teacher Conferences',
      date: 'July 15-16, 2025',
      time: '9:00 AM - 4:00 PM',
      location: 'School Auditorium',
      icon: 'event'
    },
    {
      name: 'Summer Enrichment Program Begins',
      date: 'August 1, 2025',
      time: '8:30 AM',
      location: 'Various Classrooms',
      icon: 'summer_temp'
    },
    {
      name: 'New Student Orientation',
      date: 'August 28, 2025',
      time: '10:00 AM',
      location: 'School Gymnasium',
      icon: 'group_add'
    }
  ];
}
