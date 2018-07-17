using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.UnitofWork;
using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Admin.Controllers
{
    public class UserManagementController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkContext _workContext;
        private readonly IUserAccountService _userAccountService;
        private readonly IUserRoleService _userRole;
        private readonly IPermissionService _permissionSvc;
        private readonly UserFactory _userFactory;
        private readonly NotificationFactory _notificationFactory;

        public UserManagementController(IUserAccountService userAccountService, IWorkContext workContext, IPermissionService permission, UserFactory userFactory, NotificationFactory notificationFactory, IUserRoleService userRole, IUnitOfWork unitOfWork )
        {
            _userAccountService = userAccountService;
            _workContext = workContext;
            _permissionSvc = permission;
            _userFactory = userFactory;
            _notificationFactory = notificationFactory;
            _userRole = userRole;
            _unitOfWork = unitOfWork;
        }

        public ActionResult AllUsers()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageUsers))
                return AccessDeniedView();

            return View();
        }

        public ActionResult CreateRole()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageUsers))
                return AccessDeniedView();

            var permission = _unitOfWork.Repository<PermissionModel>().SqlQuery("select distinct Id, [Name] as PermissionName from Permission").ToList();
            ViewBag.PermissionName = permission;
            return View();
        }

        [HttpPost]
        public ActionResult SaveRolePermissions(List<RolePermissionModel> permissions, string roleName)
        {
            var result = new ApiResult<string>();

            var perms = permissions.Where(x => x.IsPermissionName == true).ToList();
            var role = new ApplicationRole
            {
                Name = roleName,
                SystemName = roleName,
                IsActive = true,
                OrganizationId = _workContext.User.OrganizationId
            };

            var r = _userRole.Create(role);
            if (r.Succeeded)
            {

                foreach (var item in perms)
                {

                    var permission = _permissionSvc.Find(item.Id);

                    if (permission!=null)
                    {
                        role.Permissions.Add(permission);
                        _userRole.Update(role);
                       
                    }
                }
                result.HasError = false;
                result.Message = "Role with its permissions has been successfully saved!";
            }
            else
            {
                result.HasError = true;
                result.Message = "Unable to save role with its permissions. Role name already exists";
            }
            
            return Json(result);
        }
    }
}