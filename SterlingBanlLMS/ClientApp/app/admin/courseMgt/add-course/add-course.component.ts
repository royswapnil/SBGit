import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute, NavigationEnd } from "@angular/router";
import * as $ from 'jquery';
import { FormBuilder, FormGroup, FormControl, FormArray, Validators, FormControlName, AbstractControl, NgForm } from '@angular/forms';

@Component({
    templateUrl: "add-course.component.html",
    styleUrls: ['./add-course.component.css']
})

export class AddCourseComponent implements OnInit {
    ngOnInit(): void {

        $(document).ready(function () {
            //sticky headers
            // Hide Header on on scroll down
            var didScroll;
            var lastScrollTop = 0;
            var delta = 5;
            var navbarHeight = $('header.second').outerHeight();
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
                if (Math.abs(lastScrollTop - st) <= delta) return;
                // If they scrolled down and are past the navbar, add class .header_up.
                // This is necessary so you never see what is "behind" the navbar.
                if (st > lastScrollTop && st > navbarHeight) {
                    // Scroll Down
                    $('header.second').addClass('header_up');
                    $('.pallete1').addClass('pallete_up');
                } else {
                    // Scroll Up
                    if (st + $(window).height() < $(document).height()) {
                        $('header.second').removeClass('header_up');
                        $('.pallete1').removeClass('pallete_up');
                    }
                }
                lastScrollTop = st;
            }
            //end of sticky headers

            //menu Button
            $('.menuBtn1').click(function () {
                $('.pallete1').toggleClass('pallete_big');
                $('.menuTab1').toggleClass('shrink');
            });
            //notification, msg and profile pop up


            $('.msg').click(function () {

                $('.msgBox').toggleClass("showPop");
                $('.notyBox, .probBox').removeClass("showPop");

            });

            $('.noty').click(function () {

                $('.notyBox').toggleClass("showPop");
                $('.msgBox, .probBox').removeClass("showPop");

            });

            $('.user').click(function () {

                $('.probBox').toggleClass("showPop");
                $('.msgBox, .notyBox').removeClass("showPop");

            });

            //add course and topics


            $('.formArea1 .btnDarkPink').click(function () {
                $('.formArea1').hide();
                $('.progress1 .tabloid:nth-child(2)').addClass('active');
                $('.progressIndicator1').css("width", "75%");

                setTimeout(function () {
                    $('.formArea2').show();
                }, 500);
            });

            $('.formArea2 .btnGrey').click(function () {
                $('.formArea2').hide();
                $('.progress1 .tabloid:nth-child(2)').removeClass('active');
                $('.progressIndicator1').css("width", "25%");

                setTimeout(function () {
                    $('.formArea1').show();

                }, 500);

            });


            $('.formArea2 .btnDarkPink').click(function () {
                $('.formArea2').hide();

                setTimeout(function () {
                    $('.formArea3').show();

                    $('.progress1 .tabloid:nth-child(3)').addClass('active');
                    $('.progressIndicator1').css("width", "100%");

                }, 500);

            });


            $('.fab3').click(function () {
                $('.relay1').show();
                $('.relay3').hide();
            });

            $('.fab4').click(function () {
                $('.relay3').show();
                $('.relay1').hide();
            });


            $('select.typeUpload').change(function () {

                var nag = $(this).val();

                if (nag == "Text" || nag == "Quiz") {
                    $('.relay4').show();

                    $('.fab3').prop("checked", true);
                    $('.fab4').prop("checked", false);
                    $('.relay2, .relay3').hide();
                    $('.relay1').show();
                }
                else {
                    $('.relay4').hide();
                    $('.relay2').show();
                    $('.relay1, .relay3').hide();
                }

            });

        });
    }
}