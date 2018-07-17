import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute, NavigationEnd } from "@angular/router";
import * as $ from 'jquery';

@Component({
    templateUrl: "tenant.component.html",
    styleUrls: ['./tenant.component.css']
})

export class MultiTenantComponent implements OnInit {
    ngOnInit(): void {
        $(document).ready(function () {
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

        })

    }

}