using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.Admin.Controllers
{
    public class BulkUploadController : BaseController
    {
        private readonly IPermissionService _permissionSvc;
        public BulkUploadController(IPermissionService permissionSvc)
        {
            _permissionSvc = permissionSvc;
        }
        // GET: Admin/BulkUpload
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult Course()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageCourse))
            {
                return AccessDeniedView();
            }

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UploadCourseFiles(HttpPostedFileBase file)
        {
            if(Request.Files.Count == 0)
            {
                return NotFoundView();
            }

            if (Request.Files[0].ContentLength > 0 )
            {
                var name = Request.Files[0].FileName;
                if(Path.GetExtension(name).ToUpper().Equals(".XLS")|| Path.GetExtension(name).ToUpper().Equals(".XLSX"))
                {
                //    var excelFile = new ExcelReader(Request.Files[0].InputStream).ReadAllSheets;
                //    var myCourse = excelFile.ReadAllSheets<CourseExcelModel>();
                }
                

            }
                return View();
        }
    }
}