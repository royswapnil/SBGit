using Nop.Core.Caching;
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Z.EntityFramework.Plus;

namespace SterlingBankLMS.Core.Factories
{
    public class ReportFactory : GenericService<Report>
    {
        public readonly string REPORTDDL = "report.ddl";
        public readonly string AuditTrackParams = "audittrackparams-{0}-{1}-{2}-{3}-{4}-{5}";
        private readonly ICacheManager _cacheManager;
        public ReportFactory(IUnitOfWork unitOfWork, ICacheManager cacheManager) : base(unitOfWork)
        {
            _cacheManager = cacheManager;
        }

        public IEnumerable<AuditTrackerDto> GetPagedAuditList(AuditSearchCriteriaDto model, int pageSize, int pageNumber, out int TotalRecords)
        {
            if (model.buttonclick == true)
            {
                _cacheManager.RemoveByPatternwithoutregex(AuditTrackParams);
            }

            string xmlstring = ToXML(model.entityType);

            string key = string.Format(AuditTrackParams, xmlstring,
                     model.startdate, model.enddate, model.type, model.user, pageNumber / (100 / pageSize));
            var auditlist = _cacheManager.Get(key, () => {
                return ExecuteProcedure<AuditTrackerDto>("Sp_lms_cdc", xmlstring,
                     model.startdate, model.enddate, model.type, model.user, pageNumber / (100 / pageSize), pageSize * (100 / pageSize)).ToList();
            });

            TotalRecords = auditlist.Select(c => c.TotalRecords).FirstOrDefault();
            //auditlist = auditlist.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            auditlist = auditlist.Where(c => c.SeqId > (pageNumber) * pageSize && c.SeqId <= (pageNumber + 1) * pageSize).ToList();
            return auditlist;
        }

        public IEnumerable<GenerateReportDataTableDto> GetPagedGenReportList(GenerateReportDto model, int pageSize, int pageNumber, out int TotalRecords)
        {
            _cacheManager.Remove(REPORTDDL);
            var reportList = ExecuteProcedure<GenerateReportDataTableDto>("SP_GenerateReport", model.ReportType,
                     model.StartDate, model.EndDate, model.StaffId == "" ? null : model.StaffId, null, model.GroupId == "" ? null : model.GroupId, model.GradeId == "" ? null : model.GradeId).ToList();

            _cacheManager.Set(REPORTDDL, reportList, 60);

            TotalRecords = reportList.Count();
            reportList = reportList.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            return reportList;
        }

        public string ToXML(int[] entitytype)
        {
            XmlDocument xmlToSave = new XmlDocument();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Content\\EntityXml\\spexeccdc.xml";
            xmlToSave.Load(baseDirectory);

            XElement element = XElement.Parse(xmlToSave.InnerXml);
            var entity = from a in element.Descendants("entitylist")
                         select new TrackDataConfig
                         {
                             entityId = Convert.ToInt32(a.Element("entityId").Value),
                             entityname = a.Element("entityname").Value,
                             entity = a.Descendants("entity")
                                         .Select(x => new entities
                                         {
                                             tablename = x.Element("tablename").Value,
                                             aliasname = x.Element("aliasname").Value,
                                             cdctablename = x.Element("cdctablename").Value,
                                             relatedcolumns = x.Descendants("relatedcolumns")
                                             .Select(y=>new relatedcolumn
                                                     {
                                                         id = Convert.ToInt32(y.Element("id").Value),
                                                         childtable = y.Element("childtable").Value,
                                                         columnname = y.Element("columnname").Value,
                                                         namecolumn = y.Element("namecolumn").Value
                                                     }).ToList(),
                                             showcolumns = x.Descendants("showcolumns")
                                             .Select(y => new showcolumn
                                             {
                                                 columnname = y.Element("columnname").Value
                                             }).ToList()
                                         }).ToList()
                         };

            var entityList = entity.Where(c => entitytype.Contains(c.entityId)).Select(d => d.entity).ToList();
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(entityList.GetType());
            serializer.Serialize(stringwriter, entityList);
            return stringwriter.ToString();
        }

        public Report GetReport(int Id, bool track, string type)
        {
            var report = new Report();
            if (type == "sorts")
            {
                //report = GetIncluding(x => x.Id == Id, track, x => x.Sorts.Where(y => !y.IsDeleted));
                report = GetIncluding(x => x.Id == Id, track, x => x.Sorts);
            }
            else if (type == "schedule")
            {
                //report = GetIncluding(x => x.Id == Id, track, x => x.ReportSchedules.Where(y => !y.IsDeleted));
                report = GetIncluding(x => x.Id == Id, track, x => x.ReportSchedules);
            }
            else if (type == "users")
            {
                //report = GetIncluding(x => x.Id == Id, track, x => x.ReportUserList.Where(y => !y.IsDeleted));
                report = GetIncluding(x => x.Id == Id, track, x => x.ReportUserList);
            }

            return report;
        }

