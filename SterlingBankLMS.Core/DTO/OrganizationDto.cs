using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Core.DTO
{
    public class OrganizationDto
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string Email { get; set; }
        public string LogoUrl { get; set; }
        public string SubDomain { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TotalRecords { get; set; }
        public OrganizationalStatus OrganizationalStatus { get; set; }

        public string OrganizationalStatusFormat => Enum.GetName(typeof(OrganizationalStatus), OrganizationalStatus);
        public string CreatedDateFormat => CreatedDate.ToString("MMM dd, yyyy");
    }

    public class GetOrganization
    {
        public int id { get; set; }
    }
}
