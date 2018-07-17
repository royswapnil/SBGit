using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Admin.Controllers
{
    public class LearningGroupController : BaseController
    {
        private readonly IPermissionService _permissionSvc;
        public LearningGroupController(IPermissionService permissionSvc)
        {
            _permissionSvc = permissionSvc;
        }

        // GET: Admin/LearningGroup
        public ActionResult Index()
        {
            //if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageLearningGroup))
            //{
            //    return AccessDeniedView();
            //}

            return View();
        }
    }
}