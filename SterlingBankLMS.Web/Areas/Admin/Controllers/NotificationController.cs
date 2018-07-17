using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Admin.Controllers
{
    public class NotificationController : BaseController
    {
        private readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionSvc;
        private readonly NotificationFactory _notificationFactory;

        public NotificationController(IWorkContext workContext, IPermissionService permission, NotificationFactory notificationFactory)
        {
            _workContext = workContext;
            _permissionSvc = permission;
            _notificationFactory = notificationFactory;
        }
        // GET: Admin/Notification
        public ActionResult Index()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageNotification))
                return AccessDeniedView();

            var notifications = _notificationFactory.All(x => x.IsDeleted == false && x.OrganizationId == _workContext.User.OrganizationId, false).OrderBy(x => x.NotificationType).ToList();
            var results = notifications.MapTo<List<Notification>, List<NotificationDto>>();
            return View(results);
        }
    }
}