using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.Areas.Admin.Models
{
   
    public class CourseTable
    {
        public int Id { get; set; }
        public string LearningAreaName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ExamRetakeCount { get; set; }
        [JsonIgnore]
        public DateTime? DueDate { get; set; }
        public string DueDateFormat => DueDate == null ? "none" : Convert.ToDateTime(DueDate).ToString("dd MMM, yyyy");
        public int? PassGrade { get; set; }
        public int ModuleCount { get; set; }
        [JsonIgnore]
        public int TotalRecords { get; set; }

    }

}