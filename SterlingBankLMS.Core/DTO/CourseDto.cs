using SterlingBankLMS.Data.Models.Entities;
using System;

namespace SterlingBankLMS.Core.DTO
{
    public class CatalogCourseDto
    {
        public int Id { get; set; }
        public double? AverageRating { get; set; }
        public string DueDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string LearningArea { get; set; }
        public int TotalCount { get; set; }
    }

    public class CourseDto : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        public int? ExamRetakeCount { get; set; }
        public DateTime? DueDate { get; set; }
        public string TimeDuration { get; set; }
        public string LearningAreaName { get; set; }
        public int LearningAreaId { get; set; }

        public string DueDateFormat => DueDate == null ? "N/A" : Convert.ToDateTime(DueDate).ToString("dd MMM, yyyy");
        public string CreatedDateFormat => CreatedDate == null ? "N/A" : Convert.ToDateTime(CreatedDate).ToString("dd MMM, yyyy");
        public int? PassGrade { get; set; }
        public int ModuleCount { get; set; }
        
        public int TotalRecords { get; set; }

        public bool IsPublished { get; set; }

        public bool HasCertificate { get; set; }

        public string IsPublishedFormat => IsPublished == true ? "Active" : "InActive";
    }
}