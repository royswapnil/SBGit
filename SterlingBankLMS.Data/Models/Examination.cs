using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class Examination : OrganizationalBaseEntity
    {
        public Course Course { get; set; }
        public int? CourseId { get; set; }
        public string ImageUrl { get; set; }
        public ExaminationType ExamType { get; set; }
        public decimal? PassGrade { get; set; }
        public int? ExamRetakeCount { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? HourLimit { get; set; }
        public double? MinuteLimit { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<ExaminationQuestion> ExamQuestions { get; set; }

    }
}