(function () {
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

        $httpProvider.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';

        $stateProvider.state('examsummary', {
            url: '/examsummary',
            templateUrl: "/common/examination/examsummary",
        }).state('questions', {
            url: '/examquestions',
            templateUrl: "/common/examination/examquestions",
        }).state('scoresummary', {
            url: '/examquestions',
            templateUrl: "/common/examination/examquestions",
        })
    });

    //main 
    LMSModule.controller('exam', ['httpRequestSvc', '$state', 'lmsUrl', '$timeout', '$stateParams',
        function (http, state, lmsUrl, timeout, stateParams) {
            var that = this;

            that.getExaminationSummary = function (id) {
                http.get(lmsUrl.claApi + 'getexaminationsummary', { examinationId: id }).then(function (response) {

                    that.exam = response.Result;
                    state.go('examsummary', { id: id })

                }).catch(catchApiError);
            }

            that.startExamination = function () {
                if (!that.exam) {
                    alert("No exam to proceed on.")
                }

                http.get(lmsUrl.apiUrl + 'startexamination', { examinationId: id }).then(function (response) {

                    var result = response.Result || undefined;
                    if (result && result.HasPreviousAttempt || !result.CantakeExam) {
                        state.go('examsummary', { id: id })
                    }
                    else {
                        state.go('exam.start', { id: id })
                    };
                }).catch(catchApiError);
            }
        }]);


    //filter

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

    //constants
    LMSModule.constant('lmsUrl', {
        claApi: appConfig.lms.claApiUrl,
    });
})();