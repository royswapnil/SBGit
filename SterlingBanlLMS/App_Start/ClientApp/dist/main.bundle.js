webpackJsonp(["main"],{

/***/ "../../../../../ClientApp/$$_lazy_route_resource lazy recursive":
/***/ (function(module, exports) {

function webpackEmptyAsyncContext(req) {
	// Here Promise.resolve().then() is used instead of new Promise() to prevent
	// uncatched exception popping up in devtools
	return Promise.resolve().then(function() {
		throw new Error("Cannot find module '" + req + "'.");
	});
}
webpackEmptyAsyncContext.keys = function() { return []; };
webpackEmptyAsyncContext.resolve = webpackEmptyAsyncContext;
module.exports = webpackEmptyAsyncContext;
webpackEmptyAsyncContext.id = "../../../../../ClientApp/$$_lazy_route_resource lazy recursive";

/***/ }),

/***/ "../../../../../ClientApp/app/admin/courseMgt/add-course/add-course.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../ClientApp/app/admin/courseMgt/add-course/add-course.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"pallete1\">\r\n\r\n    <div class=\"banner1 bkPurple\">\r\n        <i class=\"material-icons menuBtn1\">menu</i>\r\n        <span>Add a Course</span>\r\n    </div>\r\n\r\n\r\n    <div class=\"content1\">\r\n\r\n\r\n        <div class=\"boxBody1\">\r\n\r\n            <div class=\"progress1\">\r\n\r\n                <div class=\"tabloid active\">\r\n                    <div class=\"podb\">1</div>\r\n                    <span>Course details</span>\r\n                </div>\r\n\r\n                <div class=\"tabloid\">\r\n                    <div class=\"podb\">2</div>\r\n                    <span>Upload Course Material</span>\r\n                </div>\r\n\r\n                <div class=\"tabloid\">\r\n                    <div class=\"podb\">3</div>\r\n                    <span>Finish</span>\r\n                </div>\r\n\r\n\r\n                <div class=\"progressLine1\">\r\n                    <div class=\"progressIndicator1\"></div>\r\n                </div>\r\n\r\n            </div>\r\n\r\n\r\n            <form novalidate  [formGroup]=\"courseForm\" #formDir=\"ngForm\" (ngSubmit)=\"login(courseForm.value)\">\r\n                <div class=\"formArea formArea1\">\r\n\r\n                    <div class=\"formBox1 fullForm\">\r\n                        <label for=\"\">Course Title:</label>\r\n                        <input type=\"text\" placeholder=\"\" formControlName=\"courseTitle\" />\r\n                    </div>\r\n\r\n                    <div class=\"formBox1 halfForm\">\r\n                        <label for=\"\">Course Description:</label>\r\n                        <textarea name=\"\" id=\"\" placeholder=\"750 characters max...\" formControlName=\"courseDescription\"></textarea>\r\n                    </div>\r\n\r\n                    <div class=\"formBox1 halfForm\">\r\n                        <label for=\"\">Available Till:</label>\r\n                        <input type=\"date\" placeholder=\"\" formControlName=\"availableDate\" />\r\n                    </div>\r\n\r\n                    <div class=\"formBox1 halfForm\">\r\n                        <label for=\"\">Select Learning Area</label>\r\n                        <select name=\"\" id=\"\">\r\n                            <option value=\"\">Communication</option>\r\n                            <option value=\"\">Statistics</option>\r\n                            <option value=\"\">Finances</option>\r\n                        </select>\r\n                    </div>\r\n\r\n\r\n                    <div class=\"formBox1 halfForm\">\r\n                        <label for=\"\">Upload Course Image <i>.png, .jpg, .bmp</i></label>\r\n                        <input type=\"file\" placeholder=\"\" />\r\n\r\n                        <button>Upload Image</button>\r\n                    </div>\r\n\r\n                    <div class=\"formBox1 fullForm\">\r\n                        <label for=\"\">Enter course overview below</label>\r\n                        <textarea name=\"\" id=\"\" cols=\"30\" rows=\"10\" formControlName=\"courseOverview\"></textarea>\r\n                    </div>\r\n\r\n\r\n                    <div class=\"formBox1 fullForm\">\r\n                        <label for=\"\">Enter Instructional Objectives</label>\r\n                        <textarea name=\"\" id=\"\" cols=\"30\" rows=\"10\" formControlName=\"instructionalObj\"></textarea>\r\n                    </div>\r\n\r\n                    <div class=\"underlineBtn\">\r\n                        <button class=\"nextBtn btn1 btnMedium btnDarkPink\">Save and Continue</button>\r\n                    </div>\r\n\r\n\r\n\r\n                </div>\r\n\r\n\r\n\r\n                <div class=\"formArea formArea2\">\r\n\r\n                    <div class=\"formBox1 fullForm\">\r\n                        <label for=\"\">Course Title:</label>\r\n                        <input type=\"text\" placeholder=\"\" value=\"The title given before\" disabled />\r\n                    </div>\r\n\r\n\r\n                    <div class=\"formBox1 fullForm\">\r\n                        <label for=\"\">Select Module: <i>Select Module:\t(Select module name from the dropdown below OR create a new module by typing in the field next to the dropdown)</i></label>\r\n\r\n                        <div class=\"formBox1 halfForm\">\r\n                            <select name=\"\" id=\"\">\r\n                                <option value=\"INtorduction\">Introduction</option>\r\n                            </select>\r\n                        </div>\r\n\r\n\r\n                        <div class=\"formBox1 halfForm\">\r\n                            <input type=\"text\" placeholder=\"Enter new module name here...\" />\r\n                            <button>Create Module</button>\r\n                        </div>\r\n                    </div>\r\n\r\n\r\n                    <div class=\"formBox1 fullForm\">\r\n                        <label for=\"\">Enter topic name<i>(Enter topic name for this course below)</i></label>\r\n                        <input type=\"text\" placeholder=\"\">\r\n                    </div>\r\n\r\n\r\n                    <div class=\"formBox1 halfForm\">\r\n                        <label for=\"\">Content Type<i>(Select content type for this course )</i></label>\r\n                        <select name=\"\" id=\"\" class=\"typeUpload\">\r\n                            <option value=\"Text\" class=\"textPart\">Text Material</option>\r\n                            <option value=\"Video\" class=\"vidPart\">Video Material</option>\r\n                            <option value=\"Quiz\" class=\"quizPart\">quiz</option>\r\n                        </select>\r\n                    </div>\r\n\r\n\r\n                    <div class=\"formBox1 halfForm relay4\">\r\n                        <label for=\"\">Add Content<i>(Choose how you want to add content to this topic)</i></label>\r\n                       \r\n                            <input type=\"radio\" name=\"upload\" class=\"fab3\" checked><span class=\"radio\">Upload a file</span>\r\n                            <input type=\"radio\" name=\"upload\" class=\"fab4\"><span class=\"radio\">Use a Text Editor</span>\r\n\r\n                        \r\n                    </div>\r\n\r\n\r\n                    <div class=\"formBox1 halfForm\">\r\n\r\n                        <div class=\"relay relay1\">\r\n                            <label for=\"\">Formats allowed are: <i>xls, .pdf, .docx, .pptx, .csv</i></label>\r\n                            <input type=\"file\" name=\"\" id=\"\">\r\n                            <button>Upload</button>\r\n                        </div>\r\n\r\n\r\n                        <div class=\"relay relay2\">\r\n                            <label for=\"\">Format allowed are:<i>mp4, webm, avi, flv</i></label>\r\n                            <input type=\"file\" name=\"\" id=\"\">\r\n                            <button>Upload</button>\r\n                        </div>\r\n\r\n                        <div class=\"relay relay3\">\r\n                            <label for=\"\">Input your text</label>\r\n                            <textarea name=\"\" id=\"\" cols=\"30\" rows=\"10\"></textarea>\r\n                        </div>\r\n\r\n                    </div>\r\n\r\n\r\n                    <div class=\"formBox1 fullForm\">\r\n                        <button class=\"endBtn\">Add Topic</button>\r\n                    </div>\r\n\r\n\r\n                    <div class=\"boxHead1\">\r\n                        <div class=\"boxHeader1 bDPurple\">Uploaded Content</div>\r\n                    </div>\r\n\r\n                    <div class=\"boxBody1\">\r\n\r\n                        <div class=\"courseSlate\">\r\n\r\n                            <div class=\"topicNm\"><i class=\"material-icons typei\">slideshow</i>Topic 1</div>\r\n                            <div class=\"topicMod\">Module 1</div>\r\n                            <div class=\"topicAction\">\r\n                                <i class=\"material-icons\">edit</i>\r\n                                <i class=\"material-icons\">visibility</i>\r\n                                <i class=\"material-icons\" style=\"color:red;\">close</i>\r\n                            </div>\r\n                        </div>\r\n\r\n                        <div class=\"courseSlate\">\r\n\r\n                            <div class=\"topicNm\"><i class=\"material-icons typei\">description</i>Topic 1</div>\r\n                            <div class=\"topicMod\">Module 1</div>\r\n                            <div class=\"topicAction\">\r\n                                <i class=\"material-icons\">edit</i>\r\n                                <i class=\"material-icons\">visibility</i>\r\n                                <i class=\"material-icons\" style=\"color:red;\">close</i>\r\n                            </div>\r\n                        </div>\r\n\r\n\r\n                        <div class=\"courseSlate\">\r\n\r\n                            <div class=\"topicNm\"><i class=\"material-icons typei\">slideshow</i>Topic 1</div>\r\n                            <div class=\"topicMod\">Module 1</div>\r\n                            <div class=\"topicAction\">\r\n                                <i class=\"material-icons\">edit</i>\r\n                                <i class=\"material-icons\">visibility</i>\r\n                                <i class=\"material-icons\" style=\"color:red;\">close</i>\r\n                            </div>\r\n                        </div>\r\n\r\n\r\n                        <div class=\"courseSlate\">\r\n\r\n                            <div class=\"topicNm\"><i class=\"material-icons typei\">description</i>Topic 1</div>\r\n                            <div class=\"topicMod\">Module 1</div>\r\n                            <div class=\"topicAction\">\r\n                                <i class=\"material-icons\">edit</i>\r\n                                <i class=\"material-icons\">visibility</i>\r\n                                <i class=\"material-icons\" style=\"color:red;\">close</i>\r\n                            </div>\r\n                        </div>\r\n\r\n                    </div>\r\n\r\n\r\n\r\n                    <div class=\"underlineBtn\">\r\n                        <button class=\"backBtn btn1 btnMedium btnGrey\" style=\"margin-right: 20px;\">Back</button>\r\n                        <button class=\"nextBtn btn1 btnMedium btnDarkPink\">Save and Continue</button>\r\n                    </div>\r\n\r\n\r\n\r\n                </div>\r\n\r\n\r\n\r\n\r\n\r\n                <div class=\"formArea formArea3\">\r\n\r\n                    <div class=\"notyClass\">You have successfully added a newcourse</div>\r\n\r\n                    <button class=\"endNote\" type=\"submit\" [disabled]='!courseForm.valid'>Create new Course</button>\r\n                </div>\r\n\r\n\r\n            </form>\r\n\r\n        </div>\r\n\r\n    </div>\r\n</div>\r\n<admin-header></admin-header>\r\n\r\n<admin-footer></admin-footer>\r\n"

/***/ }),

/***/ "../../../../../ClientApp/app/admin/courseMgt/add-course/add-course.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var $ = __webpack_require__("../../../../jquery/dist/jquery.js");
var AddCourseComponent = (function () {
    function AddCourseComponent() {
    }
    AddCourseComponent.prototype.ngOnInit = function () {
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
                if (Math.abs(lastScrollTop - st) <= delta)
                    return;
                // If they scrolled down and are past the navbar, add class .header_up.
                // This is necessary so you never see what is "behind" the navbar.
                if (st > lastScrollTop && st > navbarHeight) {
                    // Scroll Down
                    $('header.second').addClass('header_up');
                    $('.pallete1').addClass('pallete_up');
                }
                else {
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
    };
    AddCourseComponent = __decorate([
        core_1.Component({
            template: __webpack_require__("../../../../../ClientApp/app/admin/courseMgt/add-course/add-course.component.html"),
            styles: [__webpack_require__("../../../../../ClientApp/app/admin/courseMgt/add-course/add-course.component.css")]
        })
    ], AddCourseComponent);
    return AddCourseComponent;
}());
exports.AddCourseComponent = AddCourseComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/admin/courseMgt/course-mgt.module.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var router_1 = __webpack_require__("../../../router/esm5/router.js");
var forms_1 = __webpack_require__("../../../forms/esm5/forms.js");
var shared_module_1 = __webpack_require__("../../../../../ClientApp/app/shared/shared.module.ts");
var ngx_pagination_1 = __webpack_require__("../../../../ngx-pagination/dist/ngx-pagination.js");
var ngx_toastr_1 = __webpack_require__("../../../../ngx-toastr/toastr.es5.js");
var add_course_component_1 = __webpack_require__("../../../../../ClientApp/app/admin/courseMgt/add-course/add-course.component.ts");
var dashboard_component_1 = __webpack_require__("../../../../../ClientApp/app/admin/courseMgt/dashboard/dashboard.component.ts");
var CourseManagementModule = (function () {
    function CourseManagementModule() {
    }
    CourseManagementModule = __decorate([
        core_1.NgModule({
            imports: [
                shared_module_1.SharedModule,
                forms_1.ReactiveFormsModule,
                ngx_pagination_1.NgxPaginationModule,
                ngx_toastr_1.ToastrModule.forRoot({
                    progressBar: true,
                    timeOut: 4000,
                    preventDuplicates: true
                }),
                router_1.RouterModule.forChild([
                    { path: 'admin/dashboard', component: dashboard_component_1.AdminDashboardComponent },
                    { path: 'admin/addcourse', component: add_course_component_1.AddCourseComponent }
                ])
            ],
            declarations: [
                dashboard_component_1.AdminDashboardComponent,
                add_course_component_1.AddCourseComponent
            ],
            providers: []
        })
    ], CourseManagementModule);
    return CourseManagementModule;
}());
exports.CourseManagementModule = CourseManagementModule;


/***/ }),

/***/ "../../../../../ClientApp/app/admin/courseMgt/dashboard/dashboard.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".menuTab1 a:nth-child(1) .pageTab1 {\r\n    background: #AB192D;\r\n}\r\n\r\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../ClientApp/app/admin/courseMgt/dashboard/dashboard.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"pallete1\" style=\"position:relative;left:28.5px;\">\r\n    <div class=\"banner1 bkSkyBlue\">\r\n        <i class=\"material-icons menuBtn1\">menu</i>\r\n        <span>Manage Courses</span>\r\n    </div>\r\n    \r\n    <div class=\"content1\">\r\n\r\n        <div class=\"boxHead1\">\r\n            <div class=\"boxHeader1 bDGreenishBlue\">Create</div>\r\n        </div>\r\n\r\n        <div class=\"boxBody1\">\r\n\r\n            <a [routerLink]=\"['/admin/addcourse']\">\r\n                <div class=\"bigMenu1\">\r\n                    <div class=\"smallMenu1\">\r\n                        <i class=\"material-icons\">library_add</i>\r\n                        <span>New Course</span>\r\n                    </div>\r\n                </div>\r\n            </a>\r\n\r\n\r\n            <a href=\"#\">\r\n                <div class=\"bigMenu1\">\r\n                    <div class=\"smallMenu1\">\r\n                        <i class=\"material-icons\">folder_open</i>\r\n                        <span>New Learning Area</span>\r\n                    </div>\r\n                </div>\r\n            </a>\r\n\r\n\r\n\r\n            <a href=\"#\">\r\n                <div class=\"bigMenu1\">\r\n                    <div class=\"smallMenu1\">\r\n                        <i class=\"material-icons\">folder</i>\r\n                        <span>New Learner Group</span>\r\n                    </div>\r\n                </div>\r\n            </a>\r\n\r\n\r\n\r\n        </div>\r\n\r\n\r\n\r\n\r\n\r\n        <div class=\"boxHead1\">\r\n            <div class=\"boxHeader1 bDGreenishBlue\">Assign</div>\r\n        </div>\r\n\r\n        <div class=\"boxBody1\">\r\n\r\n            <a href=\"#\">\r\n                <div class=\"bigMenu1\">\r\n                    <div class=\"smallMenu1\">\r\n                        <i class=\"material-icons\">swap_horiz</i>\r\n                        <span>Assign Course</span>\r\n                    </div>\r\n                </div>\r\n            </a>\r\n\r\n\r\n        </div>\r\n\r\n\r\n\r\n        <div class=\"boxHead1\">\r\n            <div class=\"boxHeader1 bDGreenishBlue\">Manage course contents</div>\r\n        </div>\r\n\r\n        <div class=\"boxBody1\">\r\n\r\n            <a href=\"#\">\r\n                <div class=\"bigMenu1\">\r\n                    <div class=\"smallMenu1\">\r\n                        <i class=\"material-icons\">info</i>\r\n                        <span>Course Reviews</span>\r\n                    </div>\r\n                </div>\r\n            </a>\r\n\r\n\r\n            <a href=\"#\">\r\n                <div class=\"bigMenu1\">\r\n                    <div class=\"smallMenu1\">\r\n                        <i class=\"material-icons\">delete</i>\r\n                        <span>View/Review Courses</span>\r\n                    </div>\r\n                </div>\r\n            </a>\r\n\r\n\r\n\r\n            <a href=\"#\">\r\n                <div class=\"bigMenu1\">\r\n                    <div class=\"smallMenu1\">\r\n                        <i class=\"material-icons\">assignment_returned</i>\r\n                        <span>Third Party Integration</span>\r\n                    </div>\r\n                </div>\r\n            </a>\r\n\r\n\r\n\r\n        </div>\r\n\r\n    </div>\r\n</div>\r\n\r\n<admin-header></admin-header>\r\n<spec-footer></spec-footer>\r\n"

/***/ }),

/***/ "../../../../../ClientApp/app/admin/courseMgt/dashboard/dashboard.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var $ = __webpack_require__("../../../../jquery/dist/jquery.js");
var AdminDashboardComponent = (function () {
    function AdminDashboardComponent() {
    }
    AdminDashboardComponent.prototype.ngOnInit = function () {
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
                if (Math.abs(lastScrollTop - st) <= delta)
                    return;
                // If they scrolled down and are past the navbar, add class .header_up.
                // This is necessary so you never see what is "behind" the navbar.
                if (st > lastScrollTop && st > navbarHeight) {
                    // Scroll Down
                    $('header.second').addClass('header_up');
                    $('.pallete1').addClass('pallete_up');
                }
                else {
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
        });
    };
    AdminDashboardComponent = __decorate([
        core_1.Component({
            template: __webpack_require__("../../../../../ClientApp/app/admin/courseMgt/dashboard/dashboard.component.html"),
            styles: [__webpack_require__("../../../../../ClientApp/app/admin/courseMgt/dashboard/dashboard.component.css")]
        })
    ], AdminDashboardComponent);
    return AdminDashboardComponent;
}());
exports.AdminDashboardComponent = AdminDashboardComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/admin/multiTenant/multi-tenant.module.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var router_1 = __webpack_require__("../../../router/esm5/router.js");
var forms_1 = __webpack_require__("../../../forms/esm5/forms.js");
var shared_module_1 = __webpack_require__("../../../../../ClientApp/app/shared/shared.module.ts");
var ngx_pagination_1 = __webpack_require__("../../../../ngx-pagination/dist/ngx-pagination.js");
var ngx_toastr_1 = __webpack_require__("../../../../ngx-toastr/toastr.es5.js");
var tenant_component_1 = __webpack_require__("../../../../../ClientApp/app/admin/multiTenant/tenant.component.ts");
var MultiTenantModule = (function () {
    function MultiTenantModule() {
    }
    MultiTenantModule = __decorate([
        core_1.NgModule({
            imports: [
                shared_module_1.SharedModule,
                forms_1.ReactiveFormsModule,
                ngx_pagination_1.NgxPaginationModule,
                ngx_toastr_1.ToastrModule.forRoot({
                    progressBar: true,
                    timeOut: 4000,
                    preventDuplicates: true
                }),
                router_1.RouterModule.forChild([
                    { path: 'admin/multi-tenant', component: tenant_component_1.MultiTenantComponent },
                ])
            ],
            declarations: [
                tenant_component_1.MultiTenantComponent
            ],
            providers: []
        })
    ], MultiTenantModule);
    return MultiTenantModule;
}());
exports.MultiTenantModule = MultiTenantModule;


