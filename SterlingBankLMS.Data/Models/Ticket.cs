using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class Ticket: OrganizationalBaseEntity
    {
        public string FilePath { get; set; }
        public string TicketTitle { get; set; }
        public string TicketDescription { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public TicketStatus TicketStatus { get; set; }
    }
}
