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
import { SectionListComponent } from './features/school-dashboard/section/section-list/section-list.component';
import { SchoolGradeComponent } from './features/school-dashboard/school-grade/school-grade.component';
import { SchoolSubjectComponent } from './features/school-dashboard/school-subject/school-subject.component';
import { YearListComponent } from './features/school-dashboard/year/year-list/year-list.component';
import { TeacherListComponent } from './features/school-dashboard/teacher/teacher-list/teacher-list.component';
import { TeacherFormComponent } from './features/school-dashboard/teacher/teacher-form/teacher-form.component';
import { TeacherAddComponent } from './features/school-dashboard/teacher/teacher-add/teacher-add.component';
import { TeacherEditComponent } from './features/school-dashboard/teacher/teacher-edit/teacher-edit.component';
import { TeacherDetailComponent } from './features/school-dashboard/teacher/teacher-detail/teacher-detail.component';
import { GradeListComponent } from './features/school-dashboard/section/grade-list/grade-list.component';
import { SectionSubjectListComponent } from './features/school-dashboard/section-subject/section-subject-list/section-subject-list.component';
import { SectionSubjectAssignmentComponent } from './features/school-dashboard/section-subject/section-subject-assignment/section-subject-assignment.component';
import { SchoolReportComponent } from './features/school-dashboard/school-report/school-report.component';
import { ParentDashboardComponent } from './layout/dashboard/parent-dashboard/parent-dashboard.component';
import { StudnetListComponent } from './features/school-dashboard/student/studnet-list/studnet-list.component';
import { StudentAddComponent } from './features/school-dashboard/student/student-add/student-add.component';
import { DailyAttendanceComponent } from './features/school-dashboard/attendance/daily-attendance/daily-attendance.component';
import { SectionListByTeacherIdComponent } from './features/school-dashboard/attendance/section-list-by-teacher-id/section-list-by-teacher-id.component';
import { MarkEntryComponent } from './features/school-dashboard/mark/mark-entry/mark-entry.component';
import { ChildProfileComponent } from './features/parent-dashboard/child-profile/child-profile.component';
import { ParentOverViewComponent } from './features/parent-dashboard/parent-over-view/parent-over-view.component';
import { ParentDashboardPageComponent } from './features/parent-dashboard/parent-dashboard-page/parent-dashboard-page.component';
import { PromoteSectionListComponent } from './features/school-dashboard/promote-students/promote-section-list/promote-section-list.component';
import { PromoteStudentsComponent } from './features/school-dashboard/promote-students/promote-students/promote-students.component';
import { MessagesComponent } from './features/messages/messages.component';
import { TeacherListForParentComponent } from './features/parent-dashboard/teacher-list-for-parent/teacher-list-for-parent.component';
import { TeacherContactComponent } from './features/messages/teacher-contact/teacher-contact.component';
import { StudentMarkReportComponent } from './features/reports/student-mark-report/student-mark-report.component';
import { UpdateParentProfileComponent } from './features/parent-dashboard/update-parent-profile/update-parent-profile.component';
import { ChildDailyLogComponent } from './features/parent-dashboard/child-daily-log/child-daily-log.component';
import { DailyLogListComponent } from './features/school-dashboard/daily-log/daily-log-list/daily-log-list.component';
import { SchoolPhotoUploadComponent } from './features/school-dashboard/school-photo-upload/school-photo-upload.component';
import { CoursesPageComponent } from './features/courses-page/courses-page.component';
import { ChildReportComponent } from './features/parent-dashboard/child-report/child-report.component';
import { StudentDetailComponent } from './features/school-dashboard/student/student-detail/student-detail.component';
import { JopsListComponent } from './features/jops-list/jops-list.component';
import { PrivteTeacherListComponent } from './features/privte-teacher-list/privte-teacher-list.component';
import { PrivteTeacherDetailComponent } from './features/privte-teacher-list/privte-teacher-detail/privte-teacher-detail.component';

export const routes: Routes = [
    {
        path: '',
        component: MainLayoutComponent,
        children: [
            { path: '', component: HomeComponent },
            { path: 'school/:id', component: SchoolDetailsComponent },
            { path: 'schools', component: SchoolsComponent },
            { path: 'courses', component: CoursesPageComponent },
            { path: 'login', component: LoginComponent },
            { path: 'register', component: RegisterComponent },
            { path: 'test-error', component: TestErrorComponent },
            { path: 'messages', component: MessagesComponent },
            { path: 'day', component: ChildDailyLogComponent },
            { path: 'jops', component: JopsListComponent },
            { path: 'teachers', component: PrivteTeacherListComponent },
            { path: 'teachers/detail', component: PrivteTeacherDetailComponent },

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
            { path: 'school-student-list', component: StudnetListComponent },
            { path: 'school-student-add', component: StudentAddComponent },
            { path: 'school-student-detail/:id', component: StudentDetailComponent },

            { path: 'school-teacher-list', component: TeacherListComponent },
            { path: 'school-teacher-add', component: TeacherAddComponent },
            { path: 'school-teacher-edit/:id', component: TeacherEditComponent },
            { path: 'school-teacher-detail/:id', component: TeacherDetailComponent },
            { path: 'term', component: TermListComponent },
            { path: 'report', component: SchoolReportComponent },
            { path: 'section-grade-list', component: GradeListComponent },
            { path: 'section-subject', component: SectionSubjectListComponent },
            { path: 'section-subject-assignment/:sectionId', component: SectionSubjectAssignmentComponent },
            { path: 'section-list/:id', component: SectionListComponent },
            { path: 'promote', component: PromoteSectionListComponent },
            { path: 'promote/:sectionId', component: PromoteStudentsComponent },
            { path: 'attendance', component: SectionListByTeacherIdComponent },
            { path: 'attendance/:teacherId', component: DailyAttendanceComponent },
            { path: 'mark-entry', component: MarkEntryComponent },
            { path: 'daily-log-list', component: DailyLogListComponent },
            { path: 'school-upload', component: SchoolPhotoUploadComponent },


        ],
    },
    {
        path: 'parent-dash-board',
        component: ParentDashboardComponent,
        children: [
            { path: '', redirectTo: 'overview', pathMatch: 'full' },
            { path: 'overview', component: ParentDashboardPageComponent },
            { path: 'child-report/:studentId', component: ChildReportComponent },
            { path: 'teacher-list', component: TeacherListForParentComponent },
            { path: 'student-report', component: StudentMarkReportComponent },
            { path: 'update-parent-profile', component: UpdateParentProfileComponent },
            { path: 'teacher-contact/:id', component: TeacherContactComponent },

            { path: 'child-profile/:studentId', component: ChildProfileComponent },



        ],
    },
    { path: 'not-found', component: NotFoundComponent },
    { path: 'server-error', component: ServerErrorComponent },


    { path: '**', redirectTo: 'not-found', pathMatch: 'full' },
];
