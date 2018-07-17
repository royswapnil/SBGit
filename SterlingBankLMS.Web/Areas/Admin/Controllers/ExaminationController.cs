using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.ViewModels;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ExaminationController : BaseController
    {
        private readonly IPermissionService _permissionSvc;
        public ExaminationController(IPermissionService permissionSvc)
        {
            _permissionSvc = permissionSvc;
        }
        // GET: Admin/Examination
        public ActionResult Index()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageExamination))
            {
                return AccessDeniedView();
            }


            var output = new ManagePageModel
            {
                New = false
            };
            return View(output);
        }

        [Route("Admin/Examination/Course/{id?}")]
        public ActionResult Course(int id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageExamination))
            {
                return AccessDeniedView();
            }

            var output = new ManagePageModel
            {
                New = true,
                Id = id
            };
            return View("index", output);
        }

       
        public ActionResult AddExam()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageExamination))
            {
                return AccessDeniedView();
            }

            var output = new ManagePageModel
            {
                New = true
            };
            return View("index", output);
        }

        [Route("Admin/Examination/Assign/{id?}")]
        public ActionResult Assign(int? id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageExamination))
            {
                return AccessDeniedView();
            }

            var output = new ManagePageModel
            {
                New = true,
                Id = id
            };
            return View("Assign", output);
        }

    }
}