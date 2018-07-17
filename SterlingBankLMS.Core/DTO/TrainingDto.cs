using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SterlingBankLMS.Core.DTO
{
    public class TrainingDto
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
        public string StartDateFormat => (StartPeriod == null) ? null:  StartPeriod.Value.ToString("dd/MM/yyyy");
        public string CreatedDateFormat => CreatedDate.ToString("dd/MM/yyyy");
        public string EndDateFormat => (EndPeriod == null) ? null : EndPeriod.Value.ToString("dd/MM/yyyy");
     
        public DateTime? EndPeriod { get; set; }
      
        public double? Duration { get; set; }
      
        public decimal BudgetPerStaff { get; set; }
     
        public decimal Budget { get; set; }
        public int TotalRecords { get; set; }
        public string LocationPlace => Venue + ", " + Location;
        public List<TrainingPeriodDto> TrainingPeriod { get; set; }
        public string PeriodFormat { get; set; }
    }

    public class TrainingPeriodDto
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }

        public string DayName => AppHelper.GetDescription(Day);

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public TimeSpan? StartSpan => GetTimeSpan(StartTime);

        public TimeSpan? EndSpan => GetTimeSpan(EndTime);

       public TimeSpan? GetTimeSpan (string time)
        {
            DateTime dt;
            if (!DateTime.TryParseExact(time, "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            {
                return null;
            }
            TimeSpan timeSp = dt.TimeOfDay;
            return timeSp;
        }
    }



}
