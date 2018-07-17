using DataTables.AspNet.Core;
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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SterlingBankLMS.Web.Api
{
    [RoutePrefix("api/Exam")]
    public class ExamController : BaseApiController
    {

        private readonly ExaminationFactory _examFactory;
        public readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionSvc;

        public ExamController(ExaminationFactory examFactory, IWorkContext workContext,IPermissionService permissionSvc)
        {
            _examFactory = examFactory;
            _workContext = workContext;
            _permissionSvc = permissionSvc;
        }

        
        [HttpGet]
        [Route("GetDatatableExams")]
        public IHttpActionResult GetDatatableExams(IDataTablesRequest request)
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            var exam = _examFactory.ExecuteProcedure<ExaminationModel>("Sp_GetExaminationList", _workContext.User.OrganizationId, request.Search.Value, index, request.Length).ToList();


            var returnObject = new SearchResponse<ExaminationModel>
            {
                draw = request.Draw,
                recordsTotal = exam.Count > 0 ? exam[0].TotalRecords : 0,
                recordsFiltered = exam.Count > 0 ? exam[0].TotalRecords : 0,
                data = exam
            };

            return Ok(returnObject);
        }

        [HttpGet]
        [Route("GetExams")]
        public IHttpActionResult GetExams()
        {
            var result = new ApiResult<List<ExaminationModel>>();

            var exam = _examFactory.All(x => !x.IsDeleted, false);

            var examModel = exam.MapTo<List<Examination>, List<ExaminationModel>>();

            result.HasError = false;
            result.Result = examModel;
            return Ok(result);
        }


        [HttpGet]
        [Route("GetExam")]
        public IHttpActionResult GetExam(int Id)
        {
            var result = new ApiResult<ExaminationModel>();

            var exam = _examFactory.Find(Id);

            if (exam == null)
                return BadRequest("Exam not found");

            var examModel = exam.MapTo<Examination, ExaminationModel>();

            result.HasError = false;
            result.Result = examModel;
            return Ok(result);
        }


        [HttpGet]
        [Route("GetCourseExam")]
        public IHttpActionResult GetCourseExam(int CourseId)
        {
            var result = new ApiResult<ExaminationModel>();

            var exam = _examFactory.Find(x => x.CourseId == CourseId && !x.IsDeleted, false);

          
            var examModel = exam.MapTo<Examination, ExaminationModel>();

            result.HasError = false;
            result.Result = examModel;
            return Ok(result);
        }

        [HttpPost]
        [Route("AddorUpdate")]
        public IHttpActionResult AddorUpdate(ExaminationModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageExamination))
            {
                return AccessDeniedResult();
            }

            var result = new ApiResult<ExaminationModel>();

            if (!ModelState.IsValid)
            {
                return BadRequest("Please check if you have provided valid data");
            }

            if (model.IngoreDueDate)
            {
                model.StartDate = null;
                model.EndDate = null;
            }


            if (!model.IsStandAlone)
            {
                model.ExamType = ExaminationType.CourseExam;
            }
            else
            {
                model.ExamType = ExaminationType.Independent;
                model.CourseId = null;
            }


            if (model.IgnoreTimeLimit)
            {
                model.HourLimit = null;
                model.MinuteLimit = null;
            }


            var exam = model.Id == 0 ? model.MapTo<ExaminationModel, Examination>() :
                _examFactory.IncludeFilter(x => x.ExamQuestions.Where(b => !b.IsDeleted).OrderBy(b => b.SortOrder),
                x => x.ExamQuestions.Where(b => !b.IsDeleted).Select(y => y.Options.Where(z => !z.IsDeleted)))
               .Where(a => a.Id == model.Id).FirstOrDefault();

            if (exam == null)
            {
                return BadRequest("This exam does not exist");
            }

            exam.ModifiedDate = CommonHelper.GetCurrentDate();
            exam.LastModifiedById = _workContext.User.Id;
            exam.CourseId = model.CourseId;
            exam.Description = model.Description;

            if (!model.IngoreDueDate)
            {
                exam.EndDate = DateTime.ParseExact(model.EndDateFormat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                exam.StartDate = DateTime.ParseExact(model.StartDateFormat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            exam.ExamRetakeCount = model.ExamRetakeCount;
            exam.ExamType = model.ExamType;
            exam.HourLimit = model.HourLimit;
            exam.MinuteLimit = model.MinuteLimit;
            exam.PassGrade = model.PassGrade;

            if (!_examFactory.ValidateNameExists(exam.Name, exam.Id))
                return BadRequest("Exam name already exists");

            if (exam.Id == 0)
            {
                exam.CreatedDate = CommonHelper.GetCurrentDate();
                exam.CreatedById = _workContext.User.Id;
                exam.OrganizationId = (int)_workContext.User.OrganizationId;
                _examFactory.Add(exam);
            }
            else
            {
                _examFactory.Update(exam);
            }
            var returnObj = exam.MapTo<Examination, ExaminationModel>();
            model.Id = exam.Id;
            result.HasError = false;
            result.Message = model.Id == 0 ? "Successfully added" : "Successfully updated";
            result.Result = returnObj;
            return Ok(result);
        }

        [HttpPost]
        [Route("CloneExam")]
        public IHttpActionResult CloneExam(int ExamId, string Name)
        {
            if (Name.Trim().Length == 0 || ExamId == 0)
            {
                return BadRequest("Invalid data provided");
            }
            var result = new ApiResult<bool>();
            _examFactory.CloneExam(ExamId, Name, _workContext.User.Id);
            result.HasError = true;
            result.Message = "Examination successfully duplicated";
            return Ok(result);
        }

        [HttpPost]
        [Route("AddorUpdateQuestionContent")]
        public IHttpActionResult AddorUpdateQuestionContent(QuizQuestionModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageExamination))
            {
                return AccessDeniedResult();
            }

            var result = new ApiResult<QuizQuestionModel>();

            if (!ModelState.IsValid)
                return BadRequest("Please check if you have provided valid data");

            var question = model.MapTo<QuizQuestionModel, ExaminationQuestion>();


            var savedLesson = _examFactory.AddorUpdateExamQuestion(question, _workContext.User.Id, _workContext.User.OrganizationId);

            if (savedLesson == null)
                return BadRequest("Error encountered when saving changes, please try again");

            var examModel = savedLesson.MapTo<ExaminationQuestion, QuizQuestionModel>();
            result.HasError = false;
            result.Message = model.Id == 0 ? "Successfully added" : "Successfully updated";
            result.Result = examModel;

            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteExam")]
        public IHttpActionResult DeleteExam(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageExamination))
            {
                return AccessDeniedResult();
            }
            var result = new ApiResult<ExaminationModel>();
            var attemptCount = _examFactory.ValidateExamForDelete(Id);
            if (attemptCount > 0)
                return BadRequest("This exam has has been taken by " + attemptCount + " users and cannot be deleted");

            var exam = _examFactory.IncludeFilter(x => x.ExamQuestions.Where(b => !b.IsDeleted),
                x => x.ExamQuestions.Where(b => !b.IsDeleted).Select(c => c.Options.Where(d => !d.IsDeleted)))
                .Where(x => x.Id == Id).FirstOrDefault();
            if (exam == null)
                return BadRequest("Exam cannot be found");

            _examFactory.DeleteExam(exam, _workContext.User.Id);

            result.HasError = false;
            result.Message = "Exam successfully deleted";

            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteQuestion")]
        public IHttpActionResult DeleteQuestion(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageExamination))
            {
                return AccessDeniedResult();
            }

            var result = new ApiResult<QuizQuestionModel>();
            var Exam = _examFactory.GetExamQuestionIncludingOptions(Id);

            if (Exam == null)
            {
                return BadRequest("This question does not exist");
            }

            _examFactory.DeleteExamQuestion(Exam, _workContext.User.Id);

            result.HasError = false;
            result.Message = "Successfully deleted";
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteQuestionOption")]
        public IHttpActionResult DeleteQuestionOption(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageExamination))
            {
                return AccessDeniedResult();
            }

            var result = new ApiResult<QuizQuestionOptionModel>();
            var Quiz = _examFactory.GetContext().Set<ExaminationQuestionOption>().Where(x => x.Id == Id).FirstOrDefault();
            if (Quiz == null)
            {
                return BadRequest("This option does not exist");
            }

            _examFactory.DeleteQuestionOption(Quiz, _workContext.User.Id);

            result.HasError = false;
            result.Message = "Successfully deleted";
            return Ok(result);
        }



        [HttpPost]
        [Route("ResortExamQuestions")]
        public IHttpActionResult ResortExamQuestions(ExaminationModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageExamination))
            {
                return AccessDeniedResult();
            }

            var result = new ApiResult<bool>();

            if (!ModelState.IsValid)
                return BadRequest("Invalid data provided");

            var modelQuestions = model.ExamQuestions.MapTo<List<QuizQuestionModel>, List<ExaminationQuestion>>();
            var savedModelList = _examFactory.IncludeFilter(x => x.ExamQuestions.Where(b => !b.IsDeleted))
            .Where(a => a.Id == model.Id).FirstOrDefault().ExamQuestions.ToList();

            var reSort = _examFactory.ResortExamQuestions(modelQuestions, savedModelList, _workContext.User.Id);
            if (!reSort)
                return BadRequest("We encountered an error while processing your request. Please try again");

            result.HasError = false;
            return Ok(result);
        }
        
    }
}
