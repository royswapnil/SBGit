using Newtonsoft.Json;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

using WebGrease.Css.Extensions;

using static SterlingBankLMS.Data.Models.Enums.CourseLikeStatus;
using static SterlingBankLMS.Data.Models.Enums.LessonContentType;

using static System.Linq.Enumerable;

namespace SterlingBankLMS.Web.Api
{
    /// <summary>
    /// Course Learning Area Api Controller
    /// </summary>
    [RoutePrefix("Api/CourseLearningArea")]
    public class CourseLearningAreaController : BaseApiController
    {
        private readonly CourseFactory _courseFactory;
        private readonly ModuleFactory _moduleFactory;

        private readonly LessonFactory _lessonFactory;
        private readonly LessonProgressFactory _lessonProgressFactory;
        private readonly UserCourseFactory _userCourseFactory;

        private readonly QuizQuestionFactory _quizQuestionFactory;
        private readonly QuizResponseFactory _quizResponseFactory;
        private readonly UserLessonQuizFactory _userLessonQuizFactory;

        private readonly CourseReviewFactory _courseReviewFactory;
        private readonly CourseLikeFactory _courseLikeFactory;

        private readonly SurveyFactory _surveyFactory;
        private readonly SurveyQuestionFactory _surveyQuestionFactory;
        private readonly SurveyResponseFactory _surveyResponseFactory;
        private readonly UserSurveyFactory _userSurveyFactory;

        private readonly UserExaminationFactory _userExaminationFactory;

        private readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionSvc;

        private static object locker = new object();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="courseFactory"></param>
        /// <param name="lessonProgressFactory"></param>
        /// <param name="ModuleFactory"></param>
        /// <param name="lessonFactory"></param>
        /// <param name="userCourseFactory"></param>
        /// <param name="surveyFactory"></param>
        /// <param name="quizQuestionFactory"></param>
        /// <param name="quizResponseFactory"></param>
        /// <param name="userLessonQuizFactory"></param>
        /// <param name="examinationFactory"></param>
        /// <param name="examinationAttemptFactory"></param>
        /// <param name="courseReviewFactory"></param>
        /// <param name="surveyQuestionFactory"></param>
        /// <param name="surveyResponseFactory"></param>
        /// <param name="userSurveyFactory"></param>
        /// <param name="examinationQuestionFactory"></param>
        /// <param name="courseLikeFactory"></param>
        /// <param name="examResponseFactory"></param>
        /// <param name="workContext"></param>
        /// <param name="permissionSvc"></param>
        public CourseLearningAreaController(
            CourseFactory courseFactory, LessonProgressFactory lessonProgressFactory,
            ModuleFactory ModuleFactory, LessonFactory lessonFactory,
            UserCourseFactory userCourseFactory, SurveyFactory surveyFactory,
            QuizQuestionFactory quizQuestionFactory, QuizResponseFactory quizResponseFactory,
            UserLessonQuizFactory userLessonQuizFactory, CourseReviewFactory courseReviewFactory,
            SurveyQuestionFactory surveyQuestionFactory, SurveyResponseFactory surveyResponseFactory,
            UserSurveyFactory userSurveyFactory, CourseLikeFactory courseLikeFactory,
            UserExaminationFactory userExaminationFactory,
            IWorkContext workContext, IPermissionService permissionSvc
            )
        {
            _courseFactory = courseFactory;
            _lessonProgressFactory = lessonProgressFactory;
            _moduleFactory = ModuleFactory;
            _workContext = workContext;
            _lessonFactory = lessonFactory;
            _userCourseFactory = userCourseFactory;
            _permissionSvc = permissionSvc;
            _quizQuestionFactory = quizQuestionFactory;
            _quizResponseFactory = quizResponseFactory;
            _userLessonQuizFactory = userLessonQuizFactory;
            _courseReviewFactory = courseReviewFactory;
            _surveyFactory = surveyFactory;
            _surveyQuestionFactory = surveyQuestionFactory;
            _surveyResponseFactory = surveyResponseFactory;
            _userSurveyFactory = userSurveyFactory;
            _courseLikeFactory = courseLikeFactory;
            _userExaminationFactory = userExaminationFactory;
        }
        //Todo (Samuel): Automatically close exam once timer elapses
        //[HttpPost]
        //[Route("FinishExam")]
        //public IHttpActionResult FinishExam(ClaExamFinishModel model)
        //{
        //    if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
        //        return AccessDeniedResult();
        //    }

        //    var session = _courseFactory.GetCurrentCourseSession(model.CourseId, _workContext.User.UserId);

        //    if (session == null) {
        //        return BadRequest("Session not found");
        //    }

        //    lock (locker) {
        //        var attempt = _examinationAttemptFactory.FindAttempt(model.ExamId, session.Id, true);

        //        if (attempt == null) {
        //            return BadRequest("Session not found.");
        //        }

        //        if (!attempt.Completed) {

        //            attempt.Completed = true;
        //            attempt.DateCompleted = CommonHelper.GetCurrentDate();
        //            _examinationAttemptFactory.Update(attempt);
        //        }

