using SterlingBankLMS.Data.Models;
using System.Collections.Generic;

namespace SterlingBankLMS.Web.Models.IdentityModels
{
    public class Permission : BaseEntity
    {
        private ICollection<ApplicationRole> _roles;

        public string Name { get; set; }
        public string SystemName { get; set; }

        public virtual ICollection<ApplicationRole> Roles
        {
            get { return _roles ?? (_roles = new List<ApplicationRole>()); }
            protected set { _roles = value; }
        }
    }
}