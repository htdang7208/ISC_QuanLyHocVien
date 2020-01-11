import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';

export interface CourseTraining {
  coursE_TRAINING_ID: number;
  courseid: number;
  trainingid: number;
}

export interface CourseTrainingResponse {
  errorCode: number;
  message: string;
  data: CourseTraining;
}

export interface CourseTrainingsResponse {
  errorCode: number;
  message: string;
  data: CourseTraining[];
}
@Injectable({
  providedIn: 'root'
})
export class CourseTrainingService {

  constructor(private http: HttpClient, private api: ApiService) { }

  getAll(): Observable<CourseTrainingsResponse> {
    const url = this.api.apiUrl.coursetraining;
    return this.http.get<CourseTrainingsResponse>(url);
  }

  get(TrainingId): Observable<CourseTrainingResponse> {
    const url = this.api.apiUrl.coursetraining + '/' + TrainingId;
    return this.http.get<CourseTrainingResponse>(url);
  }

  getCourseTraining(data: CourseTraining): Observable<CourseTrainingResponse> {
    const url = this.api.apiUrl.coursetraining + '/getcoursetraining';
    return this.http.post<CourseTrainingResponse>(url, data);
  }

  add(data: CourseTraining): Observable<CourseTrainingResponse> {
    const url = this.api.apiUrl.coursetraining;
    return this.http.post<CourseTrainingResponse>(url, data);
  }

  delete(id): Observable<CourseTrainingResponse> {
    const url = this.api.apiUrl.coursetraining + '/' + id;
    return this.http.delete<CourseTrainingResponse>(url);
  }

  deleteAllByCourseId(CourseId): Observable<CourseTrainingResponse> {
    const url = this.api.apiUrl.coursetraining + '/DeleteAllByCourseId/' + CourseId;
    return this.http.delete<CourseTrainingResponse>(url);
  }
}