/***/ }),

/***/ "../../../../../ClientApp/app/admin/multiTenant/tenant.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../ClientApp/app/admin/multiTenant/tenant.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"pallete1\">\r\n    <div class=\"banner1 bkSkyBlue\">\r\n        <i class=\"material-icons menuBtn1\">menu</i>\r\n        <span>Multi-Tenant User</span>\r\n    </div>\r\n\r\n\r\n\r\n    <div class=\"content1\">\r\n        <div class=\"boxed1\">\r\n\r\n            <div class=\"boxHead1\">\r\n                <div class=\"boxHeader1 bBYellow\">Quick Actions</div>\r\n            </div>\r\n\r\n            <div class=\"boxBody1\">\r\n\r\n                <div class=\"pad1 col41 bkPurple\">\r\n                    <i class=\"material-icons\">note_add</i>\r\n                    <div class=\"vintage1\">Add Organisation</div>\r\n                </div>\r\n\r\n                <div class=\"pad1 col41 bkPurpleBlue\">\r\n                    <i class=\"material-icons\">group</i>\r\n                    <div class=\"vintage1\">Total Learners</div>\r\n                </div>\r\n\r\n                <div class=\"pad1 col41 bkDarkPink\">\r\n                    <i class=\"material-icons\">account_balance</i>\r\n                    <div class=\"vintage1\">Active Organisations</div>\r\n                </div>\r\n\r\n                <div class=\"pad1 col41 bkPink\">\r\n                    <i class=\"material-icons\">book</i>\r\n                    <div class=\"vintage1\">Active Courses</div>\r\n                </div>\r\n\r\n            </div>\r\n\r\n        </div>\r\n\r\n\r\n        <div class=\"boxed1\">\r\n\r\n            <div class=\"boxHead1\">\r\n                <div class=\"boxHeader1 bBYellow\">Recently added Organisations</div>\r\n            </div>\r\n\r\n            <div class=\"boxBody1\">\r\n\r\n                <table class=\"org1\">\r\n                    <thead>\r\n                        <tr>\r\n                            <th>SN</th>\r\n                            <th>Names</th>\r\n                            <th>Date Created</th>\r\n                            <th>Action</th>\r\n                            <th></th>\r\n                        </tr>\r\n                    </thead>\r\n\r\n                    <tbody>\r\n                        <tr>\r\n                            <td>1</td>\r\n                            <td>Organisation 1</td>\r\n                            <td>April 25, 2017</td>\r\n                            <td>\r\n                                <a href=\"\"><i class=\"material-icons\">edit</i><span>Edit</span></a>\r\n                                <a href=\"\"><i class=\"material-icons\">visibility</i><span>View</span></a>\r\n                                <a href=\"\"><i class=\"material-icons\">assignment</i><span>Generate Report</span></a>\r\n                            </td>\r\n                            <td>\r\n                                <div class=\"btn1 btnGreen\">Activate</div>\r\n                            </td>\r\n\r\n\r\n                        <tr>\r\n                            <td>2</td>\r\n                            <td>Organisation 1</td>\r\n                            <td>April 25, 2017</td>\r\n                            <td>\r\n                                <a href=\"\"><i class=\"material-icons\">edit</i><span>Edit</span></a>\r\n                                <a href=\"\"><i class=\"material-icons\">visibility</i><span>View</span></a>\r\n                                <a href=\"\"><i class=\"material-icons\">assignment</i><span>Generate Report</span></a>\r\n                            </td>\r\n                            <td>\r\n                                <div class=\"btn1 btnGreen\">Activate</div>\r\n                            </td>\r\n                        </tr>\r\n\r\n\r\n\r\n                        <tr>\r\n                            <td>3</td>\r\n                            <td>Organisation 1</td>\r\n                            <td>April 25, 2017</td>\r\n                            <td>\r\n                                <a href=\"\"><i class=\"material-icons\">edit</i><span>Edit</span></a>\r\n                                <a href=\"\"><i class=\"material-icons\">visibility</i><span>View</span></a>\r\n                                <a href=\"\"><i class=\"material-icons\">assignment</i><span>Generate Report</span></a>\r\n                            </td>\r\n                            <td>\r\n                                <div class=\"btn1 btnRed\">Deactivate</div>\r\n                            </td>\r\n                        </tr>\r\n\r\n\r\n\r\n                        <tr>\r\n                            <td>4</td>\r\n                            <td>Organisation 1</td>\r\n                            <td>April 25, 2017</td>\r\n                            <td>\r\n                                <a href=\"\"><i class=\"material-icons\">edit</i><span>Edit</span></a>\r\n                                <a href=\"\"><i class=\"material-icons\">visibility</i><span>View</span></a>\r\n                                <a href=\"\"><i class=\"material-icons\">assignment</i><span>Generate Report</span></a>\r\n                            </td>\r\n                            <td>\r\n                                <div class=\"btn1 btnRed\">Deactivate</div>\r\n                            </td>\r\n                        </tr>\r\n\r\n\r\n\r\n                        <tr>\r\n                            <td>5</td>\r\n                            <td>Organisation 1</td>\r\n                            <td>April 25, 2017</td>\r\n                            <td>\r\n                                <a href=\"\"><i class=\"material-icons\">edit</i><span>Edit</span></a>\r\n                                <a href=\"\"><i class=\"material-icons\">visibility</i><span>View</span></a>\r\n                                <a href=\"\"><i class=\"material-icons\">assignment</i><span>Generate Report</span></a>\r\n                            </td>\r\n                            <td>\r\n                                <div class=\"btn1 btnGreen\">Activate</div>\r\n                            </td>\r\n                        </tr>\r\n\r\n                    </tbody>\r\n                </table>\r\n\r\n            </div>\r\n\r\n        </div>\r\n        </div>\r\n\r\n    </div>\r\n    <admin-header></admin-header>\r\n\r\n    <admin-footer></admin-footer>\r\n"

/***/ }),

/***/ "../../../../../ClientApp/app/admin/multiTenant/tenant.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var $ = __webpack_require__("../../../../jquery/dist/jquery.js");
var MultiTenantComponent = (function () {
    function MultiTenantComponent() {
    }
    MultiTenantComponent.prototype.ngOnInit = function () {
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
                if (Math.abs(lastScrollTop - st) <= delta)
                    return;
                // If they scrolled down and are past the navbar, add class .header_up.
                // This is necessary so you never see what is "behind" the navbar.
                if (st > lastScrollTop && st > navbarHeight) {
                    // Scroll Down
                    $('header.second').addClass('header_up');
                    $('.pallete1').addClass('pallete_up');
                }
                else {
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
        });
    };
    MultiTenantComponent = __decorate([
        core_1.Component({
            template: __webpack_require__("../../../../../ClientApp/app/admin/multiTenant/tenant.component.html"),
            styles: [__webpack_require__("../../../../../ClientApp/app/admin/multiTenant/tenant.component.css")]
        })
    ], MultiTenantComponent);
    return MultiTenantComponent;
}());
exports.MultiTenantComponent = MultiTenantComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/app.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../ClientApp/app/app.component.html":
/***/ (function(module, exports) {

module.exports = "\r\n<router-outlet></router-outlet>\r\n"

/***/ }),

