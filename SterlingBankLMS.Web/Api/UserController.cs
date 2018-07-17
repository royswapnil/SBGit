using DataTables.AspNet.Core;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Models.IdentityModels;
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
    [RoutePrefix("api/user")]
    public class UserController : BaseApiController
    {
        private readonly IWorkContext _workContext;
        private readonly IUserAccountService _userAcct;
        private readonly IUserRoleService _userRole;
        private readonly UserFactory _userFactory;
        private readonly NotificationHubFactory _notificationHubFactory;
        private readonly MailsFactory _mailsFactory;
        private readonly MessageQueueFactory _messageQueueFactory;

        public UserController( IWorkContext workContext, IUserAccountService userAcct, UserFactory userFactory, IUserRoleService userRole, NotificationHubFactory notificationHubFactory, MailsFactory mailsFactory, MessageQueueFactory messageQueueFactory )
        {
            _workContext = workContext;
            _userAcct = userAcct;
            _userFactory = userFactory;
            _userRole = userRole;
            _notificationHubFactory = notificationHubFactory;
            _mailsFactory = mailsFactory;
            _messageQueueFactory = messageQueueFactory;
        }

        [HttpGet]
        [Route("getallusers")]
        public IHttpActionResult GetAllUsers( IDataTablesRequest request )
        {

            var index = request.Start == 0 ? 0 : request.Start / request.Length;
            var users = _userFactory.ExecuteProcedure<UserDto>("SP_GetAllUsers", _workContext.User.OrganizationId, request.Search.Value, request.Start, request.Length).ToList();


            var obj = new SearchResponse<UserDto>
            {
                draw = request.Draw,
                recordsTotal = users.Count() > 0 ? users[0].TotalRecords : 0,
                recordsFiltered = users.Count() > 0 ? users[0].TotalRecords : 0,
                data = users
            };

            return Ok(obj);
        }

        [HttpGet]
        [Route("getroles")]
        public IHttpActionResult GetRoles()
        {
            var data = new ApiResult<List<ApplicationRole>>();
            var result = _userRole.GetRoles().ToList();
            data.Result = result;
            data.HasError = false;
            return Ok(data);
        }

        [HttpGet]
        [Route("getallemployees")]
        public IHttpActionResult GetUsers()
        {
            var data = new ApiResult<List<ApplicationUser>>();
            var result = _userAcct.GetAllUsers().OrderBy(x => x.LastName).ToList();
            data.Result = result;
            data.HasError = false;
            return Ok(data);
        }

        [HttpPost]
        [Route("SaveUser")]
        public IHttpActionResult SaveUser( UserModel model )
        {
            var result = new ApiResult<ApplicationUser>();

            if (ModelState.IsValid)
            {
                //Get Email domain name
                var domain = model.Email.Substring(model.Email.LastIndexOf("@") + 1);
                if (domain != AppConstants.SterlingEmailDomainName)
                {
                    var user = new ApplicationUser();

                    var pwd = AppHelper.GeneratePwd();
                    user.DateOfEmployment = model.DateOfEmployment;
                    user.Email = model.Email;
                    user.FirstName = model.FirstName;
                    user.Gender = model.Gender;
                    user.LastName = model.LastName;
                    user.UserType = Data.Models.Enums.UserType.Normal;
                    user.UserName = model.Email.Substring(0, model.Email.LastIndexOf("@"));
                    user.OrganizationId = _workContext.User.OrganizationId;

                    var acct = _userAcct.CreateUser(user, pwd);
                    if (acct.Succeeded)
                    {
                        var u = new User() { ApplicationUserId = user.Id };
                        _userFactory.Add(u);

                        var role = _userRole.FindById(model.RoleName);
                        _userAcct.AddToRole(user.Id, "Employee");
                        _userAcct.AddToRole(user.Id, "Employee");
                        if (role.Name != "Employee")
                        {
                            _userAcct.AddToRole(user.Id, role.Name);
                        }
                        var code = _userAcct.GenerateEmailConfirmationToken(user.Id);
                        var msg = "<p><strong>Dear " + user.FirstName + ", " + user.LastName + "</strong></p>" +
                            "<p>Your account has been created on the Learning Management System, with the following details:</p>" +
                            "<ul>" +
                            "<li>Email Address: <strong>" + user.Email + "</strong></li>" +
                            "<li>Password: <strong>" + pwd + "</strong></li>" +
                            "<li>Role: <strong>" + role.Name + "</strong></li>" +
                            "</ul>" +
                            "<p>You can login to change your password at your convenient time.</p>" +
                            "<p>Best Regards!</p>";

                        var queue = new MessageQueue();
                        queue.CreatedById = _workContext.User.Id;
                        queue.CreatedDate = DateTime.Now;
                        // queue.Comments = msg;
                        queue.ReceiverId = user.Id;
                        queue.NotificationType = Data.Models.Enums.NotificationType.NewAccount;
                        queue.OrganizationId = _workContext.User.OrganizationId;
                        queue.ActionId = user.Id;

                        _messageQueueFactory.Add(queue);

                        result.HasError = false;
                        result.Message = "User Successfully added";
                        result.Result = user;

                    }
                    else
                    {
                        result.HasError = true;
                        result.Message = "An Error occured while creating user.";
                    }
                }
                else
                {
                    result.HasError = true;
                    result.Message = "You cannot create users. All users must be created and authenticated via the active directory";
                }
            }
            else
            {
                result.HasError = true;
                result.Message = "Please check if you have provided valid data";
            }

            return Ok(result);
        }


    }
}
