import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { register as registerSwiperElements } from 'swiper/element/bundle';
// في ملف main.ts أو app.module.ts
import { Chart, ArcElement, BarElement, BarController, PieController, CategoryScale, LinearScale, Tooltip, Legend } from 'chart.js';

// تسجيل العناصر المطلوبة
Chart.register(
  ArcElement, // ضروري للـ pie/doughnut
  PieController,
  BarController, // ضروري للـ bar
  BarElement,
  CategoryScale, // ضروري للمحاور
  LinearScale,
  Tooltip,
  Legend
);

registerSwiperElements();
bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));
