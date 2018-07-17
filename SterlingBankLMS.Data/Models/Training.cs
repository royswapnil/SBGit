using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class Training: OrganizationalBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Venue { get; set; }
        public string Location { get; set; }
        
        [EnumDataType(typeof(TrainingType))]
        public TrainingType TrainingType { get; set; }

        [EnumDataType(typeof(TrainingCategory))]
        public TrainingCategory TrainingCategory { get; set; }

        public int? Year { get; set; }
        public DateTime? StartPeriod { get; set; }

        public DateTime? EndPeriod { get; set; }

        public double? DurationInMinutes { get; set; }

        public bool IsActive { get; set; }
        public string Vendor { get; set; }
        public decimal AmountPerStaff { get; set; }
        public decimal BudgetExpended { get; set; }
        public string CronExpression { get; set; }

        public string PeriodFormat { get; set; }
        public ICollection<TrainingPeriod> TrainingPeriod { get; set; }
    }
}
