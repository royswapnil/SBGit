using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class SurveyController : BaseController
    {
        private readonly IPermissionService _permissionSvc;
        public SurveyController(IPermissionService permissionSvc)
        {
            _permissionSvc = permissionSvc;
        }

        // GET: Admin/Survey
        public ActionResult Index()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
            {
                return AccessDeniedView();
            }

            var output = new SurveyPageModel
            {
                New = false
            };
            return View(output);
        }

        [Route("Admin/Survey/Course/{id?}")]
        public ActionResult Course(int id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
            {
                return AccessDeniedView();
            }
            var output = new SurveyPageModel
            {
                New = true,
                Id = id,
                SurveyType = (int) SurveyType.CourseSurvey
            };
            return View("index", output);
        }

        [Route("Admin/Survey/Exam/{id?}")]
        public ActionResult Exam(int id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
            {
                return AccessDeniedView();
            }

            var output = new SurveyPageModel
            {
                New = true,
                Id = id,
                SurveyType = (int)SurveyType.ExamSurvey
            };
            return View("index", output);
        }

        [Route("Admin/Survey/Training/{id?}")]
        public ActionResult Training(int id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
            {
                return AccessDeniedView();
            }

            var output = new SurveyPageModel
            {
                New = true,
                Id = id,
                SurveyType = (int)SurveyType.TrainingSurvey
            };
            return View("index", output);
        }

        public ActionResult Add()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
            {
                return AccessDeniedView();
            }

            var output = new SurveyPageModel
            {
                New = true
            };
            return View("index", output);
        }

        public ActionResult Template()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
            {
                return AccessDeniedView();
            }

            var output = new ManagePageModel
            {
                New = false
            };

            return View("template", output);
        }

      
        public ActionResult AddTemplate()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
            {
                return AccessDeniedView();
            }

            var output = new ManagePageModel
            {
                New = true
            };
            return View("template", output);
        }

        [Route("Admin/Survey/Assign/{id?}")]
        public ActionResult Assign(int? id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageSurvey))
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