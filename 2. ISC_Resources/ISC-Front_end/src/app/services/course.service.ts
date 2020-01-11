import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { SpecializedTrainingService, SpecialiazedTraining } from './specialized-training.service';

export class Course {
  courseid: number;
  courseame: string;
  startdate: Date;
  enddate: Date;
  note: string;
  listTrainings: SpecialiazedTraining[];
}

export interface CourseTrainingInfo {
  courseId: number;
  trainingId: number;
  trainingName: string;
}

export class CourseResponse {
  errorCode: number;
  message: string;
  data: Course;
}

export class CoursesResponse {
  errorCode: number;
  message: string;
  data: Course[];
}

@Injectable({
  providedIn: 'root'
})

export class CourseService {
  constructor(private http: HttpClient, private api: ApiService, private courseService: CourseService) { }
  getAll(): Observable<CoursesResponse> {
    const url = this.api.apiUrl.course;
    return this.http.get<CoursesResponse>(url);
  }

  get(Id): Observable<CourseResponse> {
    const url = this.api.apiUrl.course + '/' + Id;
    return this.http.get<CourseResponse>(url);
  }

  getTheLast(): Observable<CourseResponse> {
    const url = this.api.apiUrl.specializedtraining + '/getthelast';
    return this.http.get<CourseResponse>(url);
  }

  add(data: Course): Observable<CourseResponse> {
    const url = this.api.apiUrl.course;
    return this.http.post<CourseResponse>(url, data);
  }

  update(data: Course, Id): Observable<CourseResponse> {
    const url = this.api.apiUrl.course + '/' + Id;
    return this.http.put<CourseResponse>(url, data);
  }

  delete(Id): Observable<CourseResponse> {
    const url = this.api.apiUrl.course + '/' + Id;
    return this.http.delete<CourseResponse>(url);
  }
}
