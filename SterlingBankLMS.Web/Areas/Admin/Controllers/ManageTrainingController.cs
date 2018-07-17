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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vereyon.Web;

namespace SterlingBankLMS.Web.Areas.Admin.Controllers
{
    public class ManageTrainingController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkContext _workContext;
        private readonly TrainingFactory _trainingFactory;
        private readonly IUserAccountService _userAccountService;
        private readonly IPermissionService _permissionSvc;
        private readonly DepartmentFactory _deptFactory;
        private readonly TrainingVideoFactory _videoFactory;
        private readonly DepartmentBudgetFactory _budgetFactory;
        private readonly TrainingRequestFactory _requestFactory;
        private readonly UserFactory _userFactory;
        private readonly UserTrainingFactory _userTrainingFactory;
        private readonly RegionFactory _regionFactory;
        private readonly GroupFactory _groupFactory;
        private readonly NotificationHubFactory _notificationHubFactory;
        private readonly MailsFactory _mailsFactory;
        private readonly MessageQueueFactory _messageQueueFactory;



        public ManageTrainingController( TrainingFactory trainingFactory, IUserAccountService userAccountService, IWorkContext workContext,
            IPermissionService permission, DepartmentFactory deptFactory, TrainingVideoFactory videoFactory,
            DepartmentBudgetFactory budgetFactory, TrainingRequestFactory requestFactory, 
            UserFactory userFactory, RegionFactory regionFactory, GroupFactory groupFactory,
            NotificationHubFactory notificationHubFactory, MailsFactory mailsFactory,
            IUnitOfWork unitOfWork, MessageQueueFactory messageQueueFactory,UserTrainingFactory userTrainingFactory)
        {
            _trainingFactory = trainingFactory;
            _userAccountService = userAccountService;
            _workContext = workContext;
            _permissionSvc = permission;
            _deptFactory = deptFactory;
            _videoFactory = videoFactory;
            _budgetFactory = budgetFactory;
            _requestFactory = requestFactory;
            _userFactory = userFactory;
            _regionFactory = regionFactory;
            _groupFactory = groupFactory;
            _mailsFactory = mailsFactory;
            _notificationHubFactory = notificationHubFactory;
            _unitOfWork = unitOfWork;
            _userTrainingFactory = userTrainingFactory;
            _messageQueueFactory = messageQueueFactory;
        }

        public ActionResult ListTraining()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
                return AccessDeniedView();

