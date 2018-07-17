using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Core.DTO
{
    public class ReportDto
    {
        public int Id { get; set; }
        public string ReportName { get; set; }
        public string ReportRecipients { get; set; }
        public bool StaffId { get; set; }
        public bool StaffName { get; set; }
        public bool Department { get; set; }
        public bool Group { get; set; }
        public bool Grade { get; set; }
        public bool Courses { get; set; }
        public bool NumberOfCourses { get; set; }
        public bool StatusOfCourse { get; set; }
        public bool NumberOfViews { get; set; }
        public bool NumberOfAttempts { get; set; }
        public bool DateAccessed { get; set; }
        public bool TimeAccessed { get; set; }
        public bool Duration { get; set; }
        public bool ScopeOfCourse { get; set; }
        public bool AverageScore { get; set; }
        public bool LineManager { get; set; }
        public bool Location { get; set; }
        public bool TrainingBudget { get; set; }
        public bool BudegtUtitlized { get; set; }
        public bool OutstandingBudget { get; set; }
        public bool PercentageUtilization { get; set; }
        public bool NumberOfParticipants { get; set; }
        public bool CourseEvaluationScore { get; set; }
        public ICollection<ReportFieldSortDto> Sorts { get; set; }
        public ICollection<ReportScheduleDto> ReportSchedules { get; set; }
        public ICollection<ReportUsersDto> ReportUserList { get; set; }
    }

    public class ReportUsersDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class ReportScheduleDto
    {
        public int Id { get; set; }
        public string FrequencyType { get; set; }
        public DateTime? RunDate { get; set; }
        public string RunDay { get; set; }
        public string RunTime { get; set; }
        public string RunFrequency { get; set; }
        public DateTime? LastRunSuccessDate { get; set; }
        public DateTime? LastRunFailureDate { get; set; }
        public DateTime? NextRunDate { get; set; }

    }

    public class ReportFieldSortDto
    {
        public int Id { get; set; }
        public string ColumnName { get; set; }
        public string Sort { get; set; }
    }

    public class AuditTrackerDto
    {
        public int TotalRecords { get; set; }
        public int SeqId { get; set; }
        public int Trn { get; set; }
        public string OperationId { get; set; }
        public DateTime ChangedDateTime { get; set; }
        public string Name { get; set; }
        public string TableName { get; set; }
        public string AliasName { get; set; }
        public string ColumnName { get; set; }
        public string Operation { get; set; }
        public string BeforeUpdate { get; set; }
        public string AfterUpdate { get; set; }
    }

    public class AuditSearchCriteriaDto
    {
        public string user { get; set; }
        public string type { get; set; }
        public int[] entityType { get; set; }
        public DateTime? startdate { get; set; }
        public DateTime? enddate { get; set; }
        public bool buttonclick { get; set; }
    }

    public class GenerateReportDto
    {
        public string ReportType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StaffId { get; set; }
        public string GroupId { get; set; }
        public string GradeId { get; set; }
        public string DepartmentId { get; set; }
    }

    public class GenerateReportDataTableDto
    {
        public string StaffID { get; set; }
        public string StaffName { get; set; }
        public string Region { get; set; }
        public string Group { get; set; }
        public string Grade { get; set; }
        public string Courses { get; set; }
        public int? NoOfCourses { get; set; }
        public string Name { get; set; }
        public DateTime? DueDate { get; set; }
        public int? Duration { get; set; }
        public int? NoOfParticipants { get; set; }
        public string Courseevaluationscore { get; set; }
        public decimal? AverageScore { get; set; }
        public string Exams { get; set; }
        public int? Scorepercourse { get; set; }
        public int? StatusOfCourses { get; set; }
        public int? NoOfAttempts { get; set; }
        public DateTime? DateAccessed { get; set; }
        public string TimeAccessed { get; set; }
        public string Location { get; set; }
    }

    public class TrackDataConfig
    {
        public int entityId { get; set; }
        public string entityname { get; set; }
        public List<entities> entity { get; set; }
    }

    public class entities
    {
        public string tablename { get; set; }
        public string aliasname { get; set; }
        public string cdctablename { get; set; }
        public List<relatedcolumn> relatedcolumns { get; set; }
        public List<showcolumn> showcolumns { get; set; }
    }

    public class relatedcolumn
    {
        public int id { get; set; }
        public string columnname { get; set; }
        public string childtable { get; set; }
        public string namecolumn { get; set; }
    }

    public class showcolumn
    {
        public string columnname { get; set; }
    }

    public class EntityDropDown
    {
        public string id;
        public string text;
    }

    public class AllReportsDataTable
    {
        public int ReportId;
        public string ReportName;
        public string Fields;
    }
}
