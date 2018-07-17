using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Technology.Controllers
{
    public class TechSupportController : BaseController
    {
        private readonly IWorkContext _workContext;
        private readonly TicketFactory _ticketFactory;
        private readonly IUserAccountService _userAccountService;
        private readonly IPermissionService _permissionSvc;
        private readonly UserFactory _userFactory;
        private readonly NotificationFactory _notificationFactory;

        public TechSupportController( TicketFactory ticketFactory, IUserAccountService userAccountService, IWorkContext workContext, IPermissionService permission, UserFactory userFactory, NotificationFactory notificationFactory )
        {
            _ticketFactory = ticketFactory;
            _userAccountService = userAccountService;
            _workContext = workContext;
            _permissionSvc = permission;
            _userFactory = userFactory;
            _notificationFactory = notificationFactory;
        }
        public ActionResult Dashboard()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSupport))
                return AccessDeniedView();

            ViewBag.TotalTickets = _ticketFactory.Count(x => x.IsDeleted == false && x.OrganizationId == _workContext.User.OrganizationId);
            ViewBag.TreatedTickets = _ticketFactory.Count(x => x.TicketStatus == Data.Models.Enums.TicketStatus.Closed && x.IsDeleted == false && x.OrganizationId == _workContext.User.OrganizationId);
            ViewBag.EscalatedTickets = _ticketFactory.Count(x => x.TicketStatus == Data.Models.Enums.TicketStatus.Escalate && x.IsDeleted == false && x.OrganizationId == _workContext.User.OrganizationId);

            return View();
        }
        public ActionResult SingleTechTicketDetails( string ticketId, string status )
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSupport))
                return AccessDeniedView();
            var TicketId = Convert.ToInt32(ticketId);
            ViewBag.TicketId = TicketId;
            ViewBag.Status = status;
            return View();
        }

        public ActionResult EscalatedTicketCount()
        {
            var count = _ticketFactory.Count(x => x.TicketStatus == Data.Models.Enums.TicketStatus.Escalate);
            return Json(count, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewMessages()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSupport))
                return AccessDeniedView();

            return View();
        }

        public ActionResult TechTickets()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSupport))
                return AccessDeniedView();
            ViewBag.EscalatedTickets = _ticketFactory.Count(x => x.TicketStatus == Data.Models.Enums.TicketStatus.Escalate);
            return View();
        }
    }
}