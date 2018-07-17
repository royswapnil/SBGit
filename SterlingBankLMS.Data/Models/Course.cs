using System;
using System.Collections.Generic;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class Course : OrganizationalBaseEntity
    {
        public int EstimatedDuration { get; set; }
        public int? HoursPerWeek { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        //public int? ExamRetakeCount { get; set; }
        public DateTime? DueDate { get; set; }
        public int LearningAreaId { get; set; }
        public LearningArea LearningArea { get; set; }

        public string Overview { get; set; }

        public string Objectives { get; set; }
        //public int? PassGrade { get; set; }

        public bool IsPublished { get; set; }

        public bool HasCertificate { get; set; }

        public ICollection<Examination> Exams { get; set; }

        public ICollection<Module> Modules { get; set; }
    }
}