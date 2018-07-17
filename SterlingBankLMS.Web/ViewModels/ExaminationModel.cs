using Newtonsoft.Json;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;

namespace SterlingBankLMS.Web.ViewModels
{
    public class ExaminationModel : BaseModel
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Course { get; set; }
        public int? CourseId { get; set; }
        public int? ExamRetakeCount { get; set; }
        public bool IgnoreTimeLimit { get; set; }
        public double? HourLimit { get; set; }
        public double? MinuteLimit { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string EndDateFormat { get; set; }
        public string StartDateFormat { get
                ; set; }

        public string Period => (StartDate != null && EndDate != null) ? Convert.ToDateTime(StartDate).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(EndDate).ToString("dd MMM yyyy") : "N/A";

        public bool IngoreDueDate { get; set; }
        public string Duration => GetExamDuration();

        public bool IsStandAlone { get; set; }

        public ExaminationType ExamType { get; set; }
        public decimal? PassGrade { get; set; }
        [JsonIgnore]
        public int TotalRecords { get; set; }
        public List<QuizQuestionModel> ExamQuestions { get; set; }

        private string GetExamDuration()
        {
            string duration = "";
            if (HourLimit > 0)
                duration = HourLimit.ToString() + " Hour ";

            if (MinuteLimit > 0)
                duration = duration + " "+ MinuteLimit.ToString() + " Minutes";

            return duration;
        }

    }
}
