using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Enums;
using System;

namespace SterlingBankLMS.Core.DTO
{
    public class TrainingRequestDto
    {
        IFormatProvider currencyFormat = new System.Globalization.CultureInfo("HA-LATN-NG");

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TrainingName { get; set; }
        public int TrainingId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TrainingApprovalStatus TrainingApprovalStatus { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public decimal DepartmentBudgetAmt { get; set; }
        public decimal AmountPerStaff { get; set; }
        public string StaffId { get; set; }
        public string BranchName { get; set; }
        public string GradeName { get; set; }
        public string RegionName { get; set; }
        public int TotalRecords { get; set; }
        public decimal AmountSpent { get; set; }
        public Gender Gender { get; set; }
        public string GroupName { get; set; }
        public int ApplicationUserId { get; set; }
        public string ReasonForRequest { get; set; }

        public string LineManagerName { get; set; }

        public string LineManagerStaffId { get; set; }

        public string lineManagerComment { get; set; }

        public string AdminComment { get; set; }

        public DateTime? linemanageractionDate { get; set; }

        public DateTime? AdminActionDate { get; set; }


        public string Sex => Enum.GetName(typeof(Gender), Gender);
        public string DeptBudgetFormat => string.Format(currencyFormat, "{0:c}", DepartmentBudgetAmt);
        public string AmtPerStaffFormat => string.Format(currencyFormat, "{0:c}", AmountPerStaff);
        public string AmountSpentFormat => string.Format(currencyFormat, "{0:c}", AmountSpent);
        public string CreatedDateFormat => CreatedDate.ToString("dd-MMM-yyyy");
        public string NameFormat => FirstName + ", " + LastName;
        public string TrainingApprovalEnumName => TrainingApprovalStatus.ToString();
        public string TrainingApprovalFormat => AppHelper.GetDescription(TrainingApprovalStatus);
        public bool IsApproval { get; set; }
    }
}
