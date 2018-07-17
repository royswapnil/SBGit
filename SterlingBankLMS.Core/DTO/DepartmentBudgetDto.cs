using System;
using System.ComponentModel.DataAnnotations;

namespace SterlingBankLMS.Core.DTO
{
    public class DepartmentBudgetDto
    {
        IFormatProvider currencyFormat = new System.Globalization.CultureInfo("HA-LATN-NG");

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int Year { get; set; }

        [Required]
        public decimal Budget { get; set; }
        public decimal AmountSpent { get; set; }
        public int TotalRecords { get; set; }
        public string GroupName { get; set; }
        public int GroupId { get; set; }
        public int? RegionId { get; set; }
        public string RegionName { get; set; }
        public string BudgetFormat => string.Format(currencyFormat, "{0:c}", Budget);
        public string AmountSpentFormat => string.Format(currencyFormat, "{0:c}", AmountSpent);
        public string AmountRemainingFormat => string.Format(currencyFormat, "{0:c}", Budget - AmountSpent);

    }
}
