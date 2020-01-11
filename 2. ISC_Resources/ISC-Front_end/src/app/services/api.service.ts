import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor() { }
  baseUrl = 'https://localhost:44328/api/';
  apiUrl = {
    admins: this.baseUrl + 'admins',
    companies: this.baseUrl + 'companies',
    course: this.baseUrl + 'courses',
    coursetraining: this.baseUrl + 'coursetrainings',
    login: this.baseUrl + 'admin/login',
    trainingsubject: this.baseUrl + 'trainingsubjects',
    major: this.baseUrl + 'majors',
    specializedtraining: this.baseUrl + 'specializedtrainings',
    subjects: this.baseUrl + 'subjects',
    university: this.baseUrl + 'universities',
    worktracks: this.baseUrl + 'worktracks',
    student: this.baseUrl + 'students'
  };
}
