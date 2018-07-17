using DataTables.AspNet.Core;
using Newtonsoft.Json;
using Nop.Core.Caching;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Enums;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;

namespace SterlingBankLMS.Web.Api
{
    /// <summary>
    /// Course Api
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Course")]
    public class CourseController : BaseApiController
    {

        private readonly CourseFactory _courseFactory;
        private readonly LearningAreaFactory _LearningAreaFactory;
        private readonly ModuleFactory _ModuleFactory;
        private readonly LessonFactory _LessonFactory;
        private readonly FileUploader _fileUploader;
        private readonly LearningGroupFactory _learningGroupFactory;
        private readonly CommentsFactory _commentsFactory;

        private readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionSvc;
        private readonly ICacheManager _cacheManager;

        public CourseController(
            CourseFactory courseFactory, LearningAreaFactory LearningAreaFactory,
            ModuleFactory ModuleFactory, LessonFactory lessonFactory,
            FileUploader fileUploader, LearningGroupFactory learningGroupFactory,
            IPermissionService permissionSvc, IWorkContext workContext,
            ICacheManager cacheManager, CommentsFactory commentsFactory
            )
        {
            _courseFactory = courseFactory;
            _LearningAreaFactory = LearningAreaFactory;
            _ModuleFactory = ModuleFactory;
            _workContext = workContext;
            _LessonFactory = lessonFactory;
            _fileUploader = fileUploader;
            _permissionSvc = permissionSvc;
            _learningGroupFactory = learningGroupFactory;
            _cacheManager = cacheManager;
            _commentsFactory = commentsFactory;
        }

        /// <summary>
        /// Search Course Filter 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCourseFilterValues(CourseFilter? filter)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            switch (filter) {

                case CourseFilter.LearningArea:

                    var learningAreaDDls = _LearningAreaFactory.LearningAreaDDL(_workContext.User.OrganizationId);

                    return Ok(new ApiResult<IEnumerable<DepartmentDropdownListDto>>() { Result = learningAreaDDls });

