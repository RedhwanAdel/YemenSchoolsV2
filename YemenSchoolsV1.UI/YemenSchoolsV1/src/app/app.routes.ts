import { Routes } from '@angular/router';
import { HomeComponent } from './core/home/home.component';
import { SchoolComponent } from './features/school/school.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { ContactComponent } from './core/contact/contact.component';

export const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'schools', component: SchoolComponent },
    { path: 'contact', component: ContactComponent },
    { path: '**', component: NotFoundComponent },

];
