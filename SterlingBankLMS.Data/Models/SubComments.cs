using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class SubComments : TrackableEntity
    {
        public string CommentMessage { get; set; }
        public DateTime? CommentedDate { get; set; }

        public Comments Comments { get; set; }
        public int CommentsId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
        
    }
}
