using SterlingBankLMS.Data.Models.Entities;
using System;

namespace SterlingBankLMS.Data.Models.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class TrackableEntity : BaseEntity
    {
        public User CreatedBy { get; set; }

        public int? CreatedById { get; set; }

        public User LastModifiedBy { get; set; }

        public int? LastModifiedById { get; set; }
    }

    public class OrganizationalBaseEntity : TrackableEntity
    {
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}