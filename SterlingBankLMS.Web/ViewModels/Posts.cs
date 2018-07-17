using SterlingBankLMS.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SterlingBankLMS.Web.ViewModels
{
    public class Posts
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public DateTime PostedDate { get; set; }
    }

    public class Comments
    {
        public int ID { get; set; }
        public string CommentMsg { get; set; }
        public DateTime CommentedDate { get; set; }
        public Posts Posts { get; set; }
        public User User { get; set; }
    }


}