import { Component, OnInit, OnDestroy } from "@angular/core";
import { FormBuilder, FormGroup, FormControl, FormArray, Validators, FormControlName } from '@angular/forms';
import * as $ from "jquery";
import { Router, ActivatedRoute } from "@angular/router"
import { Subscription } from 'rxjs/Subscription';
import { Course } from "../../../models/course";
import { CourseService } from "../../../service/course.service";

@Component({
    selector: '',
    templateUrl: "course-detail.component.html",
    styleUrls: ['course-detail.component.css']
})

export class CourseDetailComponent implements OnInit, OnDestroy {  

    course: Course;
    private sub: Subscription;
    errorMessage: string;

    constructor(private router: Router, private route: ActivatedRoute, private courseService:CourseService) {

    }


    //startLearning(): void {
       
    //    this.router.navigate(['/course/', this.course.Id, '/module']);
    //    console.log(this.router.navigate(['/course/', this.course.Id, '/start_learning']));
    //}

    ngOnInit(): void {
        this.sub = this.route.paramMap.subscribe(
            params => {
                let id = +params.get('id');

                this.getCourse(id);
            }
        )



        $(document).ready(function () {

            // Hide Header on on scroll down
            var didScroll;
            var lastScrollTop = 0;
            var delta = 5;
            var navbarHeight = $('header').outerHeight();

            $(window).scroll(function (event) {
                didScroll = true;
            });

            setInterval(function () {
                if (didScroll) {
                    hasScrolled();
                    didScroll = false;
                }
            }, 250);

            function hasScrolled() {
                var st = $(this).scrollTop();

                // Make sure they scroll more than delta
                if (Math.abs(lastScrollTop - st) <= delta)
                    return;

                // If they scrolled down and are past the navbar, add class .header_up.
                // This is necessary so you never see what is "behind" the navbar.
                if (st > lastScrollTop && st > navbarHeight) {
                    // Scroll Down

                    $('header').addClass('header_up');
                } else {
                    // Scroll Up
                    if (st + $(window).height() < $(document).height()) {
                        $('header').removeClass('header_up');

                    }
                }

                lastScrollTop = st;
            }
            //end of sticky headers

            //menu Button

            $('.menuBtn').click(function () {
                $('.menuTab').toggleClass("show");
                $(this).toggleClass("inside");
            });


            $('.long i').click(function () {
                $('.hideTab').toggleClass("shown");
                $(this).toggleClass("down");
            });

            $('.tabs .tab-links a').on('click', function (e) {
                var currentAttrValue = $(this).attr('href');

                // Show/Hide Tabs
                $('.tabs ' + currentAttrValue).slideDown(400).siblings().slideUp(400);

                // Change/remove current tab to active
                $(this).parent('li').addClass('active').siblings().removeClass('active');

                e.preventDefault();
            });

        })

        
    }
    getCourse(id: number) {
        this.courseService.getCourse(id).subscribe(
            c => this.course = c,
            err => this.errorMessage = <any>err
        );
    }

    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }

    
}