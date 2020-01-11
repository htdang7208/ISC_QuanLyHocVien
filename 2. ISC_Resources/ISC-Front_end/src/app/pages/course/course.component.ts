import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective, BsModalRef, BsModalService } from 'ngx-bootstrap';
import { Course, CourseService } from 'src/app/services/course.service';
import { SpecialiazedTraining, SpecializedTrainingService } from 'src/app/services/specialized-training.service';
import { DatetimeService } from 'src/app/services/datetime.service';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { CourseTraining, CourseTrainingService } from 'src/app/services/coursetraining.service';



@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {

  modalRef: BsModalRef;
  test: string;
  course: Course = {} as Course;
  courses: Course [] = [];
  trainings: SpecialiazedTraining[] = [];
  training: SpecialiazedTraining = {} as SpecialiazedTraining;
  selectedTrainings: SpecialiazedTraining[] = [];
  courseTraining: CourseTraining = {} as CourseTraining;
  listCourseTrainingDeleted: number[] = [];

  private alert = new Subject<string>();
  successMessage: string;

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();
  @ViewChild(DataTableDirective) dtElement: DataTableDirective;

  @ViewChild('modal') modal: ModalDirective;
  @ViewChild('deleteModal') deleteModal: ModalDirective;
  
  constructor(private courseService: CourseService, private modalService: BsModalService
            , private trainingService: SpecializedTrainingService, private datetimeService: DatetimeService
            , private courseTrainingService: CourseTrainingService) { }
  ngOnInit() {
    this.alert.subscribe((message) => this.successMessage = message);
    this.alert.pipe(
      debounceTime(3000)
    ).subscribe(() => this.successMessage = null);
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10
    };

    this.loadData();
  }

  loadData() {
    this.courseService.getAll().subscribe(
      result => {
        this.courses = result.data;
        this.rerender();
    });
    this.trainingService.getAll().subscribe(
      result => {
        this.trainings = result.data;
      });
  }

  // tslint:disable-next-line: use-life-cycle-interface
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  // tslint:disable-next-line: use-life-cycle-interface
  ngAfterViewInit(): void {this.dtTrigger.next(); }

  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
       dtInstance.destroy();
       this.dtTrigger.next();
   });
  }

  alertMessage(message) {
    this.alert.next(message);
  }

  showModal(event = null, Id: number = 0) {
    console.log(Id);
    if (event) {
      event.preventDefault();
    }
    if (Id > 0) {
      this.courseService.get(Id).subscribe( result => {
        this.course = result.data;
        console.log(this.course);
        this.selectedTrainings = this.course.listTrainings;
        this.course.startdate = this.datetimeService.formatDatetimeData(this.course.startdate);
        this.course.enddate = this.datetimeService.formatDatetimeData(this.course.enddate);
        this.modal.show();
      });
    } else {
      this.course = {} as Course;
      this.selectedTrainings = [] as SpecialiazedTraining[];
      this.modal.show();
    }
    console.log(this.listCourseTrainingDeleted);
    this.modal.show();
  }

  save() {
    if (this.course.courseid === undefined || this.course.courseid === 0) {
      this.courseService.add(this.course).subscribe(result => {
        console.log(result);
        this.courseService.getTheLast().subscribe(resultt => {
          console.log(this.selectedTrainings);
          if (this.selectedTrainings.length > 0) {
            this.selectedTrainings.forEach(item => {
              const training = {} as CourseTraining;
              training.trainingid = item.trainingid;
              training.courseid = resultt.data.courseid;
              console.log(training);
              this.courseTrainingService.add(training).subscribe(result => {
                this.loadData();
              });
            });
          }
          this.alertMessage(result.message);
        });
        this.modal.hide();
      });
    } else {
      this.courseService.update(this.course, this.course.courseid).subscribe(result => {
        if (this.listCourseTrainingDeleted.length > 0) {
          console.log(this.listCourseTrainingDeleted);
          this.listCourseTrainingDeleted.forEach(item => {
            this.deleteCourseTraing(item);
          });
          this.listCourseTrainingDeleted = [] as number[];
          this.loadData();
        }
        if (this.selectedTrainings.length > 0) {
          console.log(this.selectedTrainings);
          this.selectedTrainings.forEach(item => {
              let isExist: boolean = false;
              const addTraining = {} as CourseTraining;
              addTraining.trainingid = item.trainingid;
              addTraining.courseid = this.course.courseid;
              this.courseTrainingService.getCourseTraining(addTraining).subscribe(result => {
                if(result.data !== undefined) { isExist = true; }
                if (!isExist) {
                  this.courseTrainingService.add(addTraining).subscribe(result => {
                    this.alertMessage(result.message);
                    this.loadData();
                  });
                }
              });
            });
          this.selectedTrainings = null;
        } else {
          this.courseTrainingService.deleteAllByCourseId(this.course.courseid).subscribe(result => {
            this.alertMessage(result.message);
            this.loadData();
          });
        }
        this.modal.hide();
      });
    }
  }

  showDeleteModal(event, id) {
    this.course.courseid = id;
    this.courseService.get(id).subscribe( result => {
      this.course.listTrainings =  result.data.listTrainings;
      this.alertMessage(result.message);
    });
    event.preventDefault();
    this.deleteModal.show();
  }

  delete(event = null) {
    event.preventDefault();
    if(this.course.listTrainings.length > 0)
    {
      this.courseTrainingService.deleteAllByCourseId(this.course.courseid).subscribe(result => {
        this.courseService.delete(this.course.courseid).subscribe(item => {
          const deletedCourse = this.courses.find(x => x.courseid === this.course.courseid);
          if (item.errorCode === 0) {
            const index = this.courses.indexOf(deletedCourse);
            if (deletedCourse) {
              this.courses.splice(index);
            }
          }
          this.alertMessage(result.message);
        });
      });
    } else {
      this.courseService.delete(this.course.courseid).subscribe(result => {
        const deletedCourse = this.courses.find(x => x.courseid === this.course.courseid);
        if (result.errorCode === 0) {
          const index = this.courses.indexOf(deletedCourse);
          if (deletedCourse) {
            this.courses.splice(index);
          }
        }
        this.alertMessage(result.message);
      });
    }
    this.deleteModal.hide();
  }

  addTrainingToCourse(data: SpecialiazedTraining) {
    this.selectedTrainings.push(data);
  }

  // Xoa chuong trinh hoc khoi danh sach chuong trinh hoc cua khoa hoc tren modal
  removeFromSelectedTrainings(index) {
    const deletedTrainingId = this.selectedTrainings[index].trainingid;
    const item = {} as CourseTraining;
    item.trainingid = deletedTrainingId;
    item.courseid = this.course.courseid;
    this.selectedTrainings.splice(index, 1); // Xoa khoi selectedTraining
    this.courseTrainingService.getCourseTraining(item).subscribe(result => {
      if (result.data !== null) {  // Neu training da co trong db CourseTraining thi them Id vao listDeleted
        this.listCourseTrainingDeleted.push(result.data.coursE_TRAINING_ID);
      }
    });
  }

  deleteCourseTraing(CourseTrainingId) {
    this.courseTrainingService.delete(CourseTrainingId).subscribe(result => {
        console.log(result.message);
        this.loadData();
    });
  }

  checkSelectedTraining(id) {
    console.log(id);
    console.log(this.selectedTrainings);
    this.trainingService.get(id).subscribe(
      result => {
        console.log(result.data);
        const index = this.selectedTrainings.filter(function(el) {
          if (el.trainingid === id) {
            return el;
          }
        });
        console.log(index);
        if (index.length > 0) {
          alert('Chương trình học này đã được chọn!');
        } else {
          this.addTrainingToCourse(result.data);
        }
      }
    );
  }
}
