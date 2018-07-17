using DataTables.AspNet.Core;
using Newtonsoft.Json;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Data.UnitofWork;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;

namespace SterlingBankLMS.Web.Api
{
    [RoutePrefix("api/ManageTraining")]
    public class TrainingController : BaseApiController
    {
        private readonly TrainingFactory _trainingFactory;
        private readonly TrainingVideoFactory _videoFactory;
        private readonly IWorkContext _workContext;
        private readonly IUserAccountService _userAcct;
        private readonly IPermissionService _permissionSvc;
        private readonly UserFactory _userFactory;
        private readonly TrainingRequestFactory _requestFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DepartmentBudgetFactory _budgetFactory;
        private readonly TrainingBudgetExpenditureFactory _expenditureFactory;
        private readonly LinemanagerFactory _linemanagerFactory;
        private readonly NotificationHubFactory _notificationHubFactory;
        private readonly MailsFactory _mailsFactory;
        private readonly MessageQueueFactory _messageQueueFactory;
        private readonly IFormatProvider currencyFormat = new System.Globalization.CultureInfo("HA-LATN-NG");

        public TrainingController(TrainingFactory trainingFactory, IWorkContext workContext, TrainingVideoFactory videoFactory,
            IUserAccountService userAcct, UserFactory userFactory, TrainingRequestFactory requestFactory,
            IUnitOfWork unitOfWork, DepartmentBudgetFactory budgetFactory, TrainingBudgetExpenditureFactory expenditureFactory,
            LinemanagerFactory linemanagerFactory, NotificationHubFactory notificationHubFactory, MailsFactory mailsFactory, MessageQueueFactory messageQueueFactory,
            IPermissionService permissionService)
        {
            _permissionSvc = permissionService;
            _trainingFactory = trainingFactory;
            _workContext = workContext;
            _videoFactory = videoFactory;
            _userAcct = userAcct;
            _userFactory = userFactory;
            _requestFactory = requestFactory;
            _unitOfWork = unitOfWork;
            _budgetFactory = budgetFactory;
            _expenditureFactory = expenditureFactory;
            _linemanagerFactory = linemanagerFactory;
            _notificationHubFactory = notificationHubFactory;
            _mailsFactory = mailsFactory;
            _messageQueueFactory = messageQueueFactory;
        }

        [HttpGet]
        [Route("GetTrainingTable")]
        public IHttpActionResult GetTraining(IDataTablesRequest request)
        {

            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            int totalRecords = 0;
            var training = _trainingFactory.GetPagedTraining(_workContext.User.OrganizationId, request.Length, index, request.Search.Value, out totalRecords).ToList();

            var returnObject = new SearchResponse<TrainingDto>
            {
                draw = request.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = training
            };
            return Ok(returnObject);

        }

