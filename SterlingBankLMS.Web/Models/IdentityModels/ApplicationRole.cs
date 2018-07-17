using Microsoft.AspNet.Identity.EntityFramework;
using SterlingBankLMS.Data.Models.Entities;
using System.Collections.Generic;

namespace SterlingBankLMS.Web.Models.IdentityModels
{
    public class ApplicationRole : IdentityRole<int, UserRole>
    {
        public virtual ICollection<Permission> Permissions
        {
            get { return _permissionRecords ?? (_permissionRecords = new List<Permission>()); }
            protected set { _permissionRecords = value; }
        }

        private ICollection<Permission> _permissionRecords;

        public bool IsActive { get; set; }
        public string SystemName { get; set; }
        public int? OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}