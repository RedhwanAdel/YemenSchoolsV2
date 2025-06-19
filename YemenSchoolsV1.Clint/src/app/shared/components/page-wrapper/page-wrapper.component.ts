import { CommonModule } from '@angular/common';
import { Component, EventEmitter, input, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-page-wrapper',
  standalone: true,
  imports: [CommonModule,
    FormsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule],
  templateUrl: './page-wrapper.component.html',
  styleUrl: './page-wrapper.component.scss'
})
export class PageWrapperComponent {
  @Input() headerTitle: string = 'Form';
  @Input() headerSubtitle: string = '';
  @Input() headerIcon: string = 'info';
  @Input() saveButtonText: string = 'Save';
  @Input() saveButtonColor: string = 'primary';
  @Input() cancelButtonText: string = 'Cancel';
  @Input() cancelButtonColor: string = 'accent';
  @Input() showActions: boolean = true; // Control visibility of action buttons

  @Output() saveClicked = new EventEmitter<void>();
  @Output() cancelClicked = new EventEmitter<void>();

  onSaveClick(): void {
    this.saveClicked.emit();
  }

  onCancelClick(): void {
    this.cancelClicked.emit();
  }
}