        [HttpGet]
        [Route("GetTraining")]
        public IHttpActionResult GetTraining()
        {
            var result = new ApiResult<List<TrainingDto>>();
            var organId = _workContext.User.OrganizationId;

            var training = _trainingFactory.All(x => !x.IsDeleted && x.OrganizationId == organId, false);

            var trainingModel = training.MapTo<List<Training>, List<TrainingDto>>();

            result.HasError = false;
            result.Result = trainingModel;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetTraining")]
        public IHttpActionResult GetTraining(int Id)
        {
            var result = new ApiResult<TrainingDto>();
            var organId = _workContext.User.OrganizationId;

            var training = _trainingFactory.GetTraining(Id, false);
            if (training == null)
                return BadRequest("Training not found");

            var dto = training.MapTo<Training, TrainingDto>();



            result.HasError = false;
            result.Result = dto;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllTrainingByMonth")]
        public IHttpActionResult GetAllTrainingByMonth(string startDate, string endDate)
        {
            var result = new ApiResult<List<TrainingDto>>();
            var organId = _workContext.User.OrganizationId;

            var start = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var end = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var training = _trainingFactory.GetTrainingByPeriod(start, end, organId);
            var dto = training.MapTo<List<Training>, List<TrainingDto>>();

            result.HasError = false;
            result.Result = dto;

            return Ok(result);
        }

        [HttpPost]
        [Route("AddTrainingRequest")]
        public IHttpActionResult AddTrainingRequest(TrainingRequestDto model)
        {
            var data = new ApiResult<TrainingRequestDto>();

            var userId = _workContext.User.Id;
            var lineManagerId = _workContext.User.LineManagerId;
            var organisationId = _workContext.User.OrganizationId;
            if (!ModelState.IsValid)
            {
                return BadRequest("Sorry, you have some invalid data");
            }
            var trainingRequest = model.MapTo<TrainingRequestDto, TrainingRequest>();

            _trainingFactory.AddTrainingRequest(trainingRequest, userId, lineManagerId, organisationId);
            var dto = trainingRequest.MapTo<TrainingRequest, TrainingRequestDto>();
            data.HasError = false;
            data.Message = "Training request successfully sent";
            data.Result = dto;
            return Ok(data);
        }


        [HttpGet]
        [Route("GetUserTrainingByMonth")]
        public IHttpActionResult GetUserTrainingByMonth(string startDate, string endDate)
        {
            var result = new ApiResult<List<TrainingDto>>();
            var userId = _workContext.User.UserId;

            var start = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var end = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var training = _trainingFactory.GetUserTrainingByPeriod(start, end, userId);
            var dto = training.MapTo<List<Training>, List<TrainingDto>>();

            result.HasError = false;
            result.Result = dto;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetUserPendingTrainingRequest")]
        public IHttpActionResult GetUserPendingTrainingRequest()
        {
            var result = new ApiResult<List<TrainingRequestDto>>();
            var userId = _workContext.User.UserId;
            var trainingReqs = _requestFactory.GetUserUnapprovedTrainingRequest(userId);

            result.HasError = false;
            result.Result = trainingReqs;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetTrainingRequestPendingLineManagerApproval")]
        public IHttpActionResult GetTrainingRequestPendingLineManagerApproval()
        {
            var result = new ApiResult<List<TrainingRequestDto>>();
            var userId = _workContext.User.UserId;
            var IsManager = _userFactory.ValidateUserIsLineManager(userId);
            if (IsManager)
            {
                var trainingReqs = _requestFactory.GetTrainingRequestPendingLineManagerApproval(userId);
                result.HasError = false;
                result.Result = trainingReqs;
            }
            result.HasError = true;
            return Ok(result);
        }

        [HttpPost]
        [Route("ManagerApproveOrRejectTrainingRequest")]
        public IHttpActionResult ManagerApproveOrRejectTrainingRequest(MakerCheckerDto approvalAction)
        {
            var result = new ApiResult<bool>();
            if (!ModelState.IsValid)
            {
                return BadRequest("Sorry, you have some invalid data");
            }
            approvalAction.ActionUserId = _workContext.User.Id;
            _requestFactory.ManagerApproveOrRejectTrainingRequest(approvalAction);
            result.Message = "Request successfully " + (approvalAction.IsApproval ? "approved" : "rejected");
            result.HasError = false;
            return Ok(result);
        }

        [HttpPost]
        [Route("AdminApproveOrRejectTrainingRequest")]
        public IHttpActionResult AdminApproveOrRejectTrainingRequest(MakerCheckerDto approvalAction)
        {
            var result = new ApiResult<bool>();
            if (!ModelState.IsValid)
            {
                return BadRequest("Sorry, you have some invalid data");
            }
            approvalAction.ActionUserId = _workContext.User.Id;
            _requestFactory.AdminApproveOrRejectTrainingRequest(approvalAction);
            result.Message = "Request successfully " + (approvalAction.IsApproval ? "approved" : "rejected");
            result.HasError = false;
            return Ok(result);
        }


        [HttpGet]
        [Route("GetUserOrgTrainingByMonth")]
        public IHttpActionResult GetUserOrgTrainingByMonth(string startDate, string endDate)
        {
            var result = new ApiResult<List<TrainingCalendarDto>>();
            var userId = _workContext.User.UserId;

            var start = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var end = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var training = _trainingFactory.GetUserOrgTrainingByMonth(start, end, userId,_workContext.User.OrganizationId,null,null);

            result.HasError = false;
            result.Result = training;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetUserOrganizationTrainingList")]
        public IHttpActionResult GetUserOrganizationTrainingList(int pageSize, int pageNumber)
        {
            var result = new SearchResponse<TrainingCalendarDto>();
            var training = _trainingFactory.GetUserOrgTrainingByMonth(null, null, _workContext.User.UserId, _workContext.User.OrganizationId, pageSize, pageNumber);

            result.data = training;
            result.recordsTotal = training.Count > 0 ? training[0].TotalCount : 0;
            return Ok(result);
        }

        [HttpPost]
        [Route("AddorUpdate")]
        public IHttpActionResult AddorUpdate(TrainingModel model)
        {
            var data = new ApiResult<string>();

            var userId = _workContext.User.Id;
            var organisationId = _workContext.User.OrganizationId;
            if (!ModelState.IsValid)
            {
                return BadRequest("Sorry, you have some invalid data");
            }

            var training = model.Id == 0 ? model.MapTo<TrainingModel, Training>() : _trainingFactory.GetTraining(model.Id, true);

            var trainingDto = model.MapTo<TrainingModel, TrainingDto>();

            if (training == null)
                return BadRequest("Training not found");

            _trainingFactory.AddorUpdateTraining(training, trainingDto, userId, organisationId);
            data.HasError = false;
            data.Message = "New Training successfully " + (training.Id == 0 ? "created" : "Updated");
            return Ok(data);
        }

        [HttpDelete]
        [Route("DeletePeriod")]
        public IHttpActionResult DeletePeriod(int Id, int TrainingId)
        {

            var result = new ApiResult<TrainingPeriodDto>();

            var training = _trainingFactory.GetTraining(TrainingId, true);
            if (training == null)
                return BadRequest("Training not found");

            var periodDto = training.TrainingPeriod.ToList().MapTo<List<TrainingPeriod>, List<TrainingPeriodDto>>();
            var deleteObj = _trainingFactory.DeleteTrainingPeriod(training, periodDto, Id, _workContext.User.Id);

            if (!deleteObj)
                return BadRequest("An issue occurred when processing your request. Please try again");

            result.HasError = false;
            result.Message = "Training period successfully deleted";

            return Ok(result);
        }

        [HttpGet]
        [Route("GetBudget")]
        public IHttpActionResult GetBudget(IDataTablesRequest request)
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            var budget = _trainingFactory.ExecuteProcedure<DepartmentBudgetDto>("SP_GetDepartmentBudget", _workContext.User.OrganizationId, request.Search.Value, index, request.Length).ToList();

            var obj = new SearchResponse<DepartmentBudgetDto>
            {
                draw = request.Draw,
                recordsTotal = budget.Count > 0 ? budget[0].TotalRecords : 0,
                recordsFiltered = budget.Count > 0 ? budget[0].TotalRecords : 0,
                data = budget
            };

            return Ok(obj);
        }

        [HttpGet]
        [Route("GetGroupBudget")]
        public IHttpActionResult GetGroupBudget(int Id)

        {
            var result = new ApiResult<DepartmentBudgetDto>();
            var organId = _workContext.User.OrganizationId;

            var budget = _trainingFactory.GetBudgetToUpdate(Id, organId);

            if (budget == null)
            {
                result.HasError = true;
            }

            result.HasError = false;
            result.Result = budget;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetLinemanager")]
        public IHttpActionResult GetLinemanager()
        {
            var response = new ApiResult<List<LineManagerDropdownListDto>>();
            var line = _linemanagerFactory.LinemanagerDDL();
            response.HasError = false;
            response.Result = line;
            return Ok(response);
        }




        [HttpPost]
        [Route("UpdateTrainingBudget")]
        public IHttpActionResult UpdateTrainingBudget(DepartmentBudgetDto dto)
        {

            var response = new ApiResult<string>();
            var groupBudget = _budgetFactory.GetIncluding(x => x.Id == dto.Id, true, x => x.Group);
            if (groupBudget != null)
            {
                var initalBudget = groupBudget.Budget;
                var grouphead = _unitOfWork.Repository<ApplicationUserModel>().SqlQuery("SELECT FirstName, LastName,Email,Id FROM AspNetUsers WHERE StaffId='" + groupBudget.Group.GroupHeadStaffId + "'").FirstOrDefault();

                if (groupBudget.AmountSpent < dto.Budget)
                {
                    groupBudget.GroupId = dto.GroupId;
                    groupBudget.RegionId = dto.RegionId;
                    groupBudget.ModifiedDate = DateTime.Now;
                    groupBudget.Budget = dto.Budget;
                    groupBudget.LastModifiedById = _workContext.User.Id;
                    groupBudget.Year = dto.Year;

                    _budgetFactory.Update(groupBudget);

                    var queue = new MessageQueue();
                    queue.CreatedById = _workContext.User.Id;
                    queue.CreatedDate = DateTime.Now;
                    queue.NotificationType = NotificationType.BudgetChange;
                    queue.OrganizationId = _workContext.User.OrganizationId;
                    queue.ActionId = groupBudget.Id;
                    if (grouphead != null)
                        queue.AdditionalUsers = grouphead.Id.ToString();

                    _messageQueueFactory.Add(queue);

                    //if (grouphead != null)
                    //{
                    //    if (initalBudget < groupBudget.Budget)
                    //    {
                    //        //    var body_upreview =
                    //        //            "<p><strong>Dear " + groupBudget.Group.GroupHeadFirstName + "</strong></p>" +
                    //        //            "<br />" +
                    //        //            "<p>Budget for your Group " + groupBudget.Group.Name + ", has been reviewed upwards from " + string.Format(currencyFormat, "{0:c}", initalBudget) + " to " + string.Format(currencyFormat, "{0:c}", groupBudget.Budget) + "</p>" +
                    //        //            "<p>If this instruction did not come from you, please refer to the Learning and Developement Department.</p>" +
                    //        //            "<br>" +
                    //        //            "Regards!";

                           

                    //    }
                    //    else
                    //    {
                    //        //var body_downreview =
                    //        //"<p><strong>Dear " + groupBudget.Group.GroupHeadFirstName + "</strong></p>" +
                    //        //"<br />" +
                    //        //"<p>Budget for your Group " + groupBudget.Group.Name + ", has been reviewed downwards from " + string.Format(currencyFormat, "{0:c}", initalBudget) + " to " + string.Format(currencyFormat, "{0:c}", groupBudget.Budget) + "</p>" +
                    //        //"<p>If this instruction did not come from you, please refer to the Learning and Developement Department.</p>" +
                    //        //"<br>" +
                    //        //"Regards!";

                          

                    //    }
                    //}

                    response.HasError = false;
                    response.Message = "Budget for the Group, " + groupBudget.Group.Name + ", in Region," + groupBudget.Region.Name + ", has been updated successfully";
                }
                else
                {
                    response.HasError = true;
                    response.Message = "Amount Spent cannot be greater than budget";
                }

            }
            else
            {
                response.HasError = true;
                response.Message = "Invalid Group Budget";
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("GetGroupExpenditures")]
        public IHttpActionResult GetGroupExpenditures(IDataTablesRequest request)
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            var model = JsonConvert.DeserializeObject<ExpenditureModel>(Convert.ToString(request.AdditionalParameters["data"]));
            var groupId = model.GroupId;
            var regionId = model.RegionId;
            var expenditure = _trainingFactory.ExecuteProcedure<ExpenditureModel>("SP_GetBudgetExpenditure", groupId, regionId, _workContext.User.OrganizationId, request.Search.Value, index, request.Length).ToList();

            var obj = new SearchResponse<ExpenditureModel>
            {
                draw = request.Draw,
                recordsTotal = expenditure.Count() > 0 ? expenditure[0].TotalRecords : 0,
                recordsFiltered = expenditure.Count() > 0 ? expenditure[0].TotalRecords : 0,
                data = expenditure
            };
            return Ok(obj);
        }

        [Route("GetTrainingNames")]
        public IHttpActionResult GetTrainingNames()
        {
            var response = new ApiResult<List<TrainingDto>>();
            var training = _trainingFactory.GetAllIncluding(x => x.IsDeleted == false, false, x => x.Organization).ToList();
            var trainings = training.MapTo<List<Training>, List<TrainingDto>>();
            response.Result = trainings;
            return Ok(response);
        }

        [HttpPost]
        [Route("SaveNewExpenditure")]
        public IHttpActionResult SaveNewExpenditure(ExpenditureModel model)
        {
            var response = new ApiResult<string>();
            if (ModelState.IsValid)
            {
                var deptBudget = _budgetFactory.Find(model.DepartmentBudgetId);
                if (deptBudget != null && model.Year != DateTime.Now.Year)
                {
                    try
                    {
                        //Update the Training Amount Spent
                        deptBudget.AmountSpent += model.AmountSpent;
                        deptBudget.ModifiedDate = DateTime.Now;
                        deptBudget.LastModifiedById = _workContext.User.Id;

                        if (deptBudget.AmountSpent <= deptBudget.Budget)
                        {
                            _budgetFactory.Update(deptBudget);

                            //Add Expenditure 
                            var budgetExp = new TrainingBudgetExpenditure();
                            budgetExp.AmountSpent = model.AmountSpent;
                            budgetExp.CreatedById = _workContext.User.Id;
                            budgetExp.NumberOfParticipants = model.NumberOfParticipants;
                            budgetExp.OrganizationId = _workContext.User.OrganizationId;
                            budgetExp.RegionId = model.RegionId;
                            budgetExp.TrainingId = model.TrainingId;
                            budgetExp.GroupId = model.GroupId;
                            budgetExp.CreatedDate = DateTime.Now;
                            budgetExp.DepartmentBudgetId = model.DepartmentBudgetId;
                            budgetExp.IsDeleted = false;

                            _expenditureFactory.Add(budgetExp);
                            model.OutstandingAmount = deptBudget.Budget - deptBudget.AmountSpent;
                            response.HasError = false;
                            response.Result = model.OutstandingAmountFormat;
                            response.Message = "Expenditure successfully added";

                            //check 50%-90% of budget spent
                            var halfspent = deptBudget.Budget * 0.5m;
                            var ninthspent = deptBudget.Budget * 0.9m;
                            var grouphead = _unitOfWork.Repository<ApplicationUserModel>().SqlQuery("SELECT FirstName, LastName,Email,Id FROM AspNetUsers WHERE StaffId='" + deptBudget.Group.GroupHeadStaffId + "'").FirstOrDefault();

                            if (deptBudget.AmountSpent > halfspent && deptBudget.AmountSpent < ninthspent)
                            {
                                //var msg =
                                //    "<p><strong>Dear " + deptBudget.Group.GroupHeadFirstName + "</strong></p>" +
                                //    "<br />" +
                                //    "<ul>" +
                                //    "<li><p> Amount Spent:" + string.Format(currencyFormat, "{0:c}", deptBudget.AmountSpent) + " </p> </li>" +
                                //    "<li><p> Amount Remaining:" + string.Format(currencyFormat, "{0:c}", deptBudget.Budget - deptBudget.AmountSpent) + "</p></li>" +
                                //    "<li><p> Group Budget, " + deptBudget.Year + ":" + string.Format(currencyFormat, "{0:c}", deptBudget.Budget) + " </p> </li>" +
                                //    "</ul>" +
                                //    "<p>Regards!</p>";

                               
                                    var queue = new MessageQueue();
                                    queue.CreatedById = _workContext.User.Id;
                                    queue.CreatedDate = DateTime.Now;
                                    //queue.Comments = msg;
                                    queue.NotificationType = NotificationType.TrainingBudgetDepletion50percent;
                                    queue.OrganizationId = _workContext.User.OrganizationId;
                                    queue.ActionId = deptBudget.Id;
                                    if (grouphead != null)
                                        queue.AdditionalUsers = grouphead.Id.ToString();

                                    _messageQueueFactory.Add(queue);

                            }

                            if (deptBudget.AmountSpent > ninthspent && deptBudget.AmountSpent != deptBudget.Budget)
                            {
                                //var msg =
                                //    "<p><strong>Dear " + deptBudget.Group.GroupHeadFirstName + "</strong></p>" +
                                //    "<br />" +
                                //    "<p>Budget for your Group, " + deptBudget.Group.Name + ", has now exceeded the 90% spent mark. See the details below:</p> " +
                                //    "<ul>" +
                                //    "<li><p> Amount Spent:" + string.Format(currencyFormat, "{0:c}", deptBudget.AmountSpent) + " </p> </li>" +
                                //    "<li><p> Amount Remaining:" + string.Format(currencyFormat, "{0:c}", deptBudget.Budget - deptBudget.AmountSpent) + "</p></li>" +
                                //    "<li><p> Group Budget, " + deptBudget.Year + ":" + string.Format(currencyFormat, "{0:c}", deptBudget.Budget) + " </p> </li>" +
                                //    "</ul>" +
                                //    "<p>Regards!</p>";

                                   var queue = new MessageQueue();
                                    queue.CreatedById = _workContext.User.Id;
                                    queue.CreatedDate = DateTime.Now;
                                    //queue.Comments = msg;
                                    queue.NotificationType = NotificationType.TrainingBudgetDepletion90percent;
                                    queue.OrganizationId = _workContext.User.OrganizationId;
                                    queue.ActionId = deptBudget.Id;
                                    if (grouphead != null)
                                        queue.AdditionalUsers = grouphead.Id.ToString();
                                    
                            }
                            if (deptBudget.AmountSpent == deptBudget.Budget)
                            {
                                //var msg =
                                //    "<p><strong>Dear " + deptBudget.Group.GroupHeadFirstName + "</strong></p>" +
                                //    "<br />" +
                                //    "<p>Budget for your Group, " + deptBudget.Group.Name + ", is now 100% spent. See the details below:</p> " +
                                //    "<ul>" +
                                //    "<li><p> Amount Spent:" + string.Format(currencyFormat, "{0:c}", deptBudget.AmountSpent) + " </p> </li>" +
                                //    "<li><p> Amount Remaining:" + string.Format(currencyFormat, "{0:c}", deptBudget.Budget - deptBudget.AmountSpent) + "</p></li>" +
                                //    "<li><p> Group Budget, " + deptBudget.Year + ":" + string.Format(currencyFormat, "{0:c}", deptBudget.Budget) + " </p> </li>" +
                                //    "</ul>" +
                                //    "<p>Regards!</p>";

                               
                              
                                    var queue = new MessageQueue();
                                    queue.CreatedById = _workContext.User.Id;
                                    queue.CreatedDate = DateTime.Now;
                                    //queue.Comments = msg;
                                    queue.NotificationType = NotificationType.TrainingBudgetDepletion100percent;
                                    queue.OrganizationId = _workContext.User.OrganizationId;
                                    queue.ActionId = deptBudget.Id;

                                    if (grouphead != null)
                                    queue.AdditionalUsers = grouphead.Id.ToString();

                                    _messageQueueFactory.Add(queue);

                               
                            }
                        }
                        else
                        {
                            response.HasError = true;
                            response.Message = "You have already exhausted the budget. Expenditure was not added";
                        }

                    }
                    catch (Exception e)
                    {
                        response.HasError = true;
                        response.Message = "" + e.Message;
                    }
                }
                else
                {
                    response.HasError = true;
                    response.Message = "Error. You can only post expenditures from the current year budget.";
                }
            }
            else
            {
                response.HasError = true;
                response.Message = "Some data is invalid. Please recheck them";
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("GetTrainingVideo")]
        public IHttpActionResult GetTrainingVideo(IDataTablesRequest request)
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            var videos = _trainingFactory.ExecuteProcedure<TrainingVideoDto>("SP_GetTrainingVideo", _workContext.User.OrganizationId, request.Search.Value, index, request.Length).ToList();

            var obj = new SearchResponse<TrainingVideoDto>
            {
                draw = request.Draw,
                recordsTotal = videos.Count > 0 ? videos[0].TotalRecords : 0,
                recordsFiltered = videos.Count > 0 ? videos[0].TotalRecords : 0,
                data = videos
            };

            return Ok(obj);
        }

        [HttpDelete]
        [Route("DeleteVideo")]
        public IHttpActionResult DeleteVideo(int Id)
        {
            var result = new ApiResult<TrainingVideo>();
            var video = _videoFactory.Find(x => x.Id == Id, true);

            if (video == null)
            {
                return BadRequest("This video does not exist");
            }

            _videoFactory.DeleteTrainingVideo(video, _workContext.User.Id);

            result.HasError = false;
            result.Message = "Training Video was successfully deleted";
            return Ok(result);
        }

        [HttpGet]
        [Route("GetTrainingRequest")]
        public IHttpActionResult GetTrainingRequest(IDataTablesRequest request)
        {
            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            var requests = _trainingFactory.ExecuteProcedure<TrainingRequestDto>("SP_GetTrainingRequestList", _workContext.User.OrganizationId, request.Search.Value, index, request.Length).ToList();

            var obj = new SearchResponse<TrainingRequestDto>
            {
                draw = request.Draw,
                recordsTotal = requests.Count > 0 ? requests[0].TotalRecords : 0,
                recordsFiltered = requests.Count > 0 ? requests[0].TotalRecords : 0,
                data = requests
            };

            return Ok(obj);
        }

    }
}
