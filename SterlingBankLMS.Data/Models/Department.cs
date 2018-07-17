namespace SterlingBankLMS.Data.Models.Entities
{
    public class Department: OrganizationalBaseEntity
    {
        public string Name { get; set; }

        public int? GroupId { get; set; }
        public Group Group { get; set; }
    }
}