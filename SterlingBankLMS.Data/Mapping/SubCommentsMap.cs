using SterlingBankLMS.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Mapping
{
   public class SubCommentsMap : EntityTypeConfiguration<SubComments>
    {
        public SubCommentsMap()
        {
            HasRequired(x => x.Comments).WithMany().HasForeignKey(x => x.CommentsId).WillCascadeOnDelete(false);
            HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
        }
    }
}
