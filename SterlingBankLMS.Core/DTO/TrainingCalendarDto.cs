using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Core.DTO
{
   public class TrainingCalendarDto
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public string Name { get; set; }

        public bool Requested { get; set; }

        public bool Ongoing { get; set; }

        public DateTime? StartPeriod { get; set; }
        public string StartDateFormat => StartPeriod.HasValue ? StartPeriod.Value.ToString("dd MMM yyyy"): null;
        public string EndDateFormat => EndPeriod.HasValue ? EndPeriod.Value.ToString("dd MMM yyyy") : null;
        public DateTime? EndPeriod { get; set; }

        public string Vendor { get; set; }
        public string LocationPlace => Venue + ", " + Location;

        public TrainingCategory TrainingCategory { get; set; }
        public TrainingType TrainingType { get; set; }
        public string TrainingCategoryName => Enum.GetName(typeof(TrainingCategory), TrainingCategory);
        public string TrainingTypeName => Enum.GetName(typeof(TrainingType), TrainingType);

        public string Venue { get; set; }
        public string Location { get; set; }
        public string PeriodFormat { get; set; }

        public int TotalCount { get; set; }
    }
}
