using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.UnitofWork;
using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Admin.Controllers
{
    public class OrganizationManagementController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkContext _workContext;
        private readonly TrainingFactory _trainingFactory;
        private readonly IUserAccountService _userAccountService;
        private readonly IPermissionService _permissionSvc;
        private readonly DepartmentFactory _deptFactory;
        private readonly TrainingVideoFactory _videoFactory;
        private readonly DepartmentBudgetFactory _budgetFactory;
        private readonly TrainingRequestFactory _requestFactory;
        private readonly UserFactory _userFactory;
        private readonly CourseFactory _courseFactory;
        private readonly OrganizationFactory _orgFactory;
        private readonly NotificationHubFactory _notificationHubFactory;
        private readonly MailsFactory _mailsFactory;


        public OrganizationManagementController( TrainingFactory trainingFactory, IUserAccountService userAccountService, IWorkContext workContext, IPermissionService permission, DepartmentFactory deptFactory, TrainingVideoFactory videoFactory, DepartmentBudgetFactory budgetFactory, TrainingRequestFactory requestFactory, UserFactory userFactory, CourseFactory courseFactory, OrganizationFactory organizationFactory, NotificationHubFactory notificationHubFactory, MailsFactory mailsFactory, IUnitOfWork unitOfWork )
        {
            _trainingFactory = trainingFactory;
            _userAccountService = userAccountService;
            _workContext = workContext;
            _permissionSvc = permission;
            _deptFactory = deptFactory;
            _videoFactory = videoFactory;
            _budgetFactory = budgetFactory;
            _requestFactory = requestFactory;
            _userFactory = userFactory;
            _courseFactory = courseFactory;
            _orgFactory = organizationFactory;
            _mailsFactory = mailsFactory;
            _notificationHubFactory = notificationHubFactory;
            _unitOfWork = unitOfWork;
        }

        public ActionResult AllOrganization()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
                return AccessDeniedView();

            ViewBag.TotalLearners = _userAccountService.GetAllUsers().Count();
            ViewBag.ActiveOrg = _orgFactory.Count(x => x.OrganizationalStatus == Data.Models.Enums.OrganizationalStatus.Activated);
            ViewBag.ActiveCourses = _courseFactory.Count(x => x.IsPublished == true);

            return View();
        }
    }
}