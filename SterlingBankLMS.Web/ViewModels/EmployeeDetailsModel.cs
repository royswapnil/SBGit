using SterlingBankLMS.Data.Models.Enums;
using System;

namespace SterlingBankLMS.Web.ViewModels
{
    public class EmployeeDetailsModel
    {
        public int Id { get; set; }
        public int TotalRecords { get; set; }
        public string StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public int DepartmentId { get; set; }
        public string Branch { get; set; }
        public string Location { get; set; }
        public Gender Gender { get; set; }
        public string Functions { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Grade { get; set; }
        public string LineManagerFirstName { get; set; }
        public string LineManagerLastName { get; set; }
        public string LIneManagerStaffId { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public string LineManagerFormat => LineManagerFirstName + "," + LineManagerLastName + " (" + LIneManagerStaffId + ")";
        public string Sex => Enum.GetName(typeof(Gender), Gender);
        public string DateOfEmploymentFormat => DateOfEmployment.ToString("dd-MMM-yyyy");
        public string LineManagerNameFormat => LineManagerFirstName + ", " + LineManagerLastName;
    }

    public class ApplicationUserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}