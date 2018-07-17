using Microsoft.Office.Interop.Excel;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Data.UnitofWork;
using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vereyon.Web;

namespace SterlingBankLMS.Web.Areas.HumanResources.Controllers
{
    public class GeneralMgtController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkContext _workContext;
        private readonly IUserAccountService _userAccountService;
        private readonly IUserRoleService _userRole;
        private readonly IPermissionService _permissionSvc;
        private readonly UserFactory _userFactory;
        private readonly GroupFactory _groupFactory;
        private readonly NotificationFactory _notificationFactory;
        private readonly RegionFactory _regionFactory;
        private readonly BranchFactory _branchFactory;
        private readonly GradeFactory _gradeFactory;
        private readonly DepartmentFactory _departmentFactory;

        public GeneralMgtController( IUserAccountService userAccountService, IWorkContext workContext, IPermissionService permission, UserFactory userFactory, NotificationFactory notificationFactory, IUserRoleService userRole, IUnitOfWork unitOfWork, GroupFactory groupFactory, RegionFactory regionFactory, BranchFactory branchFactory, DepartmentFactory departmentFactory, GradeFactory gradeFactory )
        {
            _userAccountService = userAccountService;
            _workContext = workContext;
            _permissionSvc = permission;
            _userFactory = userFactory;
            _notificationFactory = notificationFactory;
            _userRole = userRole;
            _unitOfWork = unitOfWork;
            _groupFactory = groupFactory;
            _regionFactory = regionFactory;
            _departmentFactory = departmentFactory;
            _branchFactory = branchFactory;
            _gradeFactory = gradeFactory;
        }