        //        return Ok();
        //    }
        //}

        /// <summary>
        /// Mark survey as finished
        /// </summary>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("FinishSurvey/{surveyId:int}")]
        public IHttpActionResult FinishSurvey(int surveyId)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            lock (locker) {
                var surveySession = _userSurveyFactory.FindUserSurveySession(surveyId, _workContext.User.UserId, true);

                if (surveySession != null && !surveySession.IsFinished) {
                    surveySession.IsFinished = true;
                    surveySession.ModifiedDate = surveySession.CreatedDate = CommonHelper.GetCurrentDate();
                    surveySession.LastModifiedById = _workContext.User.UserId;

                    _userSurveyFactory.Update(surveySession);
                }

                return Ok();
            }
        }

        /// <summary>
        /// Get all Employee learning progress
        /// </summary>
        /// <returns></returns>
        [Route("GetLearningProgress")]
        public IHttpActionResult GetLearningProgress()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            var courseProgessList = _courseFactory.GetUserCourseProgress(_workContext.User.UserId, _workContext.User.OrganizationId);

            var courseProgress = courseProgessList.MapTo<IEnumerable<CourseProgressDto>, IEnumerable<CourseProgressModel>>();

            var result = new ApiResult<CollectionResult<CourseProgressModel>>
            {
                Result = new CollectionResult<CourseProgressModel>(courseProgress)
            };

