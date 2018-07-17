using System;

namespace SterlingBankLMS.Core.DTO
{
    public class EmployeeTrainingRecordDto
    {
        IFormatProvider currencyFormat = new System.Globalization.CultureInfo("HA-LATN-NG");
        public int TrainingId { get; set; }
        public string TrainingName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StaffId { get; set; }
        public string DepartmentName { get; set; }
        public decimal AmountPerStaff { get; set; }
        public string Venue { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public int UserTrainingId { get; set; }
        public int UserId { get; set; }

        public string StartPeriodFormat => StartPeriod.ToString("dd/MM/yyyy");
        public string EndPeriodFormat => EndPeriod.ToString("dd/MM/yyyy");
        public string NameFormat => FirstName + ", " + LastName;
        public string LocationFormat => Venue + ", " + Location;
        public string AmountPerStaffFormat => string.Format(currencyFormat, "{0:c}", AmountPerStaff);

    }

    public class EmpTrainingRecord
    {
        public string TrainingName { get; set; }
        public string Venue { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; }

        public string StartPeriodFormat => StartDate == null ? null: StartDate.Value.ToString("dd/MM/yyyy");
        public string LocationFormat => Venue + ", " + Location;

    }
}