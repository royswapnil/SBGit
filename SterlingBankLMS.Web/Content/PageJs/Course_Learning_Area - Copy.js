(function () {

    function catchApiError(err) {
        //Todo: send to error handler service
        if (err) {
            if (err.message)
                ShowNotie(err.message);

            else ShowNotie(err);
        }

        else ShowNotie(appConfig.errors[0]);
    }

    function arrayToObject(formArray) {

        var data = {};
        for (var i = 0; i < formArray.length; i++) {
            data[formArray[i]['name']] = formArray[i]['value'];
        }
        return data;
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
            url: '#s',
            templateUrl: '/common/course/startquiz',
        }).state('quiz.question', {
            url: '#q',
            templateUrl: '/common/course/quizquestion',
        }).state('quiz.answer', {
            url: '#a',
            templateUrl: '/common/course/quizresponse',
        }).state('quiz.summary', {
            url: '#summary',
            templateUrl: '/common/course/quizattemptsummary',
        }).state('finishcourse', {
            url: url,
            templateUrl: '/common/course/finish',
        }).state('review', {
            url: url,
            templateUrl: '/common/course/review',
        }).state('survey', {
            url: url,
            templateUrl: '/common/course/survey',
        }).state('survey.start', {
            url: url,
            templateUrl: '/common/course/startsurvey',
        }).state('survey.question', {
            url: url,
            templateUrl: '/common/course/surveyquestion',
        }).state('exam', {
            url: url,
            templateUrl: '/common/course/exam',
            controller: 'exam as vm',
        }).state('exam.start', {
            url: url,
            templateUrl: '/common/course/startexam',
        }).state('exam.question', {
            url: url,
            templateUrl: '/common/course/examquestion',
        }).state('exam.response', {
            url: url,
            templateUrl: '/common/course/examresponse',
        }).state('exam.summary', {
            url: url,
            templateUrl: '/common/course/exam',
        });
    });

    //main 
    LMSModule.controller('lma', ['httpRequestSvc', '$rootScope', '$state', 'lmsUrl', '$timeout', '$scope', '$stateParams',
        function (http, root, state, lmsUrl, timeout, scope, stateParams) {

            var vm = this;
            root.course = {};
            vm.currentModule = {};
            vm.currentLesson = {};
            vm.position;
            vm.isLast = true;

            function updatePageTitle(title) {
                root.pageTitle = title + ' - Learning Management System';
            }

            function gotoLesson(index) {
                var lesson = vm.currentModule.Lessons[index];

                if (lesson) {
                    loadScreen(lesson);
                }
            }

            function saveProgress(data) {
                if (data) {
                    http.post(lmsUrl.progress, data).then(function (response) {

                        vm.currentLesson.IsCompleted = true;

                    }).catch(catchApiError);
                }
            }

            function previousLesson() {
                var lesson = vm.currentModule.Lessons[vm.position - 1];

                if (lesson) {
                    loadScreen(lesson);
                }
            }

            function hasPreviousLesson(position) {
                var lessons = vm.currentModule.Lessons;

                if (lessons && lessons.length)
                    return (position >= 0) && (lessons[position - 1]);

                return false;
            }

            function hasNextLesson(position) {

                var lessons = vm.currentModule.Lessons;
                return (position > -1) && (lessons && (lessons.length - 1) > position);
            }

            function nextLesson() {
                var lesson = vm.currentModule.Lessons[vm.position + 1];

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

                var urlParams = {
                    lessonId: lesson.Id,
                    slug: root.course.Slug,
                    moduleId: vm.currentModule.Id,
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

                vm.currentLesson = lesson;
                vm.position = getLessonIndexbyId(lesson.Id);
            }

            function getLessonIndexbyId(id) {

                var lessons = vm.currentModule.Lessons;

                if (lessons && lessons.length) {
                    for (var i = 0; i < lessons.length; i++) {
                        if (lessons[i].Id === id)
                            return i;
                    }
                    return -1;
                }
            }

            function pickCurrentOrFirstLesson() {

                var lessons = vm.currentModule.Lessons;

                if (lessons && lessons.length) {
                    for (var i = 0; i < lessons.length; i++) {
                        if (lessons[i].IsCurrent) {
                            return lessons[i];
                        }
                    }
                    return lessons[0];
                }
            }

            function loadModule(url) {
                http.get(url).then(function (response) {

                    vm.modules = response.Result.modules;

                    vm.currentModule = response.Result.CurrentModule;
                    vm.hasNextModule = response.Result.HasNextModule;
                    vm.nextModuleId = response.Result.NextModuleId;
                    vm.previousModuleId = response.Result.PreviousModuleId;

                    //initial screen
                    var currentLesson = pickCurrentOrFirstLesson();

                    if (currentLesson) {
                        //location.replace();
                        loadScreen(currentLesson);
                    }

                }).catch(catchApiError);
            }

            function finishCourse() {

                http.get(lmsUrl.claApi + 'getusercourserequiredfinishcontents', { id: root.course.Id }).then(function (response) {

                    var status = response.Result;

                    if (status.HasSurvey && !status.Hastakensurvey) {

                        state.go('survey', getRouteObject());
                        return;
                    }

                    else if (status.HasExam || status.CantakeExam) {

                        state.go('exam', getRouteObject());
                        return;
                    }

                    else if (!status.HastakenReview) {

                        state.go('review', getRouteObject());
                        return;
                    }

                    else {
                        http.post(lmsUrl.claApi + 'markcoursecompleted/' + root.course.Id).then(function (response) {
                            state.go('finishcourse', getRouteObject());
                        }).catch(catchApiError);
                    }

                }).catch(catchApiError);
            }

            function postDatatoFinish(url, data) {

                http.post(url, data).then(function (response) {
                    finishCourse();
                }).catch(catchApiError);
            }

            function pickCurrentOrFirstSurveyQuestion() {
                var questions = vm.surVeyquestions;

                if (questions && questions.length) {
                    for (var i = 0; i < questions.length; i++) {
                        if (questions[i].IsCurrent) {

                            vm.surveyPosition = i;

                            return questions[i];
                        }
                    }

                    vm.surveyPosition = 0;
                    return questions[0];
                }
            }

            function postSurveyResponse(data, url) {
                return http.post(url, data);
            }

            vm.submitSurvey = function (e, url) {

                var form = $(e.currentTarget).serializeArray();

                var data = arrayToObject(form);

                postSurveyResponse(url, data).then(function () {

                    if (hasNextSurveyQuestion) {
                        getNextSurveyQuestion();
                    } else {
                        state.go('survey.finish');
                    }

                }).catch(catchApiError);
            }

            vm.getSurveyQuestions = function () {
                http.get(lmsUrl.claApi + 'getsurveyquestions', { courseId: root.course.Id }).then(function (response) {

                    vm.surVeyquestions = response.Result;

                    var question = pickCurrentOrFirstSurveyQuestion();

                    if (question) {
                        vm.currentQuestion = question;
                        state.go('survey.question', getRouteObject());
                    }
                    else finishCourse();

                }).catch(catchApiError);
            }

            vm.showCompleteCourseBtn = function () {
                return (vm.currentLesson && vm.currentLesson.IsCompleted && vm.isLast /*vm.currentLesson.IsLast*/);
            }

            vm.uploadLessonProgress = function () {
                if (vm.currentLesson) {
                    var currentProgress = {
                        lessonId: vm.currentLesson.Id,
                        moduleId: vm.currentModule.Id,
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

            vm.gotoNextModule = function (url) {
                loadModule(url + vm.nextModuleId);
            }

            vm.gotoNextLesson = function (index) {
                gotoLesson(index);
            }

            vm.showSaveProgressBtn = function () {
                return vm.currentLesson && vm.currentLesson.LessonContentType !== 'Quiz' && !vm.currentLesson.IsCompleted;
            }

            vm.submitReview = function (e, url) {
                var form = $(e.currentTarget).serializeArray();

                var data = arrayToObject(form);
                data.courseId = stateParams.courseId;

                postDatatoFinish(url, data);
            }

            vm.finishCourse = function () {
                finishCourse();
            }
        }]);

    //video
    LMSModule.controller('video', ['httpRequestSvc', '$stateParams', 'lmsUrl', '$rootScope',
        function (http, stateParams, lmsUrl, root) {
            var vm = this;

            vm.getContent = function () {

                if ($.isEmptyObject(root.course))
                    return;

                http.get(lmsUrl.lessonContent, { id: stateParams.lessonId }).then(function (response) {
                    vm.video = response.Result;
                }).catch(catchApiError);
            }
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

                }).catch(function (err) {
                    ShowNotie(appConfig.errors[0]);
                });
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

            vm.gotoSummary = function () {

                var urlParams = getRouteObject();
                state.go('quiz.summary', urlParams);
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

                http.get(url, { id: stateParams.lessonId }).then(function (response) {
                    vm.quizSummary = response.Result;

                    vm.quizSummary.computeAccuracy = function () {
                        var acc = 0;

                        if (this.Questions && this.CorrectAnswers) {

                            acc = this.CorrectAnswers * 100 / this.Questions;
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

            vm.getAttemptHistory = function (url) {

                var urlParams = getRouteObject();

                http.get(url, { id: urlParams.lessonId }).then(function (response) {

                    if (response.Result.CanTakeQuiz) {
                        state.go('quiz.start', urlParams)
                    }
                    else {
                        state.go('quiz.summary', urlParams)
                    };

                }).catch(catchApiError);
            }

            vm.gotoQuiz = function () {

                var urlParams = getRouteObject();

                http.get(lmsUrl.lessonContent, { id: urlParams.lessonId }).then(function (response) {

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
                var formarr = $(event.currentTarget).serializeArray();

                if (formarr) {
                    var data = arrayToObject(formarr);
                    data.lessonId = stateParams.lessonId;
                    data.courseId = stateParams.courseId;

                    postAnswer(url, data);
                }
            }
        }]);

    //survey
    LMSModule.controller('survey', ['httpRequestSvc', '$stateParams', '$timeout', '$state', 'stateCacheFactory', 'lmsUrl', '$rootScope',
        function (http, stateParams, timeout, state, stateCacheFactory, lmsUrl, root) {
            var cachedUrl, retryMax = 3, retryCounter = 0, retry = true, key = 'survey.questions', incr = 1;
            var vm = this;
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

            function postAnswer(url, data) {

                http.post(url, data).then(function (response) {

                    vm.quizResponse = response.Result;

                    var urlParams = getRouteObject();
                    state.go('quiz.answer', urlParams);

                }).catch(function (err) {
                    ShowNotie(appConfig.errors[0]);
                });
            }

            function initSurveyArea() {

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

            vm.gotoSummary = function () {

                var urlParams = getRouteObject();
                state.go('quiz.summary', urlParams);
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

            vm.gotoPreviousQuestion = function () {

                var current = vm.questions[vm.position - incr];
                if (current) {

                    vm.currentQuestion = current;
                    vm.position -= incr;
                }
            }

            vm.getSurveyAttemptHistory = function (url) {

                var urlParams = getRouteObject();

                http.get(url, { id: urlParams.lessonId }).then(function (response) {

                    if (response.Result.CanTakeQuiz) {
                        state.go('quiz.start', urlParams)
                    }
                    else {
                        state.go('quiz.summary', urlParams)
                    };

                }).catch(catchApiError);
            }

            vm.submitAnswer = function (event, url) {
                var formarr = $(event.currentTarget).serializeArray();

                if (formarr) {
                    var data = arrayToObject(formarr);
                    data.lessonId = stateParams.lessonId;
                    data.courseId = stateParams.courseId;

                    postAnswer(url, data);
                }
            }
        }]);


    //exam
    LMSModule.controller('exam', ['httpRequestSvc', '$stateParams', '$timeout', '$state', 'stateCacheFactory', 'lmsUrl', '$rootScope',
        function (http, stateParams, timeout, state, stateCacheFactory, lmsUrl, root) {
            var cachedUrl, retryMax = 3, retryCounter = 0, retry = true, key = 'exam.questions', incr = 1;
            var vm = this;
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

            function postAnswer(url, data) {

                http.post(url, data).then(function (response) {

                    vm.examResponse = response.Result;

                    var urlParams = getRouteObject();
                    state.go('quiz.answer', urlParams);

                }).catch(function (err) {
                    ShowNotie(appConfig.errors[0]);
                });
            }

            function initExamArea() {

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

            vm.gotoSummary = function () {

                var urlParams = getRouteObject();
                state.go('exam.summary', urlParams);
            }

            vm.gotoNextQuestion = function () {

                var current = vm.questions[vm.position + incr];
                if (current) {

                    vm.currentQuestion = current;
                    vm.position += incr;

                    var urlParams = getRouteObject();
                    state.go('exam.question', urlParams);
                }
            }

            vm.getExamSummary = function (url) {

                http.get(url, { id: stateParams.lessonId }).then(function (response) {
                    vm.quizSummary = response.Result;

                    vm.quizSummary.computeAccuracy = function () {
                        var acc = 0;

                        if (this.Questions && this.CorrectAnswers) {

                            acc = this.CorrectAnswers * 100 / this.Questions;
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

            vm.getExamAttemptHistory = function (url) {

                http.get(lmsUrl.claApi + '', { id: root.course.Id }).then(function (response) {

                    if (response.Result.CanTakeQuiz) {
                        state.go('exam.start', getRouteObject())
                    }
                    else {
                        state.go('exam.summary', getRouteObject())
                    };

                }).catch(catchApiError);
            }

            vm.gotoExam = function () {

                var urlParams = getRouteObject();

                http.get(lmsUrl.baseUrl + 'examquestions', { id: urlParams.lessonId }).then(function (response) {

                    if (response.Result.length) {

                        stateCacheFactory.set(key, response.Result);

                        initExamArea();

                        state.go('exam.question', urlParams);

                    } else {
                        state.go('exam.summary', urlParams);
                    }
                }).catch(catchApiError);
            }

            vm.submitAnswer = function (event, url) {
                var formarr = $(event.currentTarget).serializeArray();

                if (formarr) {
                    var data = arrayToObject(formarr);
                    data.lessonId = stateParams.lessonId;
                    data.courseId = stateParams.courseId;

                    postAnswer(url, data);
                }
            }
        }]);

    //document
    LMSModule.controller("document", ['httpRequestSvc', '$stateParams', '$rootScope', 'lmsUrl',
        function (http, stateParams, root, lmsUrl) {
            var vm = this;

            vm.getContent = function () {

                http.get(lmsUrl.lessonContent, { id: stateParams.lessonId }).then(function (response) {
                    var doc = response.Result;

                    var viewerElement = $("#viewer");
                    var myWebViewer = new PDFTron.WebViewer({
                        path: lmsUrl.base + 'Content/js/webviewer/lib',
                        type: "html5",
                        documentType: "pdf",
                        // l: "demo:Upplasol1971@cuvox.de:726fd6bd01cd048be609119ac9b92953c7e50250e82cc51891",
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