/***/ "../../../../../ClientApp/app/app.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var router_1 = __webpack_require__("../../../router/esm5/router.js");
var ngx_progressbar_1 = __webpack_require__("../../../../ngx-progressbar/modules/ngx-progressbar.es5.js");
var auth_service_1 = __webpack_require__("../../../../../ClientApp/app/service/auth.service.ts");
var AppComponent = (function () {
    function AppComponent(router, authService, ngProgress) {
        var _this = this;
        this.router = router;
        this.authService = authService;
        this.ngProgress = ngProgress;
        this.title = 'app';
        router.events.subscribe(function (e) {
            if (e instanceof router_1.NavigationEnd) {
                _this.location = e.url;
                // console.log("current url", e.url)
            }
        });
    }
    AppComponent.prototype.ngOnInit = function () {
        this.ngProgress.start();
    };
    AppComponent = __decorate([
        core_1.Component({
            selector: 'lms-app',
            template: __webpack_require__("../../../../../ClientApp/app/app.component.html"),
            styles: [__webpack_require__("../../../../../ClientApp/app/app.component.css")]
        }),
        __metadata("design:paramtypes", [router_1.Router, auth_service_1.AuthService, ngx_progressbar_1.NgProgress])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/app.module.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var platform_browser_1 = __webpack_require__("../../../platform-browser/esm5/platform-browser.js");
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var router_1 = __webpack_require__("../../../router/esm5/router.js");
var common_1 = __webpack_require__("../../../common/esm5/common.js");
var app_routing_1 = __webpack_require__("../../../../../ClientApp/app/app.routing.ts");
var app_component_1 = __webpack_require__("../../../../../ClientApp/app/app.component.ts");
var http_1 = __webpack_require__("../../../http/esm5/http.js");
var ngx_progressbar_1 = __webpack_require__("../../../../ngx-progressbar/modules/ngx-progressbar.es5.js");
var multi_tenant_module_1 = __webpack_require__("../../../../../ClientApp/app/admin/multiTenant/multi-tenant.module.ts");
var course_mgt_module_1 = __webpack_require__("../../../../../ClientApp/app/admin/courseMgt/course-mgt.module.ts");
var cetificate_component_1 = __webpack_require__("../../../../../ClientApp/app/employee/certificate/cetificate.component.ts");
var support_componet_1 = __webpack_require__("../../../../../ClientApp/app/employee/support/support.componet.ts");
var course_module_1 = __webpack_require__("../../../../../ClientApp/app/employee/course/course.module.ts");
var shared_module_1 = __webpack_require__("../../../../../ClientApp/app/shared/shared.module.ts");
var user_module_1 = __webpack_require__("../../../../../ClientApp/app/employee/user/user.module.ts");
var AppModule = (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            declarations: [
                app_component_1.AppComponent,
                cetificate_component_1.CertificateComponent,
                support_componet_1.SupportComponent,
            ],
            imports: [
                platform_browser_1.BrowserModule,
                router_1.RouterModule,
                http_1.HttpModule,
                app_routing_1.routing,
                course_module_1.CourseModule,
                shared_module_1.SharedModule,
                user_module_1.UserModule,
                ngx_progressbar_1.NgProgressModule,
                multi_tenant_module_1.MultiTenantModule,
                course_mgt_module_1.CourseManagementModule
            ],
            providers: [{ provide: common_1.APP_BASE_HREF, useValue: '/' }],
            bootstrap: [app_component_1.AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;


/***/ }),

/***/ "../../../../../ClientApp/app/app.routing.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = __webpack_require__("../../../router/esm5/router.js");
var cetificate_component_1 = __webpack_require__("../../../../../ClientApp/app/employee/certificate/cetificate.component.ts");
var support_componet_1 = __webpack_require__("../../../../../ClientApp/app/employee/support/support.componet.ts");
var appRoutes = [
    { path: 'support', component: support_componet_1.SupportComponent },
    { path: 'certificate', component: cetificate_component_1.CertificateComponent },
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: '**', redirectTo: 'home', pathMatch: 'full' }
];
exports.routing = router_1.RouterModule.forRoot(appRoutes, { useHash: true });


/***/ }),

/***/ "../../../../../ClientApp/app/authentication/login/login.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "@media(max-width: 500px) {\r\n    header img {\r\n        display: block;\r\n    }\r\n\r\n    header {\r\n        padding-left: 25%;\r\n    }\r\n}\r\n\r\n.menuTab {\r\n    display: none;\r\n}\r\n\r\nheader{\r\n    display:none;\r\n}\r\n.col2 {\r\n    display: none;\r\n}\r\nfooter{\r\n    display:none;\r\n}", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../ClientApp/app/authentication/login/login.component.html":
/***/ (function(module, exports) {

module.exports = "<header>\r\n    <img src=\"/Content/img/retro.png\" alt=\"\">\r\n    <img src=\"/Content/img/logo.png\" class=\"marginImg\" alt=\"\">\r\n\r\n</header>\r\n\r\n\r\n<div class=\"live\">\r\n\r\n    <div class=\"double\">\r\n        <div class=\"welcome\">\r\n            <div class=\"up\">Welcome to <b>Sterling Bank</b></div>\r\n            <div class=\"down\">Learning Management System</div>\r\n        </div>\r\n    </div>\r\n    <div class=\"double2\">\r\n        <div class=\"loginBox\">\r\n            <div class=\"loginTop\">\r\n                <div class=\"nick\">\r\n                    <i class=\"material-icons\">person</i><span>Sign in</span>\r\n                </div>\r\n            </div>\r\n            <form novalidate class=\"loginForm\" [formGroup]=\"loginFrm\" #formDir=\"ngForm\" (ngSubmit)=\"login(loginFrm.value)\">\r\n                <label for=\"\">Email address</label>\r\n                <input type=\"email\" formControlName=\"email\" />\r\n                <span class=\"help-block\" *ngIf=\"(loginFrm.get('email').touched||loginFrm.get('email').dirty)&&loginFrm.get('email').errors\">\r\n                    <span class=\"text-danger\" *ngIf=\"loginFrm.hasError('required', 'email') \">\r\n                        Email Address is required.\r\n                    </span>\r\n                    <span class=\"text-danger\" *ngIf=\"loginFrm.hasError('pattern','email')\">\r\n                        Please enter a valid email address\r\n                    </span>\r\n\r\n                </span>\r\n                \r\n                <!--<span class=\"help-block\" *ngIf=\"displayMessage.email\">\r\n                  {{displayMessage.email}}\r\n                </span>-->\r\n\r\n                <label for=\"\">Password</label>\r\n                <input type=\"password\" formControlName=\"password\" />\r\n                =\"\r\n                <span class=\"help-block\" *ngIf=\"(loginFrm.get('password').touched||loginFrm.get('password').dirty)&&loginFrm.get('password').errors\">\r\n                    <span class=\"text-danger\" *ngIf=\"loginFrm.hasError('required', 'password') \">\r\n                        Password is required.\r\n                    </span>\r\n\r\n                </span>\r\n                <h4><input type=\"checkbox\" name=\"\" id=\"\" forControlName=\"remember\"> &emsp;Remember me</h4>\r\n\r\n                <input type=\"submit\" value=\"LOG IN\" [disabled]='!loginFrm.valid'>\r\n\r\n                <h5><a href=\"#\">Forgot your Password?</a></h5>\r\n            </form>\r\n            \r\n           \r\n            <!--<br />Dirty:{{loginFrm.dirty}}\r\n            <br />Touched:{{loginFrm.touched}}\r\n            <br />Valid:{{loginFrm.valid}}\r\n            <br />Value:{{loginFrm.value | json}}-->\r\n\r\n            <!--<form novalidate class=\"loginForm\" [formGroup]=\"loginFrm\" (ngSubmit)=\"login(loginFrm.value)\">\r\n        <div [ngClass]=\"{'has-error':displayMessage.email}\">\r\n            <label for=\"\">Email address</label>\r\n            <input type=\"email\" formControlName=\"email\" style=\"width:100%\" />\r\n            <span class=\"help-block\" *ngIf=\"displayMessage.email\">\r\n                {{displayMessage.email}}\r\n            </span>\r\n        </div>\r\n\r\n        <div [ngClass]=\"{'has-error': displayMessage.password}\">\r\n            <label for=\"\">Password</label>\r\n            <input type=\"password\" formControlName=\"password\" style=\"width:100%\"/>\r\n            <span class=\"help-block\" *ngIf=\"displayMessage.password\">\r\n                {{displayMessage.password}}\r\n            </span>\r\n        </div>\r\n\r\n\r\n        <h4><input type=\"checkbox\" name=\"\" id=\"\" forControlName=\"remember\"> &emsp;Remember me</h4>\r\n\r\n        <input type=\"submit\" value=\"LOG IN\" [disabled]='!loginFrm.valid'>\r\n\r\n        <h5><a href=\"#\">Forgot your Password?</a></h5>\r\n    </form>-->\r\n        </div>\r\n    </div>\r\n\r\n\r\n</div>"

/***/ }),

/***/ "../../../../../ClientApp/app/authentication/login/login.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var forms_1 = __webpack_require__("../../../forms/esm5/forms.js");
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var router_1 = __webpack_require__("../../../router/esm5/router.js");
__webpack_require__("../../../../rxjs/_esm5/add/operator/debounceTime.js");
var ngx_toastr_1 = __webpack_require__("../../../../ngx-toastr/toastr.es5.js");
var auth_service_1 = __webpack_require__("../../../../../ClientApp/app/service/auth.service.ts");
var LoginComponent = (function () {
    //this.genericValidator = new GenericValidator(this.validationMessages);
    function LoginComponent(authService, router, fb, toastr) {
        //Define all valiations for the form.
        this.authService = authService;
        this.router = router;
        this.fb = fb;
        this.toastr = toastr;
        this.displayMessage = {};
        this.email = '';
        this.password = '';
        this.validationMessages = {
            required: 'Email Address is required',
            pattern: 'Please enter a valid email address'
        };
    }
    LoginComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.loginFrm = this.fb.group({
            email: ['', [forms_1.Validators.required, forms_1.Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+')]],
            password: ['', forms_1.Validators.required],
            remember: ''
        });
        var emailControl = this.loginFrm.get('email');
        emailControl.valueChanges.debounceTime(1000).subscribe(function (value) { return _this.setMessage(emailControl); });
    };
    LoginComponent.prototype.login = function (post) {
        this.email = post.email;
        this.password = post.password;
        this.authService.login(this.email, this.password);
        if (this.authService.redirectUrl) {
            this.router.navigateByUrl(this.authService.redirectUrl);
        }
        this.router.navigate(['/home']);
        this.toastr.success("Successfully logged in as " + this.email);
    };
    LoginComponent.prototype.setMessage = function (c) {
        var _this = this;
        this.emailMessage = '';
        if ((c.touched || c.dirty) && c.errors) {
            this.emailMessage = Object.keys(c.errors).map(function (key) { return _this.validationMessages[key]; }).join(' ');
        }
    };
    Object.defineProperty(LoginComponent.prototype, "Email", {
        get: function () { return this.loginFrm.get('emial'); },
        enumerable: true,
        configurable: true
    });
    ;
    Object.defineProperty(LoginComponent.prototype, "Password", {
        get: function () { return this.loginFrm.get('password'); },
        enumerable: true,
        configurable: true
    });
    ;
    LoginComponent = __decorate([
        core_1.Component({
            template: __webpack_require__("../../../../../ClientApp/app/authentication/login/login.component.html"),
            styles: [__webpack_require__("../../../../../ClientApp/app/authentication/login/login.component.css")]
        }),
        __metadata("design:paramtypes", [auth_service_1.AuthService, router_1.Router, forms_1.FormBuilder, ngx_toastr_1.ToastrService])
    ], LoginComponent);
    return LoginComponent;
}());
exports.LoginComponent = LoginComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/employee/certificate/certificate.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".searchArea {\r\n    background: #6f6caa;\r\n}\r\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../ClientApp/app/employee/certificate/certificate.component.html":
/***/ (function(module, exports) {

module.exports = "<emp-header></emp-header>\r\n<div class=\"wrapper\">\r\n\r\n\r\n    <div class=\"breadcrum\" style=\"background-image: url('/Content/img/certificate (1).jpg')\">\r\n        <div class=\"col8\">\r\n\r\n        </div>\r\n\r\n        <div class=\"col4\">\r\n            <div class=\"headcrum\">Customer Service Representative</div>\r\n            <div class=\"bodycrum\"><i class=\"material-icons\">room</i> <i>Marina, Lagos</i></div>\r\n            <div class=\"bodycrum\"><i style=\"font-size: 12px;margin-top: 3px;\">Staff ID: </i> <b>001558</b></div>\r\n        </div>\r\n    </div>\r\n\r\n\r\n    <div class=\"searchArea\">\r\n        <div class=\"searchbox\">\r\n            <input type=\"text\" placeholder=\"Search Courses\" />\r\n            <i class=\"material-icons\">search</i>\r\n        </div>\r\n    </div>\r\n\r\n\r\n    <div class=\"content\">\r\n        <div class=\"col9\">\r\n\r\n\r\n            <div class=\"boxed\">\r\n                <div class=\"long\">\r\n                    <div class=\"line borderBottomBlue\">\r\n                        My Completed Courses\r\n                    </div>\r\n                </div>\r\n\r\n\r\n                <div class=\"change\">\r\n                    <span>SORT BY</span>\r\n                    <select>\r\n                        <option value=\"\">most recent</option>\r\n                        <option value=\"\">course length</option>\r\n                        <option value=\"\">certificate type</option>\r\n                    </select>\r\n                </div>\r\n\r\n            </div>\r\n\r\n\r\n            <div class=\"hider\">\r\n                <div class=\"hiderHead\">\r\n                    <i class=\"material-icons\">keyboard_arrow_down</i>\r\n                    Innovation: The World's Greatest\r\n                </div>\r\n                <div class=\"hiderBody\">\r\n                    <div class=\"halves\">\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Started: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Completed: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Original Due Date: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"halves\">\r\n                        <button class=\"btnBlack btnHerd\">View/Generate Ceritificate</button>\r\n                        <sup>NB: Certificate will be generated in pdf</sup>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n\r\n            <div class=\"hider\">\r\n                <div class=\"hiderHead\">\r\n                    <i class=\"material-icons\">keyboard_arrow_down</i>\r\n                    Innovation: The World's Greatest\r\n                </div>\r\n                <div class=\"hiderBody\">\r\n                    <div class=\"halves\">\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Started: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Completed: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Original Due Date: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"halves\">\r\n                        <button class=\"btnBlack btnHerd\">View/Generate Ceritificate</button>\r\n                        <sup>NB: Certificate will be generated in pdf</sup>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n\r\n\r\n            <div class=\"hider\">\r\n                <div class=\"hiderHead\">\r\n                    <i class=\"material-icons\">keyboard_arrow_down</i>\r\n                    Innovation: The World's Greatest\r\n                </div>\r\n                <div class=\"hiderBody\">\r\n                    <div class=\"halves\">\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Started: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Completed: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Original Due Date: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"halves\">\r\n                        <button class=\"btnBlack btnHerd\">View/Generate Ceritificate</button>\r\n                        <sup>NB: Certificate will be generated in pdf</sup>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n\r\n\r\n            <div class=\"hider\">\r\n                <div class=\"hiderHead\">\r\n                    <i class=\"material-icons\">keyboard_arrow_down</i>\r\n                    Innovation: The World's Greatest\r\n                </div>\r\n                <div class=\"hiderBody\">\r\n                    <div class=\"halves\">\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Started: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Completed: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Original Due Date: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"halves\">\r\n                        <button class=\"btnBlack btnHerd\">View/Generate Ceritificate</button>\r\n                        <sup>NB: Certificate will be generated in pdf</sup>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n\r\n\r\n            <div class=\"hider\">\r\n                <div class=\"hiderHead\">\r\n                    <i class=\"material-icons\">keyboard_arrow_down</i>\r\n                    Innovation: The World's Greatest\r\n                </div>\r\n                <div class=\"hiderBody\">\r\n                    <div class=\"halves\">\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Started: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Completed: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Original Due Date: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"halves\">\r\n                        <button class=\"btnBlack btnHerd\">View/Generate Ceritificate</button>\r\n                        <sup>NB: Certificate will be generated in pdf</sup>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n\r\n\r\n            <div class=\"hider\">\r\n                <div class=\"hiderHead\">\r\n                    <i class=\"material-icons\">keyboard_arrow_down</i>\r\n                    Innovation: The World's Greatest\r\n                </div>\r\n                <div class=\"hiderBody\">\r\n                    <div class=\"halves\">\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Started: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Completed: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Original Due Date: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"halves\">\r\n                        <button class=\"btnBlack btnHerd\">View/Generate Ceritificate</button>\r\n                        <sup>NB: Certificate will be generated in pdf</sup>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n\r\n\r\n            <div class=\"hider\">\r\n                <div class=\"hiderHead\">\r\n                    <i class=\"material-icons\">keyboard_arrow_down</i>\r\n                    Innovation: The World's Greatest\r\n                </div>\r\n                <div class=\"hiderBody\">\r\n                    <div class=\"halves\">\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Started: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Completed: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n\r\n                        <div class=\"slab\">\r\n                            <div class=\"slabL\">Original Due Date: </div>\r\n                            <div class=\"slabR\">08 May 17</div>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"halves\">\r\n                        <button class=\"btnBlack btnHerd\">View/Generate Ceritificate</button>\r\n                        <sup>NB: Certificate will be generated in pdf</sup>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"col2\">\r\n\r\n\r\n            <div class=\"pod\">\r\n                <div class=\"line borderBottomGrey\">\r\n                    Coming Up  <button class=\"btnBlue\"><i class=\"material-icons\">event</i> <span>View Calendar</span></button>\r\n                </div>\r\n\r\n                <div class=\"podBlock\">\r\n                    <div class=\"podItem\">\r\n                        <div class=\"coupon\"><i class=\"material-icons blue\">event</i></div>\r\n                        <div class=\"list\">\r\n                            <span class=\"blue\">General Staff Shop</span>\r\n                            <i>Jan 10, 2018</i>\r\n                        </div>\r\n                    </div>\r\n\r\n                    <div class=\"podItem\">\r\n                        <div class=\"coupon\"><i class=\"material-icons blue\">event</i></div>\r\n                        <div class=\"list\">\r\n                            <span class=\"blue\">Live Podcast</span>\r\n                            <i>Jan 10, 2018</i>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n\r\n            </div>\r\n\r\n\r\n\r\n            <div class=\"space\"></div>\r\n            <div class=\"pod\">\r\n                <div class=\"line borderBottomGrey\">Quick Notification</div>\r\n\r\n\r\n                <div class=\"podBlock\">\r\n                    <div class=\"podItem\">\r\n                        <div class=\"coupon\"><span class=\"redBackground\">05</span></div>\r\n                        <div class=\"list\">\r\n                            <span>Courses are overdue</span>\r\n                        </div>\r\n                    </div>\r\n\r\n                    <div class=\"podItem\">\r\n                        <div class=\"coupon\"><span class=\"yellowBackground\">15</span></div>\r\n                        <div class=\"list\">\r\n                            <span>Courses are going to expire in the next 30 days.</span>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n\r\n            <div class=\"space\"></div>\r\n\r\n\r\n            <div class=\"pod\">\r\n                <div class=\"line borderBottomGrey\">Awards/Certificates</div>\r\n\r\n\r\n                <div class=\"podBlock\">\r\n                    <div class=\"podItem\">\r\n                        <div class=\"half\">\r\n                            <div class=\"blue big\">CERTIFICATES </div><b>(3)</b>\r\n                            <p>Your latest certificates</p>\r\n                            <button class=\"btnBlue\">View all</button>\r\n                        </div>\r\n\r\n                        <div class=\"half\">\r\n                            <img src=\"/Content/img/c1.jpg\" alt=\"\">\r\n                            <img src=\"/Content/img/c2.jpg\" alt=\"\">\r\n                            <img src=\"/Content/img/c3.jpg\" alt=\"\">\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<emp-footer></emp-footer>"

/***/ }),

/***/ "../../../../../ClientApp/app/employee/certificate/cetificate.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var $ = __webpack_require__("../../../../jquery/dist/jquery.js");
var CertificateComponent = (function () {
    function CertificateComponent() {
    }
    CertificateComponent.prototype.ngOnInit = function () {
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
                }
                else {
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
        $('.hider').click(function () {
            $('.hiderBody').hide();
            $('.hiderHead').css("background", "#aaa");
            $('.hiderHead i').html("keyboard_arrow_down");
            $('.hiderHead i', this).html("keyboard_arrow_up");
            $('.hiderHead', this).css("background", "#4286f4");
            $('.hiderBody', this).fadeIn();
        });
    };
    CertificateComponent = __decorate([
        core_1.Component({
            template: __webpack_require__("../../../../../ClientApp/app/employee/certificate/certificate.component.html"),
            styles: [__webpack_require__("../../../../../ClientApp/app/employee/certificate/certificate.component.css")]
        })
    ], CertificateComponent);
    return CertificateComponent;
}());
exports.CertificateComponent = CertificateComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/course-catalogue/course-catalogue.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".searchArea {\r\n    background: #993d61;\r\n}\r\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/course-catalogue/course-catalogue.component.html":
/***/ (function(module, exports) {

module.exports = "<emp-header></emp-header>\r\n<div class=\"wrapper\">\r\n    <div class=\"breadcrum\" style=\"background-image: url('/Content/img/course_catalog (1).jpg')\">\r\n        <div class=\"col8\">\r\n            <div class=\"headcrum\"></div>\r\n            <div class=\"bodycrum\"></div>\r\n        </div>\r\n\r\n        <div class=\"col4\">\r\n            <div class=\"headcrum\">Customer Service Representative</div>\r\n            <div class=\"bodycrum\">\r\n                <i class=\"material-icons\">room</i>  <i>Marina, Lagos</i>\r\n            </div>\r\n            <div class=\"bodycrum\">\r\n                <i style=\"font-size: 12px;margin-top: 3px;\">Staff ID: </i>  <b>001558</b>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"searchArea\" style=\"background: #993d61;\">\r\n        <div class=\"searchbox\">\r\n            <input type=\"text\" placeholder=\"Search Courses\" /> <i class=\"material-icons\">search</i>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"content\">\r\n        <div class=\"col12\">\r\n            <div class=\"long\">\r\n                <div class=\"line borderBottomPink\">Course Catalog</div>\r\n            </div>\r\n\r\n            <div class=\"change\">\r\n                <div class=\"holder\">\r\n                    <span>SORT BY</span>\r\n                    <select>\r\n                        <option value=\"\">All departments</option>\r\n                        <option value=\"\">course length</option>\r\n                        <option value=\"\">certificate type</option>\r\n                    </select>\r\n                </div>\r\n\r\n                <div class=\"holder\">\r\n                    <span> &emsp; OR</span>\r\n                    <select>\r\n                        <option value=\"\">Learning areas</option>\r\n                        <option value=\"\">course length</option>\r\n                        <option value=\"\">certificate type</option>\r\n                    </select>\r\n                </div>\r\n            </div>\r\n            <div class=\"col-md-6 col-md-offset-3 indloading\" role=\"alert\" *ngIf=\"indLoading\"><img src=\"../../Content/img/loading.gif\" width=\"32\" height=\"32\" /> Loading...</div>\r\n            <!-- Course Item -->\r\n            <div class=\"col44 courseItem\" *ngFor=\"let item of courses | paginate:{itemsPerPage:4, currentPage:p}\">\r\n                <a [routerLink]=\"['/course',item.Id]\" [queryParams]=\"{}\">\r\n                    <div class=\"courseImg\" [style.background-image]=\"'url('+item.ImageUrl+')'\"><i class=\"material-icons\">visibility</i></div>\r\n                </a>\r\n                <div class=\"text\">\r\n                    <a [routerLink]=\"['/course',item.Id]\" [queryParams]=\"{}\"><h5 class=\"pink\">{{item.CourseName|slice:0:50}}</h5></a>\r\n                    <p>\r\n                        {{item.CourseDescription|slice:0:180}}\r\n                        <br>\r\n                        <br> <i>Available till: </i>  <b> &nbsp; {{item.DueDate| date:'dd-MM-yyyy'}}</b>\r\n                    </p>\r\n                </div>\r\n                <div class=\"stars\">\r\n                    <i class=\"material-icons unmark\">star</i>\r\n                    <i class=\"material-icons unmark\">star</i>\r\n                    <i class=\"material-icons mark\">star_half</i>\r\n                    <i class=\"material-icons mark\">star</i>\r\n                    <i class=\"material-icons mark\">star</i>\r\n                </div>\r\n\r\n            </div>\r\n            <!-- End of Course Item -->\r\n            <!-- Course Item -->\r\n            <div class=\"col-md-6 col-md-offset-3\" style=\"margin-top:40px\">\r\n                <pagination-controls (pageChange)=\"p=$event\"></pagination-controls>\r\n            </div>\r\n        </div>\r\n        \r\n    </div>\r\n</div>\r\n<emp-footer></emp-footer>\r\n"

/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/course-catalogue/course-catalogue.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var $ = __webpack_require__("../../../../jquery/dist/jquery.js");
var course_service_1 = __webpack_require__("../../../../../ClientApp/app/service/course.service.ts");
var CourseCatalogueComponent = (function () {
    function CourseCatalogueComponent(courseService) {
        this.courseService = courseService;
        this.indLoading = false;
        this.p = 1;
        this.courses = courseService.courses;
    }
    CourseCatalogueComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.indLoading = true;
        this.courseService.getAllCourses()
            .subscribe(function (c) {
            _this.courses = c,
                _this.indLoading = false;
        }, function (error) { return _this.errorMessage = error; });
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
                }
                else {
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
    };
    CourseCatalogueComponent = __decorate([
        core_1.Component({
            template: __webpack_require__("../../../../../ClientApp/app/employee/course/course-catalogue/course-catalogue.component.html"),
            styles: [__webpack_require__("../../../../../ClientApp/app/employee/course/course-catalogue/course-catalogue.component.css")]
        }),
        __metadata("design:paramtypes", [course_service_1.CourseService])
    ], CourseCatalogueComponent);
    return CourseCatalogueComponent;
}());
exports.CourseCatalogueComponent = CourseCatalogueComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/course-detail/course-detail.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".searchArea {\r\n    background: #da6086;\r\n}\r\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/course-detail/course-detail.component.html":
/***/ (function(module, exports) {

module.exports = "<emp-header></emp-header>\r\n<div class=\"wrapper\">\r\n\r\n\r\n    <div class=\"breadcrum\" style=\"background-image: url('/Content/img/details (1).jpg');\">\r\n        <div class=\"col8\">\r\n            <div class=\"headcrum\">{{course.CourseName}}</div>\r\n            <div class=\"bodycrum\">{{course.CourseDescription|slice:0:100}}</div>\r\n        </div>\r\n\r\n        <div class=\"col4\">\r\n            <div class=\"headcrum\">Customer Service Representative</div>\r\n            <div class=\"bodycrum\"><i class=\"material-icons\">room</i> <i>Marina, Lagos</i></div>\r\n            <div class=\"bodycrum\"><i style=\"font-size: 12px;margin-top: 3px;\">Staff ID: </i> <b>001558</b></div>\r\n        </div>\r\n    </div>\r\n\r\n\r\n    <div class=\"searchArea\">\r\n        <div class=\"searchbox\">\r\n            <input type=\"text\" placeholder=\"Search Courses\" />\r\n            <i class=\"material-icons\">search</i>\r\n        </div>\r\n    </div>\r\n\r\n\r\n    <div class=\"content\">\r\n        <div class=\"col9\">\r\n\r\n\r\n            <div class=\"tabs\">\r\n                <ul class=\"tab-links\">\r\n                    <li class=\"active\"><a href=\"#tab1\">Overview</a></li>\r\n                    <li><a href=\"#tab2\" id=\"temp\">Instructional Objective</a></li>\r\n                    <li><a href=\"#tab3\">Curriculum</a></li>\r\n                </ul>\r\n\r\n                <div class=\"tab-content\">\r\n                    <div id=\"tab1\" class=\"tab active\">\r\n                        <div class=\"col33\">\r\n                            <div class=\"coupon\"><i class=\"material-icons\">hourglass_empty</i></div>\r\n                            <div class=\"list\">\r\n                                <span>Duration</span>\r\n                                <p>{{course.TimeDuration}}</p>\r\n                            </div>\r\n                        </div>\r\n                        <div class=\"col33\">\r\n                            <div class=\"coupon\"><i class=\"material-icons\">restore</i></div>\r\n                            <div class=\"list\">\r\n                                <span>3 hours</span>\r\n                                <p>per week</p>\r\n                            </div>\r\n                        </div>\r\n                        <div class=\"col33\">\r\n                            <div class=\"coupon\"><i class=\"material-icons\">event</i></div>\r\n                            <div class=\"list\">\r\n                                <span>Due date:</span>\r\n                                <p>{{course.DueDate | date:'dd-MM-yyyy'}}</p>\r\n                            </div>\r\n                        </div>\r\n\r\n                        <button class=\"btnLarge btnRed\" (click)=\"startLearning()\"><a [routerLink]=\"['/course',course.Id,'module']\">Start Learning</a></button>\r\n\r\n\r\n                        <div class=\"case\">\r\n                            <div class=\"caseHead\">About This course</div>\r\n                            <div class=\"caseBody\">\r\n                                {{course.CourseDescription}}\r\n                            </div>\r\n\r\n                            <div class=\"caseHead\">Requirements</div>\r\n                            <div class=\"caseBody\">\r\n                                <ul>\r\n                                    <li>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt </li>\r\n                                    <li>Quisque sagittis purus sit amet volutpat consequat mauris nunc.</li>\r\n                                </ul>\r\n                            </div>\r\n\r\n\r\n                        </div>\r\n                    </div>\r\n\r\n                    <div id=\"tab2\" class=\"tab\">\r\n                        <div class=\"case\">\r\n                            <div class=\"caseHeader blue center\">What will you achieve?</div>\r\n                            <p class=\"center\">By the end of the course: you'd be able to:</p>\r\n                            <div class=\"caseBody\">\r\n                                <ul class=\"pin\">\r\n                                    <li>\r\n                                        <i class=\"material-icons\">check_box</i>\r\n                                        <span>Investigate how the aggregate deemed sales price and adjusted Gross up basis are determined. </span>\r\n                                    </li>\r\n                                    <li>\r\n                                        <i class=\"material-icons\">check_box</i>\r\n                                        <span>Identify the mechanism of an IRC 338(h)10 transaction.</span>\r\n                                    </li>\r\n                                    <li>\r\n                                        <i class=\"material-icons\">check_box</i>\r\n                                        <span>Investigate how the aggregate deemed sales price and adjusted Gross up basis are determined. </span>\r\n                                    </li>\r\n                                    <li>\r\n                                        <i class=\"material-icons\">check_box</i>\r\n                                        <span>Identify the mechanism of an IRC 338(h)10 transaction.</span>\r\n                                    </li>\r\n                                    <li>\r\n                                        <i class=\"material-icons\">check_box</i>\r\n                                        <span>Investigate how the aggregate deemed sales price and adjusted Gross up basis are determined. </span>\r\n                                    </li>\r\n                                    <li>\r\n                                        <i class=\"material-icons\">check_box</i>\r\n                                        <span>Identify the mechanism of an IRC 338(h)10 transaction.</span>\r\n                                    </li>\r\n                                    <li>\r\n                                        <i class=\"material-icons\">check_box</i>\r\n                                        <span>Investigate how the aggregate deemed sales price and adjusted Gross up basis are determined. </span>\r\n                                    </li>\r\n                                    <li>\r\n                                        <i class=\"material-icons\">check_box</i>\r\n                                        <span>Identify the mechanism of an IRC 338(h)10 transaction.</span>\r\n                                    </li>\r\n                                </ul>\r\n                            </div>\r\n                        </div>\r\n\r\n\r\n                        <button class=\"btnLarge btnRed\" (click)=\"startLearning()\"><a [routerLink]=\"['/course',course.Id,'module']\">Start Learning</a></button>\r\n\r\n                    </div>\r\n\r\n                    <div id=\"tab3\" class=\"tab\">\r\n                        <div class=\"case\">\r\n                            <div class=\"caseHeader blue center\">Course Curriculum</div>\r\n                            <p class=\"center\">What topics will you cover?</p>\r\n                            <div class=\"caseBody\">\r\n                                <div class=\"curri\" *ngFor=\"let module of course.Modules; let x=index\">\r\n                                    <div class=\"curriBody\">Module {{x+1}}: &emsp;{{module.ModuleName}}</div>\r\n                                    \r\n                                        <div class=\"curriHead\" *ngFor=\"let topic of module.Topics\">\r\n                                            <ul>\r\n                                                <li><span>{{topic.TopicName}}</span></li>\r\n                                            </ul>                                            \r\n                                        </div>                                  \r\n                                </div>\r\n                               \r\n                            </div>\r\n\r\n                            <button class=\"btnLarge btnRed\" (click)=\"startLearning()\"><a [routerLink]=\"['/course',course.Id,'module']\">Start Learning</a></button>\r\n\r\n                        </div>\r\n\r\n                    </div>\r\n\r\n\r\n\r\n\r\n                </div>\r\n            </div>\r\n\r\n\r\n\r\n\r\n            <div class=\"caseHead\">Share your views below</div>\r\n            <div class=\"caseBody\">\r\n\r\n                <div class=\"comment\">\r\n                    <div class=\"ini\">JO</div>\r\n                    <div class=\"commenter\">Joy Okere</div>\r\n                    <span>made a comment</span>\r\n                    <i class=\"material-icons\">thumb_down</i><i class=\"material-icons blue\">thumb_up</i>\r\n                    <div class=\"commentTopic\">AN AFFECTIVE DILEMMA</div>\r\n                    <div class=\"commentBody\">\r\n                        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Morbi leo urna molestie at elementum.\r\n                    </div>\r\n                    <div class=\"addComment\">\r\n                        <span class=\"blue\">reply comment</span><i class=\"material-icons blue\">comments</i>\r\n                    </div>\r\n                </div>\r\n\r\n\r\n\r\n                <div class=\"comment\">\r\n                    <div class=\"ini\" style=\"background: orange\">SO</div>\r\n                    <div class=\"commenter\">me</div>\r\n                    <span>made a comment</span>\r\n                    <i class=\"material-icons\">thumb_down</i><i class=\"material-icons blue\">thumb_up</i>\r\n                    <div class=\"commentBody\">\r\n                        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Morbi leo urna molestie at elementum. onsectetur adipiscing elit, sed do eiusmod tempor incid onsectetur adipiscing elit, sed do eiusmod tempor incid\r\n                    </div>\r\n                    <div class=\"addComment\">\r\n                        <span class=\"blue\">reply comment</span><i class=\"material-icons blue\">comments</i>\r\n                    </div>\r\n                </div>\r\n\r\n\r\n                <!--<form [formGroup]=\"commentForm\" (onSubmit)=\"PostComment()\">\r\n                    <textarea name=\"\" id=\"\" cols=\"30\" rows=\"10\" placeholder=\"Leave a comment...\" formControlName=\"comment\"></textarea>\r\n                    <a class=\"btnBlue\"><i class=\"material-icons\">comment</i>Post comment</a>\r\n                </form>-->\r\n\r\n\r\n\r\n            </div>\r\n\r\n\r\n        </div>\r\n        <div class=\"col2\">\r\n\r\n\r\n            <div class=\"pod\">\r\n                <div class=\"line borderBottomGrey\">\r\n                    Coming Up  <button class=\"btnBlue\"><i class=\"material-icons\">event</i> <span>View Calendar</span></button>\r\n                </div>\r\n\r\n                <div class=\"podBlock\">\r\n                    <div class=\"podItem\">\r\n                        <div class=\"coupon\"><i class=\"material-icons blue\">event</i></div>\r\n                        <div class=\"list\">\r\n                            <span class=\"blue\">General Staff Shop</span>\r\n                            <i>Jan 10, 2018</i>\r\n                        </div>\r\n                    </div>\r\n\r\n                    <div class=\"podItem\">\r\n                        <div class=\"coupon\"><i class=\"material-icons blue\">event</i></div>\r\n                        <div class=\"list\">\r\n                            <span class=\"blue\">Live Podcast</span>\r\n                            <i>Jan 10, 2018</i>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n\r\n            </div>\r\n\r\n\r\n\r\n            <div class=\"space\"></div>\r\n            <div class=\"pod\">\r\n                <div class=\"line borderBottomGrey\">Quick Notification</div>\r\n\r\n\r\n                <div class=\"podBlock\">\r\n                    <div class=\"podItem\">\r\n                        <div class=\"coupon\"><span class=\"redBackground\">05</span></div>\r\n                        <div class=\"list\">\r\n                            <span>Courses are overdue</span>\r\n                        </div>\r\n                    </div>\r\n\r\n                    <div class=\"podItem\">\r\n                        <div class=\"coupon\"><span class=\"yellowBackground\">15</span></div>\r\n                        <div class=\"list\">\r\n                            <span>Courses are going to expire in the next 30 days.</span>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n\r\n            <div class=\"space\"></div>\r\n\r\n\r\n            <div class=\"pod\">\r\n                <div class=\"line borderBottomGrey\">Awards/Certificates</div>\r\n\r\n\r\n                <div class=\"podBlock\">\r\n                    <div class=\"podItem\">\r\n                        <div class=\"half\">\r\n                            <div class=\"blue big\">CERTIFICATES </div><b>(3)</b>\r\n                            <p>Your latest certificates</p>\r\n                            <button class=\"btnBlue\">View all</button>\r\n                        </div>\r\n\r\n                        <div class=\"half\">\r\n                            <img src=\"/Content/img/c1.jpg\" alt=\"\">\r\n                            <img src=\"/Content/img/c2.jpg\" alt=\"\">\r\n                            <img src=\"/Content/img/c3.jpg\" alt=\"\">\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n\r\n        </div>\r\n\r\n    </div>\r\n</div>\r\n<emp-footer></emp-footer>"

/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/course-detail/course-detail.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var $ = __webpack_require__("../../../../jquery/dist/jquery.js");
var router_1 = __webpack_require__("../../../router/esm5/router.js");
var course_service_1 = __webpack_require__("../../../../../ClientApp/app/service/course.service.ts");
var CourseDetailComponent = (function () {
    function CourseDetailComponent(router, route, courseService) {
        this.router = router;
        this.route = route;
        this.courseService = courseService;
    }
    //startLearning(): void {
    //    this.router.navigate(['/course/', this.course.Id, '/module']);
    //    console.log(this.router.navigate(['/course/', this.course.Id, '/start_learning']));
    //}
    CourseDetailComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.sub = this.route.paramMap.subscribe(function (params) {
            var id = +params.get('id');
            _this.getCourse(id);
        });
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
                }
                else {
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
    };
    CourseDetailComponent.prototype.getCourse = function (id) {
        var _this = this;
        this.courseService.getCourse(id).subscribe(function (c) { return _this.course = c; }, function (err) { return _this.errorMessage = err; });
    };
    CourseDetailComponent.prototype.ngOnDestroy = function () {
        this.sub.unsubscribe();
    };
    CourseDetailComponent = __decorate([
        core_1.Component({
            selector: '',
            template: __webpack_require__("../../../../../ClientApp/app/employee/course/course-detail/course-detail.component.html"),
            styles: [__webpack_require__("../../../../../ClientApp/app/employee/course/course-detail/course-detail.component.css")]
        }),
        __metadata("design:paramtypes", [router_1.Router, router_1.ActivatedRoute, course_service_1.CourseService])
    ], CourseDetailComponent);
    return CourseDetailComponent;
}());
exports.CourseDetailComponent = CourseDetailComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/course.module.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var router_1 = __webpack_require__("../../../router/esm5/router.js");
var forms_1 = __webpack_require__("../../../forms/esm5/forms.js");
var home_component_1 = __webpack_require__("../../../../../ClientApp/app/employee/home/home.component.ts");
var ngx_pagination_1 = __webpack_require__("../../../../ngx-pagination/dist/ngx-pagination.js");
var ngx_toastr_1 = __webpack_require__("../../../../ngx-toastr/toastr.es5.js");
var shared_module_1 = __webpack_require__("../../../../../ClientApp/app/shared/shared.module.ts");
var course_detail_component_1 = __webpack_require__("../../../../../ClientApp/app/employee/course/course-detail/course-detail.component.ts");
var course_catalogue_component_1 = __webpack_require__("../../../../../ClientApp/app/employee/course/course-catalogue/course-catalogue.component.ts");
var video_catalogue_component_1 = __webpack_require__("../../../../../ClientApp/app/employee/course/video-catalogue/video-catalogue.component.ts");
var video_component_1 = __webpack_require__("../../../../../ClientApp/app/employee/course/video/video.component.ts");
var toolbar_component_1 = __webpack_require__("../../../../../ClientApp/app/employee/course/video/toolbar/toolbar.component.ts");
var progress_component_1 = __webpack_require__("../../../../../ClientApp/app/employee/course/video/progress/progress.component.ts");
var auth_guard_service_1 = __webpack_require__("../../../../../ClientApp/app/guard/auth-guard.service.ts");
var timedisplay_pipe_1 = __webpack_require__("../../../../../ClientApp/app/pipes/timedisplay.pipe.ts");
var course_service_1 = __webpack_require__("../../../../../ClientApp/app/service/course.service.ts");
var video_service_1 = __webpack_require__("../../../../../ClientApp/app/service/video.service.ts");
var CourseModule = (function () {
    function CourseModule() {
    }
    CourseModule = __decorate([
        core_1.NgModule({
            imports: [
                shared_module_1.SharedModule,
                forms_1.ReactiveFormsModule,
                ngx_pagination_1.NgxPaginationModule,
                ngx_toastr_1.ToastrModule.forRoot({
                    progressBar: true,
                    timeOut: 4000,
                    preventDuplicates: true
                }),
                router_1.RouterModule.forChild([
                    { path: 'home', component: home_component_1.HomeComponent, canActivate: [auth_guard_service_1.AuthGuard] },
                    { path: 'course/:id', component: course_detail_component_1.CourseDetailComponent },
                    { path: 'course/:id/module', component: video_component_1.VideoComponent },
                    { path: 'course', component: course_catalogue_component_1.CourseCatalogueComponent, },
                    { path: 'all_videos', component: video_catalogue_component_1.VideoCatalogueComponent }
                ])
            ],
            declarations: [
                course_detail_component_1.CourseDetailComponent,
                video_component_1.VideoComponent,
                home_component_1.HomeComponent,
                course_catalogue_component_1.CourseCatalogueComponent,
                video_catalogue_component_1.VideoCatalogueComponent,
                toolbar_component_1.ToolbarComponent,
                progress_component_1.ProgressComponent,
                timedisplay_pipe_1.TimeDisplayPipe
            ],
            providers: [
                course_service_1.CourseService,
                video_service_1.VideoService
            ]
        })
    ], CourseModule);
    return CourseModule;
}());
exports.CourseModule = CourseModule;


/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/video-catalogue/video-catalogue.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".pageTab:nth-child(4) {\r\n    border: 2px solid #666;\r\n}\r\n\r\n    .pageTab:nth-child(4) .tabing {\r\n        background: #961c1d;\r\n    }\r\n\r\n@media(max-width: 500px) {\r\n    .pageTab:nth-child(4) {\r\n        border: 0px solid transparent;\r\n    }\r\n\r\n        .pageTab:nth-child(4) .tabing {\r\n            background: transparent;\r\n        }\r\n}\r\n\r\n.col2 {\r\n    display: none;\r\n}\r\n\r\n.searchArea {\r\n    background: #0a9cb7;\r\n}", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/video-catalogue/video-catalogue.component.html":
/***/ (function(module, exports) {

module.exports = "<emp-header></emp-header>\r\n<div class=\"wrapper\">\r\n\r\n    <div class=\"breadcrum\" style=\"background-image: url('/Content/img/podcast (1).jpg');\">\r\n        <div class=\"col8\">\r\n            <div class=\"headcrum\"></div>\r\n            <div class=\"bodycrum\"></div>\r\n        </div>\r\n\r\n        <div class=\"col4\">\r\n            <div class=\"headcrum\">Customer Service Representative</div>\r\n            <div class=\"bodycrum\">\r\n                <i class=\"material-icons\">room</i>  <i>Marina, Lagos</i>\r\n            </div>\r\n            <div class=\"bodycrum\">\r\n                <i style=\"font-size: 12px;margin-top: 3px;\">Staff ID: </i>  <b>001558</b>\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n\r\n\r\n    <div class=\"searchArea\">\r\n        <div class=\"searchbox\">\r\n            <input type=\"text\" placeholder=\"Search Courses\" /> <i class=\"material-icons\">search</i>\r\n        </div>\r\n    </div>\r\n\r\n\r\n\r\n\r\n    <div class=\"content\">\r\n\r\n        <div class=\"col12\">\r\n\r\n\r\n            <div class=\"change\">\r\n\r\n                <div class=\"holder\">\r\n                    <span>SORT BY</span>\r\n                    <select>\r\n                        <option value=\"\">All departments</option>\r\n                        <option value=\"\">course length</option>\r\n                        <option value=\"\">certificate type</option>\r\n                    </select>\r\n                </div>\r\n\r\n\r\n\r\n                <div class=\"holder\">\r\n                    <span> &emsp; OR</span>\r\n                    <select>\r\n                        <option value=\"\">Learning areas</option>\r\n                        <option value=\"\">course length</option>\r\n                        <option value=\"\">certificate type</option>\r\n                    </select>\r\n                </div>\r\n\r\n            </div>\r\n\r\n\r\n\r\n\r\n            <div class=\"boxes1\">\r\n\r\n\r\n                <!-- Course Item -->\r\n                <div class=\"col44 courseItem1\">\r\n                    <a href=\"video\">\r\n                        <div class=\"coursePlay\"><i class=\"material-icons\">play_circle_filled</i></div>\r\n                    </a>\r\n                    <div class=\"text\">\r\n                        <a href=\"video\"><h5 class=\"blue\">Strategic Business Analysis</h5></a>\r\n                        <p> <b>21 Feb, 18</b></p>\r\n                    </div>\r\n\r\n                </div>\r\n                <!-- End of Course Item -->\r\n                <!-- Course Item -->\r\n                <div class=\"col44 courseItem1\">\r\n                    <a href=\"video\">\r\n                        <div class=\"coursePlay\"><i class=\"material-icons\">play_circle_filled</i></div>\r\n                    </a>\r\n                    <div class=\"text\">\r\n                        <a href=\"video\"><h5 class=\"blue\">Strategic Business Analysis</h5></a>\r\n                        <p> <b>21 Feb, 18</b></p>\r\n                    </div>\r\n\r\n                </div>\r\n                <!-- End of Course Item -->\r\n                <!-- Course Item -->\r\n                <div class=\"col44 courseItem1\">\r\n                    <a href=\"video\">\r\n                        <div class=\"coursePlay\"><i class=\"material-icons\">play_circle_filled</i></div>\r\n                    </a>\r\n                    <div class=\"text\">\r\n                        <a href=\"video\"><h5 class=\"blue\">Strategic Business Analysis</h5></a>\r\n                        <p> <b>21 Feb, 18</b></p>\r\n                    </div>\r\n\r\n                </div>\r\n                <!-- End of Course Item -->\r\n                <!-- Course Item -->\r\n                <div class=\"col44 courseItem1\">\r\n                    <a href=\"video\">\r\n                        <div class=\"coursePlay\"><i class=\"material-icons\">play_circle_filled</i></div>\r\n                    </a>\r\n                    <div class=\"text\">\r\n                        <a href=\"video\"><h5 class=\"blue\">Strategic Business Analysis</h5></a>\r\n                        <p> <b>21 Feb, 18</b></p>\r\n                    </div>\r\n\r\n                </div>\r\n                <!-- End of Course Item -->\r\n                <!-- Course Item -->\r\n                <div class=\"col44 courseItem1\">\r\n                    <a href=\"video\">\r\n                        <div class=\"coursePlay\"><i class=\"material-icons\">play_circle_filled</i></div>\r\n                    </a>\r\n                    <div class=\"text\">\r\n                        <a href=\"video\"><h5 class=\"blue\">Strategic Business Analysis</h5></a>\r\n                        <p> <b>21 Feb, 18</b></p>\r\n                    </div>\r\n\r\n                </div>\r\n                <!-- End of Course Item -->\r\n                <!-- Course Item -->\r\n                <div class=\"col44 courseItem1\">\r\n                    <a href=\"video\">\r\n                        <div class=\"coursePlay\"><i class=\"material-icons\">play_circle_filled</i></div>\r\n                    </a>\r\n                    <div class=\"text\">\r\n                        <a href=\"video\"><h5 class=\"blue\">Strategic Business Analysis</h5></a>\r\n                        <p> <b>21 Feb, 18</b></p>\r\n                    </div>\r\n\r\n                </div>\r\n                <!-- End of Course Item -->\r\n                <!-- Course Item -->\r\n                <div class=\"col44 courseItem1\">\r\n                    <a href=\"video\">\r\n                        <div class=\"coursePlay\"><i class=\"material-icons\">play_circle_filled</i></div>\r\n                    </a>\r\n                    <div class=\"text\">\r\n                        <a href=\"video\"><h5 class=\"blue\">Strategic Business Analysis</h5></a>\r\n                        <p> <b>21 Feb, 18</b></p>\r\n                    </div>\r\n\r\n                </div>\r\n                <!-- End of Course Item -->\r\n                <!-- Course Item -->\r\n                <div class=\"col44 courseItem1\">\r\n                    <a href=\"video\">\r\n                        <div class=\"coursePlay\"><i class=\"material-icons\">play_circle_filled</i></div>\r\n                    </a>\r\n                    <div class=\"text\">\r\n                        <a href=\"video\"><h5 class=\"blue\">Strategic Business Analysis</h5></a>\r\n                        <p> <b>21 Feb, 18</b></p>\r\n                    </div>\r\n\r\n                </div>\r\n                <!-- End of Course Item -->\r\n                <!-- Course Item -->\r\n                <div class=\"col44 courseItem1\">\r\n                    <a href=\"video\">\r\n                        <div class=\"coursePlay\"><i class=\"material-icons\">play_circle_filled</i></div>\r\n                    </a>\r\n                    <div class=\"text\">\r\n                        <a href=\"video\"><h5 class=\"blue\">Strategic Business Analysis</h5></a>\r\n                        <p> <b>21 Feb, 18</b></p>\r\n                    </div>\r\n\r\n                </div>\r\n                <!-- End of Course Item -->\r\n                <!-- Course Item -->\r\n                <div class=\"col44 courseItem1\">\r\n                    <a href=\"video\">\r\n                        <div class=\"coursePlay\"><i class=\"material-icons\">play_circle_filled</i></div>\r\n                    </a>\r\n                    <div class=\"text\">\r\n                        <a href=\"video\"><h5 class=\"blue\">Strategic Business Analysis</h5></a>\r\n                        <p> <b>21 Feb, 18</b></p>\r\n                    </div>\r\n\r\n                </div>\r\n                <!-- End of Course Item -->\r\n                <!-- Course Item -->\r\n                <div class=\"col44 courseItem1\">\r\n                    <a href=\"video\">\r\n                        <div class=\"coursePlay\"><i class=\"material-icons\">play_circle_filled</i></div>\r\n                    </a>\r\n                    <div class=\"text\">\r\n                        <a href=\"video\"><h5 class=\"blue\">Strategic Business Analysis</h5></a>\r\n                        <p> <b>21 Feb, 18</b></p>\r\n                    </div>\r\n\r\n                </div>\r\n                <!-- End of Course Item -->\r\n                <!-- Course Item -->\r\n                <div class=\"col44 courseItem1\">\r\n                    <a href=\"video\">\r\n                        <div class=\"coursePlay\"><i class=\"material-icons\">play_circle_filled</i></div>\r\n                    </a>\r\n                    <div class=\"text\">\r\n                        <a href=\"video\"><h5 class=\"blue\">Strategic Business Analysis</h5></a>\r\n                        <p><b>21 Feb, 18</b></p>\r\n                    </div>\r\n\r\n                </div>\r\n                <!-- End of Course Item -->\r\n\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<emp-footer></emp-footer>"

/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/video-catalogue/video-catalogue.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var $ = __webpack_require__("../../../../jquery/dist/jquery.js");
var VideoCatalogueComponent = (function () {
    function VideoCatalogueComponent() {
    }
    VideoCatalogueComponent.prototype.ngOnInit = function () {
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
                }
                else {
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
    };
    VideoCatalogueComponent = __decorate([
        core_1.Component({
            template: __webpack_require__("../../../../../ClientApp/app/employee/course/video-catalogue/video-catalogue.component.html"),
            styles: [__webpack_require__("../../../../../ClientApp/app/employee/course/video-catalogue/video-catalogue.component.css")]
        })
    ], VideoCatalogueComponent);
    return VideoCatalogueComponent;
}());
exports.VideoCatalogueComponent = VideoCatalogueComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/video/progress/progress.component.html":
/***/ (function(module, exports) {

module.exports = "\r\n<div id=\"progressMeterFull\" (click)=\"videoService.seekVideo($event)\">\r\n    <div id=\"progressMeter\" (click)=\"videoService.seekVideo($event)\" [style.width.px]=\"videoService.calculatedWidth\"></div>\r\n</div>\r\n<div id=\"thumbScrubber\" (mousedown)=\"videoService.dragStart($event)\" [style.top.px]=\"videoService.calculatedScrubY-2\" [style.left.px]=\"videoService.calculatedWidth\"></div>"

/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/video/progress/progress.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var video_service_1 = __webpack_require__("../../../../../ClientApp/app/service/video.service.ts");
var ProgressComponent = (function () {
    function ProgressComponent(videoService) {
        this.videoService = videoService;
    }
    ProgressComponent = __decorate([
        core_1.Component({
            selector: 'video-progress',
            template: __webpack_require__("../../../../../ClientApp/app/employee/course/video/progress/progress.component.html"),
            styleUrls: []
        }),
        __metadata("design:paramtypes", [video_service_1.VideoService])
    ], ProgressComponent);
    return ProgressComponent;
}());
exports.ProgressComponent = ProgressComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/video/toolbar/toolbar.component.html":
/***/ (function(module, exports) {

module.exports = "<div id=\"playerToolbar\">\r\n    <a id=\"playBtn\" class=\"btn btn-default\" (click)=\"videoService.playVideo()\">\r\n        <i [ngClass]=\"{'fa-play':!videoService.isPlaying, 'fa-pause':videoService.isPlaying}\" class=\"fa\"></i>\r\n    </a>\r\n    <a id=\"muteBtn\" class=\"btn btn-default\" (click)=\"videoService.muteVideo()\">\r\n        <i [ngClass]=\"{'fa-volume-off':videoService.isMuted, 'fa-volume-up':!videoService.isMuted}\" class=\"fa\"></i>\r\n    </a>\r\n    <span id=\"videoTime\">{{videoService.currentTime | timeDisplay}}/{{videoService.totalTime | timeDisplay}}</span>\r\n    <a id=\"fsBtn\" class=\"btn btn-default pull-right\" (click)=\"videoService.fullScreen()\">\r\n        <i class=\"fa fa-expand\"></i>\r\n    </a>\r\n    <a id=\"detailBtn\" class=\"btn btn-default pull-right\" (click)=\"videoService.details\">\r\n        <i class=\"fa fa-bars\"></i>\r\n    </a>\r\n</div>"

/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/video/toolbar/toolbar.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var video_service_1 = __webpack_require__("../../../../../ClientApp/app/service/video.service.ts");
var ToolbarComponent = (function () {
    function ToolbarComponent(videoService) {
        this.videoService = videoService;
    }
    ToolbarComponent = __decorate([
        core_1.Component({
            selector: 'video-toolbar',
            template: __webpack_require__("../../../../../ClientApp/app/employee/course/video/toolbar/toolbar.component.html"),
            styleUrls: []
        }),
        __metadata("design:paramtypes", [video_service_1.VideoService])
    ], ToolbarComponent);
    return ToolbarComponent;
}());
exports.ToolbarComponent = ToolbarComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/video/video.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/video/video.component.html":
/***/ (function(module, exports) {

module.exports = "<gen-header></gen-header>\r\n<div class=\"wrapper\">\r\n\r\n    <div class=\"breadcrum\" style=\"background-image: url('/Content/img/details (1).jpg')\">\r\n        <div class=\"col8\">\r\n            <button class=\"btnRed btnSmall\"><i class=\"material-icons\">arrow_left</i><span>Go Back</span> &emsp;</button>\r\n        </div>\r\n        <div class=\"col4\">\r\n            <div class=\"headcrum\">Customer Service Representative</div>\r\n            <div class=\"bodycrum\">\r\n                <i class=\"material-icons\">room</i>  <i>Marina, Lagos</i>\r\n            </div>\r\n            <div class=\"bodycrum\">\r\n                <i style=\"font-size: 12px;margin-top: 3px;\">Staff ID: </i>  <b>001558</b>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n\r\n\r\n    <div class=\"videoTab\">\r\n\r\n\r\n        <!-- video section -->\r\n        <div class=\"col99 show\">\r\n\r\n            <div class=\"videoTopic\">\r\n                <div class=\"topicTop\"></div>\r\n                <div class=\"topicBtm\"></div>\r\n            </div>\r\n            <div id=\"fullPlayer\" (mouseup)=\"videoService.dragStop($event)\" (mousemove)=\"videoService.dragMove($event)\" (mouseleave)=\"videoService.dragStop($event)\">\r\n                \r\n                    <video id=\"videoDisplay\" (click)=\"videoService.playVideo()\"></video>\r\n                    <div id=\"bigPlayButton\" [hidden]=\"videoService.isPlaying\" (click)=\"videoService.playVideo()\"><i class=\"fa fa-play\"></i></div>\r\n                    <div id=\"videoTitle\" [hidden]=\"videoService.isPlaying\"></div>\r\n                    <video-progress></video-progress>\r\n                    <video-toolbar></video-toolbar>\r\n\r\n\r\n                    <div class=\"hightlight\">\r\n                        <div class=\"vTopic\"></div>\r\n\r\n                        <div class=\"vLike\">\r\n                            <i class=\"material-icons\">thumb_up</i> <span>32</span>\r\n                            <i class=\"material-icons\">thumb_down</i> <span>3</span>\r\n                        </div>\r\n\r\n                        <div class=\"vTime\">20 mins</div>\r\n\r\n                    </div>\r\n\r\n                </div>\r\n            \r\n\r\n            <div class=\"addComment\" style=\"float: left;width: auto;\"><span style=\"color:#222;\">View Video transscript</span><i class=\"material-icons\" style=\"color:#222;\">content_copy</i></div>\r\n            <div class=\"addComment\" style=\"float: left;width: auto;\"><span style=\"color: #E11DD2;\">Contribute to this course</span><i class=\"material-icons pink\">comments</i> &emsp;  &emsp; </div>\r\n\r\n\r\n            <button class=\"btnPink btnHerd\"><span>Mark as complete</span><i class=\"material-icons\">check</i></button>\r\n\r\n\r\n\r\n        </div>\r\n\r\n        <!-- end of video section -->\r\n        <!-- text section -->\r\n        <div class=\"col99\">\r\n\r\n            <!--<div class=\"videoTopic\">\r\n                <div class=\"topicTop\">{{course.CourseName}}:</div>\r\n                <div class=\"topicBtm\" *ngFor=\"let item of course.Modules\">{{item.ModuleName}}</div>\r\n            </div>-->\r\n\r\n            <!--<div class=\"viewArea\">\r\n                <img src=\"/Content/img/slide.png\" alt=\"\">\r\n            </div>\r\n\r\n            <div class=\"hightlight\">\r\n                <button class=\"btn btnMid btnBlack text-center\">Next Slide</button>\r\n\r\n                <div class=\"vLike\">\r\n                    <i class=\"material-icons\">thumb_up</i> <span>32</span>\r\n                    <i class=\"material-icons\">thumb_down</i> <span>3</span>\r\n                </div>\r\n\r\n\r\n            </div>\r\n\r\n\r\n\r\n            <div class=\"addComment\" style=\"float: left;width: auto;\"><span style=\"color: #E11DD2;\">Contribute to this course</span><i class=\"material-icons pink\">comments</i> &emsp;  &emsp; </div>\r\n\r\n\r\n            <button class=\"btnPink btnHerd\"><span>Mark as complete</span><i class=\"material-icons\">check</i></button>-->\r\n\r\n\r\n\r\n        </div>\r\n\r\n        <!-- end of text section -->\r\n\r\n\r\n\r\n\r\n\r\n        <div class=\"courseMenu\">\r\n            <div class=\"courseMod \">\r\n\r\n                <!-- lesson tab -->\r\n                <div *ngFor=\"let a of videoService.videos.Modules; let i=index\">\r\n                    <div class=\"lessonTab \" *ngFor=\"let b of a.Topics;let n=index\" (click)=\"videoService.selectedVideo(i,n)\">\r\n                        <div class=\"module\">{{i+1}}.{{n+1}}</div>\r\n                        <div class=\"moduleTp\">\r\n                            <div class=\"moduleT\">{{b.TopicName}} &emsp; <i>{{b.ContentType}} ()</i></div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n                \r\n                <!-- end of lesson tab -->\r\n            </div>\r\n            <a href=\"#\" class=\"Lessona\">\r\n                <div class=\"nextLesson\">\r\n                    <span>GO TO</span><i class=\"material-icons\">arrow_forward</i><b>NEXT MODULE</b>\r\n                </div>\r\n            </a>\r\n\r\n        </div>\r\n\r\n\r\n\r\n    </div>\r\n\r\n    <div class=\"content\">\r\n\r\n        <div class=\"col9\">\r\n\r\n\r\n            <div class=\"case\">\r\n                <div class=\"long\">\r\n                    <div class=\"line borderBottomBlue\">Course Forum</div>\r\n                </div>\r\n                <div class=\"caseBody\">\r\n                    <div class=\"comment\">\r\n                        <div class=\"ini\">JO</div><div class=\"offline\"></div>\r\n                        <div class=\"commenter\">Joy Okere</div>  <span>made a comment</span>\r\n                        <i class=\"material-icons\">thumb_down</i><i class=\"material-icons blue\">thumb_up</i>\r\n                        <div class=\"commentTopic\">AN AFFECTIVE DILEMMA</div>\r\n                        <div class=\"commentBody\">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Morbi leo urna molestie at elementum.</div>\r\n                        <div class=\"addComment\">\r\n                            <span class=\"blue\">reply comment</span><i class=\"material-icons blue\">comments</i>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"comment\">\r\n                        <div class=\"ini\" style=\"background: orange\">SO</div><div class=\"online\"></div>\r\n                        <div class=\"commenter\">me</div> <span>made a comment</span>\r\n                        <i class=\"material-icons\">thumb_down</i><i class=\"material-icons blue\">thumb_up</i>\r\n                        <div class=\"commentBody\">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Morbi leo urna molestie at elementum. onsectetur adipiscing elit, sed do eiusmod tempor incid onsectetur adipiscing elit, sed do eiusmod tempor incid</div>\r\n                        <div class=\"addComment\">\r\n                            <span class=\"blue\">reply comment</span><i class=\"material-icons blue\">comments</i>\r\n                        </div>\r\n                    </div>\r\n                    <form>\r\n                        <textarea name=\"\" id=\"\" cols=\"30\" rows=\"10\" placeholder=\"Leave a comment...\"></textarea>\r\n                        <a class=\"btnBlue\"><i class=\"material-icons\">comment</i>Post comment</a>\r\n                    </form>\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>\r\n</div>"

/***/ }),

/***/ "../../../../../ClientApp/app/employee/course/video/video.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var $ = __webpack_require__("../../../../jquery/dist/jquery.js");
var router_1 = __webpack_require__("../../../router/esm5/router.js");
var ngx_toastr_1 = __webpack_require__("../../../../ngx-toastr/toastr.es5.js");
var video_service_1 = __webpack_require__("../../../../../ClientApp/app/service/video.service.ts");
var VideoComponent = (function () {
    function VideoComponent(router, route, videoService, toastr) {
        this.router = router;
        this.route = route;
        this.videoService = videoService;
        this.toastr = toastr;
    }
    VideoComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.sub = this.route.paramMap.subscribe(function (params) {
            var id = +params.get('id');
            _this.videoService.getVideos(id);
        });
        this.videoService.appSetup("videoDisplay");
        $(document).ready(function () {
            $('.lessonTab').click(function () {
                $('.lessonTab').removeClass("active");
                $(this).addClass("active");
                $('.col99').toggleClass("show");
            });
        });
    };
    VideoComponent.prototype.ngOnDestroy = function () {
        this.sub.unsubscribe();
    };
    VideoComponent = __decorate([
        core_1.Component({
            selector: '',
            template: __webpack_require__("../../../../../ClientApp/app/employee/course/video/video.component.html"),
            styles: [__webpack_require__("../../../../../ClientApp/app/employee/course/video/video.component.css")]
        }),
        __metadata("design:paramtypes", [router_1.Router, router_1.ActivatedRoute, video_service_1.VideoService, ngx_toastr_1.ToastrService])
    ], VideoComponent);
    return VideoComponent;
}());
exports.VideoComponent = VideoComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/employee/home/home.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".searchArea {\r\n    background: #f3f3f3;\r\n}\r\n.breadcrum {\r\n    background: url('/Content/img/course_img.jpg');\r\n}", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../ClientApp/app/employee/home/home.component.html":
/***/ (function(module, exports) {

module.exports = "<emp-header></emp-header>\r\n\r\n<div class=\"wrapper\">\r\n\r\n    <div class=\"breadcrum\">\r\n        <div class=\"col8\">\r\n            <div class=\"headcrum\">Employee Dashboard</div>\r\n            <div class=\"bodycrum\">Description</div>\r\n        </div>\r\n\r\n        <div class=\"col4\">\r\n            <div class=\"headcrum\">Customer Service Representative</div>\r\n            <div class=\"bodycrum\"><i class=\"material-icons\">room</i> <i>Marina, Lagos</i></div>\r\n            <div class=\"bodycrum\"><i style=\"font-size: 12px;margin-top: 3px;\">Staff ID: </i> <b>001558</b></div>\r\n        </div>\r\n    </div>\r\n\r\n\r\n    <div class=\"searchArea\">\r\n        <div class=\"searchbox\">\r\n            <input type=\"text\" placeholder=\"Search Courses\" />\r\n            <i class=\"material-icons\">search</i>\r\n        </div>\r\n    </div>\r\n\r\n\r\n\r\n\r\n    <div class=\"content\">\r\n        <div class=\"col9\">\r\n\r\n\r\n\r\n            <div class=\"boxed\">\r\n                <div class=\"long\">\r\n                    <div class=\"line borderBottomBlue\">\r\n                        Assigned Courses\r\n                    </div>\r\n                </div>\r\n\r\n\r\n                <div class=\"col-md-6 col-md-offset-3 indloading\" role=\"alert\" *ngIf=\"indLoading\"><img src=\"../../Content/img/loading.gif\" width=\"32\" height=\"32\" /> Loading...</div>\r\n                <div class=\"col33 courseItem\" *ngFor=\"let item of assignedCourses\">\r\n                    <div class=\"courseImg\" [style.background-image]=\"'url('+item.ImageUrl+')'\"><i class=\"material-icons\">visibility</i></div>\r\n                    <div class=\"text\">\r\n                        <a [routerLink]=\"['/course',item.Id]\" [queryParams]=\"{}\"><h5 class=\"blue\">{{item.CourseName}}</h5></a>\r\n                        <p>\r\n                            {{item.CourseDescription | slice:0:200}}\r\n                            <br><br>\r\n                            <i>Due Date: </i> <b>{{item.DueDate| date:'dd-MM-yyyy'}}</b>\r\n                        </p>\r\n\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n            <button class=\"btnLong btnBlue\">View all</button>\r\n\r\n\r\n\r\n\r\n\r\n            <div class=\"boxed\">\r\n                <div class=\"long\">\r\n                    <div class=\"line borderBottomRed\">\r\n                        Recommended Courses\r\n                    </div>\r\n                </div>\r\n                <div class=\"col-md-6 col-md-offset-3 indloading\" role=\"alert\" *ngIf=\"indLoading\"><img src=\"../../Content/img/loading.gif\" width=\"32\" height=\"32\" /> Loading...</div>\r\n                <div class=\"col33 courseItem\" *ngFor=\"let item of recommendedCourses\">\r\n                    <img class=\"courseImg\" src=\"{{item.ImageUrl}}\"><i class=\"material-icons\">visibility</i>\r\n                    <div class=\"text\">\r\n                        <a [routerLink]=\"['/course',item.Id]\"><h5 class=\"blue\">{{item.CourseName}}</h5></a>\r\n                        <p>\r\n                            {{item.CourseDescription|slice:0:200}}\r\n                            <br><br>\r\n                            <i>Due Date: </i> <b>{{item.DueDate| date:'dd-MM-yyyy'}}</b>\r\n                        </p>\r\n\r\n                    </div>\r\n                </div>\r\n\r\n\r\n\r\n            </div>\r\n\r\n            <button class=\"btnLong btnBlue\">View all</button>\r\n\r\n\r\n            <button class=\"btnLarge btnRed btnCurve\">View Course Catalog</button>\r\n            <div class=\"long\">\r\n                <div class=\"line borderBottomOrange\">\r\n                    Learning Progress\r\n                </div>\r\n\r\n                <i class=\"material-icons\">keyboard_arrow_down</i>\r\n            </div>\r\n\r\n\r\n            <div class=\"hideTab\">\r\n\r\n                <h4>Social Enterprise: Growing a Sustainable Business</h4>\r\n                <div class=\"load\">\r\n                    <div class=\"loader\">\r\n                        <div class=\"peg range\"><div class=\"loading\" style=\"background: #b1eddc;width:100%;\"></div></div>\r\n                        <div class=\"peg loadInfo\">100% - Completed</div>\r\n                    </div>\r\n                    <div class=\"pen\">\r\n                        <button class=\"btnBlue btnReach\">View Reports</button>\r\n                    </div>\r\n                </div>\r\n\r\n\r\n                <h4>Mergers and Acquisitions: Accounting Principles</h4>\r\n                <div class=\"load\">\r\n                    <div class=\"loader\">\r\n                        <div class=\"peg range\"><div class=\"loading\" style=\"background: #f7daad;width:47%;\"></div></div>\r\n                        <div class=\"peg loadInfo\">47% - In Progress</div>\r\n                    </div>\r\n                    <div class=\"pen\">\r\n                        <button class=\"btnBlue btnReach\">Continue Learning</button>\r\n                    </div>\r\n                </div>\r\n\r\n\r\n\r\n                <h4>Successful Negotiations: Essential Strategies and Skills</h4>\r\n                <div class=\"load\">\r\n                    <div class=\"loader\">\r\n                        <div class=\"peg range\"><div class=\"loading\" style=\"background: #f97575;width:25%;\"></div></div>\r\n                        <div class=\"peg loadInfo\">25% - In Progress</div>\r\n                    </div>\r\n                    <div class=\"pen\">\r\n                        <button class=\"btnBlue btnReach\">Continue Learning</button>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n\r\n\r\n            </div>\r\n            <div class=\"col2\">\r\n\r\n\r\n                <div class=\"pod\">\r\n                    <div class=\"line borderBottomGrey\">\r\n                        Coming Up  <button class=\"btnBlue\"><i class=\"material-icons\">event</i> <span>View Calendar</span></button>\r\n                    </div>\r\n\r\n                    <div class=\"podBlock\">\r\n                        <div class=\"podItem\">\r\n                            <div class=\"coupon\"><i class=\"material-icons blue\">event</i></div>\r\n                            <div class=\"list\">\r\n                                <span class=\"blue\">General Staff Shop</span>\r\n                                <i>Jan 10, 2018</i>\r\n                            </div>\r\n                        </div>\r\n\r\n                        <div class=\"podItem\">\r\n                            <div class=\"coupon\"><i class=\"material-icons blue\">event</i></div>\r\n                            <div class=\"list\">\r\n                                <span class=\"blue\">Live Podcast</span>\r\n                                <i>Jan 10, 2018</i>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n\r\n                </div>\r\n\r\n\r\n\r\n                <div class=\"space\"></div>\r\n                <div class=\"pod\">\r\n                    <div class=\"line borderBottomGrey\">Quick Notification</div>\r\n\r\n\r\n                    <div class=\"podBlock\">\r\n                        <div class=\"podItem\">\r\n                            <div class=\"coupon\"><span class=\"redBackground\">05</span></div>\r\n                            <div class=\"list\">\r\n                                <span>Courses are overdue</span>\r\n                            </div>\r\n                        </div>\r\n\r\n                        <div class=\"podItem\">\r\n                            <div class=\"coupon\"><span class=\"yellowBackground\">15</span></div>\r\n                            <div class=\"list\">\r\n                                <span>Courses are going to expire in the next 30 days.</span>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n\r\n\r\n                <div class=\"space\"></div>\r\n\r\n\r\n                <div class=\"pod\">\r\n                    <div class=\"line borderBottomGrey\">Awards/Certificates</div>\r\n\r\n\r\n                    <div class=\"podBlock\">\r\n                        <div class=\"podItem\">\r\n                            <div class=\"half\">\r\n                                <div class=\"blue big\">CERTIFICATES </div><b>(3)</b>\r\n                                <p>Your latest certificates</p>\r\n                                <button class=\"btnBlue\">View all</button>\r\n                            </div>\r\n\r\n                            <div class=\"half\">\r\n                                <img src=\"/Content/img/c1.jpg\" alt=\"\">\r\n                                <img src=\"/Content/img/c2.jpg\" alt=\"\">\r\n                                <img src=\"/Content/img/c3.jpg\" alt=\"\">\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n\r\n\r\n            </div>\r\n       \r\n    </div>\r\n</div>\r\n<emp-footer></emp-footer>"

/***/ }),

/***/ "../../../../../ClientApp/app/employee/home/home.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var $ = __webpack_require__("../../../../jquery/dist/jquery.js");
var router_1 = __webpack_require__("../../../router/esm5/router.js");
var ngx_progressbar_1 = __webpack_require__("../../../../ngx-progressbar/modules/ngx-progressbar.es5.js");
var course_service_1 = __webpack_require__("../../../../../ClientApp/app/service/course.service.ts");
var HomeComponent = (function () {
    function HomeComponent(courseService, route, ngProgress) {
        this.courseService = courseService;
        this.route = route;
        this.ngProgress = ngProgress;
        this.indLoading = false;
        this.assignedCourses = courseService.courses;
        this.recommendedCourses = courseService.courses;
    }
    HomeComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.ngProgress.start();
        this.indLoading = true;
        this.courseService.getAllCourses()
            .subscribe(function (c) {
            _this.assignedCourses = c,
                _this.indLoading = false;
        }, function (error) { return _this.errorMessage = error; });
        this.ngProgress.done();
        this.ngProgress.start();
        this.courseService.getAllCourses()
            .subscribe(function (c) {
            _this.recommendedCourses = c,
                _this.indLoading = false;
        }, function (error) { return _this.errorMessage = error; });
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
                }
                else {
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
    };
    HomeComponent = __decorate([
        core_1.Component({
            selector: '',
            template: __webpack_require__("../../../../../ClientApp/app/employee/home/home.component.html"),
            styles: [__webpack_require__("../../../../../ClientApp/app/employee/home/home.component.css")]
        }),
        __metadata("design:paramtypes", [course_service_1.CourseService, router_1.ActivatedRoute, ngx_progressbar_1.NgProgress])
    ], HomeComponent);
    return HomeComponent;
}());
exports.HomeComponent = HomeComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/employee/support/support.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "form.report {\r\n    padding: 0px;\r\n    margin-top: 60px;\r\n    width: 100%;\r\n    margin-left: 0%;\r\n    border: 0px solid transparent;\r\n}\r\n\r\nlabel {\r\n    float: left;\r\n    width: 100%;\r\n    margin-bottom: 5px;\r\n    font-weight: bold;\r\n}\r\n\r\nform.report input {\r\n    width: 100%;\r\n    height: 35px;\r\n    border: 1px solid #ccc;\r\n    margin-bottom: 20px;\r\n    padding: 10px;\r\n    font-size: 15px;\r\n}\r\n\r\nform.report textarea {\r\n    width: 100%;\r\n    padding: 10px;\r\n    font-size: 15px;\r\n    border: 1px solid #ccc;\r\n    border-radius: 0px;\r\n    -webkit-border-radius: 0px;\r\n    height: 120px;\r\n    margin-bottom: 20px;\r\n}\r\n\r\n.searchArea {\r\n    background: #354c98;\r\n}\r\n\r\n\r\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../ClientApp/app/employee/support/support.component.html":
/***/ (function(module, exports) {

module.exports = "<emp-header></emp-header>\r\n<div class=\"wrapper\">\r\n\r\n\r\n    <div class=\"breadcrum\" style=\"background-image: url('/Content/img/support (1).jpg');\">\r\n        <div class=\"col8\">\r\n\r\n        </div>\r\n\r\n        <div class=\"col4\">\r\n            <div class=\"headcrum\">Customer Service Representative</div>\r\n            <div class=\"bodycrum\"><i class=\"material-icons\">room</i> <i>Marina, Lagos</i></div>\r\n            <div class=\"bodycrum\"><i style=\"font-size: 12px;margin-top: 3px;\">Staff ID: </i> <b>001558</b></div>\r\n        </div>\r\n    </div>\r\n\r\n\r\n    <div class=\"searchArea\">\r\n        <div class=\"searchbox\">\r\n            <input type=\"text\" placeholder=\"Search Courses\" />\r\n            <i class=\"material-icons\">search</i>\r\n        </div>\r\n    </div>\r\n\r\n\r\n    <div class=\"content\">\r\n        <div class=\"col9\">\r\n\r\n            <div class=\"bigHead\" id=\"temp1\">Frequently Asked Questions</div>\r\n            <div class=\"underline\"></div>\r\n\r\n\r\n            <div class=\"hider\">\r\n                <div class=\"hiderHead\">\r\n                    <i class=\"material-icons\">keyboard_arrow_down</i>\r\n                    I'm having trouble signing in\r\n                </div>\r\n                <div class=\"hiderBody\">\r\n                    <p style=\"font-weight: bold;\">\r\n                        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Morbi leo urna molestie at elementum. Ut diam quam nulla porttitor massa id neque aliquam vestibulum. Vel turpis nunc eget lorem dolor sed viverra ipsum nunc. Vel pretium lectus quam id leo in vitae turpis. <br><br>Nisi scelerisque eu ultrices vitae. Venenatis lectus magna fringilla urna porttitor rhoncus dolor purus non. Eget aliquet nibh praesent tristique magna sit amet. Id diam maecenas ultricies mi eget mauris pharetra et. Egestas dui id ornare arcu odio ut sem nulla.\r\n                    </p>\r\n                </div>\r\n            </div>\r\n\r\n\r\n            <div class=\"hider\">\r\n                <div class=\"hiderHead\">\r\n                    <i class=\"material-icons\">keyboard_arrow_down</i>\r\n                    I'm having trouble signing in\r\n                </div>\r\n                <div class=\"hiderBody\">\r\n                    <p style=\"font-weight: bold;\">\r\n                        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Morbi leo urna molestie at elementum. Ut diam quam nulla porttitor massa id neque aliquam vestibulum. Vel turpis nunc eget lorem dolor sed viverra ipsum nunc. Vel pretium lectus quam id leo in vitae turpis. <br><br>Nisi scelerisque eu ultrices vitae. Venenatis lectus magna fringilla urna porttitor rhoncus dolor purus non. Eget aliquet nibh praesent tristique magna sit amet. Id diam maecenas ultricies mi eget mauris pharetra et. Egestas dui id ornare arcu odio ut sem nulla.\r\n                    </p>\r\n                </div>\r\n            </div>\r\n\r\n\r\n            <div class=\"hider\">\r\n                <div class=\"hiderHead\">\r\n                    <i class=\"material-icons\">keyboard_arrow_down</i>\r\n                    I forgot my password, How can I reset it?\r\n                </div>\r\n                <div class=\"hiderBody\">\r\n                    <p style=\"font-weight: bold;\">\r\n                        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Morbi leo urna molestie at elementum. Ut diam quam nulla porttitor massa id neque aliquam vestibulum. Vel turpis nunc eget lorem dolor sed viverra ipsum nunc. Vel pretium lectus quam id leo in vitae turpis. <br><br>Nisi scelerisque eu ultrices vitae. Venenatis lectus magna fringilla urna porttitor rhoncus dolor purus non. Eget aliquet nibh praesent tristique magna sit amet. Id diam maecenas ultricies mi eget mauris pharetra et. Egestas dui id ornare arcu odio ut sem nulla.\r\n                    </p>\r\n                </div>\r\n            </div>\r\n\r\n\r\n            <div class=\"hider\">\r\n                <div class=\"hiderHead\">\r\n                    <i class=\"material-icons\">keyboard_arrow_down</i>\r\n                    I'm not receiving emails from my course\r\n                </div>\r\n                <div class=\"hiderBody\">\r\n                    <p style=\"font-weight: bold;\">\r\n                        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Morbi leo urna molestie at elementum. Ut diam quam nulla porttitor massa id neque aliquam vestibulum. Vel turpis nunc eget lorem dolor sed viverra ipsum nunc. Vel pretium lectus quam id leo in vitae turpis. <br><br>Nisi scelerisque eu ultrices vitae. Venenatis lectus magna fringilla urna porttitor rhoncus dolor purus non. Eget aliquet nibh praesent tristique magna sit amet. Id diam maecenas ultricies mi eget mauris pharetra et. Egestas dui id ornare arcu odio ut sem nulla.\r\n                    </p>\r\n                </div>\r\n            </div>\r\n\r\n\r\n            <div class=\"hider\">\r\n                <div class=\"hiderHead\">\r\n                    <i class=\"material-icons\">keyboard_arrow_down</i>\r\n                    How can I reset my courses\r\n                </div>\r\n                <div class=\"hiderBody\">\r\n                    <p style=\"font-weight: bold;\">\r\n                        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Morbi leo urna molestie at elementum. Ut diam quam nulla porttitor massa id neque aliquam vestibulum. Vel turpis nunc eget lorem dolor sed viverra ipsum nunc. Vel pretium lectus quam id leo in vitae turpis. <br><br>Nisi scelerisque eu ultrices vitae. Venenatis lectus magna fringilla urna porttitor rhoncus dolor purus non. Eget aliquet nibh praesent tristique magna sit amet. Id diam maecenas ultricies mi eget mauris pharetra et. Egestas dui id ornare arcu odio ut sem nulla.\r\n                    </p>\r\n                </div>\r\n            </div>\r\n\r\n\r\n\r\n            <div class=\"bigHead red\">Didn't find what you're looking for?</div>\r\n            <div class=\"bigHead\" style=\"margin-top: 0px;\">report a problem below</div>\r\n\r\n\r\n\r\n\r\n            <form class=\"report\">\r\n                <label for=\"\">Your staff id:</label>\r\n                <input type=\"text\">\r\n\r\n                <label for=\"\">Subject:</label>\r\n                <input type=\"text\">\r\n\r\n                <label for=\"\">Describe the problem you encountered here</label>\r\n                <textarea name=\"\" id=\"\" cols=\"30\" rows=\"10\"></textarea>\r\n\r\n                <label for=\"\"><span>Upload a screenshot</span> <i class=\"red\">.jpg, .bmp, .png</i></label>\r\n                <input type=\"file\">\r\n\r\n\r\n                <button class=\"btnBlue btnMid\">Report</button>\r\n            </form>\r\n\r\n            </div>\r\n            <div class=\"col2\">\r\n\r\n\r\n                <div class=\"pod\">\r\n                    <div class=\"line borderBottomGrey\">\r\n                        Coming Up  <button class=\"btnBlue\"><i class=\"material-icons\">event</i> <span>View Calendar</span></button>\r\n                    </div>\r\n\r\n                    <div class=\"podBlock\">\r\n                        <div class=\"podItem\">\r\n                            <div class=\"coupon\"><i class=\"material-icons blue\">event</i></div>\r\n                            <div class=\"list\">\r\n                                <span class=\"blue\">General Staff Shop</span>\r\n                                <i>Jan 10, 2018</i>\r\n                            </div>\r\n                        </div>\r\n\r\n                        <div class=\"podItem\">\r\n                            <div class=\"coupon\"><i class=\"material-icons blue\">event</i></div>\r\n                            <div class=\"list\">\r\n                                <span class=\"blue\">Live Podcast</span>\r\n                                <i>Jan 10, 2018</i>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n\r\n                </div>\r\n\r\n\r\n\r\n                <div class=\"space\"></div>\r\n                <div class=\"pod\">\r\n                    <div class=\"line borderBottomGrey\">Quick Notification</div>\r\n\r\n\r\n                    <div class=\"podBlock\">\r\n                        <div class=\"podItem\">\r\n                            <div class=\"coupon\"><span class=\"redBackground\">05</span></div>\r\n                            <div class=\"list\">\r\n                                <span>Courses are overdue</span>\r\n                            </div>\r\n                        </div>\r\n\r\n                        <div class=\"podItem\">\r\n                            <div class=\"coupon\"><span class=\"yellowBackground\">15</span></div>\r\n                            <div class=\"list\">\r\n                                <span>Courses are going to expire in the next 30 days.</span>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n\r\n\r\n                <div class=\"space\"></div>\r\n\r\n\r\n                <div class=\"pod\">\r\n                    <div class=\"line borderBottomGrey\">Awards/Certificates</div>\r\n\r\n\r\n                    <div class=\"podBlock\">\r\n                        <div class=\"podItem\">\r\n                            <div class=\"half\">\r\n                                <div class=\"blue big\">CERTIFICATES </div><b>(3)</b>\r\n                                <p>Your latest certificates</p>\r\n                                <button class=\"btnBlue\">View all</button>\r\n                            </div>\r\n\r\n                            <div class=\"half\">\r\n                                <img src=\"/Content/img/c1.jpg\" alt=\"\">\r\n                                <img src=\"/Content/img/c2.jpg\" alt=\"\">\r\n                                <img src=\"/Content/img/c3.jpg\" alt=\"\">\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n\r\n\r\n            </div>\r\n\r\n\r\n        </div>\r\n</div>\r\n<emp-footer></emp-footer>"

/***/ }),

/***/ "../../../../../ClientApp/app/employee/support/support.componet.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var $ = __webpack_require__("../../../../jquery/dist/jquery.js");
var SupportComponent = (function () {
    function SupportComponent() {
    }
    SupportComponent.prototype.ngOnInit = function () {
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
                }
                else {
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
            $('.hider').click(function () {
                $('.hiderBody').hide();
                $('.hiderHead').css("background", "#aaa");
                $('.hiderHead i').html("keyboard_arrow_down");
                $('.hiderHead i', this).html("keyboard_arrow_up");
                $('.hiderHead', this).css("background", "#4286f4");
                $('.hiderBody', this).fadeIn();
            });
        });
    };
    SupportComponent = __decorate([
        core_1.Component({
            selector: '',
            template: __webpack_require__("../../../../../ClientApp/app/employee/support/support.component.html"),
            styles: [__webpack_require__("../../../../../ClientApp/app/employee/support/support.component.css")]
        })
    ], SupportComponent);
    return SupportComponent;
}());
exports.SupportComponent = SupportComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/employee/user/profile/profile.component.html":
/***/ (function(module, exports) {

module.exports = "<emp-header></emp-header>\r\n<div class=\"wrapper\">\r\n\r\n    <div class=\"breadcrum\"style=\"background-image: url('/Content/img/profile (1).jpg')\">\r\n        <div class=\"col8\"></div>\r\n\r\n        <div class=\"col4\">\r\n            <div class=\"headcrum\">Customer Service Representative</div>\r\n            <div class=\"bodycrum\">\r\n                <i class=\"material-icons\">room</i>  <i>Marina, Lagos</i>\r\n            </div>\r\n            <div class=\"bodycrum\">\r\n                <i style=\"font-size: 12px;margin-top: 3px;\">Staff ID: </i>  <b>001558</b>\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n\r\n\r\n    <div class=\"searchArea\" style=\"background:#b1cecd\">\r\n        <div class=\"searchbox\">\r\n            <input type=\"text\" placeholder=\"Search Courses\" /> <i class=\"material-icons\">search</i>\r\n        </div>\r\n    </div>\r\n\r\n\r\n\r\n\r\n    <div class=\"content\">\r\n\r\n        <div class=\"col12\">\r\n\r\n            <div class=\"long\">\r\n                <div class=\"line borderBottomBlue\">My Personal Information</div>\r\n            </div>\r\n\r\n\r\n            <div class=\"profile\">\r\n                <div class=\"prob\">\r\n                    <div class=\"label\">Staff ID:</div>\r\n                    <div class=\"input\"><input class=\"halfer\" type=\"text\" disabled value=\"001238\"></div>\r\n                </div>\r\n\r\n                <div class=\"prob\">\r\n                    <div class=\"label\">First Name:</div>\r\n                    <div class=\"input\"><input type=\"text\" disabled value=\"SAMUEL OLUWAFEMI\"></div>\r\n                </div>\r\n\r\n                <div class=\"prob\">\r\n                    <div class=\"label\">Last Name:</div>\r\n                    <div class=\"input\"><input type=\"text\" disabled value=\"OLUMOYEKE\"></div>\r\n                </div>\r\n\r\n                <div class=\"prob\">\r\n                    <div class=\"label\">Gender:</div>\r\n                    <div class=\"input\"><input type=\"text\" class=\"halfer\" disabled value=\"MALE\"></div>\r\n                </div>\r\n\r\n                <div class=\"prob\">\r\n                    <div class=\"label\">Username:</div>\r\n                    <div class=\"input\"><input type=\"text\" disabled value=\"solumoyeke\"></div>\r\n                </div>\r\n\r\n                <div class=\"prob\">\r\n                    <div class=\"label\">Email:</div>\r\n                    <div class=\"input\"><input type=\"text\" disabled value=\"samuelolumoyeke@sterlingbankng.com\"></div>\r\n                </div>\r\n\r\n                <div class=\"prob\">\r\n                    <div class=\"label\">Phone:</div>\r\n                    <div class=\"input\">\r\n                        <input class=\"halfer\" type=\"text\" disabled value=\"08012345678\">\r\n                        <input class=\"halfer\" type=\"text\" disabled value=\"\">\r\n                    </div>\r\n                </div>\r\n\r\n                <div class=\"prob\">\r\n                    <div class=\"label\">Residence Address:</div>\r\n                    <div class=\"input\"><input type=\"text\" disabled value=\"10, Ogundare Street\"></div>\r\n                </div>\r\n\r\n                <div class=\"prob\">\r\n                    <div class=\"label\">City:</div>\r\n                    <div class=\"input\"><input type=\"text\" disabled value=\"Lagos Mainland\"></div>\r\n                </div>\r\n\r\n                <div class=\"prob\">\r\n                    <div class=\"label\">State:</div>\r\n                    <div class=\"input\"><input type=\"text\" disabled value=\"Lagos\"></div>\r\n                </div>\r\n\r\n                <div class=\"prob\">\r\n                    <div class=\"label\">Country:</div>\r\n                    <div class=\"input\"><input type=\"text\" disabled value=\"Nigeria\"></div>\r\n                </div>\r\n\r\n                <div class=\"prob\">\r\n                    <div class=\"label\">Location:</div>\r\n                    <div class=\"input\"><input type=\"text\" disabled value=\"HEAD OFFICE\"></div>\r\n                </div>\r\n\r\n                <div class=\"prob\">\r\n                    <div class=\"label\">Department:</div>\r\n                    <div class=\"input\"><input type=\"text\" disabled value=\"Human Resource Management\"></div>\r\n                </div>\r\n\r\n\r\n                <div class=\"prob\">\r\n                    <div class=\"label\">Job Title:</div>\r\n                    <div class=\"input\"><input type=\"text\" disabled value=\"HRM - Career Development and Talent Management\"></div>\r\n                </div>\r\n\r\n                <div class=\"prob\">\r\n                    <div class=\"label\">Role:</div>\r\n                    <div class=\"input\"><input type=\"text\" disabled value=\"Senior Executive\"></div>\r\n                </div>\r\n\r\n\r\n            </div>\r\n\r\n        </div>\r\n    </div>\r\n</div>\r\n<emp-footer></emp-footer>"

/***/ }),

/***/ "../../../../../ClientApp/app/employee/user/profile/profile.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var $ = __webpack_require__("../../../../jquery/dist/jquery.js");
var ProfileComponent = (function () {
    function ProfileComponent() {
    }
    ProfileComponent.prototype.ngOnInit = function () {
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
                }
                else {
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
    };
    ProfileComponent = __decorate([
        core_1.Component({
            template: __webpack_require__("../../../../../ClientApp/app/employee/user/profile/profile.component.html"),
        })
    ], ProfileComponent);
    return ProfileComponent;
}());
exports.ProfileComponent = ProfileComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/employee/user/user.module.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var router_1 = __webpack_require__("../../../router/esm5/router.js");
var forms_1 = __webpack_require__("../../../forms/esm5/forms.js");
var shared_module_1 = __webpack_require__("../../../../../ClientApp/app/shared/shared.module.ts");
var ngx_toastr_1 = __webpack_require__("../../../../ngx-toastr/toastr.es5.js");
var animations_1 = __webpack_require__("../../../platform-browser/esm5/animations.js");
var profile_component_1 = __webpack_require__("../../../../../ClientApp/app/employee/user/profile/profile.component.ts");
var auth_guard_service_1 = __webpack_require__("../../../../../ClientApp/app/guard/auth-guard.service.ts");
var login_component_1 = __webpack_require__("../../../../../ClientApp/app/authentication/login/login.component.ts");
var auth_service_1 = __webpack_require__("../../../../../ClientApp/app/service/auth.service.ts");
var UserModule = (function () {
    function UserModule() {
    }
    UserModule = __decorate([
        core_1.NgModule({
            imports: [
                shared_module_1.SharedModule,
                forms_1.ReactiveFormsModule,
                forms_1.FormsModule,
                animations_1.BrowserAnimationsModule,
                ngx_toastr_1.ToastrModule.forRoot({
                    progressBar: true,
                    timeOut: 4000,
                    preventDuplicates: true
                }),
                router_1.RouterModule.forChild([
                    { path: 'profile', component: profile_component_1.ProfileComponent },
                    { path: 'login', component: login_component_1.LoginComponent }
                ])
            ],
            declarations: [
                profile_component_1.ProfileComponent,
                login_component_1.LoginComponent
            ],
            providers: [
                auth_guard_service_1.AuthGuard,
                auth_service_1.AuthService
            ]
        })
    ], UserModule);
    return UserModule;
}());
exports.UserModule = UserModule;


/***/ }),

/***/ "../../../../../ClientApp/app/guard/auth-guard.service.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var router_1 = __webpack_require__("../../../router/esm5/router.js");
var auth_service_1 = __webpack_require__("../../../../../ClientApp/app/service/auth.service.ts");
var AuthGuard = (function () {
    function AuthGuard(authService, router) {
        this.authService = authService;
        this.router = router;
    }
    AuthGuard.prototype.canActivate = function (route, state) {
        return this.checkLoggedIn(state.url);
    };
    AuthGuard.prototype.checkLoggedIn = function (url) {
        if (this.authService.isLoggedIn()) {
            return true;
        }
        this.authService.redirectUrl = url;
        this.router.navigate(["/login"]);
        return false;
    };
    AuthGuard = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [auth_service_1.AuthService, router_1.Router])
    ], AuthGuard);
    return AuthGuard;
}());
exports.AuthGuard = AuthGuard;


/***/ }),

