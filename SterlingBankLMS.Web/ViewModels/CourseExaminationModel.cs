using System;

namespace SterlingBankLMS.Web.ViewModels
{
    public class CourseExaminationModel
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Course { get; set; }

        public string Description { get; set; }
        public int? ExamRetakeCount { get; set; }
        public int? HourLimit { get; set; }
        public int? MinuteLimit { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public decimal? PassGrade { get; set; }
        public bool CanTakeExam { get; set; }
    }
}