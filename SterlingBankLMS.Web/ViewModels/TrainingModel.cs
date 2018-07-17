using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.ViewModels
{
    public class TrainingModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Programme Title cannot be empty")]
        public string Name { get; set; }
        public string Description { get; set; }

        public string Venue { get; set; }
        public string Location { get; set; }

        public DateTime CreatedDate { get; set; }
        [Required]
        public int TrainingCategory { get; set; }
        public int TrainingType { get; set; }
        public string TrainingCategoryName => Enum.GetName(typeof(TrainingCategory), TrainingCategory);
        public string TrainingTypeName => Enum.GetName(typeof(TrainingType), TrainingType);

        public string Vendor { get; set; }

        public DateTime? StartPeriod { get; set; }
        public string StartDateFormat { get; set; }
        public string CreatedDateFormat => CreatedDate.ToString("dd/MM/yyyy");
        public string EndDateFormat { get; set; }

        public DateTime? EndPeriod { get; set; }

        public double? Duration { get; set; }

        public decimal BudgetPerStaff { get; set; }

        public decimal Budget { get; set; }
        public int TotalRecords { get; set; }
        public string LocationPlace => Venue + ", " + Location;
        public List<TrainingPeriodDto> TrainingPeriod { get; set; }
        public string PeriodFormat { get; set; }
    }
}