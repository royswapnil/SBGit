using System;

namespace SterlingBankLMS.Web.ViewModels
{
    public class GroupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrganizationId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
