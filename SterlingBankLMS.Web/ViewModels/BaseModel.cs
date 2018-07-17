using System;

namespace SterlingBankLMS.Web.ViewModels
{
    public class BaseModel
    {
        public int Id { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public int CreatedById { get; set; }

        public int OrganizationId { get; set; }

        public string LastModifiedBy { get; set; }

        public int? LastModifiedById { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }

       
    }
}