
using SterlingBankLMS.Core.DTO;
using SterlingBankLMS.Web.Models.IdentityModels;
using System;

namespace SterlingBankLMS.Web.ViewModels
{
    public class CourseLmaDetailsModel
    {
        public AdvertDto TopSectionAd { get; set; }
        public int CourseId { get; set; }
        public int? ModuleId { get; set; }
        public int? LessonId { get; set; }
        //public string Slug { get; set; }
    }

    public class ExamDetailsModel
    {
        public int ExaminationId { get; set; }
    }

    public class PostsVM
    {
        public int? PostID { get; set; }
        public string Message { get; set; }
        public DateTime PostedDate { get; set; }
    }

    public class CommentsVM
    {
        public int ComID { get; set; }
        public string CommentMsg { get; set; }
        public DateTime CommentedDate { get; set; }
        public PostsVM Posts { get; set; }
        //public UserVM Users { get; set; }
        public int? ParentId { get; set; }
        public int Status { get; set; } // change type
        public ParentMessage ParentMessage { get; set; }
        public string pmess { get; set; }
        public ApplicationUserVM ApplicationUser { get;set;}
        public int LessonId { get; set; }
    }

    public class UserVM
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string imageProfile { get; set; }
    }


    public class ParentMessage
    {
        public int? ParId { get; set; }
        public string Parmessage { get; set; }
        public string Parusername { get; set; }
        public DateTime? Pardate { get; set; }
    }

    public class ApplicationUserVM
    {
        public int? AppUserId { get; set; }
        public string AppUserName { get; set; }
        
    }

}