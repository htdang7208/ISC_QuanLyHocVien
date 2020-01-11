import { Component, OnInit, ViewChild } from '@angular/core';
import { University, UniversityService } from 'src/app/services/university.service';
import { Major, MajorService } from 'src/app/services/major.service';
import { StudentFull, Student, StudentService } from 'src/app/services/student.service';
import { Subject } from 'rxjs';
import { DataTableDirective } from 'angular-datatables';
import { ModalDirective } from 'ngx-bootstrap';
import { DatetimeService } from 'src/app/services/datetime.service';
import { debounceTime } from 'rxjs/operators';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {

  universities: University[] = [];
  majors: Major[] = [];
  students: StudentFull[] = [];
  student: Student = { certification: false, deposits: false, gender: true } as Student;

  private alert = new Subject<string>();
  successMessage: string;

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();
  @ViewChild(DataTableDirective) dtElement: DataTableDirective;

  @ViewChild('modal') modal: ModalDirective;
  @ViewChild('deleteModal') deleteModal: ModalDirective;

// tslint:disable-next-line: max-line-length
  constructor(public datetimeService: DatetimeService, public majorService: MajorService, public universityService: UniversityService, public studentService: StudentService) {
  }

  ngOnInit() {
    this.alert.subscribe((message) => this.successMessage = message);
    this.alert.pipe(
      debounceTime(3000)
    ).subscribe(() => this.successMessage = null);
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10
    };
    this.universityService.getUniversities().subscribe(result => {
      this.universities = result.data;
      console.log(result.data);
    });
    this.majorService.getMajors().subscribe(result => {
      this.majors = result.data;
      console.log(result.data);
    });
    this.loadData();
  }

// tslint:disable-next-line: use-life-cycle-interface
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  showModal(form: NgForm, event = null, id: number = 0) {
    if (event) {
      event.preventDefault();
    }
    form.reset();

    if (id > 0) {
      this.studentService.getStudent(id).subscribe(result => {
        this.student = result.data;
        console.log(this.student);
        this.student.datereadytowork = this.datetimeService.formatDatetimeData(this.student.datereadytowork);
        this.student.dob = this.datetimeService.formatDatetimeData(this.student.dob);
        this.modal.show();
      });
    } else {
      this.student = { certification: false, deposits: false, gender: true } as Student;
      this.modal.show();
    }
  }

  showDeleteModal(event, id) {
    this.student.studentid = id;
    event.preventDefault();
    this.deleteModal.show();
  }

  loadData() {
    this.studentService.getStudents().subscribe(result => {
      this.students = result.data;
      this.rerender();
    });
  }

  save() {
    if (this.student.studentid === undefined || this.student.studentid === 0) {
      this.studentService.addStudent(this.student).subscribe(aresult => {
        this.modal.hide();
        this.loadData();
        this.alertMessage(aresult.message);
      });

    } else {
      this.studentService.updateStudent(this.student).subscribe(aresult => {
        this.modal.hide();
        this.loadData();
        this.alertMessage(aresult.message);
      });
    }
  }

  delete() {
    this.studentService.deleteStudent(this.student.studentid).subscribe(result => {
      if (result.errorCode === 0) {
        const deleteStudent = this.students.find( x => x.id === this.student.studentid);
        if (deleteStudent) {
          const index = this.students.indexOf(deleteStudent);
          this.students.splice(index, 1);
        }
        this.deleteModal.hide();
        this.alertMessage(result.message);
      }
    });
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

}
