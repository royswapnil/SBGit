using SterlingBankLMS.Data.Models.Entities;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class LineManager : OrganizationalBaseEntity
    {
        public int? LineManagerId { get; set; }
        public User LIneManager { get; set; }
        public int? EmployeeId { get; set; }
        public User Employee { get; set; }
        public bool IsActive { get; set; }
    }
}
