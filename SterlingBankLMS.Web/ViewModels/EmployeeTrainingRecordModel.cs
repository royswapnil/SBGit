using System;

namespace SterlingBankLMS.Web.ViewModels
{
    public class EmployeeTrainingRecordModel
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

        public string StartPeriodFormat => StartPeriod.ToString("dd-MMM-yyyy");
        public string EndPeriodFormat => EndPeriod.ToString("dd-MMM-yyyy");
        public string NameFormat => FirstName + ", " + LastName;
        public string LocationFormat => Venue + ", " + Location;
        public string AmountPerStaffFormat => string.Format(currencyFormat, "{0:c}", AmountPerStaff);
    }
}