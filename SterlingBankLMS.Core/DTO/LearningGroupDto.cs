using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Core.DTO
{
    public class LearningGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string TagFormat { get; set; }
        public List<Tags> Tags { get; set; }

        public int TagCount { get; set; }
        public int CourseCount { get; set; }
        public int ExamCount { get; set; }
        public int TrainingCount { get; set; }
        public int SurveyCount { get; set; }

        public int TotalRecords { get; set; }
    }

    public class Tags 
    {

        public string Name { get; set; }
        public LearningGroupTagType TagType { get; set; }
        public int TagValue { get; set; }

    }
}