            var output = new ManagePageModel
            {
                New = false
            };
            return View("Index", output);
        }

        public ActionResult NewTraining()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.EmployeeRecords))
                return AccessDeniedView();

            var output = new ManagePageModel
            {
                New = true
            };
            return View("Index", output);
        }

        public ActionResult Calendar()
        {
            return View();
        }


        //[HttpPost]
        //public ActionResult NewTraining( TrainingDto model )
        //{
        //    var data = new ApiResult<string>();

        //    var userId = _workContext.User.Id;
        //    var organisationId = _workContext.User.OrganizationId;
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _trainingFactory.AddTraining(model, userId, organisationId);
        //            data.HasError = false;
        //            data.Message = "New Training successfully created!";
        //        }
        //        catch (Exception e)
        //        {
        //            data.HasError = true;
        //            data.Message = "" + e.Message;
        //        }
        //    }
        //    else
        //    {
        //        data.HasError = true;
        //        data.Message = "Sorry, you have some invalid data";
        //    }
        //    return Json(data);
        //}

        //[HttpPost]
        //public ActionResult UpdateTraining( TrainingDto dto )
        //{
        //    if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
        //        return AccessDeniedView();

        //    var userId = _workContext.User.Id;
        //    var organisationId = _workContext.User.OrganizationId;
        //    if (ModelState.IsValid)
        //    {
        //        _trainingFactory.UpdateTraining(dto);
        //        TempData["Success"] = string.Concat("Changes for", dto.Name.ToUpper(), " was successfully updated!");
        //        return RedirectToAction("ListTraining", "ManageTraining");

        //    }
        //    TempData["Error"] = "Error occured while updating changes for " + dto.Name.ToUpper();
        //    return RedirectToAction("ListTraining", "ManageTraining");

        //}

        [Route("Admin/ManageTraining/AssignTraining/{id?}")]
        public ActionResult AssignTraining(int? id, string q)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
                return AccessDeniedView();


            var output = new ManagePageModel
            {
                New = true,
                Id = id
            };
            return View("AssignTraining", output);
        }


        //public ActionResult AssignTraining( string q )
        //{
        //    if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
        //        return AccessDeniedView();

        //    var organisationId = _workContext.User.OrganizationId;

        //    var trainingName = _trainingFactory.PopulateTrainingName(organisationId);
        //    if (!string.IsNullOrEmpty(q))
        //    {
        //        trainingName = trainingName.Where(x => x.TrainingName.ToUpper().Contains(q.ToUpper())).ToList();
        //    }

        //    ViewBag.TrainingName = trainingName;
        //    var output = new ManagePageModel
        //    {
        //        New = true
        //    };
        //    return View(output);
        //}


       

     

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetLearningGroupList( string learner )
        {
            if (string.IsNullOrEmpty(learner))
                throw new ArgumentNullException("learner");

            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
                return AccessDeniedView();

            var organisationId = _workContext.User.OrganizationId;

            if (learner == "group")
            {
                var groupList = _trainingFactory.ExecuteProcedure<GroupModel>("SP_GetGroupNames", organisationId).ToList();
                var result = (from s in groupList
                              select new
                              {
                                  id = s.Id,
                                  name = s.Name

                              }).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);

            }

            else if (learner == "gender")
            {
                var genderList = new List<GenderModel>()
                {
                    new GenderModel
                    {
                        Id=1,
                        Type="Male",

                    },
                    new GenderModel
                    {
                        Id=2,
                        Type="Female"
                    }
                };
                var result = (from s in genderList
                              select new
                              {
                                  id = s.Id,
                                  name = s.Type

                              }).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            else if (learner == "grade")
            {
                var gradeList = _trainingFactory.ExecuteProcedure<GradeModel>("SP_GetGradeNames", organisationId).ToList();
                var result = (from s in gradeList
                              select new
                              {
                                  id = s.Id,
                                  name = s.Name

                              }).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);

            }

            else if (learner == "role")
            {

            }
            else if (learner == "individuals")
            {
                var individuals = _trainingFactory.ExecuteProcedure<GetNameModel>("SP_GetNames", organisationId);
                var result = (from s in individuals
                              select new
                              {
                                  id = s.Id,
                                  name = s.FirstName + ", " + s.LastName

                              }).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var region = _trainingFactory.ExecuteProcedure<RegionModel>("SP_GetRegionNames", organisationId);
                var result = (from s in region
                              select new
                              {
                                  id = s.Id,
                                  name = s.Name

                              }).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        public ActionResult TrainingRequest()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
                return AccessDeniedView();

            return View();
        }

        public ActionResult TrainingBudget()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
                return AccessDeniedView();
            var region = _regionFactory.ExecuteProcedure<RegionModel>("SP_GetRegionNames", _workContext.User.OrganizationId).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            var group = _groupFactory.ExecuteProcedure<GroupModel>("SP_GetGroupNames", _workContext.User.OrganizationId).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            ViewBag.GroupId = group;
            ViewBag.RegionId = region;



            return View();
        }

        public ActionResult NewTrainingBudget()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
                return AccessDeniedView();

            var region = _regionFactory.ExecuteProcedure<RegionModel>("SP_GetRegionNames", _workContext.User.OrganizationId).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            var group = _groupFactory.ExecuteProcedure<GroupModel>("SP_GetGroupNames", _workContext.User.OrganizationId).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            ViewBag.GroupId = group;
            ViewBag.RegionId = region;
            return View();
        }

        [HttpPost]
        public ActionResult NewTrainingBudget( DepartmentBudgetDto dto )
        {
            var data = new ApiResult<string>();
            var organisationId = _workContext.User.OrganizationId;
            var userId = _workContext.User.Id;
            dto.Year = DateTime.Now.Year;


            if (ModelState.IsValid)
            {
                //Check if training budget already exist
                var budget = _budgetFactory.GetIncluding(x => x.Year == dto.Year && x.GroupId == dto.GroupId && x.RegionId == dto.RegionId, false, x => x.Group);
                if (budget == null)
                {
                    try
                    {
                        _trainingFactory.AddTrainingBudget(dto, userId, organisationId);
                        data.HasError = false;
                        data.Message = "Budget was successfully added";
                    }
                    catch (Exception e)
                    {
                        data.HasError = true;
                        data.Message = "" + e.Message;

                    }

                }
                else
                {
                    data.HasError = true;
                    data.Message = "Budget for this Group and Region already exists";
                }

            }
            else
            {
                data.HasError = true;
                data.Message = "Data is invalid";
            }
            return Json(data);
        }

        [HttpPost]
        public ActionResult UpdateTrainingBudget( DepartmentBudgetDto dto )
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
                return AccessDeniedView();
            var region = _regionFactory.ExecuteProcedure<RegionModel>("SP_GetRegionNames", _workContext.User.OrganizationId).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            var group = _groupFactory.ExecuteProcedure<GroupModel>("SP_GetGroupNames", _workContext.User.OrganizationId).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            var userId = _workContext.User.Id;
            var organisationId = _workContext.User.OrganizationId;
            if (ModelState.IsValid)
            {
                var amtSpent = _budgetFactory.GetIncluding(x => x.Id == dto.Id, false).AmountSpent;
                if (amtSpent >= dto.Budget)
                {
                    _trainingFactory.UpdateBudget(dto);
                    FlashMessage.Confirmation(string.Concat("Changes was successfully updated!"));
                    return RedirectToAction("TrainingBudget", "ManageTraining");
                }
                else
                {

                    ViewBag.GroupId = group;
                    ViewBag.RegionId = region;

                    FlashMessage.Danger("Error occured while updating changes. Amount spent cannot be greater than the Budget.");
                    return RedirectToAction("TrainingBudget", "ManageTraining");
                }
            }

            ViewBag.GroupId = group;
            ViewBag.RegionId = region;

            FlashMessage.Danger("Error occured while updating changes");
            return RedirectToAction("TrainingBudget", "ManageTraining");
        }

        public ActionResult NewExpenditure()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
                return AccessDeniedView();
            return View();
        }

        public ActionResult ListTrainingVideo()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
                return AccessDeniedView();


            return View();
        }

        public ActionResult NewTrainingVideo()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
                return AccessDeniedView();


            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            string fileName = "";
            string Message = "";
            string fname = "";
            bool hasError = false;
            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                if (file.ContentLength > 0)
                {

                    var extension = Path.GetExtension(file.FileName);
                    if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".gif" && extension != ".JPG" && extension != ".jpeg" && extension != ".PNG" && extension != ".GIF" && extension != ".pdf" && extension != ".PDF" && extension != ".mp3" && extension != ".MP3" && extension != ".zip" && extension != ".ZIP")
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

                        fname = Server.MapPath(AppConstants.VideoUploadPath) + fname;
                        file.SaveAs(fname);

                        Message += "Video was successfully uploaded. Click Save button to save to database";
                        hasError = false;

                    }
                    else
                    {
                        Message += "Sorry, file can only be Video";
                        hasError = true;

                    }

                }

            }
            var a = new ApiResult<string>();
            a.Message = Message;
            a.HasError = hasError;
            a.Result = AppConstants.VideoUploadPath + fileName;

            return Json(a);

        }

        [HttpPost]
        public ActionResult SaveUploadVideo( TrainingVideoDto dto )
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.EmployeeRecords))
                return AccessDeniedView();
            var response = new ApiResult<string>();
            var userId = _workContext.User.Id;
            var organisationId = _workContext.User.OrganizationId;
            if (ModelState.IsValid)
            {
                try
                {
                    var video = new TrainingVideo();
                    video.CreatedDate = DateTime.Now;
                    video.IsDeleted = false;
                    video.LastModifiedById = userId;
                    video.ModifiedDate = DateTime.Now;
                    video.OrganizationId = organisationId;
                    video.TrainingVideoName = dto.TrainingVideoName;
                    video.TrainingVideoUrl = dto.TrainingVideoUrl;
                    video.CreatedById = userId;

                    _videoFactory.Add(video);

                    //Send notifications to all staff
                    
                        //var msg = "<p><strong>Dear User</strong></p>" +
                        //    "<p>A new Training Video has been uploaded to the Learning Management System. You can access it by following this url :</p>" +
                        //    "<ul>" +
                        //    "<li><a href=" + Request.Url.Authority + dto.TrainingVideoUrl + " target='_blank'>" + dto.TrainingVideoName + "</a></li>" +
                        //    "</ul>" +
                        //    "<p>Please do well to watch it.</p>" +
                        //    "<p>Best Regards</p>";

                        var queue = new MessageQueue();
                        queue.ActionId = video.Id;
                        queue.CreatedById = _workContext.User.Id;
                        queue.CreatedDate = DateTime.Now;
                        queue.NotificationType = NotificationType.NewTrainingVideo;
                        queue.OrganizationId = _workContext.User.OrganizationId;

                        _messageQueueFactory.Add(queue);

                    response.HasError = false;
                    response.Message = "Training Videos has been successfully saved";

                }
                catch (Exception e)
                {
                    response.HasError = true;
                    response.Message = "" + e.Message;
                }

            }
            else
            {
                response.HasError = true;
                response.Message = "Sorry, some data are invalid. Recheck them and try again";
            }
            return Json(response);
        }

        public ActionResult _GetDeptDetails( string deptId )
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
                return AccessDeniedView();
            var id = Convert.ToInt32(deptId);
            var details = _budgetFactory.GetDeptBudget(id);
            if (details != null)
            {
                ViewBag.Budget = details.Budget == 0 ? 0m : details.Budget;
                ViewBag.AmountSpent = details.AmountSpent;
                ViewBag.DeptName = details.Group.Name;
                ViewBag.AmountRemaining = details.Budget - details.AmountSpent;
                return PartialView("_GetDeptDetails");
            }

            ViewBag.Budget = 0m;
            ViewBag.AmountSpent = 0m;
            ViewBag.DeptName = "No Name!";
            ViewBag.AmountRemaining = 0m;
            return PartialView("_GetDeptDetails");
        }

        public ActionResult _GetTrainingDetails( string trainingId )
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
                return AccessDeniedView();
            var id = Convert.ToInt32(trainingId);
            var training = _trainingFactory.GetTrainingDetails(id);
            return PartialView("_GetTrainingDetails", training);
        }


        public ActionResult _GetTrainingRequestDetails( int Id)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
                return AccessDeniedView();
            //var empid = Convert.ToInt32(requestId);
            //var employeeDetailsDto = _userFactory.GetEmployeeDetails(empid, _workContext.User.OrganizationId);

            //if (employeeDetailsDto == null)
            //{
            //    return HttpNotFound();
            //}
            //var employeeVm = employeeDetailsDto.MapTo<EmployeeDetailsDto, EmployeeDetailsModel>();
            //ViewBag.TrainingName = trainingName;
            //ViewBag.AmountPerStaff = amtPerStaff;
            //ViewBag.Status = status;
            //ViewBag.RequestId = id;
            //ViewBag.Line = line;
            //ViewBag.Reason = reason;
            var trainingRequest = _requestFactory.GetTrainingRequest(Id);
            return PartialView("_GetTrainingRequestDetails", trainingRequest);
        }
        public ActionResult TrainingBulkUpload()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.ManageTraining))
                return AccessDeniedView();

            return View();
        }
        public JsonResult UploadBulkTraining()
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
                if (fileExtension.EndsWith(".xls") || fileExtension.EndsWith(".xlsx") || fileExtension.EndsWith(".XLS") || fileExtension.EndsWith(".XLSX"))
                {
                    string fileLocation = Server.MapPath("~/ExcelFiles/" + fullFileName);
                    FileInfo fileinfo = new FileInfo(fileLocation);
                    upload.SaveAs(fileLocation);
                    FileStream stream = new FileStream(fileLocation, FileMode.Open);

                    Application application = new Application();
                    Workbook workBook = application.Workbooks.Open(Server.MapPath("~/ExcelFiles/" + fullFileName));
                    Worksheet workSheet = (Worksheet)workBook.ActiveSheet;
                    Range range = workSheet.UsedRange;
                    int Usercount = 0;
                    int trainingCount = 0;

                    for (int row = 2; row <= range.Rows.Count; row++)
                    {
                        var training = new Training();

                        string trianingName = ((Range)range.Cells[row, 1]).Text;
                        if (trianingName != "")
                        {
                            string vendor = ((Range)range.Cells[row, 2]).Text;
                            TrainingType type = (TrainingType)Enum.Parse(typeof(TrainingType), ((Range)range.Cells[row, 3]).Text);
                            TrainingCategory category = (TrainingCategory)Enum.Parse(typeof(TrainingCategory), ((Range)range.Cells[row, 4]).Text);
                            string location = ((Range)range.Cells[row, 5]).Text;
                            string venue = ((Range)range.Cells[row, 6]).Text;
                            var sd = DateTime.ParseExact(((Range)range.Cells[row, 7]).Text, "d-MMM-yy", null);
                            DateTime startDate = sd;
                            DateTime endDate = DateTime.ParseExact(((Range)range.Cells[row, 8]).Text, "d-MMM-yy", null);
                            int hours = Convert.ToInt32(((Range)range.Cells[row, 10]).Text) ?? 0;
                            string staffId = ((Range)range.Cells[row, 11]).Text;
                            decimal courseFees, otherfees, totalFees;
                            Decimal.TryParse(Convert.ToString(((Range)range.Cells[row, 22]).Value2), out courseFees);
                            Decimal.TryParse(Convert.ToString(((Range)range.Cells[row, 21]).Value2), out otherfees);
                            Decimal.TryParse(Convert.ToString(((Range)range.Cells[row, 22]).Value2), out totalFees);

                            var tr = _trainingFactory.GetAllIncluding(x => x.Name == trianingName, false).ToList();
                            if (!tr.Any())
                            {
                                training.AmountPerStaff = courseFees;
                                training.BudgetExpended = totalFees;
                                training.CreatedById = _workContext.User.Id;
                                training.CreatedDate = DateTime.Now;
                                training.DurationInMinutes = Convert.ToDouble(hours * 60);
                                training.EndPeriod = endDate;
                                training.IsActive = false;
                                training.Location = location;
                                training.Name = trianingName;
                                training.OrganizationId = _workContext.User.OrganizationId;
                                training.StartPeriod = startDate;
                                training.TrainingCategory = category;
                                training.TrainingType = type;
                                training.Vendor = vendor;
                                training.Venue = venue;
                                training.Year = startDate.Year;

                                _trainingFactory.Add(training);
                                trainingCount++;
                            }



                            if (staffId != null)
                            {
                                var user = _unitOfWork.Repository<ApplicationUser>().SqlQuery("SELECT * FROM AspNetUsers WHERE StaffId ='" + staffId + "'").FirstOrDefault();
                                if (user != null)
                                {
                                    var trainingId = training.Id == 0 ? tr[0].Id : training.Id;
                                    var userId = user.Id == 0 ? 0 : user.Id;
                                    var usertr = _userTrainingFactory.All(a => a.TrainingId == trainingId && a.UserId == userId, false).ToList();
                                    if (!usertr.Any())
                                    {
                                        var userTraining = new UserTraining();
                                        userTraining.CreatedDate = DateTime.Now;
                                        userTraining.OrganizationId = _workContext.User.OrganizationId;
                                        userTraining.CreatedById = _workContext.User.Id;
                                        userTraining.TrainingId = trainingId;
                                        userTraining.UserId = userId;

                                        _userTrainingFactory.Add(userTraining);
                                        Usercount++;
                                    }

                                }

                            }

                        }

                    }

                    stream.Close();
                    stream.Dispose();
                    fileinfo.Delete();

                    response.HasError = false;
                    response.Message = Usercount + " data successfully uploaded. " + trainingCount + " Trainings successfully added";

                }
                else
                {
                    response.HasError = true;
                    response.Message = "Only a valid Excel file should be used";
                }
            }
            else
            {
                response.HasError = true;
                response.Message = "No Excel file selected";
            }

            return Json(response);
        }

        public FileResult DownloadSampleExcel( string fileName )
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Content" + "\\" + "excel" + "\\" + fileName;
            string name = Path.GetFileName(filePath);
            return File(filePath, "application/force-download", name);
        }
    }
}