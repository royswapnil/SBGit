namespace SterlingBankLMS.Data.Models.Entities
{
    public class DepartmentBudget: OrganizationalBaseEntity
    {
        public decimal AmountSpent { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }

        public int Year { get; set; }

        public decimal Budget { get; set; }

        public bool IsActive { get; set; } //// for updates on changes made
    }
}
