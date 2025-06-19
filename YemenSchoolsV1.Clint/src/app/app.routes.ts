import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { SchoolDetailsComponent } from './features/schools/school-details/school-details.component';
import { DashboardLayoutComponent } from './layout/dashboard/dashboard-layout/dashboard-layout.component';
import { CityListComponent } from './dashboard/pages/cities/city-list/city-list.component';
import { SchoolListComponent } from './dashboard/pages/schools/school-list/school-list.component';
import { DashboardHomeComponent } from './dashboard/pages/dashboard-home/dashboard-home.component';
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import { RegionListComponent } from './dashboard/pages/regions/region-list/region-list.component';
import { LoginComponent } from './features/account/login/login.component';
import { SchoolDetailDashboardComponent } from './dashboard/pages/schools/school-detail-dashboard/school-detail-dashboard.component';
import { SchoolAddComponent } from './dashboard/pages/schools/school-add/school-add.component';
import { SchoolEditComponent } from './dashboard/pages/schools/school-edit/school-edit.component';

export const routes: Routes = [
    {
        path: '',
        component: MainLayoutComponent,
        children: [
            { path: '', component: HomeComponent },
            { path: 'school-details', component: SchoolDetailsComponent },
            { path: 'login', component: LoginComponent },
        ]
    },
    {
        path: 'dash-board',
        component: DashboardLayoutComponent,
        children: [
            { path: '', redirectTo: 'overview', pathMatch: 'full' }, // أول صفحة في الداشبورد
            { path: 'overview', component: DashboardHomeComponent }, // إحصائيات مثلاً
            { path: 'cities', component: CityListComponent },
            { path: 'regions', component: RegionListComponent },
            { path: 'schools', component: SchoolListComponent },
            { path: 'schools-detail', component: SchoolDetailDashboardComponent },
            { path: 'schools-add', component: SchoolAddComponent },
            { path: 'schools-edit', component: SchoolEditComponent },
            // أضف المزيد حسب الحاجة
        ],
    },

    { path: '**', redirectTo: 'not-found', pathMatch: 'full' },
];