/***/ "../../../../../ClientApp/app/pipes/timedisplay.pipe.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var TimeDisplayPipe = (function () {
    function TimeDisplayPipe() {
    }
    TimeDisplayPipe.prototype.transform = function (value) {
        var args = [];
        for (var _i = 1; _i < arguments.length; _i++) {
            args[_i - 1] = arguments[_i];
        }
        var hh = Math.floor(value / 3600);
        var mm = Math.floor(value / 60) % 60;
        var ss = Math.floor(value) % 60;
        return hh + ":" + (mm < 10 ? "0" : "") + mm + ":" + (ss < 10 ? "0" : "") + ss;
    };
    TimeDisplayPipe = __decorate([
        core_1.Pipe({ name: 'timeDisplay' })
    ], TimeDisplayPipe);
    return TimeDisplayPipe;
}());
exports.TimeDisplayPipe = TimeDisplayPipe;


/***/ }),

/***/ "../../../../../ClientApp/app/service/auth.service.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var ngx_toastr_1 = __webpack_require__("../../../../ngx-toastr/toastr.es5.js");
var AuthService = (function () {
    function AuthService(toastr) {
        this.toastr = toastr;
    }
    AuthService.prototype.isLoggedIn = function () {
        return !!this.currentUser;
    };
    AuthService.prototype.login = function (email, password) {
        this.currentUser = {
            id: 2,
            email: email,
            password: password
        };
        console.log("User: " + this.currentUser.email + " logged in");
    };
    AuthService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [ngx_toastr_1.ToastrService])
    ], AuthService);
    return AuthService;
}());
exports.AuthService = AuthService;


