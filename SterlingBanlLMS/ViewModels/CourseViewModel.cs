using SterlingBankLMS.Data.Models;
using System;
using System.Collections.Generic;

namespace SterlingBankLMS.Web.ViewModels
{
    public class CourseViewModel : BaseEntity
    {
        public string CourseName{get;set;}
        public string CourseDescription { get; set; }
        public string ImageUrl { get; set; }
        public int CourseRetakeCount { get; set; }
        public DateTime DueDate { get; set; }
        public string TimeDuration { get; set; }
        public IEnumerable<ModuleViewModel> Modules { get; set; }

    }
}