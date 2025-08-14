import { CommonModule } from '@angular/common';
import { Component, ViewEncapsulation } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatCalendar, MatDatepickerModule } from '@angular/material/datepicker';

@Component({
  selector: 'app-calendar',
  standalone: true,
  imports: [
    MatCalendar, MatDatepickerModule,
    CommonModule, MatCardModule, MatButtonModule, MatIconModule
  ],
  templateUrl: './calendar.component.html',
  styleUrl: './calendar.component.scss',
  encapsulation: ViewEncapsulation.None // لتمكين تخصيص الأنماط العالمية للمكونات الداخلية

})
export class CalendarComponent {
  selectedDate: Date | null = null;

}
