import { trigger, transition, style, animate } from '@angular/animations';
import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
@Component({
  selector: 'app-stat-card-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule
  ],

  templateUrl: './stat-card-dashboard.component.html',

  styleUrl: './stat-card-dashboard.component.scss',
  animations: [
    // This animation is for the counter-up effect
    trigger('counter', [
      transition(':increment', [
        style({ opacity: 0, transform: 'translateY(10px)' }),
        animate('0.5s ease-out', style({ opacity: 1, transform: 'translateY(0)' }))
      ]),
    ])
  ]
})
export class StatCardDashboardComponent implements OnInit, OnChanges {
  @Input() title: string = 'Title';
  @Input() value: number = 0;
  @Input() icon: string = 'info'; // Material icon name (e.g., 'school', 'group', 'attach_money')
  @Input() isDarkMode: boolean = false; // Input for theming (true for dark mode, false for light)

  displayValue: number = 0; // Value used for the counter-up animation
  private animationFrameId: number = 0;
  private startTime: number = 0;
  private duration: number = 1000; // 1 second for the count-up animation

  constructor() { }

  ngOnInit(): void {
    this.startCountAnimation();
  }

  ngOnChanges(changes: SimpleChanges): void {
    // Restart animation if the 'value' input changes
    if (changes['value'] && !changes['value'].firstChange) {
      this.startCountAnimation();
    }
  }

  /**
   * Starts the counter-up animation for the display value.
   */
  private startCountAnimation(): void {
    cancelAnimationFrame(this.animationFrameId); // Stop any previous animation
    this.startTime = 0; // Reset start time
    this.displayValue = 0; // Start counting from zero

    const initialValue = 0; // Always start from 0 for the animation
    const targetValue = this.value;
    const animateCount = (timestamp: number) => {
      if (!this.startTime) {
        this.startTime = timestamp;
      }
      const progress = (timestamp - this.startTime) / this.duration;
      const easedProgress = this.easeOutQuad(progress); // Apply easing function

      this.displayValue = Math.min(targetValue, initialValue + easedProgress * (targetValue - initialValue));

      if (progress < 1) {
        this.animationFrameId = requestAnimationFrame(animateCount);
      } else {
        this.displayValue = targetValue; // Ensure it ends precisely on the target value
      }
    };

    this.animationFrameId = requestAnimationFrame(animateCount);
  }

  /**
   * Easing function for a smoother animation.
   * Quadratic easing out.
   */
  private easeOutQuad(t: number): number {
    return t * (2 - t);
  }
}
