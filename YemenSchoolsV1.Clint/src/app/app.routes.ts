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
import { SchoolsComponent } from './features/schools/schools.component';
import { SchoolDashboardComponent } from './layout/school-dashboard/school-dashboard.component';
import { SchoolDashOverviewComponent } from './features/school-dashboard/school-dash-overview/school-dash-overview.component';
import { StageListComponent } from './features/school-dashboard/stage/stage-list/stage-list.component';
import { TermListComponent } from './features/school-dashboard/term/term-list/term-list.component';
import { GradeListComponent } from './features/school-dashboard/grade/grade-list/grade-list.component';
import { SectionListComponent } from './features/school-dashboard/section/section-list/section-list.component';
import { SchoolGradeComponent } from './features/school-dashboard/school-grade/school-grade.component';
import { SchoolSubjectComponent } from './features/school-dashboard/school-subject/school-subject.component';
import { YearListComponent } from './features/school-dashboard/year/year-list/year-list.component';
import { TeacherListComponent } from './features/school-dashboard/teacher/teacher-list/teacher-list.component';
import { TeacherFormComponent } from './features/school-dashboard/teacher/teacher-form/teacher-form.component';
import { TeacherAddComponent } from './features/school-dashboard/teacher/teacher-add/teacher-add.component';
import { TeacherEditComponent } from './features/school-dashboard/teacher/teacher-edit/teacher-edit.component';
import { TeacherDetailComponent } from './features/school-dashboard/teacher/teacher-detail/teacher-detail.component';

export const routes: Routes = [
    {
        path: '',
        component: MainLayoutComponent,
        children: [
            { path: '', component: HomeComponent },
            { path: 'school/:id', component: SchoolDetailsComponent },
            { path: 'schools', component: SchoolsComponent },
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
            { path: 'schools-detail/:id', component: SchoolDetailDashboardComponent },
            { path: 'schools-add', component: SchoolAddComponent },
            { path: 'schools-edit/:id', component: SchoolEditComponent },
        ],
    },
    {
        path: 'school-dash-board',
        component: SchoolDashboardComponent,
        children: [
            { path: '', redirectTo: 'overview', pathMatch: 'full' },
            { path: 'overview', component: SchoolDashOverviewComponent },
            { path: 'school-grade', component: SchoolGradeComponent },
            { path: 'school-subject', component: SchoolSubjectComponent },
            { path: 'school-years', component: YearListComponent },
            { path: 'school-term', component: TermListComponent },
            { path: 'school-teacher-list', component: TeacherListComponent },
            { path: 'school-teacher-add', component: TeacherAddComponent },
            { path: 'school-teacher-edit/:id', component: TeacherEditComponent },
            { path: 'school-teacher-detail/:id', component: TeacherDetailComponent },
            { path: 'term', component: TermListComponent },
            { path: 'grade', component: GradeListComponent },
            { path: 'section', component: SectionListComponent },

        ],
    },
    { path: 'not-found', component: NotFoundComponent },
    { path: 'server-error', component: ServerErrorComponent },


    { path: '**', redirectTo: 'not-found', pathMatch: 'full' },
];