/***/ }),

/***/ "../../../../../ClientApp/app/service/course.service.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var http_1 = __webpack_require__("../../../http/esm5/http.js");
var Observable_1 = __webpack_require__("../../../../rxjs/_esm5/Observable.js");
__webpack_require__("../../../../rxjs/_esm5/add/operator/do.js");
__webpack_require__("../../../../rxjs/_esm5/add/operator/catch.js");
__webpack_require__("../../../../rxjs/_esm5/add/observable/throw.js");
__webpack_require__("../../../../rxjs/_esm5/add/operator/map.js");
__webpack_require__("../../../../rxjs/_esm5/add/observable/of.js");
var ngx_toastr_1 = __webpack_require__("../../../../ngx-toastr/toastr.es5.js");
var CourseService = (function () {
    function CourseService(http, toastr) {
        this.http = http;
        this.toastr = toastr;
        this.infoUrl = "/api/course/getcourseinfo";
        this.firstModuleUrl = "/api/course/getfirstmodule";
        this.topicUrl = "/api/course/gettopic";
        this.indLoading = false;
    }
    CourseService.prototype.getAllCourses = function () {
        var _this = this;
        this.indLoading = true;
        return this.http.get("/api/course/getcourses")
            .map(function (x) { return _this.courses = x.json().Result; })
            .catch(this.handleError);
    };
    CourseService.prototype.getCourse = function (id) {
        var _this = this;
        var url = this.infoUrl + "?id=" + id;
        return this.http.get(url)
            .map(function (course) { return _this.course = course.json().Result; })
            .catch(this.handleError);
    };
    CourseService.prototype.getFirstModule = function (id) {
        var _this = this;
        var url = this.firstModuleUrl + "?id=" + id;
        return this.http.get(url)
            .map(function (course) { return _this.course = course.json().Result; })
            .catch(this.handleError);
    };
    CourseService.prototype.getTopic = function (id) {
        var _this = this;
        var url = this.topicUrl + "?id=" + id;
        return this.http.get(url)
            .map(function (topic) { return _this.topic = topic.json().Result; })
            .catch(this.handleError);
    };
    CourseService.prototype.handleError = function (error) {
        this.toastr.error("Error Fetching files from server");
        return Observable_1.Observable.throw(error.json().error || 'Error Fetching file from server');
    };
    CourseService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.Http, ngx_toastr_1.ToastrService])
    ], CourseService);
    return CourseService;
}());
exports.CourseService = CourseService;


