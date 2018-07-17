using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.ViewModels;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Admin.Controllers
{
    public class ManageCourseController : BaseController
    {
        private readonly CourseFactory _courseFactory;
        private readonly LearningAreaFactory _LearningAreaFactory;
        private readonly IPermissionService _permissionSvc;

        public ManageCourseController(CourseFactory courseFactory, LearningAreaFactory LearningAreaFactory, IPermissionService permissionSvc)
        {
            _courseFactory = courseFactory;
            _LearningAreaFactory = LearningAreaFactory;
            _permissionSvc = permissionSvc;
        }

        // GET: Admin/ManageCourse
        public ActionResult Index()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse))
            {
                return AccessDeniedView();
            }
            return View();
        }

        
        public ActionResult LearningArea()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse))
            {
                return AccessDeniedView();
            }

            return View("LearningArea");
        }

        public ActionResult AddCourse()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse))
            {
                return AccessDeniedView();
            }

            var output = new ManagePageModel
            {
                New = true
            };
            return View("AddCourse", output);
        }
        public ActionResult ViewCourse()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse))
            {
                return AccessDeniedView();
            }

            var output = new ManagePageModel
            {
                New = false
            };
            return View("AddCourse", output);
        }

        [Route("Admin/Course/Assign/{id?}")]
        public ActionResult Assign(int? id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse))
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