            return Ok(result);
        }

        /// <summary>
        /// Like or Unlike course
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //Todo: implement the UI conrol for this action.
        [Route("LikeOrUnlikeCourse")]
        public IHttpActionResult LikeOrUnlikeCourse(LikeDislikeModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            lock (locker) {
                var userLike = _courseLikeFactory.FindUserLikeStatus(model.CourseId, _workContext.User.UserId, true);

                if (userLike == null) {
                    _courseLikeFactory.CreateNewUserLike(NewCourseLike(model));
                }
                else {
                    if (model.Like == model.DisLike) {
                        userLike.Status = Undecided;
                        _courseLikeFactory.Update(userLike);
                    }
                    else {
                        userLike.Status = model.DisLike ? UnLike : Like;
                        _courseLikeFactory.Update(userLike);
                    }
                }
            }

            return Ok();
        }

        private CourseLike NewCourseLike(LikeDislikeModel model)
        {
            var courseLike = new CourseLike
            {
                UserId = _workContext.User.UserId,
                CourseId = model.CourseId,
                CreatedDate = CommonHelper.GetCurrentDate(),
                Status = model.Like ? Like : UnLike
            };

            courseLike.CreatedById = _workContext.User.UserId;

            return courseLike;
        }

        //[Route("SubmitExamResponse")]
        //public IHttpActionResult SubmitExamResponse(ClaExamResponseModel model)
        //{
        //    if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
        //        return AccessDeniedResult();
        //    }

        //    if (!model.IsValid(out List<string> errors)) {
        //        return BadRequest(errors.FirstOrDefault());
        //    }

        //    lock (locker) {

        //        var session = _courseFactory.GetCurrentCourseSession(model.CourseId, _workContext.User.UserId);

        //        if (session == null) {
        //            return BadRequest("You can not take an examination now.");
        //        }

        //        var attempt = _examinationAttemptFactory.FindAttempt(model.ExamId, session.Id);

        //        if (attempt == null) {
        //            return BadRequest("Session not found.");
        //        }

        //        if (attempt.Completed) {
        //            return BadRequest("Examination session has been completed already.");
        //        }

        //        if (attempt.DueDate <= CommonHelper.GetCurrentDate()) {
        //            return BadRequest("Examination session has expired.");
        //        }

        //        var question = _examinationQuestionFactory.GetQuestionAndOptions(model.QuestionId);

        //        if (question == null) {
        //            return BadRequest();
        //        }

        //        var userOptionIds = model.Answers;

        //        if (!question.IsMultipleChoice) {
        //            var selectedOption = question.Options.FirstOrDefault(x => x.Id == userOptionIds[0]);

        //            if (selectedOption == null) {
        //                return BadRequest();
        //            }

        //            var questionResponse = _examResponseFactory.FindQuestionResponseForAttempt(attempt.Id, model.QuestionId);

        //            if (questionResponse == null) {

        //                questionResponse = new ExaminationQuizResponse
        //                {
        //                    ExaminationQuestionId = model.QuestionId,
        //                    ExaminationAttemptId = attempt.Id,
        //                    IsAnswer = selectedOption.IsAnswer,
        //                    Value = selectedOption.Value,
        //                    CreatedDate = CommonHelper.GetCurrentDate(),
        //                    CreatedById = _workContext.User.UserId
        //                };

        //                _examResponseFactory.Add(questionResponse);
        //            }
        //            else {
        //                if (selectedOption.Value != questionResponse.Value) {
        //                    questionResponse.IsAnswer = selectedOption.IsAnswer;
        //                    questionResponse.Value = selectedOption.Value;
        //                    questionResponse.ModifiedDate = CommonHelper.GetCurrentDate();

        //                    _examResponseFactory.Update(questionResponse);
        //                }
        //            }
        //        }
        //        else {
        //            var userSelectedOps = userOptionIds.SelectMany(x => question.Options.Where(o => o.Id == x)).ToList();

        //            if (userSelectedOps == null || !userSelectedOps.Any()) {
        //                return BadRequest();
        //            }

        //            var questionResponses = _examResponseFactory.FindResponsesForSession(attempt.Id, model.QuestionId);

        //            if (questionResponses.Any())
        //                _examResponseFactory.Delete(questionResponses);

        //            questionResponses = new List<ExaminationQuizResponse>();

        //            foreach (var item in userSelectedOps) {

        //                var quizResponse = new ExaminationQuizResponse
        //                {
        //                    ExaminationQuestionId = model.QuestionId,
        //                    ExaminationAttemptId = attempt.Id,
        //                    IsAnswer = item.IsAnswer,
        //                    Value = item.Value,
        //                    CreatedDate = CommonHelper.GetCurrentDate(),
        //                    CreatedById = _workContext.User.UserId
        //                };
        //                questionResponses.Add(quizResponse);
        //            }

        //            _examResponseFactory.Add(questionResponses);
        //        }
        //    }
        //    return Ok();
        //}

        /// <summary>
        /// Starts course Exam
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("StartExamination")]
        //public IHttpActionResult StartExam(int courseId)
        //{
        //    if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
        //        return AccessDeniedResult();
        //    }

        //    var session = _courseFactory.GetCurrentCourseSession(courseId, _workContext.User.UserId);

        //    if (session == null) {
        //        return BadRequest("You can not take an examination now!");
        //    }

        //    var response = new ApiResult<dynamic>();

        //    var exam = _examinationFactory.GetCourseExaminationAndAttemptCount(session.Id, _workContext.User.OrganizationId);

        //    if (exam == null) {
        //        return BadRequest("No examination found.");
        //    }

        //    if (!CanTakeExam(exam)) {
        //        return BadRequest("Sorry, you can no longer take this exam.");
        //    }

        //    var examQuestions = _examinationQuestionFactory.GetExaminationQuestions(exam.Id);

        //    lock (locker) {
        //        var attempt = _examinationAttemptFactory.FindAttempt(exam.Id, session.Id);

        //        if (attempt == null) {
        //            attempt = _examinationAttemptFactory.StartNewAttemtpt(exam, session);
        //        }

        //        var questionModels = examQuestions.MapTo<IEnumerable<ExaminationQuestion>, List<ClaExamQuestionModel>>();

        //        var lastAttemptAnswer = _examResponseFactory.GetCurrentSessionLastAttemptAnswer(exam.Id, _workContext.User.UserId);
        //        GetNextExamQuestion(questionModels, questionModels.FirstOrDefault(x => x.Id == lastAttemptAnswer?.ExaminationQuestionId));

        //        response.Result = questionModels;
        //    }

        //    return Ok(response);
        //}

        //[HttpGet]
        //[Route("GetUserCanTakeCourseExam")]
        //public IHttpActionResult GetUserCanTakeCourseExam(int courseId)
        //{
        //    if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
        //        return AccessDeniedResult();
        //    }

        //    var session = _courseFactory.GetCurrentCourseSession(courseId, _workContext.User.UserId);
        //    if (session == null) {
        //        return BadRequest("You can not take an examination now!");
        //    }

        //    var exam = _examinationFactory.GetCourseExaminationAndAttemptCount(session.Id, _workContext.User.OrganizationId);

        //    if (exam == null) {
        //        return BadRequest("No examination found.");
        //    }

        //    return Ok(new ApiResult<dynamic>
        //    {
        //        Result = new
        //        {
        //            Exam = exam,
        //            CantakeExam = CanTakeExam(exam)
        //        }
        //    });
        //}

        /// <summary>
        /// Validate if user can mark a course session as finished
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [Route("GetUserCourseRequiredFinishContents")]
        public IHttpActionResult GetUserCourseRequiredFinishContents(int courseId)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            var status = _courseFactory.HasSurveyOrReview(courseId, _workContext.User.Id, _workContext.User.OrganizationId);

            var surveyOrReviewStatus = status.MapTo<UserCourseStatusDto, UserCourseStatusModel>();

            return Ok(new ApiResult<dynamic>
            {
                Result = surveyOrReviewStatus
            });
        }

        /// <summary>
        /// Submit course Rating and review message
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SubmitCourseReview")]
        public IHttpActionResult SubmitCourseReview(CourseReviewModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            if (!model.IsValid(out List<string> errors)) {
                return BadRequest(errors.FirstOrDefault());
            }

            var previousUserReview = _courseReviewFactory.GetUserRatingForCourse(_workContext.User.UserId, model.CourseId);

            if (previousUserReview == null) {
                previousUserReview = model.MapTo<CourseReviewModel, CourseReview>();

                previousUserReview.UserId = _workContext.User.UserId;
                _courseReviewFactory.CreateReview(previousUserReview);
            }

            return Ok(model);
        }

        /// <summary>
        /// Get survey Questions
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [Route("GetSurveyQuestions")]
        public IHttpActionResult GetSurveyQuestions(int courseId)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            var surveyQtns = _surveyQuestionFactory.GetSurveyQuestions(courseId, _workContext.User.OrganizationId, out int? surveyId);

            var surveyQtnsModel = surveyQtns.MapTo<IEnumerable<SurveyQuestion>, IEnumerable<ClaSurveyQuestionModel>>();

            return Ok(new ApiResult<dynamic>
            {
                Result = new
                {
                    Questions = surveyQtnsModel,
                    SurveyId = surveyId
                }
            });
        }

        /// <summary>
        /// Get Course Details
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCourseDetails")]
        public IHttpActionResult CourseDetails(int courseId)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            var courseDetailDto = _courseFactory.GetClaCourseDetails(courseId, _workContext.User.OrganizationId);

            if (courseDetailDto == null) {
                return NotFoundResult();
            }

            var courseDetail = courseDetailDto.MapTo<ClaCourseDto, ClaCourseModel>();

            var response = new ApiResult<dynamic>()
            {
                Result = courseDetail
            };

            return Ok(response);
        }

        /// <summary>
        /// Get Course Module Contents
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCourseModule")]
        public IHttpActionResult GetCourseModules(int courseId)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            if (!_courseFactory.ValidateClaCourse(courseId, null, null, _workContext.User.OrganizationId)) {
                return BadRequest();
            }

            var courseModules = _moduleFactory.GetClaModulesForCourse(courseId, _workContext.User.UserId);
            var courseModulesModel = courseModules.MapTo<IEnumerable<Module>, IEnumerable<ClaModuleModel>>();

            lock (locker) {

                var session = _courseFactory.GetCurrentCourseSession(courseId, _workContext.User.UserId);

                if (session == null) {
                    session = _userCourseFactory.CreateNewSession(_workContext.User.UserId, courseId, _workContext.User.OrganizationId);

                    SetCurrentModuleAndLesson(courseModulesModel, null, null);
                }
                else {
                    SetCurrentModuleAndLesson(courseModulesModel, session.CurrentModuleNumber, session.CurrentLessonNumber);
                    SetWatchedLessons(_moduleFactory.GetAllCompletedLessons(session.Id), courseModulesModel);
                }

                var response = new ApiResult<dynamic>()
                {
                    Result = new
                    {
                        Modules = courseModulesModel,
                        HasActiveSession = session != null,
                    }
                };

                return Ok(response);
            }
        }

        /// <summary>
        /// Get Course Module Contents
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [Route("GetCourseModule")]
        public IHttpActionResult GetCourseModule(int courseId, int moduleId)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            if (!_courseFactory.ValidateClaCourse(courseId, null, null, _workContext.User.OrganizationId)) {
                return BadRequest();
            }

            var courseModules = _moduleFactory.GetClaModulesForCourse(courseId, _workContext.User.UserId);
            var currentModule = courseModules.FirstOrDefault(x => x.Id == moduleId);

            if (currentModule == null) {
                return BadRequest();
            }

            var courseModulesModel = courseModules.MapTo<IEnumerable<Module>, IEnumerable<ClaModuleModel>>();
            SetCurrentModuleAndLesson(courseModulesModel, moduleId, null);

            lock (locker) {

                var session = _courseFactory.GetCurrentCourseSession(courseId, _workContext.User.UserId);

                if (session == null) {
                    session = _userCourseFactory.CreateNewSession(_workContext.User.UserId, courseId, _workContext.User.OrganizationId);
                }
                else {
                    SetWatchedLessons(_moduleFactory.GetAllCompletedLessons(session.Id), courseModulesModel);
                }

                var response = new ApiResult<dynamic>
                {
                    Result = new
                    {
                        Modules = courseModulesModel,
                        HasActiveSession = session != null
                    }
                };

                return Ok(response);
            }
        }

        /// <summary>
        /// Get Course Module Contents
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="moduleId"></param>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        [Route("GetCourseModule")]
        public IHttpActionResult GetCourseModule(int courseId, int moduleId, int lessonId)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            if (!_courseFactory.ValidateClaCourse(courseId, null, null, _workContext.User.OrganizationId)) {
                return BadRequest();
            }

            var courseModules = _moduleFactory.GetClaModulesForCourse(courseId, _workContext.User.UserId);
            var currentModule = courseModules.FirstOrDefault(x => x.Id == moduleId);

            var courseModulesModel = courseModules.MapTo<IEnumerable<Module>, IEnumerable<ClaModuleModel>>();
            SetCurrentModuleAndLesson(courseModulesModel, moduleId, lessonId);

            lock (locker) {

                var session = _courseFactory.GetCurrentCourseSession(courseId, _workContext.User.UserId);

                if (session == null) {
                    session = _userCourseFactory.CreateNewSession(_workContext.User.UserId, courseId, _workContext.User.OrganizationId);
                }
                else {
                    SetWatchedLessons(_moduleFactory.GetAllCompletedLessons(session.Id), courseModulesModel);
                }

                var response = new ApiResult<dynamic>
                {
                    Result = new
                    {
                        Modules = courseModulesModel,
                        HasActiveSession = session != null
                    }
                };

                return Ok(response);
            }
        }

        /// <summary>
        /// Save user lesson progress for for course session or create new course session if none exists.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveProgress")]
        public async Task<IHttpActionResult> SaveLessonProgress(UserCourseProgressModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            var courseExist = _courseFactory.ValidateClaCourse(model.CourseId,
                null, null, _workContext.User.OrganizationId);

            if (!courseExist) {
                return BadRequest();
            }

            var session = _courseFactory.GetCurrentCourseSession(model.CourseId,
                _workContext.User.UserId);

            if (session is null) {
                return BadRequest();
            }

            lock (locker) {

                var lesson = _lessonFactory.GetLessonInCourse(model.LessonId,
                                            model.ModuleId, model.CourseId,
                                            _workContext.User.OrganizationId);

                if (lesson is null) {
                    return BadRequest();
                }

                //Never mark quiz as completed till attempted...
                if (lesson.Type == Quiz) {
                    return BadRequest("Please check your input.");
                }

                var progress = _lessonProgressFactory.GetSessionLessonProgress(model.LessonId, model.ModuleId, session.Id);

                if (progress is null) {
                    CreateLessonProgress(model, lesson.Name, session.Id);
                }
                else {
                    if (!progress.IsCompleted) {
                        progress.IsCompleted = true;
                        progress.ModifiedDate = CommonHelper.GetCurrentDate();
                        progress.LastModifiedById = _workContext.User.Id;
                        _lessonProgressFactory.Update(progress);
                    }
                }
                _courseFactory.ClearCachedUserCourseProgress(_workContext.User.UserId, _workContext.User.OrganizationId);
            }

            //stamp current location
            await UpdateCourseSessionLocation(model, session);

            return Ok();
        }

        private LessonProgress CreateLessonProgress(UserCourseProgressModel model, string name, int sessionId)
        {
            var lp = new LessonProgress
            {
                UserCourseId = sessionId,
                ModuleId = model.ModuleId,
                IsCompleted = true,
                CreatedById = _workContext.User.UserId,
                LessonId = model.LessonId,
                Name = name
            };

            lp.EndDate = lp.CreatedDate = CommonHelper.GetCurrentDate();

            _lessonProgressFactory.Add(lp);

            return lp;
        }

        /// <summary>
        /// Marks a session as completed provided you have consumed all course contents
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MarkCourseCompleted/{courseId:int}")]
        public IHttpActionResult MarkCourseSessionCompleted(int courseId)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            var courseSession = _courseFactory.GetCurrentCourseSession(courseId, _workContext.User.UserId);

            if (courseSession == null) {
                return BadRequest("No session");
            }

            if (courseSession.CourseStatus == UserCourseStatus.Complete)
                return Ok();

            var sessionCompletedLessons = _userCourseFactory.GetCourseSessionCompletedLession(courseSession.Id);

            if (sessionCompletedLessons.TotalLesson > sessionCompletedLessons.CompletedLesson) {
                return BadRequest("You need to complete all lesson(s) first.");
            }

            else {
                lock (locker) {

                    courseSession.CourseStatus = UserCourseStatus.Complete;
                    courseSession.Completed = true;
                    courseSession.EndDate = courseSession.ModifiedDate = CommonHelper.GetCurrentDate();
                    courseSession.LastModifiedById = _workContext.User.Id;

                    _userCourseFactory.Update(courseSession);

                   // AddAvailableExamSession(courseId);

                    _courseFactory.ClearCachedUserAssignedCourses(_workContext.User.UserId, _workContext.User.OrganizationId);
                    _courseFactory.ClearCachedUserCourseProgress(_workContext.User.UserId, _workContext.User.OrganizationId);
                }
            }

            return Ok();
        }

        //void AddAvailableExamSession(int courseId)
        //{
        //    var exam = _userExaminationFactory.GetAvailableExam(courseId, _workContext.User.UserId);

        //    if ((exam != null && exam.UserAttemptCounts <= 0)) {
        //        _userExaminationFactory.CreateNewUserExamination(_workContext.User.UserId, exam);
        //    }
        //}

        /// <summary>
        /// Get lesson contents
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetLessonContent")]
        [HttpGet]
        public IHttpActionResult GetLessonContent(int id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            //user may not have right...
            var lesson = _lessonFactory.GetUserLesson(id, _workContext.User.OrganizationId);

            if (lesson.IsNull()) {
                return AccessDeniedResult();
            }

            if (lesson.Is(Quiz)) {
                return BadRequest("Please check your input.");
            }
            else {

                if (!lesson.IsExternalContent && !string.IsNullOrWhiteSpace(lesson.ContentUrl)) {
                    lesson.ContentUrl = CommonHelper.ToAbsolutePath(lesson.ContentUrl);
                }

                return Ok(new ApiResult<dynamic>
                {
                    Result = new
                    {
                        lesson.Id,
                        lesson.MimeType,
                        lesson.ContentUrl,
                        lesson.HtmlContent,
                    }
                });
            }
        }

        /// <summary>
        /// Starts or continue a quiz session
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("StartQuiz/{lessonId:int}")]
        public IHttpActionResult StartQuiz(int lessonId)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            //user may not have right...
            var lesson = _lessonFactory.GetUserLesson(lessonId, _workContext.User.OrganizationId);

            if (lesson.IsNull() || !lesson.Is(Quiz)) {
                return BadRequest();
            }

            var courseSession = _courseFactory.GetCurrentCourseSession(lesson.CourseId, _workContext.User.UserId);

            if (courseSession.IsNull()) {
                BadRequest("Please check you input.");
            }

            lock (locker) {
                var attempts = _userLessonQuizFactory.CountUserQuizSessionAttempts(lesson.Id, courseSession.Id);

                if (attempts >= lesson.QuizRetakeCount) {
                    return BadRequest("You have reached maximum re-take for this quiz!");
                }

                var currentSession = _userLessonQuizFactory.FindCurrentUserQuizSession(lesson.Id, courseSession.Id);

                if (currentSession.IsNull()) {
                    CreateQuizSession(lesson.Id, courseSession);
                }

                //Todo: this change request has triggred a bug...
                var quizQtns = _quizQuestionFactory.GetLessonQuizQuestionsAndOptions(lesson);

                var quizQuestionModels = quizQtns.MapTo<IEnumerable<QuizQuestion>, List<ClaQuizQuestionModel>>();
                var lastUserOption = _quizResponseFactory.GetCurrentQuizSessionLastAttemptAnswer(lessonId, _workContext.User.UserId);

                if (!lastUserOption.IsNull()) {
                    var lastQuizQuestionModel = quizQuestionModels.FirstOrDefault(x => x.Id == lastUserOption?.QuizId);
                    GetNextQuestion(quizQuestionModels, lastQuizQuestionModel);
                }

                return Ok(new ApiResult<dynamic>
                {
                    Result = quizQuestionModels
                });
            }
        }

        /// <summary>
        /// Submit quiz response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("SubmitQuizResponse")]
        [HttpPost]
        public IHttpActionResult SubmitQuizAnswer(ClaQuizResponseModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            if (!model.IsValid(out List<string> errors)) {
                return BadRequest(errors.FirstOrDefault());
            }

            lock (locker) {

                // user needs a course session
                var courseSession = _courseFactory.GetCurrentCourseSession(model.CourseId, _workContext.User.UserId);

                if (courseSession.IsNull()) {
                    return BadRequest();
                }

                var quizSession = _userLessonQuizFactory.FindCurrentUserQuizSession(model.LessonId, courseSession.Id);

                if (quizSession.IsNull() || quizSession.IsFinished) {
                    return BadRequest("Invalid quiz session.");
                }

                var quiz = _quizQuestionFactory.GetQuizAndOptions(model.QuestionId);

                if (quiz.IsNull()) {
                    return BadRequest("quiz not found.");
                }

                var userOptionIds = model.Answers;

                var userSelectedOptions = userOptionIds.SelectMany(x => quiz.Options.Where(o => o.Id == x)).ToList();

                if (userSelectedOptions == null) {
                    return BadRequest("Please select valid option(s)");
                }

                var quizResponse = _quizResponseFactory.FindQuizResponseForSession(quizSession.Id, model.QuestionId);

                if (quizResponse == null) {

                    quizResponse = new QuizResponse
                    {
                        QuizId = model.QuestionId,
                        UserLessonQuizId = quizSession.Id,
                        CreatedDate = CommonHelper.GetCurrentDate(),
                        CreatedById = _workContext.User.UserId
                    };

                    if (quiz.AnswerType == AnswerType.CheckBox) {

                        quizResponse.Value = JsonConvert.SerializeObject(userSelectedOptions.Select(x => x.Title));

                        if (quiz.IsMultipleChoice) {
                            quizResponse.IsAnswer = userSelectedOptions.All(x => x.IsAnswer);
                        }
                        else {
                            quizResponse.IsAnswer = userSelectedOptions.Any(x => x.IsAnswer);
                        }
                    }

                    else {
                        quizResponse.IsAnswer = userSelectedOptions[0].IsAnswer;
                        quizResponse.Value = JsonConvert.SerializeObject(new string[] { userSelectedOptions[0].Title });
                    }

                    _quizResponseFactory.Add(quizResponse);
                }

                var response = new ApiResult<dynamic>
                {
                    Result = new
                    {
                        YourAnswer = userSelectedOptions.Select(x => x.Title),
                        IsCorrect = quizResponse.IsAnswer,
                        IsMultiple = quiz.IsMultipleChoice,
                    }
                };

                return Ok(response);
            }
        }


        //Todo: pick current survey item
        [Route("SubmitSurveyResponse")]
        [HttpPost]
        public IHttpActionResult SubmitSurvey(SurveyRespsonseModel vm)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            lock (locker) {

                var survey = _surveyFactory.FindSurvey(vm.SurveyId);

                if (survey == null) {
                    return BadRequest("Survey not found or previously deleted.");
                }

                var userSurveySession = _userSurveyFactory.FindUserSurveySession(vm.SurveyId, _workContext.User.UserId);

                if (userSurveySession == null) {
                    userSurveySession = CreateSurveySession(vm.SurveyId);
                }

                else if (userSurveySession.IsFinished) {
                    return BadRequest("Sorry, You have already attempted this survey.");
                }

                var surveyQuestionAndAnswers = _surveyQuestionFactory.GetsQuestionWithOptions(vm.QuestionId, _workContext.User.UserId);

                if (surveyQuestionAndAnswers == null) {
                    return BadRequest("Question not found or previously deleted.");
                }

                if (surveyQuestionAndAnswers.IsMultipleChoice) {
                    var responses = new List<SurveyQuestionResponse>();
                    foreach (var item in vm.Answers) {

                        var userOption = surveyQuestionAndAnswers.Options.FirstOrDefault(x => x.Id == item);

                        if (userOption == null) {
                            return BadRequest();
                        }

                        var surveyResponse = new SurveyQuestionResponse
                        {
                            UserSurveyId = userSurveySession.Id,
                            Value = userOption.Value,
                            CreatedDate = CommonHelper.GetCurrentDate()
                        };

                        surveyResponse.CreatedById = surveyResponse.UserId = _workContext.User.Id;

                        responses.Add(surveyResponse);
                    }

                    _surveyResponseFactory.Add(responses);
                }

                else {
                    SurveyQuestionOptions userOption = null;

                    if (surveyQuestionAndAnswers.AnswerType != AnswerType.Text) {
                        userOption = surveyQuestionAndAnswers.Options.FirstOrDefault(x => x.Id == vm.Answers[0]);

                        if (userOption == null) {
                            return BadRequest();
                        }
                    }

                    var surveyResponse = new SurveyQuestionResponse
                    {
                        UserSurveyId = userSurveySession.Id,
                        Value = userOption?.Value ?? vm.Answer,
                        CreatedDate = CommonHelper.GetCurrentDate(),
                        SurveyQuestionId = surveyQuestionAndAnswers.Id,
                    };

                    surveyResponse.CreatedById = surveyResponse.UserId = _workContext.User.Id;
                    _surveyResponseFactory.Add(surveyResponse);
                }
            }

            return Ok();
        }

        /// <summary>
        /// Get user quiz attempt summary.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("QuizSummary")]
        public IHttpActionResult QuizScoreSummary(int id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            //user may not have right...
            var lessonDto = _lessonFactory.GetUserLesson(id, _workContext.User.OrganizationId);

            if (lessonDto.IsNull() || !lessonDto.Is(Quiz)) {
                return BadRequest();
            }

            // needs a curse session
            var courseSession = _courseFactory.GetCurrentCourseSession(lessonDto.CourseId, _workContext.User.UserId);

            if (courseSession.IsNull()) {
                return BadRequest();
            }

            var attemptSummaryDto = _userLessonQuizFactory.ComputeQuizSummary(courseSession.Id, lessonDto.Id);

            return Ok(new ApiResult<dynamic>
            {
                Result = attemptSummaryDto
            });
        }

        //Todo: prevent finish here if user doesnt exceed pass mark.
        /// <summary>
        /// Finish quiz
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("FinishQuiz/{lessonId:int}")]
        public IHttpActionResult FinishQuiz(int lessonId)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            //user may not have right...
            var lesson = _lessonFactory.GetUserLesson(lessonId, _workContext.User.OrganizationId);

            if (lesson.IsNull() || !lesson.Is(Quiz)) {
                return BadRequest("Please check your input.");
            }

            // user needs a curse session
            var courseSession = _courseFactory.GetCurrentCourseSession(lesson.CourseId, _workContext.User.UserId);

            if (courseSession.IsNull()) {
                return BadRequest("You cannot finish this quiz now.");
            }

            var userQuizSession = _userLessonQuizFactory.FindCurrentUserQuizSession(lesson.Id, courseSession.Id);

            if (userQuizSession.IsNull()) {
                return BadRequest("Quiz session completed already.");
            }

            if (!userQuizSession.IsFinished) {

                lock (locker) {

                    var lessonPrg = _lessonProgressFactory.GetSessionLessonProgress(lesson.Id, lesson.ModuleId, userQuizSession.UserCourseId);

                    if (lessonPrg == null) {
                        var lessonPrgvm = new UserCourseProgressModel
                        {
                            LessonId = lesson.Id,
                            ModuleId = lesson.ModuleId,
                        };

                        CreateLessonProgress(lessonPrgvm, null, courseSession.Id);
                    }

                    userQuizSession.IsFinished = true;
                    userQuizSession.LastModifiedById = _workContext.User.UserId;
                    userQuizSession.EndDate = CommonHelper.GetCurrentDate();

                    _userLessonQuizFactory.Update(userQuizSession);
                }
            }

            return Ok();
        }

        /// <summary>
        /// Check if current user can take quiz
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserCanTakeQuiz")]
        public IHttpActionResult CheckUserCanTakeQuiz(int lessonId)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            //user may not have right...
            var lesson = _lessonFactory.GetUserLesson(lessonId, _workContext.User.OrganizationId);

            if (lesson.IsNull()) {
                return AccessDeniedResult();
            }

            if (!lesson.Is(Quiz)) {
                return BadRequest();
            }

            var courseSession = _courseFactory.GetCurrentCourseSession(lesson.CourseId, _workContext.User.UserId);

            if (courseSession.IsNull()) {
                return BadRequest("Please check your input.");
            }

            var attempts = _userLessonQuizFactory.CountUserQuizSessionAttempts(lesson.Id, courseSession.Id);

            return Ok(new ApiResult<dynamic>
            {
                Result = new
                {
                    HasPreviousAttempt = attempts > 0,
                    CanTakeQuiz = attempts < lesson.QuizRetakeCount
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("StampCurrentLocation")]
        public async Task<IHttpActionResult> StampCurrentLocation(UserCourseProgressModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            var currentCourseSession = _courseFactory.GetCurrentCourseSession(model.CourseId, _workContext.User.UserId);

            if (currentCourseSession != null)
                await UpdateCourseSessionLocation(model, currentCourseSession);

            return Ok();
        }

        private UserSurvey CreateSurveySession(int surveyId)
        {
            var userSurvey = new UserSurvey { SurveyId = surveyId, UserId = _workContext.User.UserId };

            userSurvey.ModifiedDate = userSurvey.CreatedDate = CommonHelper.GetCurrentDate();
            userSurvey.LastModifiedById = userSurvey.CreatedById = userSurvey.UserId;

            _userSurveyFactory.Add(userSurvey);

            return userSurvey;
        }

        private UserLessonQuiz CreateQuizSession(int lessonId, UserCourse currentCourseSession)
        {
            var userQuizSession = new UserLessonQuiz
            {
                UserCourseId = currentCourseSession.Id,
                LessonId = lessonId,
                CreatedById = _workContext.User.UserId,
            };

            userQuizSession.StartDate = userQuizSession.CreatedDate = CommonHelper.GetCurrentDate();

            _userLessonQuizFactory.Add(userQuizSession);

            return userQuizSession;
        }

        private void GetNextQuestion(List<ClaQuizQuestionModel> questionModels, ClaQuizQuestionModel lastQuestion)
        {
            var currentIdx = questionModels.IndexOf(lastQuestion);
            var nextQuiz = currentIdx + 1 >= questionModels.Count ? null : questionModels[++currentIdx] ?? questionModels.FirstOrDefault();

            if (nextQuiz != null)
                nextQuiz.IsCurrent = true;
        }

        private void GetNextExamQuestion(List<ClaExamQuestionModel> questionModels, ClaExamQuestionModel lastQuestion)
        {
            var currentIdx = questionModels.IndexOf(lastQuestion);
            var nextQqtn = currentIdx + 1 >= questionModels.Count ? null : questionModels[++currentIdx] ?? questionModels.FirstOrDefault();

            if (nextQqtn != null)
                nextQqtn.IsCurrent = true;
        }

        private void SetWatchedLessons(IEnumerable<CompletedLessonDto> watchedLessons, IEnumerable<ClaModuleModel> modules)
        {
            modules.ForEach(module => {

                module.Lessons.ForEach(lesson => {
                    watchedLessons.ForEach(wl => {
                        if (wl.Id == lesson.Id)
                            lesson.IsCompleted = true;
                    });
                });
            });
        }

        private void SetCurrentModuleAndLesson(IEnumerable<ClaModuleModel> modules, int? moduleId, int? lessonId)
        {
            var currentModule = modules.FirstOrDefault(x => x.Id == moduleId) ?? modules.FirstOrDefault();

            if (currentModule != null) {

                currentModule.IsCurrent = true;

                SetCurrentLesson(currentModule, lessonId);
            }
        }

        private void SetCurrentLesson(ClaModuleModel module, int? lessonId = null)
        {
            var firstLesson = module.Lessons.FirstOrDefault(x => x.Id == lessonId) ?? module.Lessons.FirstOrDefault();

            if (firstLesson != null)
                firstLesson.IsCurrent = true;
        }

        private Task UpdateCourseSessionLocation(UserCourseProgressModel model, UserCourse currentCourseSession)
        {
            return Task.Factory.StartNew(() => {
                currentCourseSession.CurrentModuleNumber = model.ModuleId;
                currentCourseSession.CurrentLessonNumber = model.LessonId;

                currentCourseSession.ModifiedDate = CommonHelper.GetCurrentDate();

                _userCourseFactory.Update(currentCourseSession);
            });
        }
    }
}