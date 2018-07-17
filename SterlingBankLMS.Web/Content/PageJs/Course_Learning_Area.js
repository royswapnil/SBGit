(function () {
    var videoPlayer;
    function catchApiError(err) {
        //Todo: send to error handler service
        if (err) {

            if (err.Message) {
                err.HasError = true;
                ShowNotie(err);

            }
            else if (err.data && err.data.Message) {
                err.data.HasError = true;
                ShowNotie(err.data);
            }
            else {
                ShowNotie({ HasError: true, Message: err });
            }
        }

        else ShowNotie(appConfig.errors[0]);
    }

    var LMSModule = angular.module('sterlinglms', ['requestSvc', 'ui.router', 'ngSanitize']);

    //config
    LMSModule.config(function ($httpProvider, $stateProvider, $locationProvider) {
        var url = '/:courseId/:moduleId/:lessonId/:slug';

        //use html5 mode url
        $locationProvider.html5Mode(true);

        $httpProvider.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';

        $stateProvider.state('video', {
            url: url,
            templateUrl: "/common/course/video",
            controller: 'video as vm',
        }).state('document', {
            url: url,
            templateUrl: "/common/course/document",
            controller: 'document as vm',
        }).state('text', {
            url: url,
            templateUrl: '/common/course/text',
            controller: 'text as vm',
        }).state('quiz', {
            url: url,
            templateUrl: '/common/course/quiz',
            controller: 'quiz as vm',
        }).state('quiz.start', {
            templateUrl: '/common/course/startquiz',
        }).state('quiz.question', {
            templateUrl: '/common/course/quizquestion',
        }).state('quiz.answer', {
            templateUrl: '/common/course/quizresponse',
        }).state('quiz.summary', {
            templateUrl: '/common/course/quizattemptsummary',
        }).state('review', {
            url: url,
            templateUrl: '/common/course/review',
        }).state('survey', {
            url: url,
            controller: 'survey as vm',
            templateUrl: '/common/course/survey',
        }).state('survey.start', {
            templateUrl: '/common/course/startsurvey',
        }).state('survey.question', {
            templateUrl: '/common/course/surveyquestion',
        }).state('survey.finish', {
            templateUrl: '/common/course/finishsurvey',
        }).state('exam', {
            url: url,
            templateUrl: '/common/course/exam',
            controller: 'exam as vm'
        }).state('exam.start', {
            templateUrl: '/common/course/startexam'
        }).state('exam.question', {
            templateUrl: '/common/course/examquestion',
        }).state('exam.summary', {
            templateUrl: '/common/course/examattemptsummary',
        }).state('completecourse', {
            url: url,
            templateUrl: '/common/course/completecourse',
        });
    });

    //main 
    LMSModule.controller('lma', ['httpRequestSvc', '$rootScope', '$state', 'lmsUrl', '$timeout', '$scope', '$stateParams',
        function (http, root, state, lmsUrl, timeout, scope, stateParams) {

            var vm = this;
            root.course = {};
            vm.lessons = [];
            vm.modules = [];
            root.currentLesson = {};
            vm.position;

            function updatePageTitle(title) {
                root.pageTitle = title + ' - Learning Management System';
            }

            function gotoLesson(id) {

                var lesson = _.find(vm.lessons, function (item) { return item.Id === id });

                if (lesson) {
                    loadScreen(lesson);
                }

            }

            function saveProgress(data) {
                if (data) {
                    http.post(lmsUrl.progress, data).then(function (response) {

                        root.currentLesson.IsCompleted = true;

                    }).catch(catchApiError);
                }
            }

            function previousLesson() {
                var lesson = vm.lessons[vm.position - 1];

                if (lesson) {
                    loadScreen(lesson);
                }
            }

            function hasPreviousLesson(position) {
                var lessons = vm.lessons;

                if (lessons && lessons.length)
                    return (position >= 0) && (lessons[position - 1]);

                return false;
            }

            function hasNextLesson(position) {

                var lessons = vm.lessons;
                return (position > -1) && (lessons && (lessons.length - 1) > position);
            }

            function nextLesson() {
                var lesson = vm.lessons[vm.position + 1];

                if (lesson) {
                    loadScreen(lesson);
                }
            }

            function getRouteObject() {
                return {
                    lessonId: stateParams.lessonId,
                    slug: stateParams.slug,
                    moduleId: stateParams.moduleId,
                    courseId: stateParams.courseId
                };
            }

            function loadScreen(lesson) {

                if ($(".viewcomments") !== null) {
                    $(".viewcomments").addClass("hideviewcomments");
                }

                if (root.currentLesson) {
                    if (lesson.Id === root.currentLesson.Id)
                        return;
                }

                var urlParams = {
                    lessonId: lesson.Id,
                    slug: root.course.Slug,
                    moduleId: lesson.ModuleId,
                    courseId: root.course.Id
                };

                switch (lesson.LessonContentType) {

                    case 'Video':
                        state.go('video', urlParams);
                        break;

                    case 'Document':
                        state.go('document', urlParams);
                        break;

                    case 'Quiz':
                        state.go('quiz', urlParams);
                        break;

                    case 'Text':
                        state.go('text', urlParams);
                        break;
                }

                root.currentLesson = lesson;
                vm.position = _.findIndex(vm.lessons, function (i) { return i.Id === lesson.Id });
            }

            function pickCurrentOrFirstLesson() {

                var lessons = vm.lessons;

                if (lessons && lessons.length) {
                    var lesson = _.find(lessons, function (lsn) {
                        return lsn.IsCurrent;
                    });

                    return lesson || lessons[0];
                }
            }

            function loadModule(url) {
                http.get(url).then(function (response) {

                    var modules = response.Result.Modules;

                    _.forEach(modules, function (module) {
                        Array.prototype.push.apply(vm.lessons, module.Lessons);
                    });

                    vm.modules = modules;

                    //initial screen
                    var currentLesson = pickCurrentOrFirstLesson();

                    if (currentLesson) {
                        loadScreen(currentLesson);
                    }

                }).catch(catchApiError);
            }

            function finishCourse() {

                http.get(lmsUrl.claApi + 'getusercourserequiredfinishcontents', { courseId: root.course.Id }).then(function (response) {

                    var status = response.Result;

                    if (status.HasSurvey && !status.HastakenSurvey) {

                        state.go('survey', getRouteObject());
                        return;
                    }

                    else if (status.HasExam && status.CantakeExam) {

                        state.go('exam', getRouteObject());
                        return;
                    }

                    else if (!status.HastakenReview) {

                        state.go('review', getRouteObject());

                        return;
                    }

                    else {
                        state.go('completecourse', getRouteObject());
                    }

                }).catch(catchApiError);
            }

            function postDatatoFinish(url, data) {

                http.post(url, data).then(function (response) {
                    finishCourse();
                }).catch(catchApiError);
            }

            vm.showCompleteCourseBtn = function () {
                return (root.currentLesson && root.currentLesson.IsCompleted && !hasNextLesson(vm.position));
            }

            vm.markCourseCompleted = function () {

                http.post(lmsUrl.claApi + 'markcoursecompleted/' + root.course.Id).then(function (response) {

                    window.location.assign(lmsUrl.base);

                }).catch(catchApiError);
            }

            vm.uploadLessonProgress = function () {
                if (root.currentLesson) {
                    var currentProgress = {
                        lessonId: root.currentLesson.Id,
                        moduleId: root.currentLesson.ModuleId,
                        courseId: root.course.Id
                    };

                    saveProgress(currentProgress);
                }
            }

            vm.hasPreviousLesson = function () {
                return hasPreviousLesson(vm.position);
            }

            vm.previousLesson = function () {
                previousLesson();
            }

            root.hasNextLesson = function () {
                return hasNextLesson(vm.position);
            }

            root.nextLesson = function () {
                nextLesson();
            }

            vm.focusCommentBox = function (selector) {
                angular.element(selector).get(0).focus();
            }

            vm.getCourseInformation = function (url) {
                http.get(url).then(function (response) {

                    root.course = response.Result;

                    if (root.course) {
                        updatePageTitle(root.course.Name);

                        //load initial content...mvc supplies the route here
                        loadModule(lmsUrl.currentContent);
                    }

                }).catch(catchApiError);
            }

            vm.gotoNextLesson = function (id) {
                gotoLesson(id);
            }

            vm.showSaveProgressBtn = function () {
                return root.currentLesson && root.currentLesson.LessonContentType !== 'Quiz' && !root.currentLesson.IsCompleted;
            }

            vm.submitReview = function (e, url) {
                var data = $(e.currentTarget).serialize2JSON();

                data.courseId = stateParams.courseId;

                postDatatoFinish(url, data);
            }

            vm.likeCourse = function () {

            }

            root.finishCourse = function () {
                finishCourse();
            }
        }]);

    //video
    LMSModule.controller('video', ['httpRequestSvc', '$stateParams', 'lmsUrl', '$rootScope', '$scope',
        function (http, stateParams, lmsUrl, root, scope) {
            var vm = this;
            vm.getContent = function () {


                if ($.isEmptyObject(root.course))
                    return;

                http.get(lmsUrl.lessonContent, { id: stateParams.lessonId }).then(function (response) {

                    vm.video = response.Result;

                    videoPlayer.src({ type: vm.video.MimeType, src: vm.video.ContentUrl });

                }).catch(catchApiError);
            }

            videojs("vid").ready(function () {
                videoPlayer = this;
            });

            scope.$on('$destroy', function (e, data) {
                if ((videoPlayer !== undefined) && (videoPlayer !== null)) {
                    videoPlayer.dispose();
                }
            });
        }]);

    //text
    LMSModule.controller("text", ['httpRequestSvc', '$stateParams', 'lmsUrl', function (http, stateParams, lmsUrl) {
        var vm = this;

        vm.getContent = function () {

            http.get(lmsUrl.lessonContent, { id: stateParams.lessonId }).then(function (response) {
                vm.text = response.Result;

            }).catch(catchApiError);
        }
    }]);

    //quiz
    LMSModule.controller('quiz', ['httpRequestSvc', '$stateParams', '$timeout', '$state', 'stateCacheFactory', 'lmsUrl', '$rootScope',
        function (http, stateParams, timeout, state, stateCacheFactory, lmsUrl, root) {
            var cachedUrl, retryMax = 3, retryCounter = 0, retry = true, key = 'quiz.questions', incr = 1;
            var vm = this;
            vm.questions = [];
            vm.position;

            function saveState(url) {

                cachedUrl = url;

                var currentProgress = {
                    lessonId: stateParams.lessonId,
                    moduleId: stateParams.moduleId,
                    courseId: stateParams.courseId
                };

                http.post(url, currentProgress).then(function (response) {
                    retry = false;
                }).catch(function (err) {
                    //Todo: send to error handler
                    console.error(err);

                }).finally(function () {
                    if (retry)
                        if (retryCounter < retryMax) {
                            ++retryCounter;
                            timeout(saveProgress(cachedUrl), 10000);
                        }
                });
            }

            function getRouteObject() {
                return {
                    lessonId: stateParams.lessonId,
                    slug: stateParams.slug,
                    moduleId: stateParams.moduleId,
                    courseId: stateParams.courseId
                };
            }

            function postAnswer(url, data) {

                http.post(url, data).then(function (response) {

                    vm.quizResponse = response.Result;

                    var urlParams = getRouteObject();
                    state.go('quiz.answer', urlParams);

                }).catch(catchApiError);
            }

            function initQuizArea() {

                vm.questions = stateCacheFactory.get(key);

                vm.total = vm.questions.length || 0;

                vm.currentQuestion = pickCurrentOrFirstQuestion();
            }

            function hasNext() {
                return vm.questions && vm.position < vm.questions.length - incr;
            }

            function pickCurrentOrFirstQuestion() {
                var questions = vm.questions;

                if (questions && questions.length) {
                    for (var i = 0; i < questions.length; i++) {
                        if (questions[i].IsCurrent) {

                            vm.position = i;

                            return questions[i];
                        }
                    }

                    vm.position = 0;
                    return questions[0];
                }
            }

            vm.hasNext = function () {
                return hasNext();
            }

            vm.isLast = function () {
                return vm.position === vm.questions.length - incr;
            }

            vm.finishQuiz = function () {
                var urlParam = getRouteObject();
                http.post(lmsUrl.claApi + 'finishquiz/' + urlParam.lessonId).then(function () {

                    root.currentLesson.IsCompleted = true;

                    state.go('quiz.summary', urlParam);

                }).catch(catchApiError);
            }

            vm.gotoNextQuestion = function () {

                var current = vm.questions[vm.position + incr];
                if (current) {

                    vm.currentQuestion = current;
                    vm.position += incr;

                    var urlParams = getRouteObject();
                    state.go('quiz.question', urlParams);
                }
            }

            vm.getQuizSummary = function (url) {

                http.get(lmsUrl.claApi + 'quizsummary', { id: stateParams.lessonId }).then(function (response) {
                    vm.quizSummary = response.Result;

                    vm.quizSummary.computeAccuracy = function () {
                        var acc = 0;

                        if (this.Questions && this.CorrectAnswers) {

                            acc = this.MaxScore * 100 / this.Questions;
                        }
                        return acc + '%';
                    }

                }).catch(catchApiError);
            }

            vm.gotoPreviousQuestion = function () {

                var current = vm.questions[vm.position - incr];
                if (current) {

                    vm.currentQuestion = current;
                    vm.position -= incr;
                }
            }

            vm.allowTakeQuiz = function (url) {

                var urlParams = getRouteObject();

                http.get(url, { lessonId: urlParams.lessonId }).then(function (response) {

                    if (response.Result.HasPreviousAttempt || !response.Result.CanTakeQuiz) {
                        state.go('quiz.summary', urlParams)
                    }
                    else {
                        state.go('quiz.start', urlParams)
                    }

                }).catch(catchApiError);
            }

            vm.gotoQuiz = function () {

                var urlParams = getRouteObject();

                http.post(lmsUrl.claApi + 'startquiz/' + urlParams.lessonId).then(function (response) {

                    if (response.Result.length) {

                        stateCacheFactory.set(key, response.Result);

                        saveState(lmsUrl.quizStamp);

                        initQuizArea();

                        state.go('quiz.question', urlParams);

                    } else {
                        state.go('quiz.summary', urlParams);
                    }
                }).catch(catchApiError);
            }

            vm.submitAnswer = function (event, url) {

                var data = $(event.currentTarget).serialize2JSON();

                if (!data) {
                    alert('You must select an answer to this quiz!');
                    return;
                }

                data.lessonId = stateParams.lessonId;
                data.courseId = stateParams.courseId;

                postAnswer(url, data);
            }
        }]);

    //survey
    LMSModule.controller('survey', ['httpRequestSvc', '$stateParams', '$state', 'lmsUrl', '$rootScope',
        function (http, stateParams, state, lmsUrl, root) {
            var incr = 1;
            var vm = this;
            vm.survey = {};
            vm.position;

            function getRouteObject() {
                return {
                    lessonId: stateParams.lessonId,
                    slug: stateParams.slug,
                    moduleId: stateParams.moduleId,
                    courseId: stateParams.courseId
                };
            }

            function hasNext() {
                return vm.survey.Questions && vm.position < vm.survey.Questions.length - incr;
            }

            function postSurveyResponse(data, url) {
                return http.post(url, data);
            }

            function pickCurrentOrFirstSurveyQuestion() {
                var questions = vm.survey.Questions;

                if (questions && questions.length) {
                    for (var i = 0; i < questions.length; i++) {
                        if (questions[i].IsCurrent) {

                            vm.position = i;
                            return questions[i];
                        }
                    }

                    vm.position = 0;
                    return questions[0];
                }
            }

            function getNextSurveyQuestion() {

                var current = vm.survey.Questions[vm.position + incr];
                if (current) {

                    vm.currentQuestion = current;
                    vm.position += incr;

                    state.go('survey.question', getRouteObject());
                }
            }

            vm.getNextSurveyQuestion = function () {
                getNextSurveyQuestion();
            }

            vm.submitSurveyAnswer = function (e, url) {

                var data = $(e.currentTarget).serialize2JSON();

                postSurveyResponse(data, url).then(function () {

                    if (hasNext()) {
                        getNextSurveyQuestion();
                    } else {

                        http.post(lmsUrl.claApi + 'finishsurvey/' + vm.survey.SurveyId).then(function () {

                            state.go('survey.finish', getRouteObject());

                        }).catch(catchApiError);
                    }

                }).catch(catchApiError);
            }

            vm.gotoCompleteCourse = function () {
                root.finishCourse();
            }

            vm.getSurveyQuestions = function () {
                http.get(lmsUrl.claApi + 'getsurveyquestions', { courseId: root.course.Id }).then(function (response) {

                    vm.survey = response.Result;

                    state.go('survey.start', getRouteObject());

                }).catch(catchApiError);
            }

            vm.startSurvey = function () {
                var question = pickCurrentOrFirstSurveyQuestion();

                if (question) {
                    vm.currentQuestion = question;
                    state.go('survey.question', getRouteObject());
                }
                else vm.finishCourse();
            }

            vm.hasNext = function () {
                return hasNext();
            }

            vm.isLast = function () {
                return vm.position === vm.survey.Questions.length - incr;
            }

            vm.moduleMenu = function (moduleId) {
                return vm.currentLesson.ModuleId === moduleId;
            }
        }]);

    //exam
    LMSModule.controller('exam', ['httpRequestSvc', '$stateParams', '$state', 'lmsUrl', '$rootScope',
        function (http, stateParams, state, lmsUrl, root) {
            var vm = this;
            var incr = 1;
            vm.questions = [];
            vm.position;

            function getRouteObject() {
                return {
                    lessonId: stateParams.lessonId,
                    slug: stateParams.slug,
                    moduleId: stateParams.moduleId,
                    courseId: stateParams.courseId
                };
            }

            function hasNext() {
                return vm.questions && vm.position < vm.questions.length - incr;
            }

            function pickCurrentOrFirstQuestion() {
                var questions = vm.questions;

                if (questions && questions.length) {
                    for (var i = 0; i < questions.length; i++) {
                        if (questions[i].IsCurrent) {

                            vm.position = i;

                            return questions[i];
                        }
                    }

                    vm.position = 0;
                    return questions[0];
                }
            }

            function hasPrevious(position) {
                var questions = vm.questions;

                if (questions && questions.length)
                    return (position >= 0) && (questions[vm.position - incr]);

                return false;
            }

            function getNextQuestion() {

                var current = vm.questions[vm.position + incr];
                if (current) {

                    vm.currentQuestion = current;
                    vm.position += incr;
                }
            }

            vm.hasNext = function () {
                return hasNext();
            }

            vm.hasPrevious = function () {
                return hasPrevious(vm.position);
            }

            vm.isLast = function () {
                return vm.position === vm.questions.length - incr;
            }

            vm.gotoPreviousQuestion = function () {

                var current = vm.questions[vm.position - incr];
                if (current) {

                    vm.currentQuestion = current;
                    vm.position -= incr;
                }
            }

            vm.allowTakeExam = function (url) {

                http.get(url, { courseId: root.course.Id }).then(function (response) {

                    if (response.Result && response.Result.CantakeExam) {

                        vm.exam = response.Result.Exam;

                        state.go('exam.start', getRouteObject())
                    }
                    else {
                        state.go('exam.summary', getRouteObject())
                    };

                }).catch(catchApiError);
            }

            vm.startExam = function () {
                http.get(lmsUrl.claApi + 'startexamination', { courseId: root.course.Id }).then(function (response) {

                    if (response.Result && response.Result.length) {

                        vm.questions = response.Result

                        vm.total = vm.questions.length;

                        var currentQuestion = pickCurrentOrFirstQuestion();

                        if (currentQuestion) {

                            vm.currentQuestion = currentQuestion;
                            state.go('exam.question', getRouteObject());
                        }

                    } else {
                        state.go('exam.summary', getRouteObject());
                    }
                }).catch(catchApiError);
            }

            vm.submitAnswer = function (e, url) {

                var data = $(e.currentTarget).serialize2JSON();

                http.post(url, data).then(function (response) {

                    if (hasNext()) {
                        getNextQuestion();
                    } else {
                        var go = confirm('Exam has finished!. View your score.')
                        if (go) {
                            http.post(lmsUrl.claApi + 'finishexam', { examId: vm.exam.Id, courseId: root.course.Id }).then(function () {

                                state.go('exam.summary', getRouteObject());

                            }).catch(catchApiError);
                        }
                    }

                }).catch(catchApiError);

            }
        }]);

    //document
    LMSModule.controller("document", ['httpRequestSvc', '$stateParams', '$rootScope', 'lmsUrl',
        function (http, stateParams, root, lmsUrl) {
            var vm = this;

            vm.getContent = function () {

                http.get(lmsUrl.lessonContent, { id: stateParams.lessonId }).then(function (response) {
                    // vm.officeDocUrl = lmsUrl.officeViewer + response.Result.ContentUrl;
                    var doc = response.Result;

                    var viewerElement = $("#viewer");
                    var myWebViewer = new PDFTron.WebViewer({
                        path: lmsUrl.base + 'Content/js/webviewer/lib',
                        type: "html5",
                        documentType: "pdf",
                        l: "demo:Upplasol1971@cuvox.de:726fd6bd01cd048be609119ac9b92953c7e50250e82cc51891",
                        initialDoc: doc.ContentUrl,
                        enableReadOnlyMode: true,
                        //enableAnnotations: false,
                        hideAnnotationPanel: true,
                        //showToolbarControl: false,
                        useDownloader: false
                    }, viewerElement.get(0));

                }).catch(catchApiError);
            }
        }]);

    //filter
    LMSModule.filter('trustUrl', function ($sce) {
        return function (url) {
            return $sce.trustAsResourceUrl(url);
        };
    });

    LMSModule.filter('trustHtml', function ($sce) {
        return function (html) {
            return $sce.trustAsHtml(html);
        };
    });

    //directives
    LMSModule.directive('lmsLoader', ['$http', function ($http) {
        return {
            restrict: 'A',
            link: function (scope, elm, attrs) {
                scope.isLoading = function () {
                    return $http.pendingRequests.length > 0;
                };

                scope.$watch(scope.isLoading, function (v) {
                    if (v) {
                        elm.show();
                    } else {
                        elm.hide();
                    }
                });
            }
        };
    }]);

    //factories
    LMSModule.factory('stateCacheFactory', [function () {
        var data = [];
        var state = {
            set: function (key, val) { data[key] = val; },
            get: function (key) { return data[key]; }
        }
        return state;
    }]);

    //constants
    LMSModule.constant('lmsUrl', {
        lessonContent: appConfig.lms.lessonContentUrl,
        quizStamp: appConfig.lms.quizStampUrl,
        currentContent: appConfig.lms.currentContentUrl,
        officeViewer: appConfig.lms.officeViewerUrl,
        progress: appConfig.lms.userProgressUrl,
        base: appConfig.baseUrl,
        claApi: appConfig.lms.claApiUrl,
    });
})();