using System;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class ForumDiscussion : OrganizationalBaseEntity
    {
        public int UserId { get; set; }
        public int ForumTopic { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }        
    }
}