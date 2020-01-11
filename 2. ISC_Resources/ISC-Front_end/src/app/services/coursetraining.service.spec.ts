import { TestBed } from '@angular/core/testing';

import { CoursetrainingService } from './coursetraining.service';

describe('CoursetrainingService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CoursetrainingService = TestBed.get(CoursetrainingService);
    expect(service).toBeTruthy();
  });
});
