namespace SterlingBankLMS.Data.Models.Entities
{
    public class Group : OrganizationalBaseEntity
    {
        public string Name { get; set; }
        public string GroupHeadFirstName { get; set; }
        public string GroupHeadLastName { get; set; }
        public string GroupHeadStaffId { get; set; }
    }
}