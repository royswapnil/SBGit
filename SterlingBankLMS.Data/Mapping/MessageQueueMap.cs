﻿using SterlingBankLMS.Data.Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SterlingBankLMS.Data.Mapping
{
    public class MessageQueueMap : EntityTypeConfiguration<MessageQueue>
    {
        public MessageQueueMap()
        {
        
            HasRequired(x => x.CreatedBy).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Organization).WithMany().HasForeignKey(x => x.OrganizationId).WillCascadeOnDelete(false);
        }
    }
}