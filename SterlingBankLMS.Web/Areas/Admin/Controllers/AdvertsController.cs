using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.ViewModels;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Admin.Controllers
{
    public class AdvertsController : BaseController
    {
        private readonly AdvertFactory _AdFactory;
        private readonly OrganizationFactory _organizationFactory;
        private readonly IPermissionService _permissionSvc;

        public AdvertsController(AdvertFactory AdFactory, OrganizationFactory organizationFactory, IPermissionService permissionSvc)
        {
            _AdFactory = AdFactory;
            _organizationFactory = organizationFactory;
            _permissionSvc = permissionSvc;
        }


        public ActionResult Banner()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageAdverts))
            {
                return AccessDeniedView();
            }

            var output = new ManagePageModel
            {
                New = false
            };
            return View(output);
        }

        // GET: Admin/Adverts
        public ActionResult Index()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageAdverts))
            {
                return AccessDeniedView();
            }

            var output = new ManagePageModel
            {
                New = false
            };
            return View(output);
        }

        public ActionResult AddNew()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageAdverts))
            {
                return AccessDeniedView();
            }

            var output = new ManagePageModel
            {
                New = true
            };
            return View("index", output);
        }
    }
}