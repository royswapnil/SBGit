using DataTables.AspNet.Core;
using Rentrdoid.Common.Logger;
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
using System.Linq;
using System.Text;
using System.Web.Http;

namespace SterlingBankLMS.Web.Api
{
    /// <summary>
    /// Employee api endpoint
    /// </summary>
    [RoutePrefix("api/Employee")]
    public class EmployeeController : BaseApiController
    {
        private readonly UserFactory _userFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionService;
        private readonly IUserAccountService _userAccountService;
        private readonly IUserRoleService _userRole;
        private readonly UserCertificateFactory _certFactory;
        private readonly UserTrainingFactory _userTrainingFactory;
        private readonly NotificationHubFactory _notificationHubFactory;
        private readonly MailsFactory _mailsFactory;
        private readonly MessageQueueFactory _messageQueueFactory;
        private readonly LinemanagerFactory _linemanagerFactory;
        private readonly ILogger _logger;


        public EmployeeController(UserFactory userFactory, IWorkContext workContext, ILogger logger,
            IPermissionService permissionService, IUserAccountService userAccountService, IUserRoleService userRole, UserCertificateFactory certificateFactory, IUnitOfWork unitOfWork, NotificationHubFactory notificationHubFactory, MailsFactory mailsFactory, MessageQueueFactory messageQueueFactory, LinemanagerFactory linemanagerFactory, UserTrainingFactory userTrainingFactory)
        {
            _logger = logger;
            _userFactory = userFactory;
            _workContext = workContext;
            _permissionService = permissionService;
            _userAccountService = userAccountService;
            _userRole = userRole;
            _certFactory = certificateFactory;
            _unitOfWork = unitOfWork;
            _mailsFactory = mailsFactory;
            _notificationHubFactory = notificationHubFactory;
            _messageQueueFactory = messageQueueFactory;
            _linemanagerFactory = linemanagerFactory;
            _userTrainingFactory = userTrainingFactory;
        }


        /// <summary>
        /// Returns all Employees with search filter
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [Route("GetEmployees")]

        public IHttpActionResult GetEmployees([FromUri] EmployeSearchModel search)
        {
            if (search == null)
                return BadRequest("no search params supplied.");

            search.ValidateSearchQuery();

            if (!_permissionService.TryCheckAccess(PermissionProvider.EmployeeRecords))
                return AccessDeniedResult();

            var recordsDto = _userFactory.GetEmployeeList(search.PageIndex, search.PageSize,
               _workContext.User.OrganizationId, search.Keywords, search.Group);

            var records = recordsDto.MapTo<IEnumerable<EmployeeDto>, IEnumerable<EmployeeListModel>>();

            var response = new ApiResult<CollectionResult<EmployeeListModel>>
            {
                Result = new CollectionResult<EmployeeListModel>(records)
                {
                    Total = recordsDto.FirstOrDefault()?.TotalCount ?? 0
                }
            };

            return Ok(response);
        }


        /// <summary>
        /// Get all organization active users
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <returns></returns>

        [Route("GetActiveEmployees")]
        public IHttpActionResult GetActiveEmployees()
        {
            var response = new ApiResult<List<ApplicationUser>>();
            var users = _userAccountService.GetAllUsers(x => x.LockoutEnabled == false && x.OrganizationId == _workContext.User.OrganizationId).ToList();
            response.Result = users;
            return Ok(response);
        }


        /// <summary>
        /// Returns an Employee training collection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetEmployeeTrainingRecords")]
        public IHttpActionResult GetEmployeeTrainingRecords(int id)
        {
            if (!_permissionService.TryCheckAccess(PermissionProvider.EmployeeRecords))
                return AccessDeniedResult();
            var response = new ApiResult<List<EmployeeTrainingRecordModel>>();

            var recordsDto = _userFactory.GetEmployeeTrainingRecords(id, _workContext.User.OrganizationId);

            var records = recordsDto.MapTo<IEnumerable<EmployeeTrainingRecordDto>, List<EmployeeTrainingRecordModel>>();

            if (records.Count != 0) {
                response.Result = records;
                response.HasError = false;
            }
            else {
                response.HasError = true;
            }

            return Ok(response);
        }


