namespace SterlingBankLMS.Data.Models.Entities
{
    public class ForumCategory: OrganizationalBaseEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}