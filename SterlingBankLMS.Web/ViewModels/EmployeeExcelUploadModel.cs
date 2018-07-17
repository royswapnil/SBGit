using ExcelManager;
using System;

namespace SterlingBankLMS.Web.ViewModels
{
    public class EmployeeExcelUploadModel
    {
        [ExcelReaderCell("Staff Id")]
        public string StaffId { get; set; }

        [ExcelReaderCell("FirstName")]
        public string FirstName { get; set; }
        [ExcelReaderCell("Surname")]
        public string SurName { get; set; }
        [ExcelReaderCell("UserName")]
        public string UserName { get; set; }
        [ExcelReaderCell("Email Address")]
        public string EmailAddress { get; set; }
        [ExcelReaderCell("Date Of Employment")]
        public string DateOfEmployment { get; set; }
        [ExcelReaderCell("Date of Birth")]
        public string DateOfBirth { get; set; }
        [ExcelReaderCell("Sex")]
        public string Sex { get; set; }
        [ExcelReaderCell("Grade")]
        public string Grade { get; set; }
        [ExcelReaderCell("Branches")]
        public string Branch { get; set; }
        [ExcelReaderCell("Regions")]
        public string Regions { get; set; }
        [ExcelReaderCell("Groups")]
        public string Groups { get; set; }
        [ExcelReaderCell("Function")]
        public string Function { get; set; }
        [ExcelReaderCell("Department")]
        public string Department { get; set; }

        [ExcelReaderCell("LINE MANAGER FIRSTNAME")]
        public string LineManagerFirstName { get; set; }

        [ExcelReaderCell("LINE MANAGER SURNAME")]
        public string LineManagerLastName { get; set; }

        public int? BranchId { get; internal set; }
        public int? DepartmentId { get; internal set; }
        public int? GroupId { get; internal set; }
        public int? GradeId { get; internal set; }
        public int? RegionId { get; internal set; }
        public DateTime DateOfBirthD { get; internal set; }
        public DateTime DateOfEmploymentD { get; internal set; }
    }
}