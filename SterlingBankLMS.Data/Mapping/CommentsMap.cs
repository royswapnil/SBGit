using SterlingBankLMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Mapping
{
    public class CommentsMap : EntityTypeConfiguration<Comments>
    {
        public CommentsMap()
        {
            HasRequired(x => x.Posts).WithMany().HasForeignKey(x => x.PostId).WillCascadeOnDelete(false);
            HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
            HasRequired(x => x.Organization).WithMany().HasForeignKey(x => x.OrganizationId).WillCascadeOnDelete(false);
        }
    }
}
