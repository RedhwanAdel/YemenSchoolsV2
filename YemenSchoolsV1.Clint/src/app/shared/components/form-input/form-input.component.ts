import { CommonModule, NgIf } from '@angular/common';
import { Component, Input, Optional, Self } from '@angular/core';
import { ReactiveFormsModule, FormControl, ControlValueAccessor, NgControl } from '@angular/forms';
import { MatError, MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-form-input',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule, // Essential for FormControl
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatError
  ],
  templateUrl: './form-input.component.html',
  styleUrl: './form-input.component.scss'
})
export class FormInputComponent implements ControlValueAccessor {
  @Input() label: string = 'Label';
  @Input() type: string = 'text';
  @Input() placeholder: string = '';
  @Input() icon: string = '';

  constructor(@Self() @Optional() public ngControl: NgControl) {
    if (this.ngControl) {
      this.ngControl.valueAccessor = this;
    }
  }

  onChange: any = () => { };
  onTouched: any = () => { };

  writeValue(value: string): void {
  }

  registerOnChange(fn: any): void {
  }

  registerOnTouched(fn: any): void {
  }
  get control() {
    return this.ngControl.control as FormControl
  }
}
