﻿using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class TrainingMap : EntityTypeConfiguration<Training>
    {
        public TrainingMap()
        {
            //HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
            //HasRequired(x => x.Organization).WithMany().HasForeignKey(x => x.OrganizationId).WillCascadeOnDelete(false);
        }
    }
}