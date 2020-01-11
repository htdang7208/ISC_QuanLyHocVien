import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Company } from './company.service';
import { Student } from './student.service';

export interface WorktrackInfo {
  worktrackid: number;
  startdate: Date;
  contractdate: Date;
  status: number;
  note: string;
  student: Student;
  company: Company;
}

export interface Worktrack {
  worktrackid: number;
  companyid: number;
  studentid: number;
  startdate: Date;
  contractdate: Date;
  status: number;
  note: string;
}

export interface WorktrackResponse {
  data: Worktrack;
  errorCode: number;
  message: string;
}

export interface WorktracksResponse {
  data: Worktrack[];
  errorCode: number;
  message: string;
}

export interface WorktracksInfoResponse {
  data: WorktrackInfo[];
  errorCode: number;
  message: string;
}
@Injectable({
  providedIn: 'root'
})
export class WorktrackService {

  constructor(private api: ApiService, private http: HttpClient) { }
  getAll(): Observable<WorktracksInfoResponse> {
    return this.http.get<WorktracksInfoResponse>(this.api.apiUrl.worktracks);
  }
  get(id): Observable<WorktrackResponse> {
    return this.http.get<WorktrackResponse>(`${this.api.apiUrl.worktracks}/${id}`);
  }
  add(data: Worktrack): Observable<WorktrackResponse> {
    return this.http.post<WorktrackResponse>(this.api.apiUrl.worktracks, data);
  }
  update(data: Worktrack): Observable<WorktrackResponse> {
    const url = `${this.api.apiUrl.worktracks}/${data.worktrackid}`;
    return this.http.put<WorktrackResponse>(url, data);
  }
  delete(id): Observable<WorktrackResponse> {
    const url = `${this.api.apiUrl.worktracks}/${id}`;
    return this.http.delete<WorktrackResponse>(url);
  }
}
