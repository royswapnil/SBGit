using Nop.Core.Caching;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.UnitofWork;
using SterlingBankLMS.Web.Controllers;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.Web.Helpers;

namespace SterlingBankLMS.Web.Areas.Admin.Controllers
{
    public class ReportManagementController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkContext _workContext;
        private readonly IUserAccountService _userAccountService;
        private readonly IUserRoleService _userRole;
        private readonly IPermissionService _permissionSvc;
        private readonly UserFactory _userFactory;
        private readonly ReportFactory _reportFactory;
        private readonly ICacheManager _cacheManager;

        public ReportManagementController(IUserAccountService userAccountService, IWorkContext workContext, IPermissionService permission, UserFactory userFactory, IUserRoleService userRole, IUnitOfWork unitOfWork, ReportFactory reportFactory, ICacheManager cacheManager)
        {
            _userAccountService = userAccountService;
            _workContext = workContext;
            _permissionSvc = permission;
            _userFactory = userFactory;
            _userRole = userRole;
            _unitOfWork = unitOfWork;
            _reportFactory = reportFactory;
            _cacheManager = cacheManager;
        }
        public ActionResult NewReport(int? Id, string ReportTitle)
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.Reporting))
                return AccessDeniedView();
            if (Id != 0 && Id != null)
            {
                var report = new ReportDto();
                var reportdata = _reportFactory.GetIncluding(x => x.Id == Id.Value && !x.IsDeleted, false, x => x.Sorts.OrderBy(c => c.Sort).Where(p => !p.IsDeleted), y => y.ReportSchedules.Where(q => !q.IsDeleted), z => z.ReportUserList.Where(r => !r.IsDeleted));
                var model = reportdata.MapTo<Report, ReportDto>();
                ViewBag.CurrentCulture = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
                return View(model);
            }
            if (ReportTitle != null)
                ViewBag.ReportName = ReportTitle;

            return View();
        }

        public ActionResult DownloadCSV(int id)
        {
            var model = _cacheManager.Get<IEnumerable<GenerateReportDataTableDto>>("report.ddl");
            if (model == null)
                return RedirectToAction("GenerateReport");
            return File(new System.Text.UTF8Encoding().GetBytes(GenStringForFiles(model, id)), "text/csv", "ReportCsv.csv");
        }

        public ActionResult DownloadTxt(int id)
        {
            var model = _cacheManager.Get<IEnumerable<GenerateReportDataTableDto>>("report.ddl");
            if (model == null)
                return RedirectToAction("GenerateReport");
            return File(new System.Text.UTF8Encoding().GetBytes(GenStringForFiles(model,id)), "text/txt", "ReportTxt.txt");
        }

        public void DownloadXls(int id)
        {
            var model = _cacheManager.Get<IEnumerable<GenerateReportDataTableDto>>("report.ddl");
            //if (model == null)
            //    return RedirectToAction("GenerateReport");

            WebGrid grid = new WebGrid(source: model, canPage: false, canSort: false);
            string gridData = string.Empty;
            if (id == 1)
            {
                gridData = grid.GetHtml(tableStyle: "table table-striped table-hover table-condensed",
                columns: grid.Columns(
                        grid.Column("StaffId", header: "Staff Id"),
                        grid.Column("StaffName", header: "Staff Name"),
                        grid.Column("Region", header: "Region"),
                        grid.Column("Group", header: "Group"),
                        grid.Column("Grade", header: "Grade"),
                        grid.Column("Courses", header: "Courses"),
                        grid.Column("NoOfCourses", header: "No Of Courses"),
                        grid.Column("AverageScore", header: "Average Score")
                        )
                    ).ToString();
            }
            if (id == 2)
            {
                gridData = grid.GetHtml(tableStyle: "table table-striped table-hover table-condensed",
                columns: grid.Columns(
                        grid.Column("Name", header: "Name"),
                        grid.Column("DueDate", header: "Due Date"),
                        grid.Column("Duration", header: "Duration"),
                        grid.Column("AverageScore", header: "Average Score"),
                        grid.Column("NoOfParticipants", header: "No of Participants"),
                        grid.Column("Courseevaluationscore", header: "Course evaluation score")
                        )
                    ).ToString();
            }
            if (id == 3)
            {
                gridData = grid.GetHtml(tableStyle: "table table-striped table-hover table-condensed",
                columns: grid.Columns(
                        grid.Column("StaffID", header: "Staff ID"),
                        grid.Column("StaffName", header: "Staff Name"),
                        grid.Column("Region", header: "Region"),
                        grid.Column("Group", header: "Group"),
                        grid.Column("Grade", header: "Grade"),
                        grid.Column("Exams", header: "Exams"),
                        grid.Column("Scorepercourse", header: "Score per course")
                        )
                    ).ToString();
            }

            if (id == 4)
            {
                gridData = grid.GetHtml(tableStyle: "table table-striped table-hover table-condensed",
                columns: grid.Columns(
                        grid.Column("StaffID", header: "Staff ID"),
                        grid.Column("StaffName", header: "Staff Name"),
                        grid.Column("Region", header: "Region"),
                        grid.Column("Group", header: "Group"),
                        grid.Column("Grade", header: "Grade"),
                        grid.Column("Courses", header: "Courses"),
                        grid.Column("StatusOfCourses", header: "Status of Courses"),
                        grid.Column("NoOfAttempts", header: "No of Attempts"),
                        grid.Column("DateAccessed", header: "Date Accessed"),
                        grid.Column("TimeAccessed", header: "Time Accessed"),
                        grid.Column("Duration", header: "Duration"),
                        grid.Column("Location", header: "Location")
                        )
                    ).ToString();
            }

            if (id == 5)
            {
                gridData = grid.GetHtml(tableStyle: "table table-striped table-hover table-condensed",
                columns: grid.Columns(
                        grid.Column("StaffID", header: "Staff ID"),
                        grid.Column("StaffName", header: "Staff Name"),
                        grid.Column("Region", header: "Region"),
                        grid.Column("Group", header: "Group"),
                        grid.Column("Grade", header: "Grade"),
                        grid.Column("Courses", header: "Courses"),
                        grid.Column("NoOfCourses", header: "No of Courses"),
                        grid.Column("NoOfAttempts", header: "No of Attempts"),
                        grid.Column("AverageScore", header: "Average Score"),
                        grid.Column("Location", header: "Location")
                        )
                    ).ToString();
            }

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=ReportXls.xls");
            Response.ContentType = "application/ms-excel";
            Response.Write(gridData);
            Response.End();
        }

        public string GenStringForFiles(IEnumerable<GenerateReportDataTableDto> model, int id)
        {
            var sb = new StringBuilder();
            if (id == 1)
            {
                sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7}{8}", "Staff Id", "Staff Name", "Region ", "Group", "Grade", "Courses", "No Of Courses",
                "Average Course", Environment.NewLine);
                foreach (var item in model)
                {
                    sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7}{8}", item.StaffID, item.StaffName, item.Region, item.Group, item.Grade,
                        item.Courses, item.NoOfCourses, item.AverageScore, Environment.NewLine);
                }
            }
            if (id == 2)
            {
                sb.AppendFormat("{0},{1},{2},{3},{4},{5}{6}", "Name", "Due Date", "Duration", "Average Score", "No of Participants",
                    "Course Evaluation Score", Environment.NewLine);
                foreach (var item in model)
                {
                    sb.AppendFormat("{0},{1},{2},{3},{4},{5}{6}", item.Name, item.DueDate, item.Duration, item.AverageScore, item.NoOfParticipants,
                        item.Courseevaluationscore, Environment.NewLine);
                }
            }
            if (id == 3)
            {
                sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6}{7}", "Staff Id", "Staff Name", "Region ", "Group", "Grade", "Exams", "Score per course", Environment.NewLine);
                foreach (var item in model)
                {
                    sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6}{7}", item.StaffID, item.StaffName, item.Region, item.Group, item.Grade,
                        item.Exams, item.Scorepercourse, Environment.NewLine);
                }
            }
            if (id == 4)
            {
                sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}{12}", "Staff Id", "Staff Name", "Region ", "Group", "Grade", "Courses",
                "Status of Courses", "No of Attempts","Date Accessed","Time Accessed","Duration","Location", Environment.NewLine);
                foreach (var item in model)
                {
                    sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}{12}", item.StaffID, item.StaffName, item.Region, item.Group, item.Grade,
                        item.Courses, item.StatusOfCourses, item.NoOfAttempts, item.DateAccessed, item.TimeAccessed,
                        item.Duration, item.Location, Environment.NewLine);
                }
            }
            if (id == 5)
            {
                sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8}{9}", "Staff Id", "Staff Name", "Region ", "Group", "Grade", "Courses", "No Of Courses",
                "Average Score","Location", Environment.NewLine);
                foreach (var item in model)
                {
                    sb.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8}{9}", item.StaffID, item.StaffName, item.Region, item.Group, item.Grade,
                        item.Courses, item.NoOfCourses, item.AverageScore, Environment.NewLine);
                }
            }
            return sb.ToString();
        }

        [HttpPost]
        public ActionResult SaveNewReport(ReportDto dto)
        {
            var data = new ApiResult<string>();
            if (ModelState.IsValid)
            {
                try
                {
                    var report = new Report();
                    var rep = dto.MapTo<ReportDto, Report>();

                    var userId = _workContext.User.Id;
                    var organisationId = _workContext.User.OrganizationId;
                    if (dto.Id > 0)
                    {
                        _reportFactory.UpdateReport(dto, userId, organisationId);

                        if (!String.IsNullOrEmpty(dto.ReportName) && String.IsNullOrEmpty(ViewBag.ReportName))
                        {
                            ViewBag.ReportName = dto.ReportName;
                        }
                    }
                    else
                    {
                        if (rep.Sorts != null)
                        {
                            rep.Sorts.Select(s =>
                            {
                                s.CreatedDate = DateTime.Now; s.CreatedById = _workContext.User.Id;
                                s.LastModifiedById = _workContext.User.Id; s.OrganizationId = _workContext.User.OrganizationId; s.IsDeleted = false; return s;
                            }).ToList();
                        }

                        if (rep.ReportSchedules != null)
                        {
                            rep.ReportSchedules.Select(c =>
                            {
                                c.CreatedDate = DateTime.Now; c.CreatedById = _workContext.User.Id;
                                c.LastModifiedById = _workContext.User.Id; c.OrganizationId = _workContext.User.OrganizationId;
                                c.IsDeleted = false; return c;
                            }).ToList();
                        }

                        if (rep.ReportUserList != null)
                        {
                            rep.ReportUserList.Select(c =>
                            {
                                c.CreatedDate = DateTime.Now; c.CreatedById = _workContext.User.Id;
                                c.LastModifiedById = _workContext.User.Id; c.OrganizationId = _workContext.User.OrganizationId; c.IsDeleted = false; return c;
                            }).ToList();
                        }

                        rep.IsDeleted = false; rep.CreatedDate = DateTime.Now;
                        rep.CreatedById = _workContext.User.Id;
                        rep.LastModifiedById = _workContext.User.Id;
                        rep.OrganizationId = _workContext.User.OrganizationId;
                        _reportFactory.Add(rep);
                    }


                    data.HasError = false;
                    data.Message = "Report fields has been successfully saved";
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
                data.Message = "Error occured while saving the data";
            }
            return Json(data);
        }

        public ActionResult GenerateReport()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.Reporting))
                return AccessDeniedView();

            return View();
        }

        public ActionResult AuditTrail()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.Reporting))
                return AccessDeniedView();
            _cacheManager.RemoveByPatternwithoutregex(_reportFactory.AuditTrackParams);
            return View();
        }

        public ActionResult ApproveComments()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.Reporting))
                return AccessDeniedView();
            _cacheManager.RemoveByPatternwithoutregex(_reportFactory.AuditTrackParams);
            return View();
        }

        public ActionResult AllReports()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.Reporting))
                return AccessDeniedView();
            return View();
        }

        public ActionResult UserCourseProgress()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.Reporting))
                return AccessDeniedView();
            return View();
        }
        public ActionResult UserExamProgress()
        {
            if (!_permissionSvc.TryCheckAccess(PermissionProvider.Reporting))
                return AccessDeniedView();
            return View();
        }
    }
}