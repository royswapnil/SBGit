using Microsoft.Office.Interop.Excel;
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
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Areas.HumanResources.Controllers
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

        public EmployeeRecordsController( IUserAccountService userAccountService, IWorkContext workContext, IPermissionService permission, UserFactory userFactory, NotificationFactory notificationFactory, IUserRoleService userRole, IUnitOfWork unitOfWork, GroupFactory groupFactory, RegionFactory regionFactory, BranchFactory branchFactory, DepartmentFactory departmentFactory, GradeFactory gradeFactory, LinemanagerFactory linemanagerFactory )
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

        public ActionResult ProfileDetails( [Required] int id )
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.EmployeeRecords))
                return AccessDeniedView();

            var employeeDetailsDto = _userFactory.GetEmployeeDetails(id, _workContext.User.OrganizationId);

            if (employeeDetailsDto == null)
            {
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
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                if (file.ContentLength > 0)
                {

                    var extension = Path.GetExtension(file.FileName);
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".JPG" || extension == ".jpeg" || extension == ".PNG")
                    {
                        // Checking for Internet Explorer    
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                            fileName = fname;
                        }
                        else
                        {
                            fname = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + file.FileName.Replace(" ", "_");
                            fileName = fname;
                        }

                        fname = Server.MapPath(AppConstants.CertUploadPath) + fname;
                        file.SaveAs(fname);


                        response.Message = "Certificate was successfully uploaded. Click Save button to save to database";
                        response.HasError = false;
                        response.Result = AppConstants.CertUploadPath + fileName;


                    }
                    else
                    {
                        response.Message = "Sorry, file can only be image";
                        response.HasError = true;

                    }

                }
                else
                {
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
            if (files.Count != 0)
            {
                HttpPostedFileBase upload = files[0];

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

                    try
                    {
                        for (int row = 167; row <= range.Rows.Count; row++)
                        {
                            ApplicationUser user = new ApplicationUser();

                            string branch = ((Range)range.Cells[row, 11]).Text;
                            string department = ((Range)range.Cells[row, 15]).Text;
                            string grade = ((Range)range.Cells[row, 10]).Text;
                            string group = ((Range)range.Cells[row, 13]).Text;
                            string region = ((Range)range.Cells[row, 12]).Text;
                            string sex = ((Range)range.Cells[row, 9]).Text;

                            Gender gender = (Gender)Enum.Parse(typeof(Gender), GetSex(sex));

                            DateTime dob = DateTime.ParseExact(((Range)range.Cells[row, 8]).Text, "dd/MM/yyyy", null);
                            DateTime doe = DateTime.ParseExact(((Range)range.Cells[row, 7]).Text, "dd/MM/yyyy", null);
                            string username = ((Range)range.Cells[row, 5]).Text;
                            string firstname = ((Range)range.Cells[row, 3]).Text;
                            string lastname = ((Range)range.Cells[row, 4]).Text;
                            string linefirstname = ((Range)range.Cells[row, 16]).Text;
                            string linelastname = ((Range)range.Cells[row, 17]).Text;
                            string staffid = ((Range)range.Cells[row, 2]).Text;
                            string functions = ((Range)range.Cells[row, 14]).Text;
                            var usertype = UserType.ActiveDirectory;
                            string email = ((Range)range.Cells[row, 6]).Text;

                            var emp = _userAccountService.GetAllUsers(x => x.Email == email).ToList();
                            if (!emp.Any())
                            {
                                user.BranchId = _branchFactory.GetIncluding(x => x.Name == branch, false).Id;
                                user.DateOfBirth = dob;
                                user.DateOfEmployment = doe;
                                user.DepartmentId = _departmentFactory.GetIncluding(x => x.Name == department, false).Id;
                                user.Email = email;
                                user.EmailConfirmed = true;
                                user.FirstName = firstname;
                                user.Functions = functions;
                                user.Gender = gender;
                                user.GradeId = _gradeFactory.GetIncluding(x => x.Name == grade, false).Id;
                                user.GroupId = _groupFactory.GetIncluding(x => x.Name == group, false).Id;
                                user.LastName = lastname;
                                user.LineManagerFirstName = linefirstname;
                                user.LineManagerLastName = linelastname;
                                var name = _unitOfWork.Repository<ApplicationUser>().SqlQuery("SELECT * FROM AspNetUsers WHERE FirstName like '%" + user.LineManagerFirstName + "%' AND LastName like '%" + user.LineManagerLastName + "%'").FirstOrDefault();
                                if (name != null)
                                {
                                    user.LineManagerStaffId = name.StaffId;
                                }
                                user.OrganizationId = _workContext.User.OrganizationId;
                                user.RegionId = _regionFactory.GetIncluding(x => x.Name == region, false).Id;
                                user.StaffId = staffid;
                                user.UserName = username;
                                user.UserType = usertype;

                                var appUser = _userAccountService.CreateUser(user);

                                if (appUser.Succeeded)
                                {
                                    _userFactory.Add(new User { ApplicationUserId = user.Id });
                                    _userAccountService.AddToRole(user.Id, "Employee");
                                }

                                //var linemanager = _unitOfWork.Repository<LineManagerDto>().SqlQuery("SELECT Id as LineManagerId, FirstName as LineManagerFirstName, LastName as LineManagerLastName FROM AspNetUsers WHERE StaffId='" + user.LineManagerStaffId + "'").FirstOrDefault();
                                //if (linemanager != null)
                                //{
                                //    var existingLine = _linemanagerFactory.All(x => x.LineManagerId == linemanager.LineManagerId && x.EmployeeId == user.Id, false).ToList();
                                //    if (!existingLine.Any())
                                //    {
                                //        var linemgr = new LineManager();
                                //        linemgr.CreatedById = _workContext.User.Id;
                                //        linemgr.CreatedDate = DateTime.Now;
                                //        linemgr.EmployeeId = user.Id;
                                //        linemgr.IsActive = true;
                                //        linemgr.LineManagerId = linemanager.LineManagerId;
                                //        linemgr.OrganizationId = _workContext.User.OrganizationId;

                                //        _linemanagerFactory.Add(linemgr);
                                //    }
                                //}


                            }

                        }
                        stream.Close();
                        stream.Dispose();
                        fileinfo.Delete();

                        response.HasError = false;
                        response.Message = "All data successfully uploaded";

                    }
                    catch (Exception e)
                    {

                        response.HasError = true;
                        response.Message = "" + e.InnerException.InnerException;
                    }


                }
                else
                {
                    response.HasError = true;
                    response.Message = "File is not a valid  excel document";
                }
            }
            else
            {
                response.HasError = true;
                response.Message = "No excel file selected";
            }

            return Json(response);
        }

        private string GetSex( string x )
        {
            var sex = "";
            if (x == "MALE")
            {
                sex = "Male";
            }
            else
            {
                sex = "Female";
            }
            return sex;
        }

        public ActionResult UpdateNewEmployees()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.EmployeeRecords))
                return AccessDeniedView();

            return View();
        }

        
    }
}