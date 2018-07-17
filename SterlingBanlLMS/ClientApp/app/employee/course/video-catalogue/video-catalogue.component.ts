﻿import { Component, OnInit } from "@angular/core";
import * as $ from 'jquery';

@Component({
    templateUrl: "video-catalogue.component.html",
    styleUrls: ['video-catalogue.component.css']
})

export class VideoCatalogueComponent implements OnInit {
    ngOnInit(): void {

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
            

        });

    }

}