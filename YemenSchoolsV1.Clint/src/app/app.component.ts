import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BusyService } from './core/services/busy.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,
    MatProgressSpinnerModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'YemenSchoolsV1.Clint';
  busyService = inject(BusyService);

}
