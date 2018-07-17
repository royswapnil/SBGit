using SterlingBankLMS.Data.Models.Enums;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class Organization : TrackableEntity
    {
        public string Name { get; set; }

        public string ApplicationName { get; set; }
        public bool IsLockedOut { get; set; } // subscription model for multi tenants
        public string Email { get; set; }
        public string SubDomain { get; set; }

        public bool UseHrms { get; set; }

        public int? HrmsId { get; set; }
        public Hrms Hrms { get; set; }

        public string LogoUrl { get; set; }
        public OrganizationalStatus OrganizationalStatus { get; set; }
    }
}