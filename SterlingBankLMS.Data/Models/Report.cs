using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class Report : OrganizationalBaseEntity
    {
        public string ReportName { get; set; }
        public string ReportRecipients { get; set; }
        public DateTime? BeginDateReportGenerate { get; set; }
        public DateTime? EndDateReportGenerate { get; set; }
        public ReportGenerationFrequency ReportGenerationFrequency { get; set; }
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
        public ICollection<ReportFieldSort> Sorts { get; set; }
        public ICollection<ReportSchedule> ReportSchedules { get; set; }
        public ICollection<ReportUsers> ReportUserList { get; set; }
    }
}
