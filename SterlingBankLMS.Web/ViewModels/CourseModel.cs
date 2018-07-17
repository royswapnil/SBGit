using MultipartDataMediaFormatter.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SterlingBankLMS.Web.ViewModels
{
    public class CourseModel : BaseModel
    {
        public LearningAreaModel LearningArea { get; set; }

        public string LearningAreaName { get; set; }
        public string Name { get; set; }
       
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
       // public int? ExamRetakeCount { get; set; }

        public int EstimatedDuration { get; set; }
        public int? HoursPerWeek { get; set; }

        public DateTime? DueDate { get; set; }

        public string DueDateFormat => DueDate == null ? "none" : Convert.ToDateTime(DueDate).ToString("dd MMM, yyyy");

        [Required(ErrorMessage = "Learning area is required")]
        public int LearningAreaId { get; set; }
        public bool IgnoreDueDate { get; set; }
        public bool CourseIgnoreRetake { get; set; }
        public string TimeDuration { get; set; }

        //public int? PassGrade { get; set; }

        public int CurrentStep { get; set; }

        public List<ModuleModel> Modules { get; set; }


        public int ModuleCount { get; set; }

        public HttpFile ImageUpload { get; set; }

        public string Overview { get; set; }

        public string Objectives { get; set; }
        public int ThumbsUp { get; set; }
        public int ThumbsDown { get; set; }

        public bool IsPublished { get; set; }

        public bool HasCertificate { get; set; }
    }
}