import { Component, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegionModel } from '../../../features/region/models/region.model';

@Component({
  selector: 'app-drop-down-list',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './drop-down-list.component.html',
  styleUrl: './drop-down-list.component.css'
})
export class DropDownListComponent {
  selctedId: string = '';
  data = input.required<any[]>();
  selectedstring = input.required<string>();
  filter = output<string>();
  onchangelist() {
    this.filter.emit(this.selctedId);
  }

}
