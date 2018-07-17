using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class FAQController : BaseController
    {
        private readonly IPermissionService _permissionSvc;
        public FAQController(IPermissionService permissionSvc)
        {
            _permissionSvc = permissionSvc;
        }

        // GET: Admin/FAQ
        public ActionResult Index()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageFAQ))
            {
                return AccessDeniedView();
            }
            return View();
        }
    }
}