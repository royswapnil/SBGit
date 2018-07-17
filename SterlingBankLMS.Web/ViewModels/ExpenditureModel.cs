using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Web.ViewModels
{
    public class ExpenditureModel
    {
        IFormatProvider currencyFormat = new System.Globalization.CultureInfo("HA-LATN-NG");
        public int GroupId { get; set; }
        public int RegionId { get; set; }
        public int DepartmentBudgetId { get; set; }
        public int TrainingId { get; set; }
        public string GroupName { get; set; }
        public string RegionName { get; set; }
        public int Id { get; set; }
        public string TrainingName { get; set; }
        public DateTime StartPeriod { get; set; }
        public string Location { get; set; }
        public string Venue { get; set; }
        public decimal AmountSpent { get; set; }
        public int NumberOfParticipants { get; set; }
        public int TotalRecords { get; set; }
        public int Year { get; set; }
        public decimal OutstandingAmount { get; set; }

        public string TrainingLocationFormat => Venue + ", " + Location;
        public string AmountExpendedFormat => string.Format(currencyFormat, "{0:c}", AmountSpent);
        public string TrainingDateFormat => StartPeriod.ToString("dd-MMM-yyyy");
        public string OutstandingAmountFormat => string.Format(currencyFormat, "{0:c}", OutstandingAmount);
    }
}