        public ActionResult Index()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.GeneralMgt))
                return AccessDeniedView();

            return View();
        }
        public ActionResult BulkUploads()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.GeneralMgt))
                return AccessDeniedView();

            return View();
        }

        [HttpPost]
        public ActionResult BulkUploads( HttpPostedFileBase upload )

        {
            if (upload == null)
            {
                FlashMessage.Danger("Please select an excel file");
                return View();
            }
            else
            {
                string fileExtension = Path.GetExtension(upload.FileName);
                string fileName = Path.GetFileName(upload.FileName);
                string add = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string fullFileName = fileName + "_" + add + fileExtension;
                if (fileExtension.EndsWith(".xls") || fileExtension.EndsWith(".xlsx") || fileExtension.EndsWith(".XLS"))
                {
                    string fileLocation = Server.MapPath("~/ExcelFiles/" + fullFileName);
                    FileInfo fileinfo = new FileInfo(fileLocation);
                    upload.SaveAs(fileLocation);
                    FileStream stream = new FileStream(fileLocation, FileMode.Open);

                    Application application = new Application();
                    Workbook workBook = application.Workbooks.Open(Server.MapPath("~/ExcelFiles/" + fullFileName));
                    Worksheet workSheet = (Worksheet)workBook.ActiveSheet;
                    Range range = workSheet.UsedRange;

                    //response.Message = "";
                    //response.Message += "<ul>";
                    //for (int row = 2; row <= range.Rows.Count; row++)
                    //{

                    //    string group = ((Range)range.Cells[row, 13]).Text;
                    //    string linefirstname = ((Range)range.Cells[row, 16]).Text;
                    //    string linelastname = ((Range)range.Cells[row, 17]).Text;
                    //    string staffid = ((Range)range.Cells[row, 2]).Text;
                    //    string branch = ((Range)range.Cells[row, 11]).Text;
                    //    string department = ((Range)range.Cells[row, 15]).Text;
                    //    string grade = ((Range)range.Cells[row, 10]).Text;
                    //    string region = ((Range)range.Cells[row, 12]).Text;
                    //    string gender = ((Range)range.Cells[row, 9]).Text;

                    //    if (group == null)
                    //    {
                    //        response.Message += "<li>Group on row:" + row + ", cannot be null</li>";
                    //        response.HasError = true;
                    //    }
                    //    if (branch == null)
                    //    {
                    //        response.Message += "<li>Branch on row:" + row + ", cannot be null</li>";
                    //        response.HasError = true;
                    //    }
                    //    if (department == null)
                    //    {
                    //        response.Message += "<li>Department on row:" + row + ", cannot be null</li>";
                    //        response.HasError = true;
                    //    }
                    //    if (region == null)
                    //    {
                    //        response.Message += "<li>Region on row:" + row + ", cannot be null</li>";
                    //        response.HasError = true;
                    //    }


                    //}

                    try
                    {
                        for (int row = 1200; row <= range.Rows.Count; row++)
                        {
                            Group g = new Group();
                            string group = ((Range)range.Cells[row, 13]).Text;
                            string linefirstname = ((Range)range.Cells[row, 16]).Text;
                            string linelastname = ((Range)range.Cells[row, 17]).Text;
                            string staffid = ((Range)range.Cells[row, 2]).Text;
                            string branch = ((Range)range.Cells[row, 11]).Text;
                            string department = ((Range)range.Cells[row, 15]).Text;
                            string grade = ((Range)range.Cells[row, 10]).Text;
                            string region = ((Range)range.Cells[row, 12]).Text;
                            string gender = ((Range)range.Cells[row, 9]).Text;



                            var grps = _groupFactory.GetAllIncluding(x => x.Name == group, false).ToList();
                            if (!grps.Any(x => x.Name == group))
                            {
                                g.Name = group;
                                g.OrganizationId = _workContext.User.OrganizationId;
                                g.CreatedById = _workContext.User.Id;
                                g.GroupHeadFirstName = linefirstname;
                                g.GroupHeadLastName = linelastname;
                                g.GroupHeadStaffId = staffid;
                                g.CreatedDate = DateTime.Now;
                                g.IsDeleted = false;

                                _groupFactory.Add(g);
                            }


                            var d = new Department();
                            var depts = _departmentFactory.GetAllIncluding(x => x.Name == department, false).ToList();
                            if (!depts.Any(x => x.Name == department))
                            {
                                d.Name = department;
                                d.CreatedById = _workContext.User.Id;
                                d.CreatedDate = DateTime.Now;
                                d.GroupId = g.Id == 0 ? grps[0].Id : g.Id;
                                d.OrganizationId = _workContext.User.OrganizationId;
                                d.IsDeleted = false;

                                _departmentFactory.Add(d);

                            }

                            var r = new Region();
                            var regions = _regionFactory.GetAllIncluding(x => x.Name == region, false).ToList();
                            if (!regions.Any(x => x.Name == region))
                            {
                                r.Name = region;
                                r.CreatedById = _workContext.User.Id;
                                r.CreatedDate = DateTime.Now;
                                r.OrganizationId = _workContext.User.OrganizationId;
                                r.IsDeleted = false;

                                _regionFactory.Add(r);

                            }

                            var gr = new Grade();
                            var grades = _gradeFactory.GetAllIncluding(x => x.Name == grade, false).ToList();
                            if (!grades.Any(x => x.Name == grade))
                            {
                                gr.Name = grade;
                                gr.CreatedById = _workContext.User.Id;
                                gr.CreatedDate = DateTime.Now;
                                gr.OrganizationId = _workContext.User.OrganizationId;
                                gr.IsDeleted = false;
                                gr.SortOrder = row;

                                _gradeFactory.Add(gr);

                            }

                            var br = new Branch();
                            var branches = _branchFactory.GetAllIncluding(x => x.Name == branch, false).ToList();
                            if (!branches.Any(x => x.Name == branch))
                            {
                                br.Name = branch;
                                br.CreatedById = _workContext.User.Id;
                                br.CreatedDate = DateTime.Now;
                                br.OrganizationId = _workContext.User.OrganizationId;
                                br.IsDeleted = false;

                                _branchFactory.Add(br);

                            }
                        }


                        stream.Close();
                        stream.Dispose();
                        fileinfo.Delete();

                        FlashMessage.Confirmation("Successful upload");
                    }
                    catch (Exception e)
                    {

                        FlashMessage.Danger("" + e.InnerException.Message);
                    }


                }
                else
                {

                    FlashMessage.Danger("This is not a valid excel file");
                }

            }

            return View();
        }

        public FileResult DownloadSampleExcel( string fileName )
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Content" + "\\" + "excel" + "\\" + fileName;
            string name = Path.GetFileName(filePath);
            return File(filePath, "application/force-download", name);
        }

    }
}