        [Route("GetTrainingRecordDetails")]
        public IHttpActionResult GetTrainingRecordDetails(int userTrainingId, int userId)
        {
            if (!_permissionService.TryCheckAccess(PermissionProvider.EmployeeRecords))
                return AccessDeniedResult();

            //var id = Convert.ToInt32(userId);
            //var utId = Convert.ToInt32(userTrainingId);

            var response = new ApiResult<EmployeeTrainingRecordDto>();
            var recordsDto = _userFactory.GetEmployeeTrainingRecords(userId, _workContext.User.OrganizationId).ToList();

            var trainingDetails = recordsDto.Where(x => x.UserTrainingId == userTrainingId).FirstOrDefault();
            if (trainingDetails != null) {
                response.HasError = false;
                response.Result = trainingDetails;
            }
            else {
                response.HasError = true;
                response.Message = "Training Details does not exist for this user";
            }

            return Ok(response);

        }

        [Route("GetUserTraining")]
        public IHttpActionResult GetUserTraining(int userId)
        {
            var response = new ApiResult<List<EmpTrainingRecord>>();
            var trainings = _userTrainingFactory.GetAllIncluding(x => x.UserId == userId, false, y => y.Training).Select(x => new EmpTrainingRecord
            {
                TrainingName = x.Training.Name,
                Location = x.Training.Location,
                StartDate = x.Training.StartPeriod,
                Venue = x.Training.Venue
            }).ToList();

            if (trainings.Count == 0) {
                response.HasError = true;
            }
            else {
                response.HasError = false;
                response.Result = trainings;
            }
            return Ok(response);

        }
        /// <summary>
        /// Returns an Employee certificate collection
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetEmployeeCertificates")]
        public IHttpActionResult GetEmployeeCertificates(int id)
        {
            if (!_permissionService.TryCheckAccess(PermissionProvider.EmployeeRecords))
                return AccessDeniedResult();

            var records = _userFactory.GetEmployeeCertificates(id, _workContext.User.OrganizationId).ToList();

            var response = new ApiResult<List<UserCertificateDto>>();
            if (records.Count > 0) {
                response.HasError = false;
                response.Result = records;
            }
            else {
                response.HasError = true;
                response.Message = "No Certificates uploaded for this staff";
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("SaveNewEmployee")]
        public IHttpActionResult SaveNewEmployee(EmployeeModel model)
        {
            var response = new ApiResult<string>();
            if (ModelState.IsValid) {

                var linemanager = _unitOfWork.Repository<LineManagerDto>().SqlQuery("SELECT Id as LineManagerId, FirstName as LineManagerFirstName, LastName as LineManagerLastName FROM AspNetUsers WHERE StaffId=" + model.LineManagerStaffId).FirstOrDefault();
                if (linemanager != null) {
                    var employeeExists = _userAccountService.FindUserByEmail(model.Email);
                    if (employeeExists == null) {

                        var employee = new ApplicationUser
                        {
                            DateOfBirth = model.DateOfBirth,
                            DateOfEmployment = model.DateOfEmployment ?? DateTime.Now,
                            Email = model.Email,
                            FirstName = model.FirstName,
                            Gender = model.Gender,
                            LastName = model.LastName,
                            UserType = UserType.Normal,
                            UserName = model.Email.Substring(0, model.Email.LastIndexOf("@")),
                            OrganizationId = _workContext.User.OrganizationId,
                            Address = model.Address,
                            BranchId = model.Branch,
                            DepartmentId = model.Department,

                            //Todo: remove this once email infrastructure is available
                            EmailConfirmed = true,

                            Functions = model.Function,
                            GradeId = model.Grade,
                            GroupId = model.Group,
                            LineManagerFirstName = linemanager.LineManagerFirstName,
                            LineManagerLastName = linemanager.LineManagerLastName,
                            LineManagerStaffId = model.LineManagerStaffId,
                            PhoneNumber = model.Phone,
                            RegionId = model.Region,
                            StaffId = model.StaffId
                        };

                        var pwd = "sterlingbank@test"; //AppHelper.GeneratePwd();

                        var acct = _userAccountService.CreateUser(employee, pwd);
                        if (acct.Succeeded) {

                            var u = new User() { ApplicationUserId = employee.Id };
                            _userFactory.Add(u);

                            var role = _userRole.FindById(model.RoleId.Value);
                            if (role != null) {
                                _userAccountService.AddToRole(employee.Id, role.Name);
                            }

                            var msg = "<p><strong>Dear " + employee.FirstName + ", " + employee.LastName + "</strong></p>" +
                                "<p>Your account has been created on the Learning Management System, with the following details:</p>" +
                                "<ul>" +
                                "<li>Email Address: <strong>" + employee.Email + "</strong></li>" +
                                "<li>Password: <strong>" + pwd + "</strong></li>" +
                                "<li>Gender: <strong>" + Enum.GetName(typeof(Gender), employee.Gender) + "</strong></li>" +
                                "<li>Email Address: <strong>" + employee.Email + "</strong></li>" +
                                "<li>Line Manager: <strong>" + employee.LineManagerLastName + ", " + employee.LineManagerFirstName + "</strong></li>" +
                                "<li>Role: <strong>" + role.Name + "</strong></li>" +
                                "</ul>" +
                                "<p>You can login to change your password at your convenient time.</p>" +
                                "<p>Best Regards!</p>";

                            var queue = new MessageQueue
                            {
                                CreatedById = _workContext.User.Id,
                                CreatedDate = CommonHelper.GetCurrentDate(),
                                Comments = msg,
                                NotificationType = NotificationType.EmployeeUpdate,
                                OrganizationId = _workContext.User.OrganizationId,
                                ActionId = employee.Id
                            };

                            _messageQueueFactory.Add(queue);

                            var existingLine = _linemanagerFactory.All(x => x.LineManagerId == linemanager.LineManagerId && x.EmployeeId == employee.Id, false).ToList();
                            if (!existingLine.Any()) {
                                var linemgr = new LineManager
                                {
                                    CreatedById = _workContext.User.Id,
                                    CreatedDate = CommonHelper.GetCurrentDate(),
                                    EmployeeId = u.Id,
                                    IsActive = true,
                                    LineManagerId = linemanager.LineManagerId,
                                    OrganizationId = _workContext.User.OrganizationId
                                };

                                _linemanagerFactory.Add(linemgr);
                            }

                            response.HasError = false;
                            response.Message = "Employee Successfully created";
                            response.Result = linemanager.LineManager;
                        }
                        else {
                            response.HasError = true;
                            response.Message = "An Error occured while creating employee.";
                        }

                    }
                    else {
                        response.HasError = true;
                        response.Message = "Employee with email, " + model.Email + ", already exists on the platform. Update their details instead";
                    }
                }
                else {
                    response.HasError = true;
                    response.Message = "There is no Employee with this line manager staffId, " + model.LineManagerStaffId;
                }
            }
            else {
                response.HasError = true;
                response.Message = "Some data is invalid. Please recheck them.";
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("GetEmployeeDetailsUpdate")]
        public IHttpActionResult GetEmployeeDetailsUpdate(int id)
        {
            if (!_permissionService.TryCheckAccess(PermissionProvider.EmployeeRecords))
                return AccessDeniedResult();

            var employeeDetail = _userFactory.ExecuteProcedure<EmployeeModel>("SP_GetEmployeeDetailsUpdate", id, _workContext.User.OrganizationId).FirstOrDefault();
            var response = new ApiResult<EmployeeModel>();
            if (employeeDetail != null) {
                response.Result = employeeDetail;
                response.HasError = false;
            }
            else {
                response.HasError = true;
                response.Message = "Employee is not found";
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("UpdateNewEmployee")]
        public IHttpActionResult UpdateNewEmployee(EmployeeModel model)
        {
            var data = new ApiResult<string>();
            if (ModelState.IsValid) {
                var linemanager = _unitOfWork.Repository<LineManagerDto>().SqlQuery("SELECT Id as LineManagerId, FirstName as LineManagerFirstName, LastName as LineManagerLastName FROM AspNetUsers WHERE StaffId=" + model.LineManagerStaffId).FirstOrDefault();
                if (linemanager != null) {
                    var employee = _userAccountService.FindUserById(model.Id);
                    if (employee != null) {
                        employee.Address = model.Address;
                        employee.BranchId = model.Branch;
                        employee.DateOfBirth = model.DateOfBirth;
                        employee.DateOfEmployment = model.DateOfEmployment ?? DateTime.Now;
                        employee.DepartmentId = model.Department;
                        employee.Email = model.Email;
                        employee.FirstName = model.FirstName;
                        employee.Functions = model.Function;
                        employee.Gender = model.Gender;
                        employee.GradeId = model.Grade;
                        employee.GroupId = model.Group;
                        employee.LastName = model.LastName;
                        employee.LineManagerFirstName = linemanager.LineManagerFirstName;
                        employee.LineManagerLastName = linemanager.LineManagerLastName;
                        employee.LineManagerStaffId = model.LineManagerStaffId;
                        employee.PhoneNumber = model.Phone;
                        employee.RegionId = model.Region;
                        employee.StaffId = model.StaffId;
                        employee.UserName = model.Email.Substring(0, model.Email.LastIndexOf("@"));

                        var acct = _userAccountService.Update(employee);
                        if (acct.Succeeded) {

                            if (model.Password != null) {
                                _userAccountService.ResetPassword(employee.Id, "", model.Password);
                            }
                            var role = _userRole.FindById(model.RoleId ?? 0);
                            var userRole = _userAccountService.GetRoles(employee.Id);
                            foreach (var r in userRole) {
                                _userAccountService.RemoveFromRole(employee.Id, r);
                            }
                            if (role.Name != "Employee") {
                                _userAccountService.AddToRole(employee.Id, role.Name);
                            }

                            var us = _userAccountService.FindUserById(employee.Id);
                            var msg = "<p><strong>Dear " + employee.FirstName + ", " + employee.LastName + "</strong></p>" +
                                    "<p>Your account has been updated on the Learning Management System, with the following details:</p>" +
                                    "<ul>" +
                                    "<li>Full Name: <strong>" + employee.FirstName + ", " + employee.LastName + "</strong></li>" +
                                    "<li>Gender: <strong>" + Enum.GetName(typeof(Gender), employee.Gender) + "</strong></li>" +
                                    "<li>Email Address: <strong>" + employee.Email + "</strong></li>" +
                                    "<li>Grade: <strong>" + us.Grade.Name + "</strong></li>" +
                                    "<li>Branch: <strong>" + us.Branch.Name + "</strong></li>" +
                                    //"<li>Date Of Employnment: <strong>" + employee.DateOfEmployment.ToString("dd-MMMM-yyyy") + "</strong></li>" +
                                    "<li>Department: <strong>" + employee.Department.Name + "</strong></li>" +
                                    "<li>Group: <strong>" + employee.Group.Name + "</strong></li>" +
                                    "<li>Region: <strong>" + employee.Region.Name + "</strong></li>" +
                                    "<li>Line Manager: <strong>" + employee.LineManagerLastName + ", " + employee.LineManagerFirstName + "</strong></li>" +
                                    "<li>Role: <strong>" + role.Name + "</strong></li>" +
                                    "</ul>" +
                                    "<p>If some of the data are wrong, please send an email to " + _workContext.User.Email + " for rectification.</p>" +
                                    "<p>Best Regards!</p>";

                            var queue = new MessageQueue();
                            queue.CreatedById = _workContext.User.Id;
                            queue.CreatedDate = DateTime.Now;
                            queue.Comments = msg;
                            queue.NotificationType = NotificationType.EmployeeUpdate;
                            queue.OrganizationId = _workContext.User.OrganizationId;
                            queue.ActionId = employee.Id;

                            _messageQueueFactory.Add(queue);

                            var existingLine = _linemanagerFactory.All(x => x.LineManagerId == linemanager.LineManagerId && x.EmployeeId == employee.Id, false).ToList();
                            if (!existingLine.Any()) {
                                var linemgr = new LineManager();
                                linemgr.CreatedById = _workContext.User.Id;
                                linemgr.CreatedDate = DateTime.Now;
                                linemgr.EmployeeId = employee.Id;
                                linemgr.IsActive = true;
                                linemgr.LineManagerId = linemanager.LineManagerId;
                                linemgr.OrganizationId = _workContext.User.OrganizationId;

                                _linemanagerFactory.Add(linemgr);
                            }

                            data.HasError = false;
                            data.Message = "Updates for " + model.FirstName + ", successfully done ";
                        }
                        else {
                            data.HasError = true;
                            data.Message = "Updates was unsuccessfull";
                        }
                    }
                    else {
                        data.HasError = true;
                        data.Message = "Sorry, Employee with this profile, does not exist.";
                    }
                }
                else {
                    data.HasError = true;
                    data.Message = "There is no Employee with this line manager staffId, " + model.LineManagerStaffId;
                }

            }
            else {
                data.HasError = true;
                data.Message = "Some of the data is invalid. Please kindly recheck them.";
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("SaveCertificateUploads")]
        public IHttpActionResult SaveCertificateUploads(CertificateUploadModel model)
        {
            var result = new ApiResult<string>();
            if (ModelState.IsValid) {
                if (model.TemplateUrl != string.Empty) {
                    try {
                        var cert = new UserCertificate();
                        cert.CertificateApprovalStatus = model.ApprovalStatus;
                        cert.CreatedById = _workContext.User.Id;
                        cert.IsDeleted = false;
                        cert.Name = model.Name;
                        cert.OrganizationId = _workContext.User.OrganizationId;
                        cert.TemplateUrl = model.TemplateUrl;
                        cert.UserId = model.UserId;
                        cert.Description = model.Description;
                        cert.DateObtained = model.DateObtained;
                        cert.ExpiryDate = model.ExpiryDate;
                        cert.CreatedDate = DateTime.Now;

                        _certFactory.Add(cert);

                        result.HasError = false;
                        result.Message = "Certificate details successfully saved";

                    }
                    catch (Exception e) {
                        result.HasError = true;
                        result.Message = "" + e.Message;
                    }
                }
                else {
                    result.HasError = true;
                    result.Message = "You have not uploaded any certificate";
                }

            }
            else {
                result.HasError = true;
                result.Message = "Some data are invalid. Please check them";
            }
            return Ok(result);
        }

        [Route("getnewemployeesToUpdate")]
        public IHttpActionResult getnewemployeesToUpdate(IDataTablesRequest request)
        {
            var response = new ApiResult<UnupdatedEmployee>();
            var index = request.Start == 0 ? 0 : request.Start / request.Length;

            var sql = "SELECT {0} FROM AspNetUsers {1}";
            var whereSql = new StringBuilder();
            var searchSql = new StringBuilder();
            whereSql.Append(" WHERE  OrganizationId = " + _workContext.User.OrganizationId + " AND StaffId IS NULL");

            if (!string.IsNullOrEmpty(request.Search.Value))
                searchSql.Append(" AND ((Email LIKE '%" + request.Search.Value + "%')");

            var countSql = "declare @TotalRecords int = (select count(Id) from AspNetUsers" + whereSql.ToString() + ") ";
            var dataSql = string.Format(sql.ToString(), "@TotalRecords TotalRecords, Id as UserId,Email", whereSql.ToString() + searchSql);

            var mysql = new StringBuilder();
            mysql.Append(countSql);
            mysql.Append(dataSql);
            mysql.Append(" ORDER  BY Id OFFSET(" + request.Length * index + ") ROWS FETCH NEXT " + request.Length + " ROWS ONLY");


            var newEmployees = _unitOfWork.Repository<UnupdatedEmployee>().SqlQuery(mysql.ToString(), "").ToList();
            var returnObject = new SearchResponse<UnupdatedEmployee>
            {
                draw = request.Draw,
                recordsTotal = newEmployees.Count > 0 ? newEmployees[0].TotalRecords : 0,
                recordsFiltered = newEmployees.Count > 0 ? newEmployees[0].TotalRecords : 0,
                data = newEmployees
            };
            return Ok(returnObject);
        }

    }
}