        public IEnumerable<AllReportsDataTable> GetReportFields(string search, int pageIndex, int pageSize, out int TotalRecords)
        {
            var q = UnitOfWork.Repository<ReportFieldSort>().TableNoTracking.Where(c => c.IsDeleted != true &&
            (string.IsNullOrEmpty(search) || (!string.IsNullOrEmpty(search) && c.Report.ReportName.Contains(search))))
            .OrderBy(c => c.Sort).GroupBy(p => p.Report, p => p.ColumnName, (key, g) => new
            {
                key.ReportName,
                key.Id,
                fields = g.ToList()
            }).OrderByDescending(c => c.Id).Skip(pageIndex * pageSize).Take(pageSize).Future();

            var queryCount = (UnitOfWork.Repository<ReportFieldSort>().TableNoTracking.Where(a =>
                              !a.IsDeleted).GroupBy(p => p.Report).DeferredCount().FutureValue());
            TotalRecords = queryCount.Value;
            return q.AsEnumerable()
            .Select(x => new AllReportsDataTable
            {
                ReportId = x.Id,
                ReportName = x.ReportName,
                Fields = string.Join(", ", x.fields)
            })
            .ToList();
        }
        public void UpdateReport(ReportDto dto, int userId, int organizationId)
        {
            this.UnitOfWork.BeginTransaction();
            var datenow = Core.Helper.AppHelper.GetCurrentDate();

            #region Sorts
            var reportsorts = GetReport(dto.Id, true, "sorts");

            if (reportsorts != null && dto.Sorts != null)
            {
                foreach (var item in dto.Sorts)
                {
                    var oldsort = reportsorts.Sorts.Where(x => x.ColumnName == item.ColumnName).FirstOrDefault();

                    if (oldsort == null)
                    {
                        reportsorts.Sorts.Add(new ReportFieldSort
                        {
                            CreatedById = userId,
                            CreatedDate = datenow,
                            ReportId = reportsorts.Id,
                            ColumnName = item.ColumnName,
                            Sort = item.Sort,
                            LastModifiedById = userId,
                            ModifiedDate = datenow,
                            OrganizationId = organizationId
                        });
                    }
                    else
                    {
                        oldsort.ReportId = reportsorts.Id;
                        oldsort.ColumnName = item.ColumnName;
                        oldsort.Sort = item.Sort;
                        oldsort.LastModifiedById = userId;
                        oldsort.ModifiedDate = datenow;
                        oldsort.OrganizationId = organizationId;
                    }

                }

                foreach (var item in reportsorts.Sorts)
                {
                    var newsort = dto.Sorts.Where(x => x.ColumnName == item.ColumnName).FirstOrDefault();
                    if (newsort == null)
                    {
                        item.IsDeleted = true;
                    }
                    else
                    {
                        if (item.IsDeleted == true)
                        {
                            item.IsDeleted = false;
                        }
                    }
                }

            }
            #endregion

            #region Users
            var reportusers = GetReport(dto.Id, true, "users");

            if (reportusers != null)
            {
                if (dto.ReportUserList != null)
                {
                    foreach (var item in dto.ReportUserList)
                    {
                        var olduser = reportusers.ReportUserList.Where(x => x.UserId == item.UserId).FirstOrDefault();

                        if (olduser == null)
                        {
                            reportusers.ReportUserList.Add(new ReportUsers
                            {
                                CreatedById = userId,
                                CreatedDate = datenow,
                                ReportId = reportsorts.Id,
                                UserId = item.UserId,
                                UserName = item.UserName,
                                LastModifiedById = userId,
                                ModifiedDate = datenow,
                                OrganizationId = organizationId
                            });
                        }
                        else
                        {
                            olduser.ReportId = reportusers.Id;
                            olduser.UserId = item.UserId;
                            olduser.UserName = item.UserName;
                            olduser.LastModifiedById = userId;
                            olduser.ModifiedDate = datenow;
                            olduser.OrganizationId = organizationId;
                        }
                    }
                }

                foreach (var item in reportusers.ReportUserList)
                {
                    var newuser = new ReportUsersDto();

                    if (dto.ReportUserList != null)
                    {
                        newuser = dto.ReportUserList.Where(x => x.UserId == item.UserId).FirstOrDefault();
                    }
                    else
                    {
                        newuser = null;
                    }

                    if (newuser == null)
                    {
                        if (!item.IsDeleted)
                        {
                            item.IsDeleted = true;
                        }
                    }
                    else
                    {
                        if (item.IsDeleted)
                        {
                            item.IsDeleted = false;
                        }
                    }
                }
            }
            #endregion

            #region Schedule
            var reportschedule = GetReport(dto.Id, true, "schedule");

            if (reportschedule != null && dto.ReportSchedules != null)
            {
                foreach (var item in dto.ReportSchedules)
                {
                    var oldschedule = reportschedule.ReportSchedules.Where(x => x.FrequencyType == item.FrequencyType && x.RunDay == item.RunDay).FirstOrDefault();

                    if (oldschedule == null)
                    {
                        reportschedule.ReportSchedules.Add(new ReportSchedule
                        {
                            CreatedById = userId,
                            CreatedDate = datenow,
                            ReportId = reportschedule.Id,
                            FrequencyType = item.FrequencyType,
                            RunDate = item.RunDate,
                            RunDay = item.RunDay,
                            RunTime = item.RunTime,
                            RunFrequency = item.RunFrequency,
                            LastRunSuccessDate = item.LastRunSuccessDate,
                            LastRunFailureDate = item.LastRunFailureDate,
                            NextRunDate = item.NextRunDate,
                            LastModifiedById = userId,
                            ModifiedDate = datenow,
                            OrganizationId = organizationId
                        });
                    }
                    else
                    {
                        oldschedule.FrequencyType = item.FrequencyType;
                        oldschedule.RunDate = item.RunDate;
                        oldschedule.RunDay = item.RunDay;
                        oldschedule.RunTime = item.RunTime;
                        oldschedule.RunFrequency = item.RunFrequency;
                        oldschedule.LastRunSuccessDate = item.LastRunSuccessDate;
                        oldschedule.LastRunFailureDate = item.LastRunFailureDate;
                        oldschedule.NextRunDate = item.NextRunDate;
                        oldschedule.LastModifiedById = userId;
                        oldschedule.ModifiedDate = datenow;
                        oldschedule.OrganizationId = organizationId;
                    }

                }

                foreach (var item in reportschedule.ReportSchedules)
                {
                    var newschedule = dto.ReportSchedules.Where(x => x.FrequencyType == item.FrequencyType && x.RunDay == item.RunDay).FirstOrDefault();

                    if (newschedule == null)
                    {
                        item.IsDeleted = true;
                    }
                    else
                    {
                        if (item.IsDeleted == true)
                        {
                            item.IsDeleted = false;
                        }
                    }
                }
            }


            #endregion

            var report = reportschedule;
            if (report != null)
            {
                if (reportsorts != null)
                {
                    if (reportsorts.Sorts != null)
                    {
                        report.Sorts = reportsorts.Sorts;
                    }
                }

                if (reportusers != null)
                {
                    if (reportusers.ReportUserList != null)
                    {
                        report.ReportUserList = reportusers.ReportUserList;
                    }
                }

                if (reportschedule != null)
                {
                    if (reportschedule.ReportSchedules != null)
                    {
                        report.ReportSchedules = reportschedule.ReportSchedules;
                    }
                }

                report.LastModifiedById = userId;
                report.OrganizationId = organizationId;
                report.ModifiedDate = DateTime.Now;
                report.ReportName = dto.ReportName;
                report.StaffId = dto.StaffId;
                report.StaffName = dto.StaffName;
                report.Department = dto.Department;
                report.Group = dto.Group;
                report.Grade = dto.Grade;
                report.Courses = dto.Courses;
                report.NumberOfCourses = dto.NumberOfCourses;
                report.StatusOfCourse = dto.StatusOfCourse;
                report.NumberOfViews = dto.NumberOfViews;
                report.NumberOfAttempts = dto.NumberOfAttempts;
                report.DateAccessed = dto.DateAccessed;
                report.TimeAccessed = dto.TimeAccessed;
                report.Duration = dto.Duration;
                report.ScopeOfCourse = dto.ScopeOfCourse;
                report.AverageScore = dto.AverageScore;
                report.LineManager = dto.LineManager;
                report.Location = dto.Location;
                report.TrainingBudget = dto.TrainingBudget;
                report.BudegtUtitlized = dto.BudegtUtitlized;
                report.OutstandingBudget = dto.OutstandingBudget;
                report.PercentageUtilization = dto.PercentageUtilization;
                report.NumberOfParticipants = dto.NumberOfParticipants;
                report.CourseEvaluationScore = dto.CourseEvaluationScore;
                report.ReportRecipients = dto.ReportRecipients;
                Update(report);
            }

            this.UnitOfWork.Commit();
        }

    }
}