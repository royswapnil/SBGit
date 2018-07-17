import { Component, OnInit } from "@angular/core";
import * as $ from "jquery";
import { ActivatedRoute } from "@angular/router";
import { NgProgress } from 'ngx-progressbar';
import { Course } from "../../models/course";
import { CourseService } from "../../service/course.service";

@Component({
    selector: '',
    templateUrl: "./home.component.html",
    styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

    indLoading: boolean = false;
    errorMessage: string;
    assignedCourses: Course[];
    recommendedCourses: Course[];

    constructor(private courseService: CourseService, private route: ActivatedRoute, public ngProgress: NgProgress) {
        this.assignedCourses = courseService.courses;
        this.recommendedCourses = courseService.courses;
    }
    ngOnInit(): void {
        this.ngProgress.start();
        this.indLoading = true;
        this.courseService.getAllCourses()
            .subscribe(c => {
                this.assignedCourses = c,
                    this.indLoading = false;
            },
            error => this.errorMessage = <any>error,
        );
        this.ngProgress.done();

        this.ngProgress.start();
        this.courseService.getAllCourses()
            .subscribe(c => {
                this.recommendedCourses = c,
                    this.indLoading = false;

            },
            error => this.errorMessage = <any>error);
        this.ngProgress.done();

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

}