import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import {NgxPaginationModule} from 'ngx-pagination';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';
import { CourseComponent } from './pages/course/course.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { SpecializedTrainingComponent } from './pages/specialized-training/specialized-training.component';
import { UniversitymajorComponent } from './pages/universitymajor/universitymajor.component';
import { CompanyComponent } from './pages/company/company.component';
import { SubjectComponent } from './pages/subject/subject.component';
import { WorktrackComponent } from './pages/worktrack/worktrack.component';
import { AuthGuard } from './auth.guard';
import { AuthInterceptor } from './services/auth.interceptor';
import { CookieService } from 'ngx-cookie-service';
import { DataTablesModule } from 'angular-datatables';
import { Select2Module } from 'ng2-select2';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { StudentComponent } from './pages/student/student.component';
import { AdminComponent } from './pages/admin/admin.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    LoginComponent,
    CourseComponent,
    SpecializedTrainingComponent,
    UniversitymajorComponent,
    CompanyComponent,
    SubjectComponent,
    WorktrackComponent,
    StudentComponent,
    AdminComponent

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ModalModule,
    HttpClientModule,
    FormsModule,
    ModalModule.forRoot(),
    NgxPaginationModule,
    DataTablesModule,
    Select2Module,
    NgbModule.forRoot()
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    AuthGuard,
    CookieService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
