﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SBLMSComments.ViewModels
{
    //public class PostsVM
    //{
    //    public int? PostID { get; set; }
    //    public string Message { get; set; }
    //    public DateTime PostedDate { get; set; }
    //}

    //public class CommentsVM
    //{
    //    public int ComID { get; set; }
    //    public string CommentMsg { get; set; }
    //    public DateTime CommentedDate { get; set; }
    //    public PostsVM Posts { get; set; }
    //    public UserVM Users { get; set; }
    //    public int? ParentId { get; set; }
    //    public int Status { get; set; } // change type
    //    public ParentMessage ParentMessage { get; set; }
    //    public string pmess { get; set; }
    //}

    //public class UserVM
    //{
    //    public int UserID { get; set; }
    //    public string Username { get; set; }
    //    public string imageProfile { get; set; }
    //}

    public class SubCommentsVM
    {
        public int SubComID { get; set; }
        public string CommentMsg { get; set; }
        public DateTime CommentedDate { get; set; }
        //public CommentsVM Comment { get; set; }
        //public UserVM User { get; set; }
        //public ParentMessage ParentMessage { get; set; }
    }

    //public class ParentMessage
    //{
    //    public int? ParId { get; set; }
    //    public string Parmessage { get; set; }
    //    public string Parusername { get; set; }
    //    public DateTime? Pardate { get; set; }
    //}


}