                default:
                    return Ok();
            }
        }

        /// <summary>
        /// Get course module names for course details
        /// </summary>
        /// <param name="id">courseId</param>
        /// <returns></returns>
        [Route("GetCourseModules")]
        public IHttpActionResult GetCourseModules(int id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            var modulesDto = _ModuleFactory.GetCourseModulesAndLessonsDto(courseId: id, orgId: _workContext.User.OrganizationId);
            var modules = modulesDto.MapTo<IEnumerable<CourseModuleNameDto>, List<CourseModuleNameModel>>();

            var response = new ApiResult<CollectionResult<CourseModuleNameModel>>
            {
                Result = new CollectionResult<CourseModuleNameModel>(modules)
                {
                    Total = modules.Count
                }
            };
            return Ok(response);
        }

        /// <summary>
        /// Search and filter Courses
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("SearchCourse")]
        public IHttpActionResult Search([FromUri] CourseSearchModel model)
        {
            if (model == null)
                return BadRequest("No search params");

            model.ValidateSearchQuery();

            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedResult();
            }

            var coursesDto = _courseFactory.SearchCourses(model.Keywords, (int?) model.Filter, model.FilterBy, _workContext.User.OrganizationId, model.PageIndex, model.PageSize);
            var courses = coursesDto.MapTo<IEnumerable<CatalogCourseDto>, IEnumerable<CatalogCourseModel>>();

            var totalCount = coursesDto.FirstOrDefault()?.TotalCount ?? 0;
            var response = new ApiResult<dynamic>
            {
                Result = new
                {
                    Courses = courses,
                    HasMore = CollectionExtensions.HasMorePage(totalCount, model.PageIndex, model.PageSize),
                    TotalCount = totalCount
                }
            };

            return Ok(response);
        }

        /// <summary>
        /// Add or update learning area using id of model 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ApiResult Object</returns>

        [HttpPost]
        [Route("AddorUpdateLearningArea")]
        public IHttpActionResult AddorUpdateLearningArea(LearningAreaModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse)) {
                return AccessDeniedResult();
            }

            var result = new ApiResult<LearningAreaDto>();

            if (ModelState.IsValid) {
                var learningArea = model.Id == 0 ? new LearningArea() : _LearningAreaFactory.Find(model.Id);
                if (learningArea != null) {
                    learningArea.Id = model.Id;
                    learningArea.Name = model.Name;
                    learningArea.ModifiedDate = CommonHelper.GetCurrentDate();
                    learningArea.LastModifiedById = _workContext.User.Id;

                    if (!_LearningAreaFactory.ValidateNameExists(learningArea.Name, learningArea.Id))
                        return BadRequest("This learning area already exists");

                    if (learningArea.Id == 0) {
                        learningArea.CreatedDate = CommonHelper.GetCurrentDate();
                        learningArea.CreatedById = _workContext.User.Id;
                        learningArea.OrganizationId = (int) _workContext.User.OrganizationId;
                        _LearningAreaFactory.Add(learningArea);
                    }
                    else {
                        _LearningAreaFactory.Update(learningArea);
                    }
                    _LearningAreaFactory.RemoveLearningAreaCacheKey(_workContext.User.OrganizationId);

                    result.HasError = false;
                    result.Message = model.Id == 0 ? "Successfully added" : "Successfully updated";
                    result.Result = new LearningAreaDto { LearningArea = learningArea };

                }
                else {
                    result.HasError = true;
                    result.Message = "This learning area does not exist";
                }

            }
            else {
                result.HasError = true;
                result.Message = "Please check if you have provided valid data";
            }

            return Ok(result);

        }


        [HttpGet]
        [Route("GetLearningArea")]
        public IHttpActionResult GetLearningArea(int Id)
        {
            var result = new ApiResult<LearningAreaDto>();

            var learningArea = _LearningAreaFactory.GetLearningArea(Id);
            if (learningArea != null) {
                result.HasError = false;
                result.Result = learningArea;
            }
            else {
                result.HasError = true;
                result.Message = "Learning area does not exist";
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("GetLearningAreas")]
        public IHttpActionResult GetLearningAreas()
        {
            var result = new ApiResult<List<LearningAreaDto>>();

            var learningArea = _LearningAreaFactory.GetLearningAreas(_workContext.User.OrganizationId);
            result.HasError = false;
            result.Result = learningArea;
            return Ok(result);
        }

        [HttpGet]
        [Route("GetCourses")]
        public IHttpActionResult GetCourses()
        {
            var result = new ApiResult<List<CourseDto>>();

            var courses = _courseFactory.All(x => x.OrganizationId == _workContext.User.OrganizationId && !x.IsDeleted, false).Select(x => new CourseDto
            {
                Id = x.Id,
                LearningAreaId = x.LearningAreaId,
                DueDate = x.DueDate,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                Name = x.Name
                //ExamRetakeCount = x.ExamRetakeCount

            }).ToList();
            result.HasError = false;
            result.Result = courses;
            return Ok(result);
        }

        [HttpGet]
        [Route("GetDatatableCourses")]
        public IHttpActionResult GetDatatableCourses(IDataTablesRequest request)
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            var course = _courseFactory.GetCoursesTable(request.Search.Value, index, request.Length, _workContext.User.OrganizationId).ToList();


            var returnObject = new SearchResponse<CourseDto>
            {
                draw = request.Draw,
                recordsTotal = course.Count > 0 ? course[0].TotalRecords : 0,
                recordsFiltered = course.Count > 0 ? course[0].TotalRecords : 0,
                data = course
            };

            return Ok(returnObject);
        }

        [HttpGet]
        [Route("GetCourse")]
        public IHttpActionResult GetCourse(int Id)
        {
            var result = new ApiResult<CourseModel>();

            var course = _courseFactory.IncludeFilter(x => x.Modules.Where(b => !b.IsDeleted).OrderBy(b => b.SortOrder))
               .Where(a => a.Id == Id).FirstOrDefault();

            if (course == null)
                return BadRequest("Course not found");

            var courseModel = course.MapTo<Course, CourseModel>();
            result.HasError = false;
            result.Result = courseModel;
            return Ok(result);
        }

        /// <summary>
        /// Get Lessons within a course module via module id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetModuleLesson")]
        public IHttpActionResult GetModuleLesson(int Id)
        {
            var result = new ApiResult<IList<ModuleLessonModel>>();

            string key = "moduleLessons-" + Id;

            var Lesson = _LessonFactory.IncludeFilter(x => x.Questions.Where(y => !y.IsDeleted).OrderBy(y => y.SortOrder),
                         x => x.Questions.Where(y => !y.IsDeleted).Select(z => z.Options.Where(a => !a.IsDeleted)))
                         .Where(x => x.ModuleId == Id && !x.IsDeleted).OrderBy(x => x.SortOrder).ToList();

            if (Lesson.Count == 0) {
                result.HasError = true;
                result.Message = "No lessons added";
                return Ok(result);
            }

            var LessonModel = Lesson.MapTo<List<Lesson>, List<ModuleLessonModel>>();
            result.HasError = false;
            result.Result = LessonModel;
            return Ok(result);
        }

        /// <summary>
        /// Delete course if not taken by any user
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// 
        [HttpDelete]
        [Route("DeleteCourse")]
        public IHttpActionResult DeleteCourse(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse)) {
                return AccessDeniedResult();
            }

            //TODO: DELETE EXAM AS WELL

            var result = new ApiResult<CourseModel>();

            if (_courseFactory.ValidateCourseForDelete(Id) > 0)
                return BadRequest("This course has begun and has assigned users and cannot be deleted");

            var course = _courseFactory.Find(Id);
            if (course == null)
                return BadRequest("Course cannot be found");

            course.IsDeleted = true;
            course.LastModifiedById = _workContext.User.Id;
            course.ModifiedDate = AppHelper.GetCurrentDate();
            _courseFactory.Update(course);

            result.HasError = false;
            result.Message = "Course successfully deleted";

            return Ok(result);
        }


        [HttpPost]
        [Route("UpdatePublishCourse")]
        public IHttpActionResult UpdatePublishCourse(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse)) {
                return AccessDeniedResult();
            }

            var result = new ApiResult<bool>();

            var course = _courseFactory.Find(Id);
            if (course == null)
                return BadRequest("Course cannot be found");

            course.IsPublished = !course.IsPublished;
            _courseFactory.Update(course);

            result.Result = course.IsPublished;
            result.HasError = false;
            result.Message = "Course successfully " + (course.IsPublished ? "published" : "Un published");

            return Ok(result);
        }


        [HttpPost]
        [Route("AddorUpdate")]
        public IHttpActionResult AddorUpdate(CourseModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse)) {
                return AccessDeniedResult();
            }
            var result = new ApiResult<Course>();

            if (ModelState.IsValid) {
                if (model.IgnoreDueDate)
                    model.DueDate = null;

                var course = model.Id == 0 ? model.MapTo<CourseModel, Course>() : _courseFactory.Find(model.Id);

                if (course != null) {
                    if (model.ImageUpload != null && model.CurrentStep == 1) {
                        string pathstring = AppConstants.FileUploadPath + (int)_workContext.User.OrganizationId + "/";
                        var path = _fileUploader.UploadFile(model.ImageUpload, pathstring);
                        if (path != null) {
                            course.ImageUrl = path;
                        }
                        else {
                            return BadRequest("An error occurred");
                        }

                    }

                    course.ModifiedDate = CommonHelper.GetCurrentDate();
                    course.LastModifiedById = _workContext.User.Id;

                    var validateCourseName = true;

                    if (model.CurrentStep == 1) {
                        course.LearningAreaId = model.LearningAreaId;
                        course.Name = model.Name;
                        course.Objectives = model.Objectives;
                        course.Overview = model.Overview;
                        course.Description = model.Description;
                        course.ShortDescription = model.ShortDescription;
                        course.DueDate = model.DueDate;
                        course.Objectives = model.Objectives;
                        course.EstimatedDuration = model.EstimatedDuration;
                        course.HoursPerWeek = model.HoursPerWeek;
                        course.ShortDescription = model.ShortDescription;
                        validateCourseName = _courseFactory.ValidateCourseNameExists(course.Name, course.Id);
                    }
                    else {
                        //course.ExamRetakeCount = model.CourseIgnoreRetake ? null : model.ExamRetakeCount;
                        //course.PassGrade = model.PassGrade;
                        course.IsPublished = model.IsPublished;
                        course.HasCertificate = model.HasCertificate;
                    }

                    if (!validateCourseName)
                    {
                        return BadRequest("This course name already exists");
                    }

                    if (course.Id == 0) {
                        course.CreatedDate = CommonHelper.GetCurrentDate();
                        course.CreatedById = _workContext.User.Id;
                        course.OrganizationId = (int)_workContext.User.OrganizationId;
                        _courseFactory.Add(course);

                    }
                    else {
                        _courseFactory.Update(course);
                        _courseFactory.ClearCachedCourse(_workContext.User.OrganizationId, course.Id);
                    }

                    result.HasError = false;
                    result.Message = model.Id == 0 ? "Successfully added" : "Successfully updated";
                    result.Result = course;

                }
                else {
                    result.HasError = true;
                    result.Message = "This course does not exist";
                }

            }
            else {
                result.HasError = true;
                result.Message = "Please check if you have provided valid data";
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("CloneCourse")]
        public IHttpActionResult CloneCourse(int CourseId, string Name)
        {
            if(Name.Trim().Length == 0 || CourseId == 0)
            {
                return BadRequest("Invalid data provided");
            }
            var result = new ApiResult<bool>();
            _courseFactory.CloneCourse(CourseId, Name, _workContext.User.Id);
            result.HasError = true;
            result.Message = "Course successfully duplicated";
            return Ok(result);
        }

        [HttpPost]
        [Route("ResortCourseItems")]
        public IHttpActionResult ResortCourseItems(CourseModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse)) {
                return AccessDeniedResult();
            }
            var result = new ApiResult<bool>();

            if (!ModelState.IsValid)
                return BadRequest("Invalid data provided");

            var modelModuleList = model.Modules.MapTo<List<ModuleModel>, List<Module>>();
            var savedModelList = _courseFactory.IncludeFilter(x => x.Modules.Where(b => !b.IsDeleted), x => x.Modules.Select(y => y.Lessons.Where(z => !z.IsDeleted)))
            .Where(a => a.Id == model.Id).FirstOrDefault().Modules.ToList();

            var reSort = _ModuleFactory.ResortModule(modelModuleList, savedModelList, _workContext.User.Id);
            if (!reSort)
                return BadRequest("We encountered an error while processing your request. Please try again");

            result.HasError = false;
            return Ok(result);
        }


        [HttpDelete]
        [Route("DeleteLearningArea")]
        public IHttpActionResult DeleteLearningArea(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse)) {
                return AccessDeniedResult();
            }
            var result = new ApiResult<LearningAreaModel>();
            var learningAreaDto = _LearningAreaFactory.GetLearningArea(Id);

            if (learningAreaDto != null) {
                if (learningAreaDto.CourseCount == 0) {
                    learningAreaDto.LearningArea.IsDeleted = true;
                    _LearningAreaFactory.Update(learningAreaDto.LearningArea);
                    _LearningAreaFactory.RemoveLearningAreaCacheKey(_workContext.User.OrganizationId);
                    result.HasError = false;
                    result.Message = "Successfully deleted";
                }
                else {
                    return BadRequest("You cannot delete a learning area that has associated courses");
                }
            }
            else {
                return BadRequest("Learning area does not exist");
            }
            return Ok(result);
        }


        [HttpPost]
        [Route("AddorUpdateModule")]
        public IHttpActionResult AddorUpdateModule(ModuleModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse)) {
                return AccessDeniedResult();
            }
            var result = new ApiResult<Module>();

            if (!ModelState.IsValid) {
                return BadRequest("Please check if you have provided valid data");
            }
            var module = model.Id == 0 ? new Module() : _ModuleFactory.Find(model.Id);

            if (module == null) {
                return BadRequest("This course module does not exist");
            }

            module.Id = model.Id;
            module.Name = model.Name;
            module.ModifiedDate = CommonHelper.GetCurrentDate();
            module.LastModifiedById = _workContext.User.Id;

            if (module.Id == 0) {
                module.CreatedDate = CommonHelper.GetCurrentDate();
                module.CreatedById = _workContext.User.Id;
                //module.OrganizationId = (int) _workContext.User.OrganizationId;
                module.CourseId = model.CourseId;
                var curSort = _ModuleFactory.GetModuleMaxSort(model.CourseId);
                module.SortOrder = curSort + 1;
                _ModuleFactory.Add(module);
            }
            else {
                _ModuleFactory.Update(module);
            }

            result.HasError = false;
            result.Message = model.Id == 0 ? "Successfully added" : "Successfully updated";
            result.Result = module;

            return Ok(result);

        }

        [HttpPost]
        [Route("AddorUpdateLesson")]
        public IHttpActionResult AddorUpdateLesson(ModuleLessonModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse)) {
                return AccessDeniedResult();
            }
            var result = new ApiResult<Lesson>();

            if (!ModelState.IsValid) {
                return BadRequest("Please check if you have provided valid data");
            }
            var lesson = model.Id == 0 ? new Lesson() : _LessonFactory.Find(model.Id);

            if (lesson == null) {
                return BadRequest("This course module does not exist");
            }


            lesson.ModifiedDate = CommonHelper.GetCurrentDate();
            lesson.LastModifiedById = _workContext.User.Id;

            if (lesson.Id == 0) {
                lesson.CreatedDate = CommonHelper.GetCurrentDate();
                lesson.CreatedById = _workContext.User.Id;
                lesson.OrganizationId = (int) _workContext.User.OrganizationId;
                lesson.ModuleId = model.ModuleId;
                lesson.SortOrder = _LessonFactory.GetModuleLessonMaxSort(model.ModuleId) + 1;
                lesson.LessonContentType = model.LessonContentType;
                lesson.Name = model.Name;
                _LessonFactory.Add(lesson);
            }
            else {

                lesson.Name = model.Name;
                _LessonFactory.Update(lesson);
            }

            result.HasError = false;
            result.Message = model.Id == 0 ? "Successfully added" : "Successfully updated";
            result.Result = lesson;

            return Ok(result);

        }

        [HttpDelete]
        [Route("DeleteModule")]
        public IHttpActionResult DeleteModule(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse)) {
                return AccessDeniedResult();
            }
            var result = new ApiResult<Module>();
            var Module = _ModuleFactory.GetIncluding(x => x.Id == Id, true, x => x.Lessons);

            //TODO: Validate Course Module Delete

            if (Module == null) {
                return BadRequest("This module does not exist");
            }

            _ModuleFactory.DeleteModule(Module, _workContext.User.Id);

            result.HasError = false;
            result.Message = "Successfully deleted";
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteLesson")]
        public IHttpActionResult DeleteLesson(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse)) {
                return AccessDeniedResult();
            }
            var result = new ApiResult<Lesson>();
            var lesson = _LessonFactory.GetIncluding(x => x.Id == Id, true, x => x.Questions);

            //TODO: Validate Course Module Delete

            if (lesson == null) {
                return BadRequest("This lesson does not exist");
            }

            _LessonFactory.DeleteLesson(lesson, _workContext.User.Id);

            result.HasError = false;
            result.Message = "Successfully deleted";
            return Ok(result);
        }

        /// <summary>
        /// Delete Quiz Lesson content Question
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteQuiz")]
        public IHttpActionResult DeleteQuiz(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse)) {
                return AccessDeniedResult();
            }

            var result = new ApiResult<QuizQuestionModel>();
            var Question = _LessonFactory.GetContext().Set<QuizQuestion>().Where(x => x.Id == Id).FirstOrDefault();

            //TODO: Validate Course Module Delete

            if (Question == null) {
                return BadRequest("This question does not exist");
            }

            _LessonFactory.DeleteQuizQuestion(Question, _workContext.User.Id);

            result.HasError = false;
            result.Message = "Successfully deleted";
            return Ok(result);
        }


        [Route("DeleteQuizOption")]
        public IHttpActionResult DeleteQuizOption(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse))
            {
                return AccessDeniedResult();
            }

            var result = new ApiResult<QuizQuestionOptionModel>();
            var option = _LessonFactory.GetQuizQuestionOption(Id);
            if (option == null) {
                return BadRequest("This option does not exist");
            }

            _LessonFactory.DeleteQuizOption(option, _workContext.User.Id);

            result.HasError = false;
            result.Message = "Successfully deleted";
            return Ok(result);
        }

        /// <summary>
        /// Add or Update Lesson Content Details; Video,Text,Document,Quiz
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddorUpdateLessonContent")]
        public IHttpActionResult AddorUpdateLessonContent(ModuleLessonModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse)) {
                return AccessDeniedResult();
            }
            var result = new ApiResult<ModuleLessonModel>();

            if (ModelState.IsValid) {

                var lesson = model.MapTo<ModuleLessonModel, Lesson>();


                var savedLesson = _LessonFactory.AddorUpdateLessonContent(lesson, _workContext.User.Id, _workContext.User.OrganizationId);

                if (savedLesson != null) {
                    var lessonModel = savedLesson.MapTo<Lesson, ModuleLessonModel>();
                    result.HasError = false;
                    result.Message = model.Id == 0 ? "Successfully added" : "Successfully updated";
                    result.Result = lessonModel;
                }
                else {
                    return BadRequest("Error encountered when saving changes, please try again");
                }

            }
            else {
                return BadRequest("Please check if you have provided valid data");
            }

            return Ok(result);
        }


        [HttpPost]
        [Route("AddorUpdateQuizQuestion")]
        public IHttpActionResult AddorUpdateQuizQuestion(QuizQuestionModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse)) {
                return AccessDeniedResult();
            }
            var result = new ApiResult<QuizQuestionModel>();

            if (ModelState.IsValid) {

                var quiz = model.MapTo<QuizQuestionModel, QuizQuestion>();


                var savedQuestion = _LessonFactory.AddorUpdateQuizContent(quiz, _workContext.User.Id, _workContext.User.OrganizationId);

                if (savedQuestion != null) {
                    var quizModel = savedQuestion.MapTo<QuizQuestion, QuizQuestionModel>();
                    result.HasError = false;
                    result.Message = model.Id == 0 ? "Successfully added" : "Successfully updated";
                    result.Result = quizModel;
                }
                else {
                    return BadRequest("Error encountered when saving changes, please try again");
                }

            }
            else {
                return BadRequest("Please check if you have provided valid data");
            }

            return Ok(result);
        }

        /// <summary>
        /// Resort Quiz Questions Order
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("ResortQuestions")]
        public IHttpActionResult ResortQuestions(ModuleLessonModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse))
            {
                return AccessDeniedResult();
            }

            var result = new ApiResult<bool>();

            if (!ModelState.IsValid)
                return BadRequest("Invalid data provided");

            var modelQuestions = model.MapTo<ModuleLessonModel, Lesson>();


            var reSort = _LessonFactory.ResortLessonQuizQuestions(modelQuestions, _workContext.User.Id);
            if (!reSort)
                return BadRequest("We encountered an error while processing your request. Please try again");

            result.HasError = false;
            return Ok(result);
        }

        /// <summary>
        /// Get Course Module Lesson Content
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetLessonContent")]
        public IHttpActionResult GetLessonContent(int Id)
        {

            var result = new ApiResult<ModuleLessonModel>();
            var lessonContent = _LessonFactory.Find(Id);
            if (lessonContent == null)
                return BadRequest("Content not found");

            var lessonContentModel = lessonContent.MapTo<Lesson, ModuleLessonModel>();
            result.HasError = false;
            result.Result = lessonContentModel;
            return Ok(result);
        }

        [Route("getCommentsPaginate")]
        public IHttpActionResult getCommentsPaginate(int pageSize, int pageNumber)
        {
            var data = new SearchResponse<CommentMessageModel>();
            var comments = _commentsFactory.ExecuteProcedure<CommentMessageModel>("SP_GetCommentDetails", _workContext.User.OrganizationId).ToList().Where(x => x.Status == 1).ToList();
            if (comments != null)
            {
                data.recordsTotal = comments.Where(x => x.IsDeleted == false && x.Status == 1).Count();
                data.data = comments.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }

            return Ok(data);
        }

        [Route("GetCommentDetails")]
        public IHttpActionResult GetCommentDetails(string Id)
        {
            var id = Convert.ToInt32(Id);

            var data = new ApiResult<CommentMessageModel>();
            var comments = _commentsFactory.ExecuteProcedure<CommentMessageModel>("SP_GetCommentDetails", _workContext.User.OrganizationId).ToList();
            var comment = comments.Where(x => x.Id == id).FirstOrDefault();
            var userid = comment.UserId;
            comment.FlaggedCount = comments.Where(x => x.UserId == userid && x.Flagged == true).Count();
            
            if (comment != null)
            {
                data.HasError = false;
                data.Result = comment;
            }
            else
            {
                data.HasError = true;
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("DeleteComment")]
        public IHttpActionResult DeleteComment(List<CommentBulkActionModel> comments)
        {
            var data = new ApiResult<string>();
            var comment = comments.Where(x => x.IsChecked == true).ToList();
            foreach (var item in comment)
            {
                var cmt = _commentsFactory.GetIncluding(x => x.Id == item.Id, true, x => x.Organization);
                if (cmt != null)
                {
                    cmt.IsDeleted = true;
                    cmt.Status = 2;
                    cmt.LastModifiedById = _workContext.User.Id;
                    cmt.ModifiedDate = DateTime.Now;

                    _commentsFactory.Update(cmt);
                }
                else
                {
                    data.HasError = true;
                    data.Message = "Some data are invalid";
                    return Ok(data);
                }
            }
            data.HasError = false;
            data.Message = comment.Count() + " Comments successfully deleted";
            return Ok(data);
        }

        [HttpPost]
        [Route("ApproveComment")]
        public IHttpActionResult ApproveComment(List<CommentBulkActionModel> comments)
        {
            var data = new ApiResult<string>();
            var comment = comments.Where(x => x.IsChecked == true).ToList();
            foreach (var item in comment)
            {
                var cmt = _commentsFactory.GetIncluding(x => x.Id == item.Id, true, x => x.Organization);
                if (cmt != null)
                {
                    cmt.Status = 0;
                    cmt.LastModifiedById = _workContext.User.Id;
                    cmt.ModifiedDate = DateTime.Now;

                    _commentsFactory.Update(cmt);
                }
                else
                {
                    data.HasError = true;
                    data.Message = "Some data are invalid";
                    return Ok(data);
                }
            }
            data.HasError = false;
            data.Message = comment.Count() + " Comments successfully approved";
            return Ok(data);
        }

        [HttpPost]
        [Route("CommentEditSave")]
        public IHttpActionResult CommentEditSave(CommentEditActionModel comment)
        {
            var data = new ApiResult<string>();
            var cmt = _commentsFactory.GetIncluding(x => x.Id == comment.Id, true, x => x.Organization);
            if (cmt != null)
            {
                cmt.CommentMessage = comment.CommentMessage;
                cmt.LastModifiedById = _workContext.User.Id;
                cmt.ModifiedDate = DateTime.Now;
                _commentsFactory.Update(cmt);
            }
            else
            {
                data.HasError = true;
                data.Message = "Some data are invalid";
                return Ok(data);
            }
            data.HasError = false;
            data.Message = "Comment successfully saved";
            return Ok(data);
        }

    }
}