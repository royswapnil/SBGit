using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Web.Areas.Common.Models;
using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Enums;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Common.Controllers
{
    /// <summary>
    /// Employee Course Controller
    /// </summary>
    public class CourseController : BaseController
    {
        private readonly CourseFactory _courseFactory;
        private readonly ExaminationFactory _examinationFactory;
        private readonly IPermissionService _permissionSvc;
        private readonly IWorkContext _workContext;
        private readonly AdvertFactory _advertFactory;
        private readonly CommentsFactory _commentsFactory;

        public CourseController(
            CourseFactory courseFactory, IWorkContext workContext,
            IPermissionService permissionSvc, ExaminationFactory examinationFactory,AdvertFactory advertFactory, CommentsFactory commentsFactory)
        {
            _courseFactory = courseFactory;
            _workContext = workContext;
            _permissionSvc = permissionSvc;
            _examinationFactory = examinationFactory;
            _advertFactory = advertFactory;
            _commentsFactory = commentsFactory;
        }

        public ActionResult LearningHistory()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedView();
            }
            return View();
        }

        public ActionResult CourseDetails(string url, int id = 0)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedView();
            }

            var course = _courseFactory.GetCourseDetails(id, _workContext.User.OrganizationId);

            //TODO: CACHE MAKE SINGLE FETCH TO DB
            var advert = _advertFactory.GetCurrentTopLocationBanner((int)AdLocation.CourseDetails);         
            if (course != null) {
                var model = course.MapTo<CourseDetailDto, CourseDetailModel>();
                model.TopSectionAd = advert;
                return View(model);
            }
            return NotFoundView();
        }

        public ActionResult CourseLearningArea(int? moduleId, int? lessonId, string title, int id = 0)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedView();
            }

            if (!_courseFactory.ValidateClaCourse(id, moduleId, lessonId, _workContext.User.OrganizationId)) {
                return NotFoundView();
            }

            var advert = _advertFactory.GetCurrentTopLocationBanner((int)AdLocation.TakeCourse);
            
            var model = new CourseLmaDetailsModel
            {
                TopSectionAd = advert,
                CourseId = id,
                LessonId = lessonId,
                ModuleId = moduleId,
            };

            return View(model);
        }

        public ActionResult Video()
        {
            return PartialView("_video");
        }

        public ActionResult Document()
        {
            return PartialView("_document");
        }

        public ActionResult Text()
        {
            return PartialView("_text");
        }

        public ActionResult StartQuiz()
        {
            return PartialView("_startquiz");
        }

        public ActionResult Quiz()
        {
            return PartialView("_quiz");
        }

        public ActionResult QuizAttemptSummary()
        {
            return PartialView("_quizattemptsummary");
        }

        public ActionResult QuizResponse()
        {
            return PartialView("_quizresponse");
        }

        public ActionResult QuizQuestion()
        {
            return PartialView("_quizquestion");
        }

        public ActionResult Examination() {
            return View();
        }

        public ActionResult Exam()
        {
            return PartialView("_exam");
        }

        public ActionResult StartExam()
        {
            return PartialView("_startexam");
        }

        public ActionResult ExamQuestion()
        {
            return PartialView("_examquestion");
        }

        public ActionResult ExamAttemptSummary()
        {
            return PartialView("_examattemptsummary");
        }

        public ActionResult Review()
        {
            return PartialView("_review");
        }

        public ActionResult Survey()
        {
            return PartialView("_survey");
        }

        public ActionResult StartSurvey()
        {
            return PartialView("_startsurvey");
        }

        public ActionResult SurveyQuestion()
        {
            return PartialView("_surveyquestion");
        }

        public ActionResult FinishSurvey()
        {
            return PartialView("_finishsurvey");
        }

        public ActionResult CompleteCourse()
        {
            return PartialView("_completecourse");
        }

        //public ActionResult Exam(string url, int courseId = 0)
        //{
        //    if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
        //        return AccessDeniedView();
        //    }

        //    var exam = _examinationFactory
        //                        .All(x => !x.IsDeleted && x.CourseId == courseId, false)
        //                        .FirstOrDefault();

        //    if (exam == null) {
        //        return NotFoundView();
        //    }

        //    return View(exam);
        //}

        public ActionResult Courses()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedView();
            }

            var advert = _advertFactory.GetCurrentTopLocationBanner((int)AdLocation.CourseCatalogue);

            var model = new CourseCataloguePageModel
            {
                TopSectionAd = advert
            };
            return View(model);
        }

        public ActionResult Emp_certificate()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedView();
            }

            return View();
        }

        public ActionResult Videos()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedView();
            }

            return View();
        }


        public PartialViewResult GetComments(int postId,int currLessonId)
        {
            //DbContext
            Models.SBLMSEntities dbContext = new Models.SBLMSEntities();
            IList<CommentsVM> comments = (from m in dbContext.Comments
                                               join e1 in dbContext.Comments on m.ParentId equals e1.Id                                               
                                              into groupjoin
                                               from e1 in groupjoin.DefaultIfEmpty()
                                              where m.LessonId == currLessonId && m.IsDeleted == false && m.Status !=2
                                               select new CommentsVM
                                               {
                                                   ComID = m.Id,
                                                   CommentedDate = m.CommentedDate.Value,
                                                   CommentMsg = m.CommentMessage,
                                                   ParentId = m.ParentId,
                                                   ParentMessage = new ParentMessage
                                                   {
                                                       ParId = m.ParentId,//e1.ParentId,
                                                       Parmessage = e1.CommentMessage,
                                                       Parusername = e1.User.AspNetUser.FirstName.ToString() + " " + e1.User.AspNetUser.LastName.ToString(),
                                                       Pardate = e1.CommentedDate,//e1.CommentedDate.Value
                                                   },
                                                   ApplicationUser = new ApplicationUserVM
                                                   {
                                                       AppUserId = m.User.AspNetUser.Id,
                                                       AppUserName = m.User.AspNetUser.FirstName.ToString() +" " + m.User.AspNetUser.LastName.ToString()
                                                   },
                                                   Posts = new PostsVM
                                                   {
                                                       PostID = m.PostId,
                                                       Message = m.CommentMessage
                                                   },
                                                   LessonId = m.LessonId,
                                                   Status = m.Status
                                               }).ToList();


            Debugger.NotifyOfCrossThreadDependency();

           return PartialView("_comments",comments);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult AddComment(CommentsVM comment, int postId,int currLessonId)
        {
            //bool result = false;
            Comment commentEntity = null;
            Models.SBLMSEntities dbContext = new Models.SBLMSEntities();
            int userId = _workContext.User.Id;
            int organizationalId = _workContext.User.OrganizationId;

            var user = dbContext.Users.FirstOrDefault(u => u.ApplicationUserId == userId);
            var post = dbContext.Posts.FirstOrDefault(p => p.Id == postId);
            var newCommentId = 0;

            if (comment != null)
            {
                commentEntity = new Comment
                {
                    CommentMessage = comment.CommentMsg,
                    CommentedDate = CommonHelper.GetCurrentDate(),
                    CreatedDate = CommonHelper.GetCurrentDate(),
                    OrganizationId = organizationalId,
                    UserId = userId,
                    ParentId = null, //To be set to Null
                    LessonId = currLessonId,
                    LastModifiedById = userId,
                    ModifiedDate = CommonHelper.GetCurrentDate()
                };

                if (user != null && post != null)
                {
                    post.Comments.Add(commentEntity);
                    user.Comments.Add(commentEntity);

                    dbContext.SaveChanges();
                    //result = true;
                    newCommentId = commentEntity.Id;
                }

                //Find the comments with flagged words and set the status
                var status =_commentsFactory.ExecuteProcedure<CommentMessageModel>("SP_FlagComments", newCommentId).ToList();

            }

            return RedirectToAction("GetComments", "Course", new { postId = postId,currLessonId= currLessonId });
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult AddSubComment(CommentsVM comment, int postId)
        {
            //bool result = false;
            Comment commentEntity = null;
            Models.SBLMSEntities dbContext = new Models.SBLMSEntities();
            int userId = _workContext.User.Id;
            int organizationalId = _workContext.User.OrganizationId;
            int currLessonId = comment.LessonId;

            var user = dbContext.Users.FirstOrDefault(u => u.ApplicationUserId == userId);
            var post = dbContext.Posts.FirstOrDefault(p => p.Id == postId);
            var newCommentId = 0;

            if (comment != null)
            {
                commentEntity = new Comment
                {
                    CommentMessage = comment.CommentMsg,
                    CommentedDate = CommonHelper.GetCurrentDate(),
                    CreatedDate = CommonHelper.GetCurrentDate(),
                    OrganizationId = organizationalId,
                    UserId = userId,
                    ParentId = comment.ParentId, //To be set to Null
                    LessonId = currLessonId,
                    LastModifiedById = userId,
                    ModifiedDate = CommonHelper.GetCurrentDate()
                };

                if (user != null && post != null)
                {
                    post.Comments.Add(commentEntity);
                    user.Comments.Add(commentEntity);

                    dbContext.SaveChanges();
                    newCommentId = commentEntity.Id;
                }
            }

            //Find the comments with flagged words and set the status
            var status = _commentsFactory.ExecuteProcedure<CommentMessageModel>("SP_FlagComments", newCommentId).ToList();
            return RedirectToAction("GetComments", "Course", new { postId = postId, currLessonId = currLessonId });
        }       
    }
}