using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Core.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string StaffId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public int TotalRecords { get; set; }

        public string NameFormat => FirstName + " " + LastName;
        public string ActiveFormat => ActiveFunction();
        public string DateFormat => DateOfEmployment.ToString("dd-MMM-yyyy");
        public string empty => string.Empty;
        public string ActiveFunction()
        {
            string result = "";
            if (IsActive == true)
            {
                result +="Active";
            }
            else
            {
                result += "InActive";
            }
            return result;
        }
    }
}
