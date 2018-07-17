using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Data.Models.Entities
{
    public class Replacements
    {
        public int Id { get; set; }
        public string ReplcementTag { get; set; }

        public Enums.NotificationType NotificationType { get; set; }

        public string Entity { get; set; }

        public string Column { get; set; }
    }
}
