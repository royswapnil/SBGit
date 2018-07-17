import { Component, OnInit, OnDestroy } from "@angular/core";
import * as $ from 'jquery';
import { Router, ActivatedRoute } from "@angular/router"
import { Subscription } from 'rxjs/Subscription';
import { ToastrService } from 'ngx-toastr';
import { VideoService } from "../../../service/video.service";
import { Course, Topic } from "../../../models/course";

@Component({
    selector: '',
    templateUrl: "video.component.html",
    styleUrls: ['video.component.css']
})

export class VideoComponent implements OnInit, OnDestroy {
    
    course: Course;
    private sub: Subscription;
    errorMessage: string;
    topic: Topic;


    constructor(private router: Router, private route: ActivatedRoute, private videoService: VideoService, private toastr: ToastrService) { }
    ngOnInit(): void {

        this.sub = this.route.paramMap.subscribe(
            params => {
                let id = +params.get('id');
                this.videoService.getVideos(id);
            }        

        )
        this.videoService.appSetup("videoDisplay");

        $(document).ready(function () {
            $('.lessonTab').click(function () {

                $('.lessonTab').removeClass("active");
                $(this).addClass("active");
                $('.col99').toggleClass("show");

            });

        })

    }
    
    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }
}