/***/ }),

/***/ "../../../../../ClientApp/app/service/video.service.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var http_1 = __webpack_require__("../../../http/esm5/http.js");
__webpack_require__("../../../../rxjs/_esm5/add/operator/map.js");
var VideoService = (function () {
    function VideoService(http) {
        var _this = this;
        this.http = http;
        this.currentPath = "";
        this.currentTitle = "loading ...";
        this.currentTime = 0;
        this.totalTime = 0;
        this.isMuted = false;
        this.isPlaying = false;
        this.isDragging = false;
        this.showDetails = false;
        this.currentDesc = "A very nice video...";
        this.infoUrl = "/api/course/getallvideos";
        this.dragStart = function (e) {
            this.isDragging = true;
        };
        this.dragMove = function (e) {
            if (this.isDragging) {
                this.calculatedWidth = e.x;
            }
        };
        this.dragStop = function (e) {
            if (this.isDragging) {
                this.isDragging = false;
                this.seekVideo();
            }
        };
        this.updateData = function (e) {
            _this.totalTime = _this.videoElement.duration;
        };
        this.updateTime = function (e) {
            _this.currentTime = _this.videoElement.currentTime;
        };
        this.timerFired = function () {
            if (!_this.isDragging) {
                _this.calculatedScrubY = _this.videoElement.offsetHeight;
                var t = _this.videoElement.currentTime;
                var d = _this.videoElement.duration;
                _this.calculatedWidth = (t / d * _this.videoElement.offsetWidth);
            }
        };
    }
    VideoService.prototype.appSetup = function (v) {
        this.videoElement = document.getElementById(v);
        this.videoElement.addEventListener("loadedmetadata", this.updateData);
        this.videoElement.addEventListener("timeupdate", this.updateTime);
        window.setInterval(this.timerFired, 500);
    };
    VideoService.prototype.getVideos = function (id) {
        var _this = this;
        var url = this.infoUrl + "?id=" + id;
        return this.http.get(url)
            .map(function (course) { return _this.videos = course.json().Result; })
            .subscribe(function (data) {
            _this.videos = data;
            _this.selectedVideo(0, 0);
        });
    };
    VideoService.prototype.selectedVideo = function (i, n) {
        this.currentTitle = this.videos.Modules[i].Topics[n]['TopicName'];
        this.videoElement.src = this.videos.Modules[i].Topics[n]["ContentUrl"];
        this.videoElement.pause();
        this.isPlaying = false;
        console.log("The current title: " + this.currentTitle);
        console.log("The current src: " + this.videoElement.src);
    };
    VideoService.prototype.seekVideo = function (e) {
        var w = document.getElementById("progressMeterFull").offsetWidth;
        var d = this.videoElement.duration;
        var s = Math.round(e.pageX / w * d);
        this.videoElement.currentTime = s;
    };
    VideoService.prototype.playVideo = function () {
        if (this.videoElement.paused) {
            this.videoElement.play();
            this.isPlaying = true;
        }
        else {
            this.videoElement.pause();
            this.isPlaying = false;
        }
    };
    VideoService.prototype.muteVideo = function () {
        if (this.videoElement.volume == 0) {
            this.videoElement.volume = 1;
            this.isMuted = false;
        }
        else {
            this.videoElement.volume = 0;
            this.isMuted = true;
        }
    };
    VideoService.prototype.fullScreen = function () {
        if (this.videoElement.requestFullscreen) {
            this.videoElement.requestFullscreen();
        }
        else if (this.videoElement.mozRequestFullscreen) {
            this.videoElement.mozRequestFullScreen();
        }
        else if (this.videoElement.webkitRequestFullscreen) {
            this.videoElement.webkitRequestFullscreen();
        }
        else if (this.videoElement.msRequestFullscreen) {
            this.videoElement.msRequestFullscreen();
        }
    };
    VideoService.prototype.details = function () {
        if (this.showDetails == false) {
            this.showDetails = true;
        }
        else {
            this.showDetails = false;
        }
    };
    VideoService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.Http])
    ], VideoService);
    return VideoService;
}());
exports.VideoService = VideoService;


