using Newtonsoft.Json;
using SterlingBankLMS.Data.Models.Enums;
using System;

namespace SterlingBankLMS.Web.ViewModels
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Department { get; set; }
        public int? Branch { get; set; }
        public int? Region { get; set; }
        public int? Group { get; set; }
        public int? RoleId { get; set; }
        public int? Grade { get; set; }
        public string Function { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LineManagerFirstName { get; set; }
        public string LineManagerLastName { get; set; }
        public string LineManagerStaffId { get; set; }
        public DateTime? LockOutDate { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public Gender Gender { get; set; }
        [JsonIgnore]
        public int TotalCount { get; set; }

        public string DateOfEmploymentFormat => Convert.ToDateTime(DateOfEmployment).ToString("dd-MMMM-yyyy");
        public string DateOfBirthFormat => Convert.ToDateTime(DateOfBirth).ToString("dd-MMMM-yyyy");

    }
}