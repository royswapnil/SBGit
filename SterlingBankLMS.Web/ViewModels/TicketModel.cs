using MultipartDataMediaFormatter.Infrastructure;
using SterlingBankLMS.Data.Models.Enums;
using System;
using System.Collections.Generic;

namespace SterlingBankLMS.Web.ViewModels
{
    public class TicketModel : BaseModel
    {
        public string FilePath { get; set; }
        public string TicketTitle { get; set; }
        public string TicketDescription { get; set; }
        public int UserId { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public HttpFile ImageUpload { get; set; }

    }
}