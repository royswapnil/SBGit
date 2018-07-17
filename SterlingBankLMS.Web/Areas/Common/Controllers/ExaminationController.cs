using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.ViewModels;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Common.Controllers
{
    public class ExaminationController : BaseController
    {
        private readonly IPermissionService _permissionSvc;
        private readonly IWorkContext _workContext;
        private readonly ExaminationFactory _examFactory;

        public ExaminationController(IWorkContext workContext,
            IPermissionService permissionSvc, ExaminationFactory examFactory)
        {
            _workContext = workContext;
            _permissionSvc = permissionSvc;
            _examFactory = examFactory;
        }

        public ActionResult Index()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedView();
            }

            return View();
        }

        public ActionResult Examination(int id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS)) {
                return AccessDeniedView();
            }

            if (!_examFactory.ValidateExamination(id, _workContext.User.OrganizationId)) {
                return NotFoundView();
            }

            return View(new ExamDetailsModel { ExaminationId = id });
        }

        public ActionResult ExamSummary()
        {
            return PartialView("_examsummary");
        }

        public ActionResult ExamQuestions()
        {
            return PartialView("_examquestion");
        }
    }
}