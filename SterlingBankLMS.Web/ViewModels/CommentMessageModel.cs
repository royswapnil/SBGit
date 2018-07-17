using SterlingBankLMS.Core.Helper;
using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SterlingBankLMS.Web.ViewModels
{
    public class CommentMessageModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CommentFromDepartment { get; set; }
        public int Id { get; set; }
        public string CommentMessage { get; set; }
        public DateTime CommentedDate { get; set; }
        public int? UserId { get; set; }
        public int? LessonId { get; set; }
        public bool? Flagged { get; set; }
        public int? Status { get; set; }
        public int? OrganizationId { get; set; }
        public int? CreatedById { get; set; }
        public int? LastModifiedById { get; set; }
        public int? PostId { get; set; }
        public bool? IsDeleted { get; set; }
        public int? FlaggedCount { get; set; }
        public string NameFormat => FirstName + ", " + LastName;
        public string DateFormat => CreatedDateFormat() + " at " + CommentedDate.ToString("HH:mm tt");
        public string Timeago => AppHelper.TimeFuture(DateTime.Now, CommentedDate);
        public string CommentFormat => Regex.Replace(CommentMessage.Length > 85 ? CommentMessage.Substring(0, 82) + "..." : CommentMessage, "<.*?>", string.Empty);

        public string CreatedDateFormat()
        {
            string date = "";
            if (CommentedDate == DateTime.Now)
            {
                date = "Today";
            }
            else if (CommentedDate == DateTime.Now.AddDays(-1))
            {
                date = "Yesterday";
            }
            else if (CommentedDate == DateTime.Now.AddDays(-2))
            {
                date = CommentedDate.ToString("ddd, dd/MM");
            }
            else if (CommentedDate == DateTime.Now.AddDays(-3))
            {
                date = CommentedDate.ToString("ddd, dd/MM");
            }
            else if (CommentedDate == DateTime.Now.AddDays(-4))
            {
                date = CommentedDate.ToString("ddd, dd/MM");
            }
            else if (CommentedDate == DateTime.Now.AddDays(-5))
            {
                date = CommentedDate.ToString("ddd, dd/MM");
            }
            else if (CommentedDate == DateTime.Now.AddDays(-6))
            {
                date = CommentedDate.ToString("ddd, dd/MM/");
            }
            else
            {
                date = CommentedDate.ToString("dd/MM/yy");
            }
            return date;
        }
    }
}
