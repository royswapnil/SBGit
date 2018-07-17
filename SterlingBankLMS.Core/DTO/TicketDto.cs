using SterlingBankLMS.Data.Models.Enums;
using System;

namespace SterlingBankLMS.Core.DTO
{
    public class TicketDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TicketTitle { get; set; }
        public string TicketDescription { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TicketId { get; set; }
        public int ApplicationUserId { get; set; }
        public string StaffId { get; set; }
        public Gender Gender { get; set; }
        public string GroupName { get; set; }
        public string GradeName { get; set; }
        public string DepartmentName { get; set; }
        public int TotalRecords { get; set; }
        public string RegionName { get; set; }
        public string FilePath { get; set; }
        public string ScreenshotFormat => FilePath;

        public string TicketStatusFormat => Enum.GetName(typeof(TicketStatus), TicketStatus);
        public string ModifiedDateFormat => CreatedDate.ToString("dd-MMM-yyyy");
        public string ModifiedTimeFormat => CreatedDate.ToString("hh:mm tt");
        public string NameFormat => FirstName + ", " + LastName;
        public string TicketIdFormat => TicketIdFormatString();
        public string Sex => Enum.GetName(typeof(Gender), Gender);
        public string TicketIdFormatString()
        {
            string id = "";
            if (TicketId.ToString().Length == 1)
            {
                id = "000" + TicketId;
            }
            else if (TicketId.ToString().Length == 2)
            {
                id = "00" + TicketId;
            }
            else if (TicketId.ToString().Length == 3)
            {
                id = "0" + TicketId;
            }
            else
            {
                id = TicketId.ToString();
            }
            return id;
        }
    }
}
