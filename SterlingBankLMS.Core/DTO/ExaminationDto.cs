using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Core.DTO
{
    public class ExaminationDto
    {
        public  int Id { get;set;}
        public string Name { get; set; }
        public int? CourseId { get; set; }
        public string ImageUrl { get; set; }
        public decimal? PassGrade { get; set; }
        public int? ExamRetakeCount { get; set; }
        public string Description { get; set; }
        public int? HourLimit { get; set; }
        public int? MinuteLimit { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ExaminationType Type { get; set; }
    }
}
