using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Admin.Controllers
{
    public class SupportManagementController : BaseController
    {
        private readonly IWorkContext _workContext;
        private readonly TicketFactory _ticketFactory;
        private readonly IUserAccountService _userAccountService;
        private readonly IPermissionService _permissionSvc;
        private readonly UserFactory _userFactory;

        public SupportManagementController( TicketFactory ticketFactory, IUserAccountService userAccountService, IWorkContext workContext, IPermissionService permission, UserFactory userFactory )
        {
            _ticketFactory = ticketFactory;
            _userAccountService = userAccountService;
            _workContext = workContext;
            _permissionSvc = permission;
            _userFactory = userFactory;
        }

        public ActionResult Dashboard()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSupport))
                return AccessDeniedView();

            ViewBag.TotalTickets = _ticketFactory.Count(x => x.IsDeleted == false && x.OrganizationId == _workContext.User.OrganizationId);
            ViewBag.NewTickets = _ticketFactory.Count(x => x.TicketStatus == Data.Models.Enums.TicketStatus.New && x.IsDeleted == false && x.OrganizationId == _workContext.User.OrganizationId);
            ViewBag.OpenTickets = _ticketFactory.Count(x => x.TicketStatus == Data.Models.Enums.TicketStatus.Open && x.IsDeleted == false && x.OrganizationId == _workContext.User.OrganizationId);
            ViewBag.ClosedTickets = _ticketFactory.Count(x => x.TicketStatus == Data.Models.Enums.TicketStatus.Closed && x.IsDeleted == false && x.OrganizationId == _workContext.User.OrganizationId);
            ViewBag.EscalatedTickets = _ticketFactory.Count(x => x.TicketStatus == Data.Models.Enums.TicketStatus.Escalate && x.IsDeleted == false && x.OrganizationId == _workContext.User.OrganizationId);

            return View();
        }

        public ActionResult SingleTicketDetails( string ticketId, string status )
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSupport))
                return AccessDeniedView();
            var TicketId = Convert.ToInt32(ticketId);
            ViewBag.TicketId = TicketId;
            ViewBag.Status = status;
            return View();
        }

        public ActionResult _GetTicketDetails( string userId, string ticketname )
        {
            var id = Convert.ToInt32(userId);
            var employeeDetailsDto = _userFactory.GetEmployeeDetails(id, _workContext.User.OrganizationId);

            if (employeeDetailsDto == null)
            {
                return HttpNotFound();
            }
            var employeeVm = employeeDetailsDto.MapTo<EmployeeDetailsDto, EmployeeDetailsModel>();
            ViewBag.TicketTitle = ticketname;
            return PartialView("_GetTicketDetails", employeeVm);
        }

        public ActionResult _ChangeTicketStatus( int ticketId, string buttonvalue, string ticketTitle )
        {
            var json = new JsonResult()
            {
                Data = new
                {
                    Id = ticketId,
                    ButtonValue = buttonvalue,
                    Title = ticketTitle
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            return json;
        }

        public ActionResult Ticket()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSupport))
                return AccessDeniedView();
            ViewBag.NewTickets = _ticketFactory.Count(x => x.TicketStatus == Data.Models.Enums.TicketStatus.New);

            return View();
        }

        public ActionResult NewTicketCount()
        {
            var count = _ticketFactory.Count(x => x.TicketStatus == Data.Models.Enums.TicketStatus.New);
            return Json(count, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult NewMessage()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSupport))
                return AccessDeniedView();

            return View();
        }

        public ActionResult TicketDetails( int TicketId, string TicketTitle )
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSupport))
                return AccessDeniedView();

            return View();
        }
    }
}