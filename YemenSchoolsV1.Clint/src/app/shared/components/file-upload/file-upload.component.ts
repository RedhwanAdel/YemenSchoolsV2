import { CommonModule } from '@angular/common';
import { Component, EventEmitter, forwardRef, Input, Output } from '@angular/core';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-file-upload',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './file-upload.component.html',
  styleUrl: './file-upload.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FileUploadComponent),
      multi: true
    }
  ]
})
export class FileUploadComponent implements ControlValueAccessor {
  @Input() accept = '*/*';
  @Input() label = 'Choose File';
  @Input() showProgress = false;
  @Input() progress = 0;

  @Output() fileChange = new EventEmitter<File | null>();

  selectedFile: File | null = null;
  fileName = '';
  disabled = false;

  private onChange: (value: any) => void = () => { };
  private onTouched: () => void = () => { };

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];
      this.fileName = this.selectedFile.name;
      this.onChange(this.selectedFile);
      this.fileChange.emit(this.selectedFile);
    }
  }

  // ControlValueAccessor implementation
  writeValue(value: any): void {
    if (value instanceof File) {
      this.selectedFile = value;
      this.fileName = value.name;
    } else {
      this.selectedFile = null;
      this.fileName = '';
    }
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  clearFile(): void {
    this.selectedFile = null;
    this.fileName = '';
    this.onChange(null);
    this.fileChange.emit(null);
  }
}
