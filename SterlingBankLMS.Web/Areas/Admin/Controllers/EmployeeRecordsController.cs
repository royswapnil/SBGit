using ExcelManager;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Data.UnitofWork;
using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace SterlingBankLMS.Web.Areas.Admin.Controllers
{
    public class EmployeeRecordsController : BaseController
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
        private readonly LinemanagerFactory _linemanagerFactory;

        public EmployeeRecordsController(IUserAccountService userAccountService, IWorkContext workContext,
            IPermissionService permission, UserFactory userFactory,
            NotificationFactory notificationFactory, IUserRoleService userRole,
            IUnitOfWork unitOfWork, GroupFactory groupFactory, RegionFactory regionFactory,
            BranchFactory branchFactory, DepartmentFactory departmentFactory,
            GradeFactory gradeFactory, LinemanagerFactory linemanagerFactory)
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
            _linemanagerFactory = linemanagerFactory;
        }

        public ActionResult ListEmployees()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.EmployeeRecords))
                return AccessDeniedView();

            var grps = _groupFactory.GroupOrganizationDDL(_workContext.User.OrganizationId);
            ViewBag.group = new SelectList(grps, "Id", "Name");

            return View();
        }

        public ActionResult ProfileDetails([Required] int id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.EmployeeRecords))
                return AccessDeniedView();

            var employeeDetailsDto = _userFactory.GetEmployeeDetails(id, _workContext.User.OrganizationId);

            if (employeeDetailsDto == null) {
                return NotFoundView();
            }

            var employeeVm = employeeDetailsDto.MapTo<EmployeeDetailsDto, EmployeeDetailsModel>();
            return View(employeeVm);
        }


        public ActionResult NewEmployee()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.EmployeeRecords))
                return AccessDeniedView();
            return View();
        }

        public ActionResult NewEmployeeCertificate()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.EmployeeRecords))
                return AccessDeniedView();
            ViewBag.ApprovalStatus = MvcUtilities.GenerateEnumSelectList(typeof(CertificateApprovalStatus), null);
            return View();
        }
        [HttpPost]
        [Route("UploadCertificate")]
        public ActionResult UploadCertificate()
        {
            string fileName = "";
            string fname = "";
            var response = new ApiResult<string>();

            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++) {
                HttpPostedFileBase file = files[i];
                if (file.ContentLength > 0) {

                    var extension = Path.GetExtension(file.FileName);
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".JPG" || extension == ".jpeg" || extension == ".PNG") {
                        // Checking for Internet Explorer    
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER") {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                            fileName = fname;
                        }
                        else {
                            fname = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + file.FileName.Replace(" ", "_");
                            fileName = fname;
                        }

                        fname = Server.MapPath(AppConstants.CertUploadPath) + fname;
                        file.SaveAs(fname);


                        response.Message = "Certificate was successfully uploaded. Click Save button to save to database";
                        response.HasError = false;
                        response.Result = AppConstants.CertUploadPath + fileName;


                    }
                    else {
                        response.Message = "Sorry, file can only be image";
                        response.HasError = true;

                    }

                }
                else {
                    response.Message = "Sorry, choose a file";
                    response.HasError = true;
                }

            }
            return Json(response);
        }
        public ActionResult EmployeeBulkUpload()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.EmployeeRecords))
                return AccessDeniedView();
            return View();
        }

        public JsonResult UploadStaffDetails()
        {
            var response = new ApiResult<string>();
            HttpFileCollectionBase files = Request.Files;
            if (files.Count != 0) {
                HttpPostedFileBase upload = files[0];

                string fileExtension = Path.GetExtension(upload.FileName);
                string fileName = Path.GetFileName(upload.FileName);

                if (fileExtension.EndsWith(".xls") || fileExtension.EndsWith(".xlsx") || fileExtension.EndsWith(".XLS")) {


                    var employees = new ExcelReader(upload.InputStream).ReadWorkBookSheets<EmployeeExcelUploadModel>(0);

                    var currDate = CommonHelper.GetCurrentDate();
                    employees.GroupBy(x => x.Branch).ForEach(e => {
                        var branch = _branchFactory.Find(x => x.Name.Equals(e.Key, StringComparison.InvariantCultureIgnoreCase) && !x.IsDeleted, false);
                        if (branch == null) {
                            branch = new Branch
                            {
                                Name = e.Key,
                                CreatedById = _workContext.User.UserId,
                                CreatedDate = currDate,
                                OrganizationId = _workContext.User.OrganizationId
                            };
                            _branchFactory.Add(branch);
                        }

                        e.ForEach(usr => {
                            usr.BranchId = branch.Id;
                        });
                    });


                    employees.GroupBy(x => x.Department).ForEach(e => {
                        var dept = _departmentFactory.Find(x => x.Name.Equals(e.Key, StringComparison.InvariantCultureIgnoreCase) && !x.IsDeleted, false);
                        if (dept == null) {
                            dept = new Department
                            {
                                Name = e.Key,
                                CreatedById = _workContext.User.UserId,
                                CreatedDate = currDate,
                                OrganizationId = _workContext.User.OrganizationId
                            };
                            _departmentFactory.Add(dept);
                        }

                        e.ForEach(usr => {
                            usr.DepartmentId = dept.Id;
                        });
                    });

                    employees.GroupBy(x => x.Groups).ForEach(e => {
                        var dept = _groupFactory.Find(x => x.Name.Equals(e.Key, StringComparison.InvariantCultureIgnoreCase) && !x.IsDeleted, false);
                        if (dept == null) {
                            dept = new Group
                            {
                                Name = e.Key,
                                CreatedById = _workContext.User.UserId,
                                CreatedDate = currDate,
                                OrganizationId = _workContext.User.OrganizationId
                            };
                            _groupFactory.Add(dept);
                        }

                        e.ForEach(usr => {
                            usr.GroupId = dept.Id;
                        });
                    });

                    employees.GroupBy(x => x.Regions).ForEach(e => {
                        var regn = _regionFactory.Find(x => x.Name.Equals(e.Key, StringComparison.InvariantCultureIgnoreCase) && !x.IsDeleted, false);
                        if (regn == null) {
                            regn = new Region
                            {
                                Name = e.Key,
                                CreatedById = _workContext.User.UserId,
                                CreatedDate = currDate,
                                OrganizationId = _workContext.User.OrganizationId
                            };
                            _regionFactory.Add(regn);
                        }

                        e.ForEach(usr => {
                            usr.RegionId = regn.Id;
                        });
                    });

                    employees.GroupBy(x => x.Grade).ForEach(e => {
                        var grd = _gradeFactory.Find(x => x.Name.Equals(e.Key, StringComparison.InvariantCultureIgnoreCase) && !x.IsDeleted, false);
                        if (grd == null) {
                            grd = new Grade
                            {
                                Name = e.Key,
                                CreatedById = _workContext.User.UserId,
                                CreatedDate = currDate,
                                OrganizationId = _workContext.User.OrganizationId
                            };
                            _gradeFactory.Add(grd);
                        }

                        e.ForEach(usr => {
                            usr.GradeId = grd.Id;
                        });
                    });

                    employees.ForEach(e => {
                        if (!string.IsNullOrEmpty(e.DateOfBirth)) {
                            if (DateTime.TryParseExact(e.DateOfBirth,"dd/MM/yyyy", CultureInfo.InvariantCulture,DateTimeStyles.None, out DateTime dt))
                                e.DateOfBirthD = dt;
                        }
                        if (!string.IsNullOrEmpty(e.DateOfEmployment)) {
                            if (DateTime.TryParseExact(e.DateOfEmployment, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
                                e.DateOfEmploymentD = dt;
                        }
                    });

                    try {

                        foreach (var usr in employees.OrderBy(x => x.DateOfEmploymentD)) {
                            var gender = usr.Sex == "MALE" ? Gender.Male : Gender.Female;
                            var usertype = UserType.ActiveDirectory;
                           
                            ApplicationUser user = new ApplicationUser
                            {
                                BranchId = usr.BranchId,
                                DepartmentId = usr.DepartmentId,
                                GroupId = usr.GroupId,
                                RegionId = usr.RegionId,
                                GradeId = usr.GradeId,
                                Email = usr.EmailAddress,
                                EmailConfirmed = true,
                                FirstName = usr.FirstName,
                                Functions = usr.Function,
                                Gender = gender,
                                LastName = usr.SurName,
                                UserName = usr.UserName,
                                IsActive = true,
                                LineManagerLastName = usr.LineManagerLastName,
                                LineManagerFirstName = usr.LineManagerFirstName,
                                DateOfBirth = usr.DateOfBirthD,
                                DateOfEmployment = usr.DateOfEmploymentD,
                                StaffId = usr.StaffId,
                                OrganizationId = _workContext.User.OrganizationId,
                                UserType = usertype,
                            };

                            var name = _unitOfWork.Repository<ApplicationUser>().SqlQuery("SELECT * FROM AspNetUsers WHERE FirstName like '%" + user.LineManagerFirstName + "%' AND LastName like '%" + user.LineManagerLastName + "%'").FirstOrDefault();
                            if (name != null) {
                                user.LineManagerStaffId = name.StaffId;
                            }

                            var appUser = _userAccountService.CreateUser(user);

                            if (appUser.Succeeded) {
                                _userFactory.Add(new User { ApplicationUserId = user.Id });
                                _userAccountService.AddToRole(user.Id, AppConstants.Role.Employee);
                            }
                        }

                        response.HasError = false;
                        response.Message = "All data successfully uploaded";

                    }
                    catch (Exception e) {

                        response.HasError = true;
                        response.Message = "" + e.InnerException.InnerException;
                    }


                }
                else {
                    response.HasError = true;
                    response.Message = "File is not a valid  excel document";
                }
            }
            else {
                response.HasError = true;
                response.Message = "No excel file selected";
            }

            return Json(response);
        }


        public ActionResult UpdateNewEmployees()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.EmployeeRecords))
                return AccessDeniedView();

            return View();
        }
    }
}