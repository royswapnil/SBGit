using DataTables.AspNet.Core;
using Newtonsoft.Json;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Core.Helper;
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
    [RoutePrefix("api/LearningGroup")]
    public class LearningGroupController : BaseApiController
    {

        private readonly ExaminationFactory _examFactory;
        public readonly IWorkContext _workContext;
        public readonly LearningGroupFactory _learningGroupFactory;
        public readonly GroupFactory _groupFactory;
        private readonly DepartmentBudgetFactory _deptBudgetFactory;
        private readonly TrainingFactory _trainingFactory;
        private readonly MessageQueueFactory _messageQueueFactory;
        private readonly IPermissionService _permissionSvc;
        private readonly SurveyFactory _surveyFactory;
        private readonly CourseFactory _courseFactory;

        public LearningGroupController(ExaminationFactory examFactory, IWorkContext workContext, LearningGroupFactory learningGroupFactory, GroupFactory groupFactory
            , DepartmentBudgetFactory deptBudgetFactory, TrainingFactory trainingFactory, MessageQueueFactory messageQueueFactory, IPermissionService permissionSvc
            , SurveyFactory surveyFactory, CourseFactory courseFactory)
        {
            _examFactory = examFactory;
            _workContext = workContext;
            _learningGroupFactory = learningGroupFactory;
            _deptBudgetFactory = deptBudgetFactory;
            _trainingFactory = trainingFactory;
            _messageQueueFactory = messageQueueFactory;
            _permissionSvc = permissionSvc;
            _surveyFactory = surveyFactory;
            _courseFactory = courseFactory;
        }




        [HttpGet]
        [Route("GetDatatableLearningGroups")]
        public IHttpActionResult GetDatatableLearningGroups(IDataTablesRequest request)
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            int totalRecords = 0;
            var learningGroup = _learningGroupFactory.GetLearningGroupTable(request.Search.Value, index, request.Length, _workContext.User.OrganizationId, out totalRecords).ToList();

            var returnObject = new SearchResponse<LearningGroupDto>
            {
                draw = request.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = learningGroup
            };

            return Ok(returnObject);
        }


        /// <summary>
        /// Get all Learning Groups
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetLearnerGroup")]
        public IHttpActionResult GetLearnerGroup()
        {
            var result = new ApiResult<List<LearningGroup>>();

            var learningGroup = _learningGroupFactory.All(x => !x.IsDeleted, false);
            result.Result = learningGroup;
            return Ok(result);
        }

        [HttpPost]
        [Route("SaveLearnerGroup")]
        public IHttpActionResult SaveLearnerGroup(LearningGroupModel.Group model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageLearningGroup))
            {
                return AccessDeniedResult();
            }
            var result = new ApiResult<LearningGroup>();

            if (!ModelState.IsValid)
            {
                return BadRequest("Please check if you have provided valid data");
            }



            /// insert into learning group
            /// 
            model.TagFormat = JsonConvert.SerializeObject(model.Tags);

            var learningGroup = model.MapTo<LearningGroupModel.Group, LearningGroup>();

            if (!_learningGroupFactory.ValidateNameExists(learningGroup.Name, learningGroup.Id))
                return BadRequest("Learning group name already exists");

            learningGroup = _learningGroupFactory.AddorUpdateLearningGroup(learningGroup, _workContext.User.Id, _workContext.User.OrganizationId);
            result.HasError = false;
            result.Result = learningGroup;
            result.Message = "Learning group successfully added";

            //Samuel: clear all cached courses
            _courseFactory.ClearAllCachedAssignedCourses(_workContext.User.OrganizationId);

            return Ok(result);
        }

        [HttpPost]
        [Route("AssignCourseToLearnerGroup")]
        public IHttpActionResult AssignCourseToLearnerGroup(List<LearningGroupModel.Courses> model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse))
            {
                return AccessDeniedResult();
            }
            var result = new ApiResult<LearningGroupCourse>();

            if (!ModelState.IsValid)
            {
                return BadRequest("Please check if you have provided valid data");
            }


            var learningGroupCourse = model.MapTo<List<LearningGroupModel.Courses>, List<LearningGroupCourse>>();

            foreach (var item in learningGroupCourse)
            {
                item.Availability = model[0].Assigned ? UserCourseAvailability.Required : UserCourseAvailability.Generic;

            }

            _learningGroupFactory.AssignLearningGroupCourses(learningGroupCourse, _workContext.User.Id);


            //Samuel: clear all cached courses
            _courseFactory.ClearAllCachedAssignedCourses(_workContext.User.OrganizationId);

            result.HasError = false;
            result.Message = "Learning group courses successfully assigned";

            return Ok(result);
        }

        [HttpGet]
        [Route("GetLearningGroupUsers")]
        public IHttpActionResult GetLearningGroupUsers(IDataTablesRequest request)
        {
            var result = new ApiResult<List<EmployeeDetailsDto>>();
            var tags = JsonConvert.DeserializeObject<List<LearningGroupModel.Tags>>(Convert.ToString(request.AdditionalParameters["data"]));
            var index = request.Start == 0 ? 0 : request.Start / request.Length;

            if (!ModelState.IsValid)
            {
                return BadRequest("Please check if you have provided valid data");
            }


            var learningGroupTags = tags.MapTo<List<LearningGroupModel.Tags>, List<LearningGroupTag>>();


            var users = _learningGroupFactory.GetUsersByLearningGroupTag(learningGroupTags, _workContext.User.OrganizationId, request.Length, index, request.Search.Value);

            //TO DO: Paginate from cache or Db

            var returnObject = new SearchResponse<EmployeeDetailsDto>
            {
                draw = request.Draw,
                recordsTotal = users.Count > 0 ? users[0].TotalRecords : 0,
                recordsFiltered = users.Count > 0 ? users[0].TotalRecords : 0,
                data = users
            };
            return Ok(returnObject);
        }

        [HttpGet]
        [Route("GetLearningGroupUsersById")]
        public IHttpActionResult GetLearningGroupUsersById(IDataTablesRequest request)
        {
            var result = new ApiResult<List<EmployeeDetailsDto>>();
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            var Id = Convert.ToInt32(request.AdditionalParameters["data"]);

            if (!ModelState.IsValid)
            {
                return BadRequest("Please check if you have provided valid data");
            }


            var users = _learningGroupFactory.GetUsersByLearningGroupID(Id,_workContext.User.OrganizationId, request.Search.Value, index, request.Length);

            //TO DO: Paginate from cache or Db

            var returnObject = new SearchResponse<LearningGroupUserDto>
            {
                draw = request.Draw,
                recordsTotal = users.Count > 0 ? users[0].TotalRecords : 0,
                recordsFiltered = users.Count > 0 ? users[0].TotalRecords : 0,
                data = users
            };
            return Ok(returnObject);
        }


        [HttpPost]
        [Route("AssignExamToGroup")]
        public IHttpActionResult AssignExamToGroup(List<LearningGroupModel.Exams> model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageExamination))
            {
                return AccessDeniedResult();
            }
            var result = new ApiResult<LearningGroupExam>();

            if (!ModelState.IsValid)
            {
                return BadRequest("Please check if you have provided valid data");
            }

            var examGroupExams = model.MapTo<List<LearningGroupModel.Exams>, List<LearningGroupExam>>();

            _learningGroupFactory.AssignExamGroupExams(examGroupExams, _workContext.User.Id,_workContext.User.OrganizationId);

            result.HasError = false;
            result.Message = "Examinations successfully assigned";

            return Ok(result);
        }


        [HttpPost]
        [Route("AssignSurveyToGroup")]
        public IHttpActionResult AssignSurveyToGroup(List<LearningGroupModel.Surveys> model)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
            {
                return AccessDeniedResult();
            }
            var result = new ApiResult<LearningGroupSurvey>();

            if (!ModelState.IsValid)
            {
                return BadRequest("Please check if you have provided valid data");
            }

            var surveyGroupSurveys = model.MapTo<List<LearningGroupModel.Surveys>, List<LearningGroupSurvey>>();

            _learningGroupFactory.AssignSurveyGroupSurveys(surveyGroupSurveys, _workContext.User.Id, _workContext.User.OrganizationId);

            result.HasError = false;
            result.Message = "Surveys successfully assigned";

            return Ok(result);
        }

        [HttpPost]
        [Route("AssignTrainingToTrainingGroup")]
        public IHttpActionResult AssignTrainingToTrainingGroup(List<LearningGroupModel.Trainings> trainings)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
            {
                return AccessDeniedResult();
            }

            var trainingGroupId = trainings[0].LearningGroupId;
            var result = new ApiResult<LearningGroupTraining>();

            if (!ModelState.IsValid)
            {
                result.HasError = true;
                result.Message = "Some data are invalid";
                return Ok(result);
            }

            var trainingGroupTraining = trainings.MapTo<List<LearningGroupModel.Trainings>, List<LearningGroupTraining>>();
            _learningGroupFactory.AssignTrainingGroupTrainings(trainingGroupTraining, _workContext.User.Id, _workContext.User.OrganizationId);

            //Get all training assigned to this training group
            var ids = trainings.Where(x => x.LearningGroupId == trainingGroupId).Select(x => new
            {
                x.TrainingId
            }).ToList();


            var trainingGroup = _learningGroupFactory.Find(x => x.Id == trainingGroupId, false);
            if (trainingGroup != null)
            {
                var tags = JsonConvert.DeserializeObject<List<LearningGroupModel.Tags>>(trainingGroup.TagFormat);
                var trainingGroupTags = tags.MapTo<List<LearningGroupModel.Tags>, List<LearningGroupTag>>();

                var users = _learningGroupFactory.GetUsersByLearningGroupID(trainingGroupId,_workContext.User.OrganizationId, null, 1, null);


                if (users.Count() != 0)
                {
                    //check how much a department has left and compare with the Staff the training is assigned to

                    var userDeptGroup = (from x in users
                                         group x by x.GroupId into y
                                         select new UserDeptCountModel()
                                         {
                                             GroupId = y.Key,
                                             GroupCount = y.Count()
                                         }).ToList();
                    foreach (var item in userDeptGroup)
                    {
                        var totalCost = 0m;
                        var dept = _deptBudgetFactory.GetIncluding(x => x.GroupId == item.GroupId, false);

                        foreach (var y in ids)
                        {
                            var amt = _trainingFactory.Find(x => x.Id == y.TrainingId, false).AmountPerStaff;
                            totalCost += amt * item.GroupCount;
                        }

                        if (totalCost >= dept.Budget)
                        {
                            result.HasError = true;
                            result.Message = dept.Group.Name + " has overspent their budget. Staff cannot be assigned training to it ";

                            return Ok(result);
                        }

                    }

                    //Every thing is fine, proceed to assign training.

                    var queue = new MessageQueue();
                    queue.ActionId = trainingGroupId;
                    queue.CreatedById = _workContext.User.Id;
                    queue.NotificationType = NotificationType.TrainingAssignment;
                    queue.OrganizationId = _workContext.User.OrganizationId;
                    queue.CreatedDate = DateTime.Now;

                    _messageQueueFactory.Add(queue);

                    //foreach (var user in users)
                    //{
                    //    foreach (var id in ids)
                    //    {
                    //        var userTraining = new UserTraining();
                    //        userTraining.OrganizationId = _workContext.User.OrganizationId;
                    //        userTraining.UserId = user.Id;
                    //        userTraining.TrainingId = id.TrainingId;
                    //        userTraining.CreatedDate = DateTime.Now;
                    //        userTraining.IsDeleted = false;
                    //        userTraining.CreatedById = _workContext.User.Id;

                    //        _userTrainingFactory.Add(userTraining);


                    //    }
                    //    var msg = "";
                    //    msg += "<p>Dear " + user.FirstName + ", " + user.LastName + "</p>" +
                    //       "<br />" +
                    //       "<p>The Department of Learning and Developement has assigned  the following Training to you:</p>";
                    //    foreach (var item in ids)
                    //    {
                    //        var training = _trainingFactory.GetIncluding(x => x.Id == item.TrainingId, false);
                    //        msg += "<ul>";
                    //        msg += "<li><p><strong>Name: </strong> " + training.Name + ", <strong>Venue: </strong> " + training.Venue + ", " + training.Location + ", <strong>Date:</strong> " + training.StartPeriod.ToString("ddd, dd-MMMM-yyyy") + "</p></li>";
                    //        msg += "</ul>";
                    //    }
                    //    msg += "<p>We look forward to having you at the training.</p>" +
                    //        "<p>Best Regards! </p>";



                    //}
                }
                else
                {
                    result.HasError = true;
                    result.Message = "No Staff belong to this Training Group ";

                    return Ok(result);
                }

            }
            result.HasError = false;
            result.Message = "Training group trainings successfully assigned";

            return Ok(result);
        }

        //TODO: CHANGE TO LESS GENERIC
        [HttpGet]
        [Route("SearchSurveyToAssignByName")]
        public IHttpActionResult SearchSurveyToAssignByName(string search)
        {
            var result = new ApiResult<IEnumerable<SurveyDto>>();
            var survey = _surveyFactory.SearchIndependentSurveyByName(search);
            result.HasError = false;
            result.Result = survey;
            return Ok(result);
        }

        //TODO: CHANGE TO LESS GENERIC
        [HttpGet]
        [Route("SearchTrainingToAssignByName")]
        public IHttpActionResult SearchTrainingToAssignByName(string search)
        {
            var result = new ApiResult<IEnumerable<TrainingDto>>();
            var training = _trainingFactory.SearchTrainingByName(search);
            result.HasError = false;
            result.Result = training;
            return Ok(result);
        }

        //TODO: CHANGE TO LESS GENERIC
        [HttpGet]
        [Route("SearchExamToAssignByName")]
        public IHttpActionResult SearchExamToAssignByName(string search)
        {
            var result = new ApiResult<IEnumerable<ExaminationDto>>();
            var exam = _examFactory.SearchIndependentExamByName(search);
            result.HasError = false;
            result.Result = exam;
            return Ok(result);
        }


        [HttpDelete]
        [Route("DeleteLearningGroup")]
        public IHttpActionResult DeleteLearningGroup(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageLearningGroup))
            {
                return AccessDeniedResult();
            }

            //TODO: DELETE EXAM AS WELL

            var result = new ApiResult<CourseModel>();

            var validateGroup = _learningGroupFactory.GetLearningGroupById(Id);

            if (validateGroup == null)
                return BadRequest("This learning group does not exist");

            if (validateGroup.SurveyCount > 0 || validateGroup.CourseCount > 0 || validateGroup.ExamCount > 0 || validateGroup.TrainingCount > 0)
                return BadRequest("This learning group has been assigned and cannot be deleted");

            _learningGroupFactory.DeleteLearningGroup(Id, _workContext.User.Id);

            result.HasError = false;
            result.Message = "Learning group successfully deleted";

            return Ok(result);
        }
    }
}
