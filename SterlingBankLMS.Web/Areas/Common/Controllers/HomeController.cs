using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Models;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Enums;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vereyon.Web;

namespace SterlingBankLMS.Web.Areas.Common.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IWorkContext _workContex;
        private readonly IPermissionService _permissionSvc;
        private readonly IUserAccountService _userAccountService;

        private readonly CourseFactory _courseFactory;
        private readonly UserFactory _userFactory;
        private readonly AdvertFactory _adFactory;
        private readonly UserCourseFactory _userCourseFactory;
        public readonly TrainingFactory _trainingFactory;
        public HomeController(
            CourseFactory courseFactory, IUserAccountService userAccountService,
            UserFactory userFactory, AdvertFactory adFactory,
            IWorkContext workContex, IPermissionService permissionSvc, UserCourseFactory userCourseFactory, TrainingFactory trainingFactory)
        {

            _courseFactory = courseFactory;
            _workContex = workContex;
            _permissionSvc = permissionSvc;

            _courseFactory = courseFactory;
            _userAccountService = userAccountService;
            _userFactory = userFactory;
            _adFactory = adFactory;
            _userCourseFactory = userCourseFactory;
            _trainingFactory = trainingFactory;
        }

        [ChildActionOnly]
        public ActionResult EmployeeSideBar()
        {

            //// TODO: CACHE THIS
            var controller = ControllerContext.ParentActionViewContext.RouteData.Values["Controller"] as string;
            var action = ControllerContext.ParentActionViewContext.RouteData.Values["Action"] as string;

            var adLocation = AdvertPageLocation.AdLocations.Find(x => x.ActionName == action && x.ControllerName == controller);
            var advert = new AdvertDto();
            if (adLocation != null)
            {
                advert = _adFactory.GetCurrentSideLocationBanner((int)adLocation.Location);
            }

            var certificates = _userCourseFactory.GetPagedUserCertificate(_workContex.User.Id, 3, 1, out int totalRecords);
            var closestTrainings = _trainingFactory.GetUpcomingTrainings(_workContex.User.Id, 3);
            var datenow = AppHelper.GetCurrentDate();

            var list = new List<UpcomingTrainingModel>();

            if (closestTrainings.Count > 0)
            {
                var firstDay = closestTrainings.First().StartPeriod;
                var startDay = firstDay.Value.Date > datenow.Date ? firstDay.Value : datenow;

                var sunday = Convert.ToDateTime(startDay.AddDays(-(int)startDay.DayOfWeek).Date);

                while (list.Count < 3)
                {
                    foreach (var training in closestTrainings)
                    {
                        if (training.PeriodFormat != null)
                        {
                            var period = JsonConvert.DeserializeObject<List<TrainingPeriodDto>>(training.PeriodFormat);
                            foreach (var day in period)
                            {
                                var startdate = sunday.AddDays((int)day.Day).AddHours(day.StartSpan.Value.Hours).AddMinutes(day.StartSpan.Value.Minutes);
                                var endtime = sunday.AddDays((int)day.Day).AddHours(day.EndSpan.Value.Hours).AddMinutes(day.EndSpan.Value.Minutes);


                            if (startdate >= startDay)
                            {
                                dynamic periods = new UpcomingTrainingModel();
                                periods.Id = training.Id;
                                periods.Name = training.Name;
                                periods.StartDate = startdate;
                                list.Add(periods);
                            }

                            }
                        }

                    }
                    sunday = sunday.AddDays(7);
                }
            }


            var trainingList = list.OrderBy(x => x.StartDate).Take(3).ToList();
            var returnObj = new EmployeeSideBarModel
            {
                SideSectionAd = adLocation == null ? null : advert,
                Certificates = certificates,
                UpcomingTraining = trainingList,
                totalCertificates = totalRecords
            };
            return View(returnObj);
        }

        public ActionResult Index()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.AccessLMS))
            {
                return AccessDeniedView();
            }

            var advert = _adFactory.GetCurrentTopLocationBanner((int)AdLocation.Home);
            return View(advert);
        }

        public ActionResult UploadStaffDetails()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadStaffDetails(HttpPostedFileBase upload)
        {

            if (upload.ContentLength == 0)
            {
                FlashMessage.Danger("Please select an excel file");
                return View("UploadStaffDetails");
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
                    FileInfo file = new FileInfo(fileLocation);
                    upload.SaveAs(fileLocation);
                    FileStream stream = new FileStream(fileLocation, FileMode.Open);

                    Application application = new Application();
                    Workbook workBook = application.Workbooks.Open(Server.MapPath("~/ExcelFiles/" + fullFileName));
                    Worksheet workSheet = (Worksheet)workBook.ActiveSheet;
                    Range range = workSheet.UsedRange;

                    for (int row = 2; row <= range.Rows.Count; row++)
                    {
                        ApplicationUser user = new ApplicationUser();

                        //user.Branch = ((Range) range.Cells[row, 11]).Text;
                        user.DateOfBirth = DateTime.ParseExact(((Range)range.Cells[row, 8]).Text, "dd/MM/yyyy", null);
                        user.DateOfEmployment = DateTime.ParseExact(((Range)range.Cells[row, 7]).Text, "dd/MM/yyyy", null);
                        //user.DepartmentName = ((Range) range.Cells[row, 15]).Text;
                        user.UserName = ((Range)range.Cells[row, 5]).Text;
                        user.FirstName = ((Range)range.Cells[row, 3]).Text;
                        //user.Grade = ((Range) range.Cells[row, 10]).Text;
                        //user.GroupName = ((Range) range.Cells[row, 13]).Text;
                        user.LastName = ((Range)range.Cells[row, 4]).Text;
                        //user.LineManagerFirstName = ((Range) range.Cells[row, 16]).Text;
                        //user.LineManagerLastName = ((Range) range.Cells[row, 17]).Text;
                        //user.Region = ((Range) range.Cells[row, 12]).Text;
                        //user.Gender = ((Range) range.Cells[row, 9]).Text;
                        user.StaffId = ((Range)range.Cells[row, 2]).Text;
                        user.Functions = ((Range)range.Cells[row, 14]).Text;
                        user.UserType = Data.Models.Enums.UserType.ActiveDirectory;
                        user.Email = ((Range)range.Cells[row, 6]).Text;

                        var appUser = _userAccountService.CreateUser(user);

                        if (appUser.Succeeded)
                        {
                            _userFactory.Add(new User { ApplicationUserId = user.Id });
                        }

                    }
                    stream.Close();
                    stream.Dispose();
                    file.Delete();
                    FlashMessage.Confirmation("Excel upload successfully done!");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    FlashMessage.Danger("Ooops! File is not of valid excel format.");
                    return View("UploadStaffDetails");
                }
            }
        }



        public JsonResult GetAdvert()
        {
            if (Session["Adverts"] == null)
            {
                var adverts = _adFactory.GetAllIncluding(x => x.IsActive & !x.IsDeleted & x.EndDate > DateTime.Now, false); //Get All Adverts that is active, not deleted and greater than today
                if (adverts == null) //If null, pass default info
                {
                    var defaults_ad = new Advert()
                    {
                        AdvertLink = "http://www.sterlingbankplc.com",
                        //FileUrl = "banner.png",
                        Title = "Default Picture"
                    };
                    Session["Adverts"] = defaults_ad;
                }
                else
                {
                    Session["Adverts"] = adverts;
                }
            }
            var ads = (List<Advert>)Session["Adverts"];
            if (ads.Count > 1) //Advert list is more than 1, do random
            {
                Random rand = new Random();
                int ad_no = rand.Next(0, ads.Count);
                return Json(ads[ad_no], JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(ads, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetNotification(string userId)
        {
            return Json(SbNotificationService.GetNotification(userId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateNotification(string userId)
        {
            return Json(SbNotificationService.UpdateNotification(userId), JsonRequestBehavior.AllowGet);
        }
    }
}