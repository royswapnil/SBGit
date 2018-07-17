using System;

namespace SterlingBankLMS.Web.ViewModels
{
    public class EmployeeCertificateModel
    {
        public int Id { get; set; }
        public string User { get; set; }

        public string Name { get; set; }

        public int CourseId { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime StartDate { get; set; }

        public string DateCompletedFormat => Convert.ToDateTime(EndDate).ToString("dd MMM, yyyy");

        public string DateStartedFormat => StartDate.ToString("dd MMM, yyyy");

        public DateTime? DueDate { get; set; }

        public string DueDateFormat => DueDate == null ? "N/A" : Convert.ToDateTime(DueDate).ToString("dd MMM, yyyy");
    }
}