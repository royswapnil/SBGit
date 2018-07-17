using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Core.DTO
{
    public class BranchDto
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int TotalRecords { get; set; }
    }

    public class RegionDto
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int TotalRecords { get; set; }
    }

    public class GroupDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupHeadFirstName { get; set; }
        public string GroupHeadLastName { get; set; }
        public string GroupHeadStaffId { get; set; }
        public int TotalRecords { get; set; }

        public string GroupHead => GroupHeadFirstName + " " + GroupHeadLastName ?? "N/A";
        public string GhStaffId => GroupHeadStaffId ?? "N/A";

        public string GradeName { get; set; }
        public string DepartmentName { get; set; }
    }

    public class DeptDto
    {
        public int DepartmentId { get; set; }
        public string DeptName { get; set; }
        public string GroupName { get; set; }
        public int TotalRecords { get; set; }
    }

    public class LineManagerDto
    {
       
        public int LineManagerId { get; set; }
        public string LineManagerFirstName { get; set; }
        public string LineManagerLastName { get; set; }
        public string LineManagerStaffId { get; set; }
        public string DepartName { get; set; }
        public int TotalRecords { get; set; }
        public string LineManager => LineManagerFirstName + " " + LineManagerLastName;
    }
}
