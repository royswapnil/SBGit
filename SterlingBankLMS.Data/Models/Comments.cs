using SterlingBankLMS.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models
{
    public class Comments : OrganizationalBaseEntity
    {
        public string CommentMessage { get; set; }
        public DateTime? CommentedDate { get; set; }

        public int PostId { get; set; }
        public Posts Posts { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }       

        public int? ParentId { get; set; }
        public int LessonId { get; set; }
        public bool Flagged { get; set; }
        public int Status { get; set; }
    }
}