/***/ }),

/***/ "../../../../../ClientApp/app/shared/admin-footer/admin-footer.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../ClientApp/app/shared/admin-footer/admin-footer.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"pallete1\">\r\n    <footer>\r\n        <div class=\"foot\">&copy;{{year}} - LEARNING MANAGEMENT SYSTEM</div>\r\n        <div class=\"power\">\r\n\r\n        </div>\r\n    </footer>\r\n</div>\r\n"

/***/ }),

/***/ "../../../../../ClientApp/app/shared/admin-footer/admin-footer.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var AdminFooterComponent = (function () {
    function AdminFooterComponent() {
    }
    AdminFooterComponent.prototype.ngOnInit = function () {
        this.year = new Date().getFullYear();
    };
    AdminFooterComponent = __decorate([
        core_1.Component({
            selector: 'admin-footer',
            template: __webpack_require__("../../../../../ClientApp/app/shared/admin-footer/admin-footer.component.html"),
            styles: [__webpack_require__("../../../../../ClientApp/app/shared/admin-footer/admin-footer.component.css")]
        })
    ], AdminFooterComponent);
    return AdminFooterComponent;
}());
exports.AdminFooterComponent = AdminFooterComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/shared/admin-header/admin-header.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".menuTab1 ul {\r\n    padding: 0;\r\n    width:100%\r\n}\r\n\r\n    .menuTab1 ul li {\r\n        width: 100%;\r\n    }\r\n.menuTab1 .link-active .pageTab1 {\r\n    background: #AB192D;\r\n}\r\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../ClientApp/app/shared/admin-header/admin-header.component.html":
/***/ (function(module, exports) {

module.exports = "<!-- Messages Popup -->\r\n<div class=\"msgBox popBox\">\r\n    <div class=\"hD\">Messages</div>\r\n    <div class=\"bD\">\r\n\r\n        <!-- INfo Tab -->\r\n        <div class=\"infoTab\">\r\n            <b>Admin</b>\r\n            <a href=\"#\">\r\n                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>\r\n            </a>\r\n            <div class=\"infoAct\">Mark as read</div>\r\n        </div>\r\n        <!-- end of InfoTab -->\r\n        <!-- INfo Tab -->\r\n        <div class=\"infoTab\">\r\n            <b>Admin</b>\r\n            <a href=\"#\">\r\n                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>\r\n            </a>\r\n            <div class=\"infoAct\">Mark as read</div>\r\n        </div>\r\n        <!-- end of InfoTab -->\r\n        <!-- INfo Tab -->\r\n        <div class=\"infoTab\">\r\n            <b>Admin</b>\r\n            <a href=\"#\">\r\n                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>\r\n            </a>\r\n            <div class=\"infoAct\">Mark as read</div>\r\n        </div>\r\n        <!-- end of InfoTab -->\r\n        <!-- INfo Tab -->\r\n        <div class=\"infoTab\">\r\n            <b>Admin</b>\r\n            <a href=\"#\">\r\n                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>\r\n            </a>\r\n            <div class=\"infoAct\">Mark as read</div>\r\n        </div>\r\n        <!-- end of InfoTab -->\r\n        <!-- INfo Tab -->\r\n        <div class=\"infoTab\">\r\n            <b>Admin</b>\r\n            <a href=\"#\">\r\n                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>\r\n            </a>\r\n            <div class=\"infoAct\">Mark as read</div>\r\n        </div>\r\n        <!-- end of InfoTab -->\r\n        <!-- INfo Tab -->\r\n        <div class=\"infoTab\">\r\n            <b>Admin</b>\r\n            <a href=\"#\">\r\n                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>\r\n            </a>\r\n            <div class=\"infoAct\">Mark as read</div>\r\n        </div>\r\n        <!-- end of InfoTab -->\r\n\r\n\r\n\r\n\r\n    </div>\r\n\r\n    <a class=\"eD\" href=\"#\">\r\n        View All Messages\r\n    </a>\r\n</div>\r\n<!-- end of Messages Popup -->\r\n<!-- Notification Popup -->\r\n\r\n<div class=\"notyBox popBox\">\r\n    <div class=\"hD\">Notification</div>\r\n    <div class=\"bD\">\r\n\r\n        <!-- INfo Tab -->\r\n        <div class=\"infoTab\">\r\n            <a href=\"#\">\r\n                <p>You have just <b>3 days</b> to complete your <b>Employment Changes in Tech</b> Course</p>\r\n            </a>\r\n            <div class=\"infoAct\">View</div>\r\n        </div>\r\n        <!-- end of InfoTab -->\r\n\r\n\r\n\r\n    </div>\r\n\r\n    <a class=\"eD\" href=\"#\">\r\n        View All Notifications\r\n    </a>\r\n</div>\r\n\r\n<!-- end of Notification Popup -->\r\n<!-- profile box -->\r\n\r\n<div class=\"probBox\">\r\n    <a href=\"profile\">\r\n        <span class=\"probTab\">Profile</span>\r\n    </a>\r\n\r\n    <a href=\"\">\r\n        <span class=\"probTab\">Logout</span>\r\n    </a>\r\n</div>\r\n\r\n<!-- end of profile box -->\r\n\r\n\r\n\r\n\r\n<header class=\"second\">\r\n    <i class=\"material-icons menuBtn1\">menu</i>\r\n    <img src=\"/Content//img/retro.png\" alt=\"\">\r\n    <img src=\"/Content/img/logo.png\" class=\"marginImg\" alt=\"\">\r\n    <ul>\r\n        <li class=\"msg\">\r\n            <i class=\"material-icons\">email</i><span class=\"redBackground\">2</span>\r\n        </li>\r\n        <li class=\"noty\">\r\n            <i class=\"material-icons\">notifications_none</i><span>19</span>\r\n        </li>\r\n        <li class=\"user\">\r\n            <i class=\"material-icons\">person</i>\r\n            <h6>Welcome <b>Samuel</b></h6>  <i class=\"material-icons\">arrow_drop_down</i>\r\n        </li>\r\n\r\n        <li>\r\n            <a href=\"#\"><i class=\"material-icons\">settings</i></a>\r\n        </li>\r\n    </ul>\r\n</header>\r\n\r\n\r\n\r\n\r\n\r\n<div class=\"menuTab1\">\r\n    <ul>\r\n        <li [routerLinkActive]=\"['link-active']\" [routerLinkActiveOptions]=\"{exact: true}\">\r\n            <a [routerLink]=\"['/admin/dashboard']\">\r\n                <div class=\"pageTab1\">\r\n                    <i class=\"material-icons\">library_books</i>\r\n                    <span>Manage Courses</span>\r\n                </div>\r\n            </a>\r\n        </li>\r\n        <li>\r\n            <a href=\"#\">\r\n                <div class=\"pageTab1\">\r\n                    <i class=\"material-icons\">folder</i>\r\n                    <span>Employee Records</span>\r\n                </div>\r\n            </a>\r\n\r\n        </li>\r\n        <li>\r\n            <a href=\"#\">\r\n                <div class=\"pageTab1\">\r\n                    <i class=\"material-icons\">email</i>\r\n                    <span>Messages</span>\r\n                </div>\r\n            </a>\r\n\r\n        </li>\r\n        <li>\r\n            <a href=\"#\">\r\n                <div class=\"pageTab1\">\r\n                    <i class=\"material-icons\">event</i>\r\n                    <span>Training Management</span>\r\n                </div>\r\n            </a>\r\n\r\n        </li>\r\n        <li>\r\n            <a href=\"#\">\r\n                <div class=\"pageTab1\">\r\n                    <i class=\"material-icons\">settings</i>\r\n                    <span>Support Center</span>\r\n                </div>\r\n            </a>\r\n\r\n        </li>\r\n        <li>\r\n            <a href=\"#\">\r\n                <div class=\"pageTab1\">\r\n                    <i class=\"material-icons\">list</i>\r\n                    <span>Reports</span>\r\n                </div>\r\n            </a>\r\n\r\n        </li>\r\n        <li [routerLinkActive]=\"['link-active']\" [routerLinkActiveOptions]=\"{exact: true}\">\r\n            <a [routerLink]=\"['/admin/multi-tenant']\">\r\n                <div class=\"pageTab1\">\r\n                    <i class=\"material-icons\">star</i>\r\n                    <span>Multi-Tenant LMS</span>\r\n                </div>\r\n            </a>\r\n\r\n        </li>\r\n        <li>\r\n            <a href=\"#\">\r\n                <div class=\"pageTab1\">\r\n                    <i class=\"material-icons\">content_copy</i>\r\n                    <span>Examination</span>\r\n                </div>\r\n            </a>\r\n\r\n        </li>\r\n        <li>\r\n            <a href=\"#\">\r\n                <div class=\"pageTab1\">\r\n                    <i class=\"material-icons\">group</i>\r\n                    <span>User Management</span>\r\n                </div>\r\n            </a>\r\n\r\n        </li>\r\n        <li>\r\n            <a href=\"#\">\r\n                <div class=\"pageTab1\">\r\n                    <i class=\"material-icons\">payment</i>\r\n                    <span>Surveys/Course Feedbacks</span>\r\n                </div>\r\n            </a>\r\n\r\n        </li>\r\n        <li>\r\n            <a href=\"#\">\r\n                <div class=\"pageTab1\">\r\n                    <i class=\"material-icons\">work</i>\r\n                    <span>Advert Management</span>\r\n                </div>\r\n            </a>\r\n\r\n        </li>\r\n        <li>\r\n            <a href=\"#\">\r\n                <div class=\"pageTab1\">\r\n                    <i class=\"material-icons\">wifi</i>\r\n                    <span>Discussion Management</span>\r\n                </div>\r\n            </a>\r\n\r\n        </li>\r\n    </ul>\r\n</div>\r\n\r\n"

/***/ }),

