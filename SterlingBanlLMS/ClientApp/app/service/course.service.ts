import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/of';
import { Course, Topic } from '../models/course';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class CourseService {
    courses: Array<Course>;
    course: Course;
    topic: Topic;
    private infoUrl = "/api/course/getcourseinfo";
    private firstModuleUrl = "/api/course/getfirstmodule";
    private topicUrl="/api/course/gettopic"
    indLoading: boolean = false;

    constructor(private http: Http, private toastr: ToastrService) { }

    getAllCourses(): Observable<Course[]> {
        this.indLoading = true;
        return this.http.get("/api/course/getcourses")
            .map((x: Response) =>  this.courses = x.json().Result)
            //.do(data => console.log('GetCourses:' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    getCourse(id: number): Observable<Course> {
        const url = `${this.infoUrl}?id=${id}`;
        return this.http.get(url)
            .map((course: Response) => this.course = course.json().Result)
            //.do(d => console.log("This product with id:" + JSON.stringify(d)))
            .catch(this.handleError);
    }

    getFirstModule(id: number): Observable<Course> {
        const url = `${this.firstModuleUrl}?id=${id}`;
        return this.http.get(url)
            .map((course: Response) => this.course = course.json().Result)
            .catch(this.handleError);
    }

    getTopic(id: number): Observable<Topic> {
        const url = `${this.topicUrl}?id=${id}`;
        return this.http.get(url)
            .map((topic: Response) => this.topic = topic.json().Result)
            .catch(this.handleError);
    }

    

    private handleError(error: Response): Observable<any> {
        this.toastr.error("Error Fetching files from server");
        return Observable.throw(error.json().error || 'Error Fetching file from server');
    }
}