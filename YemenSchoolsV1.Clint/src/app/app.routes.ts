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
import { RegisterComponent } from './features/account/register/register.component';
import { TestErrorComponent } from './features/test-error/test-error.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { ServerErrorComponent } from './shared/components/server-error/server-error.component';

export const routes: Routes = [
    {
        path: '',
        component: MainLayoutComponent,
        children: [
            { path: '', component: HomeComponent },
            { path: 'school-details', component: SchoolDetailsComponent },
            { path: 'login', component: LoginComponent },
            { path: 'register', component: RegisterComponent },
            { path: 'test-error', component: TestErrorComponent },
        ]
    },
    {
        path: 'dash-board',
        component: DashboardLayoutComponent,
        children: [
            { path: '', redirectTo: 'overview', pathMatch: 'full' },
            { path: 'overview', component: DashboardHomeComponent },
            { path: 'cities', component: CityListComponent },
            { path: 'regions', component: RegionListComponent },
            { path: 'schools', component: SchoolListComponent },
            { path: 'schools-detail', component: SchoolDetailDashboardComponent },
            { path: 'schools-add', component: SchoolAddComponent },
            { path: 'schools-edit', component: SchoolEditComponent },
        ],
    },
    { path: 'not-found', component: NotFoundComponent },
    { path: 'server-error', component: ServerErrorComponent },


    { path: '**', redirectTo: 'not-found', pathMatch: 'full' },
];
