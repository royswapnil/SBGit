using Rotativa;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Common.Controllers
{
    public class CertificateController : BaseController
    {
        private readonly UserCourseFactory _userCourseFactory;
        private readonly IUserAccountService _userAccountService;
        private readonly IPermissionService _permissionSvc;
        private readonly IWorkContext _workContext;

        public CertificateController(UserCourseFactory userCourseFactory, IUserAccountService userAccountService, IWorkContext WorkContext, IPermissionService permissionSvc)
        {
            _userCourseFactory = userCourseFactory;
            _userAccountService = userAccountService;
            _workContext = WorkContext;
            _permissionSvc = permissionSvc;
        }
        // GET: Common/Certificates
        public ActionResult Index()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS))
            {
                return AccessDeniedView();
            }

            return View();
        }
        

        public ActionResult GenerateCertificatePDf(int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS))
            {
                return AccessDeniedView();
            }

            var certififcate = _userCourseFactory.GetCertificate(Id);
            if (certififcate == null)
                return NotFoundView();

            var user = _userAccountService.FindUserById(certififcate.UserId);

            if (user.Id != _workContext.User.Id || !_workContext.User.IsInRole("Administrator"))
                return NotFoundView();



            var pdfDetails = new EmployeeCertificateModel()
            {
                CourseId = certififcate.CourseId,
                DueDate = certififcate.Course.DueDate,
                EndDate = certififcate.EndDate,
                Name = certififcate.Course.Name,
                Id = certififcate.Id,
                StartDate = certififcate.StartDate,
                User = user.FirstName + " " + user.LastName
            };

            return new ViewAsPdf(pdfDetails);
            //FileName = user.FirstName + "_" + user.LastName + "_" + pdfDetails.Name.Replace("", "_") + ".pdf" };
        }
    }
}