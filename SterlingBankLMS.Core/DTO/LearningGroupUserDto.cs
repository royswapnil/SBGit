using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Core.DTO
{
    public class LearningGroupUserDto
    {
      
        public int Id { get; set; }
        public int LearningGroupId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => FirstName + " " + LastName;

        public int GroupId { get; set; }
        public string Group { get; set; }
        public int DepartmentId { get; set; }
        public Gender Gender { get; set; }
        public string Department { get; set; }
        public int BranchId { get; set; }
        public string Branch { get; set; }
        public int RegionId { get; set; }
        public string Region { get; set; }
        public int GradeId { get; set; }
        public string Grade { get; set; }
        public int RoleId { get; set; }
        public LearningGroupTagType TagType { get; set; }

        public string Sex => Gender.ToString();

        public int TagValue { get; set; }

        public int TotalRecords { get; set; }
        
      }
}
