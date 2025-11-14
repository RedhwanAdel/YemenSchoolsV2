import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';

// Angular Material
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

// Ng2-Charts
import { BaseChartDirective } from 'ng2-charts';
import { environment } from '../../../../environments/environment';
import { ChartData, ChartOptions } from 'chart.js';

@Component({
  selector: 'app-dashboard-home',
  standalone: true,
  imports: [
    CommonModule,
    HttpClientModule,
    MatCardModule,
    MatTableModule,
    MatIconModule,
    MatButtonModule,
    BaseChartDirective
  ],
  templateUrl: './dashboard-home.component.html',
  styleUrls: ['./dashboard-home.component.scss']
})
export class DashboardHomeComponent implements OnInit {

  dashboardData: any = {
    summary: {},
    topSchoolsByStudents: [],
    topSchoolsByTeachers: [],
    studentGrowthLast6Months: [],
    recentActivities: []
  };

  summaryCards: any[] = [];
  displayedColumns: string[] = ['type', 'name', 'action', 'date'];

  studentChartData: ChartData<'line', number[], string> = {
    labels: [],
    datasets: []
  };
  chartOptions: ChartOptions = {
    responsive: true,
    scales: {
      y: {
        beginAtZero: true,
        ticks: {
          // show integer ticks
          precision: 0
        }
      }
    }
  };
  // Helpful debugging flag — set to true to print raw dashboard JSON below the chart
  debug = true;

  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.loadDashboard();
  }

  loadDashboard() {
    this.http.get(this.baseUrl + 'dashboard').subscribe((data: any) => {
      this.dashboardData = data;

      // إعداد Summary Cards
      this.summaryCards = [
        { label: 'Cities', value: data.summary.totalCities },
        { label: 'Regions', value: data.summary.totalRegions },
        { label: 'Schools', value: data.summary.totalSchools },
        { label: 'Active Schools', value: data.summary.activeSchools },
        { label: 'Teachers', value: data.summary.totalTeachers },
        { label: 'Students', value: data.summary.totalStudents },
        { label: 'Users', value: data.summary.totalUsers }
      ];

      // إعداد ChartData بالشكل الصحيح
      this.studentChartData = {
        labels: data.studentGrowthLast6Months.map((x: any) => x.month),
        datasets: [
          {
            data: data.studentGrowthLast6Months.map((x: any) => x.students),
            label: 'Students'
          }
        ]
      };
      // If numbers are small (like 3) Chart.js autoscale can still make the chart look flat
      // Compute a tight suggestedMax so small values are visible
      const values = this.studentChartData.datasets?.[0]?.data as number[] || [];
      if (values.length > 0) {
        const max = Math.max(...values);
        const padding = Math.max(1, Math.ceil(max * 0.25));
        // If max is very small (<=5) use a small fixed range
        const suggestedMax = max <= 5 ? Math.max(5, max + 2) : max + padding;
        this.chartOptions = {
          ...this.chartOptions,
          scales: {
            y: {
              beginAtZero: true,
              suggestedMax,
              ticks: { precision: 0 }
            }
          }
        };
      }
      // log the chart data for debugging
      console.log('studentChartData', this.studentChartData);

      // Force chart update if the directive is available
      // Use a short timeout to ensure change detection has applied bindings
      setTimeout(() => this.chart?.update(), 0);
    });
  }
  sortData(sort: any) {
    // Angular Material تتعامل مع الترتيب تلقائياً إذا تم تعيين this.dataSource.sort = this.sort;
    // ولكن يمكنك استخدام هذه الوظيفة لتنفيذ منطق ترتيب مخصص إذا لزم الأمر.
  }
}
