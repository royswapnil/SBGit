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
    public class TicketMessageModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TicketIssuerDepartment { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TicketTitle { get; set; }
        public string TicketDescription { get; set; }
        public int TicketId { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public bool IsDeleted { get; set; }

        public string TicketStatusFormat => Enum.GetName(typeof(TicketStatus), TicketStatus);
        public string NameFormat => FirstName + ", " + LastName + " ( " + TicketIssuerDepartment + " )";
        public string NameEmailFormat => FirstName + ", " + LastName + " [" + Email + "]";
        public string DateFormat => CreatedDateFormat() + " at " + CreatedDate.ToString("HH:mm tt");
        public string Timeago => AppHelper.TimeFuture(DateTime.Now, CreatedDate);
        public string TicketDescriptionFormat => Regex.Replace(TicketDescription.Length > 100 ? TicketDescription.Substring(0, 101) + "..." : TicketDescription, "<.*?>", string.Empty);

        public string CreatedDateFormat()
        {
            string date = "";
            if(CreatedDate== DateTime.Now)
            {
                date = "Today";
            }
            else if (CreatedDate == DateTime.Now.AddDays(-1))
            {
                date = "Yesterday";
            }
            else if (CreatedDate == DateTime.Now.AddDays(-2))
            {
                date = CreatedDate.ToString("ddd, dd/MM");
            }
            else if (CreatedDate == DateTime.Now.AddDays(-3))
            {
                date = CreatedDate.ToString("ddd, dd/MM");
            }
            else if (CreatedDate == DateTime.Now.AddDays(-4))
            {
                date = CreatedDate.ToString("ddd, dd/MM");
            }
            else if (CreatedDate == DateTime.Now.AddDays(-5))
            {
                date = CreatedDate.ToString("ddd, dd/MM");
            }
            else if (CreatedDate == DateTime.Now.AddDays(-6))
            {
                date = CreatedDate.ToString("ddd, dd/MM/");
            }
            else
            {
                date = CreatedDate.ToString("dd/MM/yy");
            }
            return date;
        }
    }
}
