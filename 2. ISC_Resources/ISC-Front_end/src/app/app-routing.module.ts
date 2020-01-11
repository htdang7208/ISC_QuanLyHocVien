import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CourseComponent } from './pages/course/course.component';
import { SpecializedTrainingComponent } from './pages/specialized-training/specialized-training.component';
import {  } from 'rxjs';
import { UniversitymajorComponent } from './pages/universitymajor/universitymajor.component';
import { CompanyComponent } from './pages/company/company.component';
import { SubjectComponent } from './pages/subject/subject.component';
import { WorktrackComponent } from './pages/worktrack/worktrack.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './auth.guard';
import { StudentComponent } from './pages/student/student.component';
import { AdminComponent } from './pages/admin/admin.component';

const routes: Routes = [
  // { path: '', component: LoginComponent, data: { title: 'Login Page' }},
  {
    path: '', component: DashboardComponent, data: { title: 'Home' },
    // canActivate: [AuthGuard], // linh canh DANG NHAP
  children: [
    // {path: 'dashboard', component: DashboardComponent},
    {path: 'course', component: CourseComponent},
    {path: 'sptraining', component: SpecializedTrainingComponent},
    {path: 'universitymajor', component: UniversitymajorComponent},
    {path: 'company', component: CompanyComponent},
    {path: 'subject', component: SubjectComponent},
    {path: 'worktrack', component: WorktrackComponent},
    {path: 'admin', component: AdminComponent},
    {path: 'student', component: StudentComponent},
  
  ]
  }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
