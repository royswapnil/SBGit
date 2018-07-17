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
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SterlingBankLMS.Web.Api
{
    [RoutePrefix("api/Survey")]
    public class SurveyController : BaseApiController
    {
        private readonly IWorkContext _workContext;
        private readonly SurveyFactory _surveyFactory;
        private readonly SurveyTemplateFactory _surveyTemplateFactory;
        public readonly LearningGroupFactory _learningGroupFactory;
        private readonly IPermissionService _permissionSvc;


        public SurveyController(IWorkContext workContext, SurveyFactory surveyFactory, SurveyTemplateFactory surveyTemplateFactory,
            LearningGroupFactory learningGroupFactory, IPermissionService permissionSvc)
        {
            _workContext = workContext;
            _surveyFactory = surveyFactory;
            _surveyTemplateFactory = surveyTemplateFactory;
            _learningGroupFactory = learningGroupFactory;
            _permissionSvc = permissionSvc;
        }
        /// <summary>
        /// Get SUrvey Paged Datatable
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSurveyDatatable")]
        public IHttpActionResult GetSurveyDatatable(IDataTablesRequest request)
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            int totalRecords = 0;
            var surveys = _surveyFactory.GetSurveyPagedList(request.Search.Value, index, request.Length, _workContext.User.OrganizationId, out totalRecords).ToList();

            var returnObject = new SearchResponse<SurveyDto>
            {
                draw = request.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = surveys
            };
            return Ok(returnObject);
        }

        /// <summary>
        /// Get A survey by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("GetSurvey")]
        public IHttpActionResult GetSurvey(int Id)
        {
            var result = new ApiResult<SurveyModel>();

            var survey = _surveyFactory.Find(Id);
            if (survey == null)
                return BadRequest("Survey not found");

            var surveyModel = survey.MapTo<Survey, SurveyModel>();

            result.HasError = false;
            result.Result = surveyModel;
            return Ok(result);
        }

        [HttpGet]
        [Route("GetCourseSurvey")]
        public IHttpActionResult GetCourseSurvey(int Id)
        {
            var result = new ApiResult<SurveyModel>();

            var survey = _surveyFactory.Find(x => x.CourseId == Id && !x.IsDeleted, false);
           

            var surveyModel = survey.MapTo<Survey, SurveyModel>();

            result.HasError = false;
            result.Result = surveyModel;
            return Ok(result);
        }

        [HttpGet]
        [Route("GetExamSurvey")]
        public IHttpActionResult GetExamSurvey(int Id)
        {
            var result = new ApiResult<SurveyModel>();

            var survey = _surveyFactory.Find(x => x.ExaminationId == Id && !x.IsDeleted, false);
         
            var surveyModel = survey.MapTo<Survey, SurveyModel>();

            result.HasError = false;
            result.Result = surveyModel;
            return Ok(result);
        }

        [HttpGet]
        [Route("GetTrainingSurvey")]
        public IHttpActionResult GetTrainingSurvey(int Id)
        {
            var result = new ApiResult<SurveyModel>();

            var survey = _surveyFactory.Find(x=>x.TrainingId == Id && !x.IsDeleted,false);
          

            var surveyModel = survey.MapTo<Survey, SurveyModel>();

            result.HasError = false;
            result.Result = surveyModel;
            return Ok(result);
        }

        [HttpPost]
        [Route("AddorUpdate")]
        public IHttpActionResult AddorUpdate(SurveyModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
                return AccessDeniedResult();

            var result = new ApiResult<SurveyModel>();
            int surveyUsers = 0;

            if (!ModelState.IsValid)
            {
                return BadRequest("Please check if you have provided valid data");
            }

            var survey = model.Id == 0 ? model.MapTo<SurveyModel, Survey>() :
                _surveyFactory.Find(model.Id);

            if (survey == null)
            {
                return BadRequest("This survey does not exist");
            }

            if (!_surveyFactory.ValidateNameExists(model.Name, model.Id))
                return BadRequest("Survey name already exists");

            if (survey.Id == 0)
            {

                survey.CreatedDate = CommonHelper.GetCurrentDate();
                survey.CreatedById = _workContext.User.Id;
                survey.OrganizationId = (int)_workContext.User.OrganizationId;
                _surveyFactory.Add(survey);
            }
            else
            {
                survey.Description = model.Description;
                surveyUsers = _surveyFactory.CountSurveyUsers(survey.Id);
                if (surveyUsers == 0)
                {
                    survey.TemplateId = model.TemplateId;
                    survey.SurveyType = model.SurveyType;
                    survey.ExaminationId = null;
                    survey.CourseId = null;
                    survey.TrainingId = null;
                    switch (survey.SurveyType)
                    {
                        case SurveyType.CourseSurvey: survey.CourseId = model.SurveyAssoID; break;
                        case SurveyType.ExamSurvey: survey.ExaminationId = model.SurveyAssoID; break;
                        case SurveyType.TrainingSurvey: survey.TrainingId = model.SurveyAssoID; break;
                    }
                }
                
                _surveyFactory.Update(survey);
            }
            var returnObj = survey.MapTo<Survey, SurveyModel>();

            result.HasError = false;
            result.Message = model.Id == 0 ? "Successfully added" : "Successfully updated";
            if (surveyUsers > 0)
            {
                result.Message = result.Message + " - Users have taken this survey, thus no changes were made to the survey type and template";
            }
            result.Result = returnObj;
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteSurvey")]
        public IHttpActionResult DeleteSurvey(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
                return AccessDeniedResult();

            var result = new ApiResult<SurveyModel>();
            //var attemptCount = _surveyTemplateFactory.ValidateSurveyForDelete(Id);
            //if (attemptCount > 0)
            //    return BadRequest("This survey has has been used by " + attemptCount + " users and cannot be deleted");

            var survey = _surveyFactory.Find(Id);
            if (survey == null)
                return BadRequest("Survey cannot be found");

            survey.IsDeleted = true;
            survey.LastModifiedById = _workContext.User.Id;
            survey.ModifiedDate = CommonHelper.GetCurrentDate();
            _surveyFactory.Update(survey);

            result.HasError = false;
            result.Message = "Survey successfully deleted";

            return Ok(result);
        }


        [HttpGet]
        [Route("GetTemplateDatatable")]
        public IHttpActionResult GetTemplateDatatable(IDataTablesRequest request)
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            int totalRecords = 0;
            var templates = _surveyTemplateFactory.GetTemplatePagedList(request.Search.Value, index, request.Length, _workContext.User.OrganizationId, out totalRecords).ToList();

            var returnObject = new SearchResponse<TemplateDto>
            {
                draw = request.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = templates
            };
            return Ok(returnObject);
        }

        [HttpGet]
        [Route("GetTemplates")]
        public IHttpActionResult GetTemplates()
        {
            var result = new ApiResult<List<SurveyTemplate>>();

            var _templates = _surveyTemplateFactory.All(x => !x.IsDeleted && x.QuestionCount > 0, false);
            result.HasError = false;
            result.Result = _templates;
            return Ok(result);
        }

        [HttpGet]
        [Route("GetTemplate")]
        public IHttpActionResult GetTemplate(int Id)
        {
            var result = new ApiResult<SurveyTemplateModel>();

            var template = _surveyTemplateFactory.GetAllIncluding(x => x.Id == Id, false, x => x.Questions.Where(y => !y.IsDeleted).OrderBy(z => z.SortOrder),
                x => x.Questions.Where(y => !y.IsDeleted).Select(z => z.Options.Where(a => !a.IsDeleted))).FirstOrDefault();

            if (template == null)
                return BadRequest("Template not found");

            var templateModel = template.MapTo<SurveyTemplate, SurveyTemplateModel>();

            result.HasError = false;
            result.Result = templateModel;
            return Ok(result);
        }

        [HttpPost]
        [Route("AddorUpdateTemplate")]
        public IHttpActionResult AddorUpdateTemplate(SurveyTemplateModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
                return AccessDeniedResult();

            var result = new ApiResult<SurveyTemplateModel>();

            if (!ModelState.IsValid)
            {
                return BadRequest("Please check if you have provided valid data");
            }

            var template = model.Id == 0 ? model.MapTo<SurveyTemplateModel, SurveyTemplate>() :
                _surveyTemplateFactory.Find(model.Id);

            if (template == null)
            {
                return BadRequest("This template does not exist");
            }

            template.Name = model.Name;

            if (!_surveyTemplateFactory.ValidateNameExists(model.Name, model.Id))
                return BadRequest("Template name already exists");


            if (template.Id == 0)
            {
                template.QuestionCount = 0;
                template.CreatedDate = CommonHelper.GetCurrentDate();
                template.CreatedById = _workContext.User.Id;
                template.OrganizationId = (int)_workContext.User.OrganizationId;
                _surveyTemplateFactory.Add(template);
            }
            else
            {
                _surveyTemplateFactory.Update(template);
            }
            var returnObj = template.MapTo<SurveyTemplate, SurveyTemplateModel>();

            result.HasError = false;
            result.Message = model.Id == 0 ? "Successfully added" : "Successfully updated";
            result.Result = returnObj;
            return Ok(result);
        }

        [HttpPost]
        [Route("AddorUpdateQuestionContent")]
        public IHttpActionResult AddorUpdateQuestionContent(QuizQuestionModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
                return AccessDeniedResult();

            var result = new ApiResult<QuizQuestionModel>();

            if (!ModelState.IsValid)
                return BadRequest("Please check if you have provided valid data");

            var question = model.MapTo<QuizQuestionModel, SurveyQuestion>();


            var savedQuestion = _surveyTemplateFactory.AddorUpdateSurveyQuestion(question, _workContext.User.Id, _workContext.User.OrganizationId);

            if (savedQuestion == null)
                return BadRequest("Error encountered when saving changes, please try again");

            var examModel = savedQuestion.MapTo<SurveyQuestion, QuizQuestionModel>();
            result.HasError = false;
            result.Message = model.Id == 0 ? "Successfully added" : "Successfully updated";
            result.Result = examModel;

            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteTemplate")]
        public IHttpActionResult DeleteTemplate(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
                return AccessDeniedResult();

            var result = new ApiResult<ExaminationModel>();
            var attemptCount = _surveyTemplateFactory.ValidateTemplateForDelete(Id);
            if (attemptCount > 0)
                return BadRequest("This template has has been used in " + attemptCount + " surveys and cannot be deleted");

            var template = _surveyTemplateFactory.IncludeFilter(x => x.Questions.Where(b => !b.IsDeleted),
                x => x.Questions.Where(b => !b.IsDeleted).Select(c => c.Options.Where(d => !d.IsDeleted)))
                .Where(x => x.Id == Id).FirstOrDefault();
            if (template == null)
                return BadRequest("Template cannot be found");

            _surveyTemplateFactory.DeleteTemplate(template, _workContext.User.Id);

            result.HasError = false;
            result.Message = "Template successfully deleted";

            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteQuestion")]
        public IHttpActionResult DeleteQuestion(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
                return AccessDeniedResult();

            var result = new ApiResult<QuizQuestionModel>();
            var templateQuestion = _surveyTemplateFactory.GetSurveyQuestionIncludingOptions(Id);

            if (templateQuestion == null)
            {
                return BadRequest("This question does not exist");
            }

            _surveyTemplateFactory.DeleteTemplateQuestion(templateQuestion, _workContext.User.Id);

            result.HasError = false;
            result.Message = "Successfully deleted";
            return Ok(result);
        }

        [Route("DeleteQuestionOption")]
        public IHttpActionResult DeleteQuestionOption(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
                return AccessDeniedResult();

            var result = new ApiResult<QuizQuestionOptionModel>();
            var option = _surveyTemplateFactory.GetContext().Set<SurveyQuestionOptions>().Where(x => x.Id == Id).FirstOrDefault();
            if (option == null)
            {
                return BadRequest("This option does not exist");
            }

            _surveyTemplateFactory.DeleteQuizOption(option, _workContext.User.Id);

            result.HasError = false;
            result.Message = "Successfully deleted";
            return Ok(result);
        }

        [HttpPost]
        [Route("ResortQuestions")]
        public IHttpActionResult ResortQuestions(SurveyTemplateModel model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
                return AccessDeniedResult();

            var result = new ApiResult<bool>();

            if (!ModelState.IsValid)
                return BadRequest("Invalid data provided");

            var modelQuestions = model.Questions.MapTo<List<QuizQuestionModel>, List<SurveyQuestion>>();
            var savedModelList = _surveyTemplateFactory.IncludeFilter(x => x.Questions.Where(b => !b.IsDeleted))
            .Where(a => a.Id == model.Id).FirstOrDefault().Questions.ToList();

            var reSort = _surveyTemplateFactory.ResortExamQuestions(modelQuestions, savedModelList, _workContext.User.Id);
            if (!reSort)
                return BadRequest("We encountered an error while processing your request. Please try again");

            result.HasError = false;
            return Ok(result);
        }

        
       
    }
}