/***/ "../../../../../ClientApp/app/shared/admin-header/admin-header.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var AdminHeaderComponent = (function () {
    function AdminHeaderComponent() {
    }
    AdminHeaderComponent.prototype.ngOnInit = function () {
    };
    AdminHeaderComponent = __decorate([
        core_1.Component({
            selector: 'admin-header',
            template: __webpack_require__("../../../../../ClientApp/app/shared/admin-header/admin-header.component.html"),
            styles: [__webpack_require__("../../../../../ClientApp/app/shared/admin-header/admin-header.component.css")]
        })
    ], AdminHeaderComponent);
    return AdminHeaderComponent;
}());
exports.AdminHeaderComponent = AdminHeaderComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/shared/employee-footer/emp-footer.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../ClientApp/app/shared/employee-footer/emp-footer.component.html":
/***/ (function(module, exports) {

module.exports = "<footer>\r\n    <div class=\"foot\">&copy;{{year}} - LEARNING MANAGEMENT SYSTEM</div>\r\n    <div class=\"power\"><img src=\"/Content/img/sbsc.png\" alt=\"\"><span>Powered by: </span></div>\r\n</footer>\r\n"

/***/ }),

/***/ "../../../../../ClientApp/app/shared/employee-footer/emp-footer.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var EmployeeFooterComponent = (function () {
    function EmployeeFooterComponent() {
    }
    EmployeeFooterComponent.prototype.ngOnInit = function () {
        this.year = new Date().getFullYear();
    };
    EmployeeFooterComponent = __decorate([
        core_1.Component({
            selector: 'emp-footer',
            template: __webpack_require__("../../../../../ClientApp/app/shared/employee-footer/emp-footer.component.html"),
            styles: [__webpack_require__("../../../../../ClientApp/app/shared/employee-footer/emp-footer.component.css")]
        })
    ], EmployeeFooterComponent);
    return EmployeeFooterComponent;
}());
exports.EmployeeFooterComponent = EmployeeFooterComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/shared/employee-header/emp-header.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "\r\n.sack .link-active .pageTab {\r\n    border: 2px solid #666;\r\n}\r\n\r\n\r\n.sack ul {\r\n    padding: 0;\r\n}\r\n\r\n.sack ul li {\r\n    width: 100%;\r\n}\r\n\r\n\r\n.sack .link-active .pageTab .tabing {\r\n    background: #961c1d;\r\n}\r\n\r\n\r\n    /*.pageTab:nth-child(1) .tabing {\r\n        background: #961c1d;\r\n    }*/\r\n.pageTab:nth-child(2) {\r\n    border: 2px solid #666;\r\n}\r\n\r\n    .pageTab:nth-child(2) .tabing {\r\n        background: #961c1d;\r\n    }\r\n\r\n.pageTab:nth-child(3) {\r\n    border: 2px solid #666;\r\n}\r\n\r\n    .pageTab:nth-child(3) .tabing {\r\n        background: #961c1d;\r\n    }\r\n@media(max-width: 500px) {\r\n    .pageTab:nth-child(2) {\r\n        border: 0px solid transparent;\r\n    }\r\n\r\n        .pageTab:nth-child(2) .tabing {\r\n            background: transparent;\r\n        }\r\n}\r\n\r\n/*.link-active a,\r\n.link-active a:hover,\r\n.link-active a:focus {\r\n    background-color: #4189C7;\r\n    color: white;\r\n}*/\r\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../ClientApp/app/shared/employee-header/emp-header.component.html":
/***/ (function(module, exports) {

module.exports = "\r\n<div>\r\n    <header>\r\n        <i class=\"material-icons menuBtn\">menu</i>\r\n        <img src=\"/Content/img/retro.png\" alt=\"\">\r\n        <img src=\"/Content/img/logo.png\" class=\"marginImg\" alt=\"\">\r\n\r\n        <ul>\r\n            <li class=\"msg\"><i class=\"material-icons\">email</i><span class=\"redBackground\">2</span></li>\r\n            <li class=\"noty\"><i class=\"material-icons\">notifications_none</i><span>3</span></li>\r\n            <li class=\"user\"><i class=\"material-icons\">person</i><h6>Welcome <b>{{LoggedInUser}}</b></h6> <i class=\"material-icons\">arrow_drop_down</i></li>\r\n            <li>\r\n                <a [routerLink]=\"['/admin/dashboard']\"><i class=\"material-icons\">settings</i></a>\r\n            </li>\r\n\r\n        </ul>\r\n    </header>\r\n</div>\r\n\r\n<div class=\"menuTab\">\r\n    <div class=\"sack\">\r\n\r\n        <ul>\r\n            <li [routerLinkActive]=\"['link-active']\" [routerLinkActiveOptions]=\"{exact: true}\">\r\n                <a [routerLink]=\"['/home']\">\r\n                    <div class=\"pageTab\">\r\n                        <div class=\"tabing\"><i class=\"material-icons\">library_books</i><span>My Courses</span></div>\r\n                    </div>\r\n                </a>\r\n            </li>\r\n            <li [routerLinkActive]=\"['link-active']\" [routerLinkActiveOptions]=\"{exact: true}\">\r\n                <a [routerLink]=\"['/profile']\">\r\n                    <div class=\"pageTab\">\r\n                        <div class=\"tabing\"><i class=\"material-icons\">library_books</i><span>My profile</span></div>\r\n                    </div>\r\n                </a>\r\n            </li>\r\n\r\n            <li [routerLinkActive]=\"['link-active']\" [routerLinkActiveOptions]=\"{exact: true}\">\r\n                <a [routerLink]=\"['/course']\">\r\n                    <div class=\"pageTab\">\r\n                        <div class=\"tabing\"><i class=\"material-icons\">library_books</i><span>\r\n                              Course Catalog</span></div>\r\n                    </div>\r\n                </a>\r\n            </li>\r\n\r\n            <li [routerLinkActive]=\"['link-active']\" [routerLinkActiveOptions]=\"{exact: true}\">\r\n                <a [routerLink]=\"['/all_videos']\">\r\n                    <div class=\"pageTab\">\r\n                        <div class=\"tabing\"><i class=\"material-icons\">library_books</i><span>My videos</span></div>\r\n                    </div>\r\n                </a>\r\n            </li>\r\n\r\n            <li>\r\n                <a>\r\n                    <div class=\"pageTab\">\r\n                        <div class=\"tabing\"><i class=\"material-icons\">people</i><span>Forums</span></div>\r\n                    </div>\r\n                </a>\r\n            </li>\r\n\r\n            <li [routerLinkActive]=\"['link-active']\" [routerLinkActiveOptions]=\"{exact: true}\">\r\n                <a [routerLink]=\"['/certificate']\">\r\n                    <div class=\"pageTab\">\r\n                        <div class=\"tabing\"><i class=\"material-icons\">turned_in</i><span>My Certificates</span></div>\r\n                    </div>\r\n                </a>\r\n            </li>\r\n\r\n            <li [routerLinkActive]=\"['link-active']\" [routerLinkActiveOptions]=\"{exact: true}\">\r\n                <a [routerLink]=\"['/support']\">\r\n                    <div class=\"pageTab\">\r\n                        <div class=\"tabing\"><i class=\"material-icons\">language</i><span>Support Center</span></div>\r\n                    </div>\r\n                </a>\r\n\r\n            </li>\r\n\r\n            <li>\r\n                <a>\r\n                    <div class=\"pageTab\">\r\n                        <div class=\"tabing\"><i class=\"material-icons\">today</i><span>Learning Calendar</span></div>\r\n                    </div>\r\n                </a>\r\n            </li>\r\n\r\n            <li [routerLinkActive]=\"['link-active']\">\r\n                <a>\r\n                    <div class=\"pageTab mylog\">\r\n                        <div class=\"tabing\">\r\n                            <i class=\"material-icons\">power_settings_new</i><span>Logout</span>\r\n                        </div>\r\n                    </div>\r\n                </a>\r\n            </li>\r\n\r\n        </ul>\r\n\r\n    </div>\r\n    <div class=\"log smack\">\r\n        <a href=\"#\"><i class=\"material-icons\">power_settings_new</i> <span>Logout</span></a>\r\n    </div>\r\n</div>\r\n"

/***/ }),

/***/ "../../../../../ClientApp/app/shared/employee-header/emp-header.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var EmployeeHeaderComponent = (function () {
    function EmployeeHeaderComponent() {
    }
    EmployeeHeaderComponent.prototype.ngOnInit = function () {
    };
    EmployeeHeaderComponent = __decorate([
        core_1.Component({
            selector: 'emp-header',
            template: __webpack_require__("../../../../../ClientApp/app/shared/employee-header/emp-header.component.html"),
            styles: [__webpack_require__("../../../../../ClientApp/app/shared/employee-header/emp-header.component.css")]
        })
    ], EmployeeHeaderComponent);
    return EmployeeHeaderComponent;
}());
exports.EmployeeHeaderComponent = EmployeeHeaderComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/shared/general-header/gen-header.component.html":
/***/ (function(module, exports) {

module.exports = "<header>\r\n    <i class=\"material-icons menuBtn\">menu</i>\r\n    <img src=\"/Content/img/retro.png\" alt=\"\">\r\n    <img src=\"/Content/img/logo.png\" class=\"marginImg\" alt=\"\">\r\n\r\n    <ul>\r\n        <li class=\"msg\"><i class=\"material-icons\">email</i><span class=\"redBackground\">2</span></li>\r\n        <li class=\"noty\"><i class=\"material-icons\">notifications_none</i><span>3</span></li>\r\n        <li class=\"user\"><i class=\"material-icons\">person</i><h6>Welcome <b>{{LoggedInUser}}</b></h6> <i class=\"material-icons\">arrow_drop_down</i></li>\r\n        <li>\r\n            <a href=\"#\"><i class=\"material-icons\">settings</i></a>\r\n        </li>\r\n\r\n    </ul>\r\n</header>"

/***/ }),

/***/ "../../../../../ClientApp/app/shared/general-header/gen-header.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var GeneralHeaderComponent = (function () {
    function GeneralHeaderComponent() {
    }
    GeneralHeaderComponent.prototype.ngOnInit = function () {
    };
    GeneralHeaderComponent = __decorate([
        core_1.Component({
            selector: 'gen-header',
            template: __webpack_require__("../../../../../ClientApp/app/shared/general-header/gen-header.component.html"),
        })
    ], GeneralHeaderComponent);
    return GeneralHeaderComponent;
}());
exports.GeneralHeaderComponent = GeneralHeaderComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/shared/shared.module.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var common_1 = __webpack_require__("../../../common/esm5/common.js");
var forms_1 = __webpack_require__("../../../forms/esm5/forms.js");
var sidebar_component_1 = __webpack_require__("../../../../../ClientApp/app/shared/sidebar.component.ts");
var emp_header_component_1 = __webpack_require__("../../../../../ClientApp/app/shared/employee-header/emp-header.component.ts");
var emp_footer_component_1 = __webpack_require__("../../../../../ClientApp/app/shared/employee-footer/emp-footer.component.ts");
var router_1 = __webpack_require__("../../../router/esm5/router.js");
var gen_header_component_1 = __webpack_require__("../../../../../ClientApp/app/shared/general-header/gen-header.component.ts");
var admin_footer_component_1 = __webpack_require__("../../../../../ClientApp/app/shared/admin-footer/admin-footer.component.ts");
var admin_header_component_1 = __webpack_require__("../../../../../ClientApp/app/shared/admin-header/admin-header.component.ts");
var spec_footer_component_1 = __webpack_require__("../../../../../ClientApp/app/shared/spec-footer/spec-footer.component.ts");
var SharedModule = (function () {
    function SharedModule() {
    }
    SharedModule = __decorate([
        core_1.NgModule({
            imports: [common_1.CommonModule, router_1.RouterModule],
            exports: [
                common_1.CommonModule,
                forms_1.FormsModule,
                sidebar_component_1.SidebarComponent,
                emp_header_component_1.EmployeeHeaderComponent,
                emp_footer_component_1.EmployeeFooterComponent,
                admin_footer_component_1.AdminFooterComponent,
                admin_header_component_1.AdminHeaderComponent,
                gen_header_component_1.GeneralHeaderComponent,
                spec_footer_component_1.SpecFooterComponent
            ],
            declarations: [sidebar_component_1.SidebarComponent, spec_footer_component_1.SpecFooterComponent, emp_header_component_1.EmployeeHeaderComponent, emp_footer_component_1.EmployeeFooterComponent, admin_footer_component_1.AdminFooterComponent, admin_header_component_1.AdminHeaderComponent, gen_header_component_1.GeneralHeaderComponent],
        })
    ], SharedModule);
    return SharedModule;
}());
exports.SharedModule = SharedModule;


/***/ }),

/***/ "../../../../../ClientApp/app/shared/sidebar.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"col2\">\r\n\r\n\r\n    <div class=\"pod\">\r\n        <div class=\"line borderBottomGrey\">\r\n            Coming Up  <button class=\"btnBlue\"><i class=\"material-icons\">event</i> <span>View Calendar</span></button>\r\n        </div>\r\n\r\n        <div class=\"podBlock\">\r\n            <div class=\"podItem\">\r\n                <div class=\"coupon\"><i class=\"material-icons blue\">event</i></div>\r\n                <div class=\"list\">\r\n                    <span class=\"blue\">General Staff Shop</span>\r\n                    <i>Jan 10, 2018</i>\r\n                </div>\r\n            </div>\r\n\r\n            <div class=\"podItem\">\r\n                <div class=\"coupon\"><i class=\"material-icons blue\">event</i></div>\r\n                <div class=\"list\">\r\n                    <span class=\"blue\">Live Podcast</span>\r\n                    <i>Jan 10, 2018</i>\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n\r\n\r\n\r\n    <div class=\"space\"></div>\r\n    <div class=\"pod\">\r\n        <div class=\"line borderBottomGrey\">Quick Notification</div>\r\n\r\n\r\n        <div class=\"podBlock\">\r\n            <div class=\"podItem\">\r\n                <div class=\"coupon\"><span class=\"redBackground\">05</span></div>\r\n                <div class=\"list\">\r\n                    <span>Courses are overdue</span>\r\n                </div>\r\n            </div>\r\n\r\n            <div class=\"podItem\">\r\n                <div class=\"coupon\"><span class=\"yellowBackground\">15</span></div>\r\n                <div class=\"list\">\r\n                    <span>Courses are going to expire in the next 30 days.</span>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n\r\n    <div class=\"space\"></div>\r\n\r\n\r\n    <div class=\"pod\">\r\n        <div class=\"line borderBottomGrey\">Awards/Certificates</div>\r\n\r\n\r\n        <div class=\"podBlock\">\r\n            <div class=\"podItem\">\r\n                <div class=\"half\">\r\n                    <div class=\"blue big\">CERTIFICATES </div><b>(3)</b>\r\n                    <p>Your latest certificates</p>\r\n                    <button class=\"btnBlue\">View all</button>\r\n                </div>\r\n\r\n                <div class=\"half\">\r\n                    <img src=\"/Content/img/c1.jpg\" alt=\"\">\r\n                    <img src=\"/Content/img/c2.jpg\" alt=\"\">\r\n                    <img src=\"/Content/img/c3.jpg\" alt=\"\">\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n\r\n</div>\r\n"

/***/ }),

/***/ "../../../../../ClientApp/app/shared/sidebar.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var SidebarComponent = (function () {
    function SidebarComponent() {
    }
    SidebarComponent = __decorate([
        core_1.Component({
            selector: 'right-sidebar',
            template: __webpack_require__("../../../../../ClientApp/app/shared/sidebar.component.html"),
        })
    ], SidebarComponent);
    return SidebarComponent;
}());
exports.SidebarComponent = SidebarComponent;


/***/ }),

/***/ "../../../../../ClientApp/app/shared/spec-footer/spec-footer.component.html":
/***/ (function(module, exports) {

module.exports = "<div class=\"pallete1\" style=\"position:relative;left:28.5px;\">\r\n    <footer>\r\n        <div class=\"foot\">&copy;{{year}} - LEARNING MANAGEMENT SYSTEM</div>\r\n        <div class=\"power\">\r\n\r\n        </div>\r\n    </footer>\r\n</div>\r\n"

/***/ }),

/***/ "../../../../../ClientApp/app/shared/spec-footer/spec-footer.component.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var SpecFooterComponent = (function () {
    function SpecFooterComponent() {
    }
    SpecFooterComponent.prototype.ngOnInit = function () {
        this.year = new Date().getFullYear();
    };
    SpecFooterComponent = __decorate([
        core_1.Component({
            selector: 'spec-footer',
            template: __webpack_require__("../../../../../ClientApp/app/shared/spec-footer/spec-footer.component.html"),
        })
    ], SpecFooterComponent);
    return SpecFooterComponent;
}());
exports.SpecFooterComponent = SpecFooterComponent;


/***/ }),

/***/ "../../../../../ClientApp/environments/environment.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
Object.defineProperty(exports, "__esModule", { value: true });
exports.environment = {
    production: false
};


/***/ }),

/***/ "../../../../../ClientApp/main.ts":
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = __webpack_require__("../../../core/esm5/core.js");
var platform_browser_dynamic_1 = __webpack_require__("../../../platform-browser-dynamic/esm5/platform-browser-dynamic.js");
var app_module_1 = __webpack_require__("../../../../../ClientApp/app/app.module.ts");
var environment_1 = __webpack_require__("../../../../../ClientApp/environments/environment.ts");
if (environment_1.environment.production) {
    core_1.enableProdMode();
}
platform_browser_dynamic_1.platformBrowserDynamic().bootstrapModule(app_module_1.AppModule)
    .catch(function (err) { return console.log(err); });


/***/ }),

/***/ 0:
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__("../../../../../ClientApp/main.ts");


/***/ })

},[0]);
//# sourceMappingURL=main.bundle.js.map